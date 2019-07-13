# Scala Try/Success/Failure

Scala's `Try` function wraps any recoverable exception in a `Failure` instance and any valid result in a `Success` instance.

```scala
import scala.util.{Failure, Success, Try}

object Main extends App {

  val random = scala.util.Random

  def divide(): Try[Float] = {
    Try(1 / random.nextInt(2)) // divisor may be 0 or 1
  }

  List.range(1, 10) foreach { i =>
    divide() match {
      case Success(n) => println(s"$i. $n")
      case Failure(e) => println(s"$i. ${e.getMessage}")
    }
  }

}
```

## Sample results

```
1. / by zero
2. / by zero
3. 1.0
4. 1.0
5. 1.0
6. / by zero
7. / by zero
8. 1.0
9. / by zero
```
