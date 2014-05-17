package something

import scala.collection.mutable.ListBuffer

object HelloScala {
  def main(args: Array[String]) {
    def getFizzBuzzSeq(i : Int) : Seq[String] = {
        val result = new ListBuffer[String]
        for (item <- 1 to i){
            if (item % 15 == 0)
                result+=FizzBuzzKeyword.fizzBuzz.toString()
            else if (item % 5 == 0)
                result+=FizzBuzzKeyword.buzz.toString()
            else if (item % 3 == 0)
                result+=FizzBuzzKeyword.fizz.toString()
            else
                result+=item.toString()
        }
        result
    }

    for(item <- getFizzBuzzSeq(50))
      print(item + " ")
  }
}
private object FizzBuzzKeyword extends Enumeration{
  val fizz = Value("Fizz")
  val buzz = Value("Buzz")
  val fizzBuzz = Value("FizzBuzz")
}