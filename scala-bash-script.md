Example for implementing a scala bash script.

```scala
#!/bin/sh
exec scala "$0" "$@"
!#

import scala.concurrent.Future
import scala.util.{Failure, Success}
import scala.concurrent.ExecutionContext.Implicits.global

object Main extends App {
    def longOp = Future[Int] {
        Thread.sleep(1000)
        1 / 1
    }

    longOp.onComplete {
        case Success(value) => println(value)
        case Failure(e) => e.printStackTrace
    }

    Thread.sleep(5000)
}
```
