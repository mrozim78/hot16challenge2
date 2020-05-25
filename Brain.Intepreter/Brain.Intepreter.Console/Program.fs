namespace Brain.Interpreter

open Brain.Interpreter.Console
open Brain.Interpreter.Console

module Program =

[<EntryPoint>]
let main argv =
    printf "Start interpreter BrainFuck"
    if argv.Length<1 
        then
        printf "\nError: No program set in argument 1"
        -1
    else   
        let program = argv.[0]
        printf "\nProgram : '%s'" program
        let input = if argv.Length=2 then argv.[1] else ""
        printf "\nInput : '%s'" input
        let result = BrainFuck(program).Process(input)
        printf "\nResult : '%s'" result     
        0        
    
