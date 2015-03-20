using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Painter2
{
    class GameOver: GameObjectList
    {
        private SpriteGameObject gameOver;
        public GameOver()
        {
            gameOver = new SpriteGameObject("spr_gameover");
            //add GameOver
            this.Add(gameOver);
            gameOver.Position = new Vector2(Painter.Screen.X / 8, Painter.Screen.Y / 5);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.KeyPressed(Keys.Space))
            {
                Painter.GameStateManager.SwitchTo("playingState");
                
            }
        }

    }
}
