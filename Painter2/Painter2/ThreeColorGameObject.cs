using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Painter2
{
    class ThreeColorGameObject : RotatableSpriteGameObject
    {
        protected SpriteSheet colorRed, colorGreen, colorBlue;
        protected Color color; 

        public ThreeColorGameObject(string redAssetName, string greenAssetName, string blueAssetName):base("")
        {
            colorRed = new SpriteSheet(redAssetName);
            colorGreen = new SpriteSheet(greenAssetName);
            colorBlue = new SpriteSheet(blueAssetName);

            Color = Color.Blue; 
        }

        public override void Reset()
        {
            base.Reset();

            color = Color.Blue;
        }

        public Color Color
        {
            get { return color; }
            set 
            {
                if (value != Color.Red && value != Color.Green && value != Color.Blue)
                    return;
                color = value;
                if (color == Color.Red)
                    sprite = colorRed;
                else if (color == Color.Green)
                    sprite = colorGreen;
                else if (color == Color.Blue)
                    sprite = colorBlue;
            }
        }
    }
}
