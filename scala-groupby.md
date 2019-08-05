# Using groupBy

A list of examples using `groupBy` on lists.
`identity` is the identity function.

```
> List('a', 'b', 'a').groupBy(identity)
res0: scala.collection.immutable.Map[Char,List[Char]] = Map(b -> List(b), a -> List(a, a))

> List(1, 2, 3, 2).groupBy(identity)
res1: scala.collection.immutable.Map[Int,List[Int]] = Map(2 -> List(2, 2), 1 -> List(1), 3 -> List(3))

> List(1, 2, 3, 2).groupBy(_ == 2)
res2: scala.collection.immutable.Map[Boolean,List[Int]] = Map(false -> List(1, 3), true -> List(2, 2))

> List("maggie", "marge", "lisa", "homer", "bart").groupBy(_.charAt(0))
res3: scala.collection.immutable.Map[Char,List[String]] = Map(h -> List(homer), b -> List(bart), m -> List(maggie, marge), l -> List(lisa))
```

Applying mapValues to the resulting map:

```
> List('a', 'b', 'a').groupBy(identity).mapValues(_.size).toList
res0: List[(Char, Int)] = List((b,1), (a,2))
```
