using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Trig_for_Animating_Waves_and_Circles
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        SpriteFont bouncingFont;
        Texture2D ballTexture;

        Rectangle wavingBallRect;

        // Ball traveling in a wave
        float ballWavestep;
        int ballWaveSpeed;  // Horizontal
        int ballWaveAmplitude;

        // Bouncing Letters
        string bouncingText;
        float bouncingTextY;
        float bouncingTextStep;
        float ballWavingOffset;
        int bouncingTextAmplitude;

        double circleX, circleY, circleStep;
        int circleAmplitude;



        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            ballWavestep = 0;
            ballWaveSpeed = 2;
            ballWaveAmplitude = 50;
            ballWavingOffset = 0;
            wavingBallRect = new Rectangle(10, 100, 50, 50);

            // Circle
            circleStep = 0;
            circleAmplitude = 50;
            circleX = 0;
            circleY = 0;


            // Text Values
            bouncingText = "BOUNCE!";
            bouncingTextAmplitude = 25;
            bouncingTextStep = 0;
            bouncingTextY = 75;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ballTexture = Content.Load<Texture2D>("baseball");
            bouncingFont = Content.Load<SpriteFont>("TextFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            // Ball Waving Logic
            ballWavingOffset = (int)Math.Round(-1 * Math.Sin(ballWavestep) * ballWaveAmplitude) + 200;
            ballWavestep += 0.1f;

            // Ball Horizontal Movement
            wavingBallRect.X += ballWaveSpeed;
            if (wavingBallRect.Right > _graphics.PreferredBackBufferWidth || wavingBallRect.X < 0)
                ballWaveSpeed *= -1;

            // Text Logic
            bouncingTextY = (int)Math.Round(-1 * Math.Sin(bouncingTextStep) * bouncingTextAmplitude);
            bouncingTextStep += 0.2f;

            // Circle Ball
            circleStep += 0.1;
            circleX = Math.Cos(circleStep) * circleAmplitude;
            circleY = -1 * Math.Sin(circleStep) * circleAmplitude;

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            // Wave Ball
            _spriteBatch.Draw(ballTexture, new Rectangle(wavingBallRect.X, wavingBallRect.Y + (int)Math.Round(ballWavingOffset), wavingBallRect.Width, wavingBallRect.Height), Color.White);
            
            // Letters Bouncing
            for (int i = 0; i < bouncingText.Length; i++) // Iterate through each letter
                _spriteBatch.DrawString(bouncingFont, bouncingText[i].ToString(), new Vector2(10 + 12 * i, 100 + (int)Math.Round(-1 * Math.Sin(bouncingTextStep + 0.2 * i) * bouncingTextAmplitude)), Color.Black);

            // Ball in circle
            _spriteBatch.Draw(ballTexture, new Rectangle(400 + (int)Math.Round(circleX), 200 + (int)Math.Round(circleY), 50, 50), Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}