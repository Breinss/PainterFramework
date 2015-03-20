using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Painter2
{

    public class Painter : GameEnvironment
    {

        public Painter()
        {
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
        }



        protected override void LoadContent()
        {
            base.LoadContent();

            screen = new Point(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);

            //Console.WriteLine("Loading Content");

            gameStateManager.AddGameState("playingState", new PainterGameWorld());
            gameStateManager.AddGameState("gameoverState", new GameOver());
            gameStateManager.SwitchTo("playingState");

            AssetManager.PlayMusic("snd_music");
        }

        

    }
}
