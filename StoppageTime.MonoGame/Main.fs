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
    Player1 : Texture2D
    Player2 : Texture2D
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
    let mutable player1 = Unchecked.defaultof<Animation>
    let mutable player2 = Unchecked.defaultof<Animation>

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
            Ball = this.Content.Load<Texture2D>("Images/ball");
            Player1 = this.Content.Load<Texture2D>("Images/Player1");
            Player2 = this.Content.Load<Texture2D>("Images/Player2");
        }
        ball <- createAnimation resources.Ball 4 (TimeSpan.FromSeconds(0.5)) |> unpause
        player1 <- createAnimation resources.Player1 4 (TimeSpan.FromSeconds(0.5)) |> unpause
        player2 <- createAnimation resources.Player2 4 (TimeSpan.FromSeconds(0.5)) |> unpause |> faceLeft

    override x.Draw gameTime = 
        this.GraphicsDevice.Clear Color.CornflowerBlue
        spriteBatch.Begin()
        renderPitch spriteBatch
        renderInformation spriteBatch
        drawAnimation spriteBatch player1 75 75
        drawAnimation spriteBatch player2 175 175
        drawAnimation spriteBatch ball 115 120
        spriteBatch.End()
        ()
    
    override x.Update gameTime = 
        player1 <- tick player1 gameTime
        player2 <- tick player2 gameTime
        ball <- tick ball gameTime
