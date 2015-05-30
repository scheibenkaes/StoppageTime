module Animation

open System
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

type AnimationState = 
    | Paused
    | Running

type Animation = 
    { CurrentFrame : int
      AnimationTime : TimeSpan
      CurrentFrameShown : TimeSpan
      Frames : Texture2D
      State : AnimationState
      Width : int
      Height : int
      NumberOfTiles : int }

let pause anim = { anim with State = Paused }
let unpause anim = { anim with State = Running }

let tick anim (gameTime : GameTime) = 
    if anim.State = Paused then anim
    else 
        let elapsed = anim.CurrentFrameShown + gameTime.ElapsedGameTime
        if elapsed >= anim.AnimationTime then 
            let nextFrame = anim.CurrentFrame + 1
            { anim with CurrentFrame = 
                            (if nextFrame >= anim.NumberOfTiles then 0
                             else nextFrame)
                        CurrentFrameShown = TimeSpan.Zero }
        else { anim with CurrentFrameShown = elapsed }

let createAnimation (texture : Texture2D) numTiles animTime = 
    { CurrentFrame = 0
      CurrentFrameShown = TimeSpan.Zero
      Frames = texture
      State = Paused
      Width = (texture.Width / numTiles)
      Height = texture.Height
      AnimationTime = animTime
      NumberOfTiles = numTiles }

let drawAnimation (sb : SpriteBatch) (animation : Animation) x y = 
    let destRect = Rectangle(x, y, animation.Width, animation.Height)
    let sourceRect = Rectangle(animation.CurrentFrame * animation.Width, 0, animation.Width, animation.Height)
    sb.Draw(animation.Frames, destRect, Nullable(sourceRect), Color.White)