namespace Brain.Interpreter.Console

open System
open System.Collections.Generic


type private Memory (map:Map<int,byte>) =
        member this.Map = map
        
        static member Empty():Memory=
            Memory Map.empty
        member this.read(idx:int):char =
            if this.Map.ContainsKey idx
                then char this.Map.[idx]
                else char 0
        member this.Inc(idx:int) : Memory =
             if this.Map.ContainsKey idx
             then
                 this.Map |> Map.map (fun key value -> if key =idx  then value+(byte 1) else value) |> Memory
             else
                 this.Map.Add(idx,(byte 0)+(byte 1)) |> Memory
        member this.Dec(idx:int): Memory =
            if this.Map.ContainsKey idx
            then
                this.Map |> Map.map (fun key value -> if key =idx  then value-(byte 1) else value) |> Memory
            else
                 this.Map.Add(idx,(byte 0)-(byte 1)) |> Memory
        member this.Write(idx:int , newVal : char)=
              this.Map.Add(idx,byte newVal) |> Memory
           

type private Input (str : string) =
        member this.Str = str
        
        static member from (str: string) :Input =
            Input str
        
        member this.head:char=
            str.Substring(0,1).[0]
        member this.tail:Input=
            str.Substring(1) |> Input
        
type private Output (str: string) =
        member this.Str = str
        
        static member Empty():Output =
            Output ""
            
        member this.append(c:char):Output = 
           Output (str+(string c))

type BrainFuck (program : string) =
   
    member this.Program = program
                  
    member this.Process(input: string  )=
        this.Step (0 ,0,Memory.Empty(), Input.from input, Output.Empty())  
        
    member private  this.Step (ip : int, dp :int,memory: Memory, input: Input,output: Output ) : string =
        if (ip>=this.Program.Length)
        then
            output.Str
        else
            match this.Program.[ip] with
            | '>' -> this.Step(ip+1,dp+1,memory,input,output)
            | '<' -> this.Step(ip+1,dp-1,memory,input,output)
            | '+' -> this.Step (ip+1,dp,dp |> memory.Inc,input,output)
            | '-' -> this.Step (ip+1,dp,dp |> memory.Dec , input,output)
            | '.' -> this.Step(ip+1,dp,memory,input,memory.read dp |> output.append)
            | ',' -> this.Step(ip+1,dp, memory.Write(dp, input.head),input.tail,output)
            | '[' -> this.Step((if memory.read(dp)=char 0 then this.JumpForward(ip) else ip+1 ) ,dp, memory, input, output)
            | ']' -> this.Step((if memory.read(dp)>char 0 then this.JumpBack(ip) else ip+1),dp, memory, input, output)
            | _ -> this.Step(ip+1,dp,memory,input,output)
        
    member this.JumpForward(ip :int):int =
        let mutable seek = 1
        let mutable ipNew = ip
        while seek>0 do
            ipNew<-ipNew+1
            seek<-match this.Program.[ipNew] with
            | '[' ->seek+1
            | ']' ->seek-1
            | _ -> seek
        ipNew+1
    member this.JumpBack(ip:int):int =
        let mutable seek = 1
        let mutable ipNew = ip
        while seek>0 do
            ipNew <- ipNew-1           
            seek <- match this.Program.[ipNew] with
            | ']' -> seek+1
            | '[' -> seek-1
            | _ -> seek
        ipNew+1    