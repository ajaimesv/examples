# Simultaneous calls to multiple URLs in Scala

Makes a series of calls to multiple URLs. Each call is wrapped in Try for having a chance to easily collect them 
if we have to. See my comments in the code.

`remoteCall` returns `Seq[Future[Try[String]]]` which is later transformed to `Future[Seq[Try[String]]]`
by calling `Future.sequence(futures)`. This way we only have one future to deal with. See [this page](https://stackoverflow.com/questions/20874186/scala-listfuture-to-futurelist-disregarding-failed-futures)
for a longer explanation.


```scala
package controllers

import javax.inject._
import play.api.libs.ws.WSClient
import play.api.mvc._

import scala.concurrent.{ExecutionContext, Future}
import scala.util.Try

/**
  * Exception class for this exercise.
  * @param msg the exception description
  */
class ApiException(msg: String) extends Exception(msg)

/**
  * Makes simultaneous calls to different endpoints.
  * @param cc
  * @param ws
  * @param ec PlayFramework's Execution context for async processing.
  */
@Singleton
class HomeController @Inject()(
  cc: ControllerComponents,
  ws: WSClient
)(
  implicit ec: ExecutionContext
) extends AbstractController(cc) {

  /**
    * Makes simultaneous calls to multiple URLs.
    * @return a sequence of futures containing the resulting response body
    *         of each call. Try wraps each result in a Success or Failure instance.
    */
  def remoteCall(): Seq[Future[Try[String]]] = {
    val urls = Seq(
      "https://reqres.in/api/users/1",
      "https://reqres.in/api/users/%",  // Bad Request
      "https://reqres.in/api/users/3",  // This call usually times out, which is useful for our exercise
      "https://reqrep.co/api/users/4")  // Unknown host

    urls map { url =>
      println(System.currentTimeMillis + " " + url)
      ws.url(url).get().map { response =>
        println(System.currentTimeMillis + " " + url)
        if (response.status == OK) response.body
        else throw new ApiException(response.statusText)
      } recover {
        case e => throw new ApiException(s"$url ${e.getMessage}")
      } transform { Try(_) } // Wrap future responses in Success or Failure instances
    }
  }

  /**
    * Main function.
    * @see https://stackoverflow.com/questions/20874186/scala-listfuture-to-futurelist-disregarding-failed-futures
    */
  def index = Action.async {

    val futures: Seq[Future[Try[String]]] = remoteCall()
    val seq    : Future[Seq[Try[String]]] = Future.sequence(futures)
    seq map { results: Seq[Try[String]] =>
      println(results.mkString("  \n"))
      Ok("")
    }

    // Wrapping in Try is useful because we can collect successful and failed responses
    // and process them individually, like in:
    //    val successes: Future[Seq[String]]    = seq.map(_.collect { case Success(x) => x })
    //    val failures : Future[Seq[Throwable]] = seq.map(_.collect { case Failure(x) => x })
    //    successes map { seq => Ok(seq.mkString(",")) }

  }

}
```

## Sample results

Simultaneous requests
```
1563023834730 https://reqres.in/api/users/1
1563023834731 https://reqres.in/api/users/%
1563023834732 https://reqres.in/api/users/3
1563023834733 https://reqrep.co/api/users/4
```

Followed some millisecs later by responses from valid requests
```
1563023834745 https://reqres.in/api/users/1
1563023834965 https://reqres.in/api/users/%
```

And finally, the results
```
Success({"data":{"id":1,"email":"george.duh@reqres.in","first_name":"George","last_name":"Duh"}})  
Failure(controllers.ApiException: https://reqres.in/api/users/% Bad Request)  
Failure(controllers.ApiException: https://reqres.in/api/users/3 handshake timed out)  
Failure(controllers.ApiException: https://reqrep.co/api/users/4 reqrep.co: nodename nor servname provided, or not known)
```
