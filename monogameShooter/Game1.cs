using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

/* TODO:
 * Player collision with balls
 * Player dashing
 * Scoring
 * Difficulty increasing as score increases
 * Make ball asteroid cause its on the moon
*/

namespace monogameShooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D ballTexture;
        BallSpawner ballSpawner;
        GUI gui;
        Player player;
        Floor floor;

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

            Texture2D playerTexture = Content.Load<Texture2D>("img/character");
            Texture2D floorTexture = Content.Load<Texture2D>("img/ground_tile");
            Texture2D floorTexture1 = Content.Load<Texture2D>("img/ground_tile_1");
            Texture2D floorTexture2 = Content.Load<Texture2D>("img/ground_tile_2");
            Texture2D heartTexture = Content.Load<Texture2D>("img/heart_icon");
            Texture2D gameOverTexture = Content.Load<Texture2D>("img/you_died_screen");
            ballTexture = Content.Load<Texture2D>("img/ball");

            ballSpawner = new BallSpawner(64, 5, ballTexture, 1);
            player = new Player(playerTexture, Vector2.Zero, 3, 30);
            floor = new Floor(floorTexture, floorTexture1, floorTexture2);
            gui = new GUI(heartTexture, gameOverTexture);

            ballSpawner.CollidedWithPlayer += player => player.damage();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState ks = Keyboard.GetState();

            player.update(ks);
            ballSpawner.update(player);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            floor.draw(_spriteBatch);
            player.draw(_spriteBatch);
            ballSpawner.draw(_spriteBatch);
            gui.draw(_spriteBatch, player);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
