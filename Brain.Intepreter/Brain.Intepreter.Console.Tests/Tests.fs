namespace Brain.Intepreter.Console.Tests

open System
open Brain.Interpreter.Console
open Microsoft.VisualStudio.TestTools.UnitTesting


[<TestClass>]
type TestClass () =
          
    [<TestMethod>]
    member this.ShouldOutputZeroByte () =
        // assign
        let program = "."
        let input = ""
        let brain = BrainFuck(program)
        let expected = [|char 0|] |>  System.String 
        //action
        let result = brain.Process(input)
        //assert        
        Assert.AreEqual(expected,result)
        
    [<TestMethod>]
    member this.ShouldIncrementTheCellValue() =
        //assign
        let program = "++++"
        let input = ""
        let brain = BrainFuck(program)
        let expected = [|char 4|] |>  System.String 
        //action
        let result = brain.Process(input)
        //assert
        Assert.AreEqual(expected,result)
        
    [<TestMethod>]
    member this.ShouldDecrementCellValue() =
        //assign
        let program = "++++---."
        let input = ""
        let brain = BrainFuck(program)
        let expected = [|char 1|] |>  System.String 
        //action
        let result = brain.Process(input)
        //assert
        Assert.AreEqual(expected,result)
        
    [<TestMethod>]
    member this.ShouldMoveDataPointerToTheRight() =
        //assign
        let program = "++++>++."
        let input = ""
        let brain = BrainFuck(program)
        let expected = [|char 2|] |>  System.String 
        //action
        let result = brain.Process(input)
        //assert
        Assert.AreEqual(expected,result)
        
    [<TestMethod>]
    member this.ShouldMoveDataPointerToTheLeft() =
        //assign
        let program=">++++>+++<-.<++."
        let input = ""
        let brain = BrainFuck(program)
        let expected = [|char 3; char 2 |] |>  System.String 
        //action
        let result = brain.Process(input)
        //assert
        Assert.AreEqual(expected,result)
    
    [<TestMethod>]
    member this.ShouldInterpretOpcodeWithUnsignedByteOverflow() =
        //assign
        let program ="+---."
        let input = ""
        let brain = BrainFuck(program)
        let expected = [|char 254|] |>  System.String 
        //action
        let result = brain.Process(input)
        //assert
        Assert.AreEqual(expected,result)
    
    [<TestMethod>]
    member this.ShouldInterpretOperationWithoutLoopAndInput() =
        //assign
        let program = "++.>+++.>+.<++.<-."
        let input = ""
        let brain = BrainFuck(program)
        let expected =[|char 2; char 3; char 1;char 5; char 1|] |>  System.String 
        //action
        let result = brain.Process(input)
        //assert
        Assert.AreEqual(expected,result)
        
    [<TestMethod>]
    member this.ShouldAcceptThreeCharsAndIncrementEachByTwo() =
         //assign
        let program = ",++.>,++.>,++."
        let brain = BrainFuck(program)
        let input = "abc"
        let expected ="cde"
        //action
        let result = brain.Process(input)
        //assert
        Assert.AreEqual(expected,result)
    
    [<TestMethod>]
    member this.ShouldInterpretLoop() =
         //assign
        let program =  "++++++++[>+++++++<-]>++.>+++++[>+++++++++<-]>.>+++++[>++++++++<-]>+."
        let input = ""
        let brain = BrainFuck(program)
        let expected =":-)"
        //action
        let result = brain.Process(input)
        //assert
        Assert.AreEqual(expected,result)
        
    [<TestMethod>]
    member this.ShouldInterpretHelloWorldProgram() =
         //assign
        let program =   "++++++++[>++++[>++>+++>+++>+<<<<-]>+>+>->>+[<]<-]>>.>---.+++++++..+++.>>.<-.<.+++.------.--------.>>+.>++."
        let input = ""
        let brain = BrainFuck(program)
        let expected ="Hello World!\n"
        //action
        let result = brain.Process(input)
        //assert
        Assert.AreEqual(expected,result)
        
      [<TestMethod>]
      member this.ShouldAcceptInputUntilCharTwoHundredTwentyFive() =
         //assign
        let program = ",+[-.,+]"
        let brain = BrainFuck(program)
        let input = "Lorem ipsum\255 dolor sit amet"
        let expected ="Lorem ipsum"
        //action
        let result = brain.Process(input)
        //assert
        Assert.AreEqual(expected,result)
        
      [<TestMethod>]
      member this.ShouldAcceptInputUntilCharZero() =
         //assign
        let program = ",[.[-],]"
        let brain = BrainFuck(program)
        let input = "F#\0 Rocks!"
        let expected ="F#"
        //action
        let result = brain.Process(input)
        //assert
        Assert.AreEqual(expected,result)
        
      [<TestMethod>]
      member this.SshouldMultiplyTwoNumbers() =
         //assign
        let program =  ",>,<[>[->+>+<<]>>[-<<+>>]<<<-]>>."
        let brain = BrainFuck(program)
        let input = [| char 12 ; char 6|] |>  System.String 
        let expected =[| char (12*6)|] |>   System.String 
        //action
        let result = brain.Process(input)
        //assert
        Assert.AreEqual(expected,result)