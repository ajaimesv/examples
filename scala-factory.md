```scala
println("Factory methods")

object Animal {

    trait Speak {
        def speak
    }

    private class Dog extends Speak {
        println("a dog is born")
        override def speak = println("woof")
    }

    private class Cat() extends Speak {
        println("a cat is born")
        override def speak = println("meow")
    }

    def apply(animal: String) = {
        animal match {
            case "dog" => new Dog
            case "cat" => new Cat
        }
    }

}

// You can't call the class directly, because it's private
// val x = new Dog

val dog = Animal("dog")
dog.speak
val cat = Animal("cat")
cat.speak

// The following lines show that:
// - You can pass a function as a parameter
// - The difference between assigning to a 'val' and a 'def' is that a def is executed until we use it,
//   while a 'val' is executed immediately.

val lassie = Animal {
    "dog"
}
Thread.sleep(3000)
lassie.speak

def rocket = Animal {
    "dog"
}
Thread.sleep(3000)
rocket.speak
```
