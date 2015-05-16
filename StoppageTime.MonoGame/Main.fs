namespace StoppageTime.MonoGame

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

open State

type MyGame (width, height) as this = 
    inherit Game()

    let graphics = new GraphicsDeviceManager(this)
    do graphics.PreferredBackBufferWidth <- width
    do graphics.PreferredBackBufferHeight <- height

    let mutable spriteBatch = Unchecked.defaultof<SpriteBatch>

    override x.LoadContent() =
        spriteBatch <- new SpriteBatch(this.GraphicsDevice)
    
    override x.Draw gameTime = 
        this.GraphicsDevice.Clear Color.CornflowerBlue
        spriteBatch.Begin()

        spriteBatch.End()
        ()
    
    override x.Update gameTime = ()
