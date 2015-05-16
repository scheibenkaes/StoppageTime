open StoppageTime.MonoGame

[<EntryPoint>]
let main argv = 
    use g = new MyGame()
    g.Run()
    0