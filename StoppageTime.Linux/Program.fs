open StoppageTime.MonoGame

[<EntryPoint>]
let main argv = 
    use g = new MyGame(800, 600)
    g.IsMouseVisible <- true
    g.Run()
    0