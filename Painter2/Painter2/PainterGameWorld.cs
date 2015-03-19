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
    class PainterGameWorld : GameObjectList
    {
        private SpriteGameObject background = null;
        private RotatableSpriteGameObject cannonBarrel = null;
        private ThreeColorGameObject cannonColor = null;
        private ThreeColorGameObject can1 = null, can2 = null, can3 = null;
        private Ball ball = null;
        private SpriteGameObject scoreBar;
        private SpriteGameObject gameOver;
        public TextGameObject scoreText;
        public GameObjectList livesSprites;
        public int maxLives;
        private int score;
        private int lives;

        public PainterGameWorld()
        {
            background = new SpriteGameObject("spr_background");
            cannonBarrel = new RotatableSpriteGameObject("spr_cannon_barrel");
            cannonBarrel.Position = new Vector2(74, 404);
            cannonBarrel.Origin = new Vector2(34, 34);

            cannonColor = new ThreeColorGameObject("spr_cannon_red", "spr_cannon_green", "spr_cannon_blue");
            cannonColor.Position = new Vector2(58,388);

            can1 = new PaintCan(450f, Color.Red);
            can2 = new PaintCan(575f, Color.Green);
            can3 = new PaintCan(700f, Color.Blue);

            ball = new Ball();

            livesSprites = new GameObjectList();

            scoreText = new TextGameObject("GameFont");
            scoreBar = new SpriteGameObject("spr_scorebar");

            maxLives = 5;

            gameOver = new SpriteGameObject("spr_gameover");
            //add background sprite game object to the gameworld
            this.Add(background);
            
            //add cannon Barrel and Color
            this.Add(cannonBarrel);
            this.Add(cannonColor);
            
            //add cans
            this.Add(can1);
            this.Add(can2);
            this.Add(can3);

            //Add ball
            this.Add(ball);

            //initialise score and lives
            this.Score = 0;
            this.lives = 5;

            //add scoretext & bar
            this.Add(scoreBar);
            this.Add(scoreText);

            //add GameOver
            this.Add(gameOver);
            gameOver.Visible = false;
            //add sprites to livesSprites list
            for (int lifeNr = 0; lifeNr < maxLives; lifeNr++)
            {
                SpriteGameObject life = new SpriteGameObject("spr_lives", 0, lifeNr.ToString());
                life.Position = new Vector2(lifeNr * life.BoundingBox.Width, 30);
                livesSprites.Add(life);
            }
            this.Add(livesSprites);

            scoreText.Position = scoreBar.Position + (scoreBar.Center - scoreText.Size/2);
            gameOver.Position = new Vector2(Painter.Screen.X/6,Painter.Screen.Y/5);

        }

        public int Score
        {
            get { return score; }
            set { 
                    score = value;
                    if (scoreText != null)
                        scoreText.Text = "Score: " + value;
                }
        }

        public int Lives
        {
            

            get { return lives; }
            set {

                    if (value > maxLives)
                    return;
                    

                    // only amount of lifes numer of sprites are visable
                    for (int lifeNr = 0; lifeNr < maxLives; lifeNr++)
                    {
                        SpriteGameObject sgo = (SpriteGameObject)livesSprites.Find(lifeNr.ToString());
                        sgo.Visible = (lifeNr < value);
                    }

                    lives = value;
                if (lives <= 0)
                {
                    gameOver.Visible = true;
                }
                }
          
        }

        public bool IsOutsideWorld(Vector2 aPosition)
        {
            return aPosition.X < 0 || aPosition.X > Painter.Screen.X || aPosition.Y > Painter.Screen.Y;
        }

        public override void Reset()
        {
            this.Score = 0;
            this.Lives = 5;
            gameOver.Visible = false;
            //base.Reset();
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if(inputHelper.KeyPressed(Keys.R))
                cannonColor.Color = Color.Red;
            else if(inputHelper.KeyPressed(Keys.G))
                cannonColor.Color = Color.Green;
            else if (inputHelper.KeyPressed(Keys.B))
                cannonColor.Color = Color.Blue;
            else if (inputHelper.KeyPressed(Keys.P))
                this.Lives = 0;
            double opposite = inputHelper.MousePosition.Y - cannonBarrel.GlobalPosition.Y;
            double adjacent = inputHelper.MousePosition.X - cannonBarrel.GlobalPosition.X;
            cannonBarrel.Angle = (float)Math.Atan2(opposite, adjacent);

            if (inputHelper.MouseLeftButtonDown() && !ball.Shooting)
            {
                ball.Shoot(inputHelper,cannonColor,cannonBarrel);
            }
            if (gameOver.Visible)
            {
                if (inputHelper.KeyPressed(Keys.Space))
                {
                    Reset();
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (ball.CollidesWith(can1))
            {
                can1.Color = ball.Color;
                ball.Reset();
            }
            if (ball.CollidesWith(can2))
            {
                can2.Color = ball.Color;
                ball.Reset();
            }
            if (ball.CollidesWith(can3))
            {
                can3.Color = ball.Color;
                ball.Reset();
            }

            //continue with normal base behaviour
            base.Update(gameTime);
        }
    }
}

