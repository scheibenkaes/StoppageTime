namespace StoppageTime.MonoGame

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

open State

type Resources = {
    Pitch : Texture2D;
    Font : SpriteFont
}

type MyGame (width, height) as this = 
    inherit Game()

    let graphics = new GraphicsDeviceManager(this)
    do graphics.PreferredBackBufferWidth <- width
    do graphics.PreferredBackBufferHeight <- height

    let mutable spriteBatch = Unchecked.defaultof<SpriteBatch>
    let mutable resources = Unchecked.defaultof<Resources>

    let renderPitch (sb: SpriteBatch) =
        sb.Draw(resources.Pitch, Vector2.Zero, Color.White)

    override x.LoadContent() =
        spriteBatch <- new SpriteBatch(this.GraphicsDevice)
        this.Content.RootDirectory <- "Content"
        resources <- {
            Pitch = this.Content.Load<Texture2D>("Images/pitch");
            Font = this.Content.Load<SpriteFont>("Fonts/Font")
        }

    override x.Draw gameTime = 
        this.GraphicsDevice.Clear Color.CornflowerBlue
        spriteBatch.Begin()
        spriteBatch
        |> renderPitch
        |> ignore
        spriteBatch.End()
        ()
    
    override x.Update gameTime = ()
