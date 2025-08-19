using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace monogameShooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D ballTexture;
        Random ballRandom = new Random();
        int ballSize = 64;
        int ballSpeed = 5;

        Player player;
        Floor floor;
        Ball ball;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1280; // Example width
            _graphics.PreferredBackBufferHeight = 720;  // Example height

            _graphics.ApplyChanges(); // Apply the changes
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D playerTexture = Content.Load<Texture2D>("character");
            Texture2D floorTexture = Content.Load<Texture2D>("ground_tile");
            Texture2D floorTexture1 = Content.Load<Texture2D>("ground_tile_1");
            Texture2D floorTexture2 = Content.Load<Texture2D>("ground_tile_2");
            ballTexture = Content.Load<Texture2D>("ball");

            ball = new Ball(new Vector2(1280 / 2, 720 / 2), ballSize, ballRandom, ballSpeed);
            player = new Player(playerTexture, Vector2.Zero);
            floor = new Floor(floorTexture, floorTexture1, floorTexture2);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState ks = Keyboard.GetState();

            player.update(ks);
            ball.update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            floor.draw(_spriteBatch);
            player.draw(_spriteBatch);
            ball.draw(_spriteBatch, ballTexture);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
