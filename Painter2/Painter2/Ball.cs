using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Painter2
{
    class Ball : ThreeColorGameObject
    {
        public bool Shooting { get; set; }

        public Ball()
            :base ("spr_ball_red","spr_ball_green","spr_ball_blue")
        {
            this.Reset();
        }

        public void Shoot(InputHelper inputHelper,ThreeColorGameObject cannonColor,RotatableSpriteGameObject cannonBarrel)
        {
            Shooting = true;
            Color = cannonColor.Color;

            Velocity = (inputHelper.MousePosition - cannonBarrel.GlobalPosition)*1.2f;
            float opp = (float) Math.Sin(cannonBarrel.Angle)*cannonBarrel.Width*0.6f;
            float adj = (float) Math.Cos(cannonBarrel.Angle)*cannonBarrel.Width*0.6f;
            Position = cannonColor.Position + new Vector2(adj, opp) + new Vector2(3, 3);
            Visible = true;
            //Assest Manager Faalt hier OverMars QQ
            //Painter.AssetManager.PlaySound("snd_shoot_paint");
        }

        public override void Reset()
        {
            base.Reset();
            Visible = false;
            velocity = Vector2.Zero;
            Shooting = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (Shooting)
            {
                velocity.X *= 0.99f;
                velocity.Y += 6;
            }
            PainterGameWorld pgw = GameWorld as PainterGameWorld;
            if (pgw.IsOutsideWorld((GlobalPosition)))
            {
                Reset();
            }
            base.Update(gameTime);
        }
    }
}
