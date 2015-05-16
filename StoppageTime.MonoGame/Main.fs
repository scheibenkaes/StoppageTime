namespace StoppageTime.MonoGame

open Microsoft.Xna.Framework

type MyGame() as this = 
    inherit Game()

    let graphics = new GraphicsDeviceManager(this)
    do graphics.PreferredBackBufferWidth <- 300
    do graphics.PreferredBackBufferHeight <- 200
    do this.IsMouseVisible <- true

    override x.LoadContent() = ()
    
    override x.Draw gameTime = 
        this.GraphicsDevice.Clear Color.CornflowerBlue
        ()
    
    override x.Update gameTime = ()
