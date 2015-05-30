module MonoGame

open System

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

open Types
open Animation

type Resources = {
    Pitch : Texture2D
    Font : SpriteFont
    Ball : Texture2D
}

let tickPerMinute = TimeSpan.FromSeconds 1.

type MyGame (width, height) as this = 
    inherit Game()

    let graphics = new GraphicsDeviceManager(this)
    do graphics.PreferredBackBufferWidth <- width
    do graphics.PreferredBackBufferHeight <- height

    let mutable spriteBatch = Unchecked.defaultof<SpriteBatch>
    let mutable resources = Unchecked.defaultof<Resources>

    let mutable ball = Unchecked.defaultof<Animation>

    let mutable state = {Match = newMatch(); Time = 0u}

    let renderPitch (sb: SpriteBatch) =
        sb.Draw(resources.Pitch, Vector2.Zero, Color.White)

    let renderInformation (sb: SpriteBatch) =
        let color = Color.Red
        sb.DrawString(resources.Font, (sprintf "Minutes: %A" state.Time), Vector2(3.f), color)
        sb.DrawString(resources.Font, 
            (sprintf "%i:%i" state.Match.GoalsTeam1.Length state.Match.GoalsTeam2.Length), 
            Vector2(float32 this.GraphicsDevice.Viewport.Width/2.f-8.f, 3.f), color)

    override x.LoadContent() =
        spriteBatch <- new SpriteBatch(this.GraphicsDevice)
        this.Content.RootDirectory <- "Content"
        resources <- {
            Pitch = this.Content.Load<Texture2D>("Images/pitch");
            Font = this.Content.Load<SpriteFont>("Fonts/Font");
            Ball = this.Content.Load<Texture2D>("Images/ball")
        }
        ball <- createAnimation resources.Ball 4 (TimeSpan.FromSeconds(0.5)) |> unpause
        

    override x.Draw gameTime = 
        this.GraphicsDevice.Clear Color.CornflowerBlue
        spriteBatch.Begin()
        renderPitch spriteBatch
        renderInformation spriteBatch
        drawAnimation spriteBatch ball 100 100
        spriteBatch.End()
        ()
    
    override x.Update gameTime = 
        ball <- tick ball gameTime
