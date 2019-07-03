
# On Futures, Promises and Actors

## Promise

While futures are defined as a type of read-only placeholder object created for a result which doesn’t yet exist, a promise can be thought of as a writable, single-assignment container, which completes a future. That is, a promise can be used to successfully complete a future with a value (by “completing” the promise) using the success method. Conversely, a promise can also be used to complete a future with an exception, by failing the promise, using the failure method.


## Futures and Actors

Futures and actors are very similar.


## Using the Scheduler

The default PlayFramework application creates the following code:

```scala
  private def getFutureMessage(delayTime: FiniteDuration): Future[String] = {
    val promise: Promise[String] = Promise[String]()
    actorSystem.scheduler.scheduleOnce(delayTime) {
      promise.success("Hi!")
    }(actorSystem.dispatcher) // run scheduled tasks using the actor system's dispatcher
    promise.future
  }
```

actorSystem is used for scheduling tasks in the future.


## Using the Default Thread Pool

> Play default thread pool - This is the thread pool in which all of your application code in Play Framework is executed. It is an Akka dispatcher, and is used by the application ActorSystem. It can be configured by configuring Akka, described below.
>
> All actions in Play Framework use the default thread pool. When doing certain asynchronous operations, for example, calling map or flatMap on a future, you may need to provide an implicit execution context to execute the given functions in. An execution context is basically another name for a ThreadPool.
>
> In most situations, the appropriate execution context to use will be the Play default thread pool. This is accessible through @Inject()(implicit ec: ExecutionContext)

For example:

```scala
class Samples @Inject()(components: ControllerComponents)(implicit ec: ExecutionContext)
    extends AbstractController(components) {
  def someAsyncAction = Action.async {
    someCalculation()
      .map { result =>
        Ok(s"The answer is $result")
      }
      .recover {
        case e: TimeoutException =>
          InternalServerError("Calculation timed out!")
      }
  }

  def someCalculation(): Future[Int] = {
    Future.successful(42)
  }
}
```


References

* [Scheduling asynchronous tasks](https://www.playframework.com/documentation/2.7.x/ScheduledTasks)
* [Using the default thread pool](https://www.playframework.com/documentation/2.7.x/ThreadPools#Using-the-default-thread-pool)
* [What is an Actor?](https://doc.akka.io/docs/akka/current/general/actors.html#what-is-an-actor-)
* [Don't use Actors for concurrency](https://www.chrisstucchio.com/blog/2013/actors_vs_futures.html)
* [When to use Actors vs Futures?](https://stackoverflow.com/questions/23922530/when-to-use-actors-vs-futures)
* [How to get started with a simple Scala/Akka Actor (Hello, world)](https://alvinalexander.com/scala/scala-akka-actor-how-get-started-simple-example-hello-world)
* [Integrating with Akka](https://www.playframework.com/documentation/2.7.x/ScalaAkka)
* [Futures and Promises](https://docs.scala-lang.org/overviews/core/futures.html)
