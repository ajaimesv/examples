
# Simplifying for comprehensions

In this example we make a call to a future that may fail. 
The function returns either Future.successful or Future.failed. Unlike other examples that return a Try,
we don't need to try to recover here, that is, once a Future fails, we stop the process and return since we 
can't keep calling other functions because of parameter dependency.

`readId` apparentely does not need the `recoverWith` call. Results are the same with or without it, although 
it may be because I'm not calling a real function from it.

```scala
def readId: Future[Int] = {
  Future { throw new Exception("oops"); 1 }.flatMap {
    case r if r > 0 => Future.successful(r)
    case _ => Future.failed(new Exception("Too small"))
  } recoverWith {
    case e: Exception => Future.failed(e)
  }
}
```

Final code:

```scala
import scala.concurrent._
import scala.concurrent.ExecutionContext.Implicits.global
import scala.util._

case class User(id: Int, name: String)
val EmptyUser = User(0, "")

def readId: Future[Int] = {
  Future { throw new Exception("oops"); 1 }.flatMap {
    case n if n > 0 => Future.successful(n)
    case _ => Future.failed(new Exception("Too small"))
  }
}

def readUser(id: Int): Future[User] =
  Future(User(id, "John"))

def updateUser(user: User): Future[User] =
  Future(user)

val result = for {
  id <- readId
  user <- readUser(id)
  updated <- updateUser(user)
} yield updated

Thread.sleep(500)
println(result)
```

## Results

```
Failure(java.lang.Exception: oops)
```

Try to run it using different values, like removing `throw new Exception("oops");` from `readId`. Try to return
a negative value.
