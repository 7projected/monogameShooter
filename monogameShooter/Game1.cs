using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

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
        private SpriteFont font;
        private bool dead = false;
        
        Texture2D ballTexture;
        BallSpawner ballSpawner;
        GUI gui;
        Player player;
        Floor floor;
        GameManager gameManager;

        Dictionary<String, Texture2D> textureDict = new Dictionary<string, Texture2D>();

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

            font = Content.Load<SpriteFont>("font");

            Texture2D playerTexture = Content.Load<Texture2D>("img/character");
            Texture2D floorTexture = Content.Load<Texture2D>("img/ground_tile");
            Texture2D floorTexture1 = Content.Load<Texture2D>("img/ground_tile_1");
            Texture2D floorTexture2 = Content.Load<Texture2D>("img/ground_tile_2");
            Texture2D heartTexture = Content.Load<Texture2D>("img/heart_icon");
            Texture2D gameOverTexture = Content.Load<Texture2D>("img/you_died_screen");

            textureDict.Add("playerTexture", playerTexture);
            textureDict.Add("floorTexture", floorTexture);
            textureDict.Add("floorTexture1", floorTexture1);
            textureDict.Add("floorTexture2", floorTexture2);
            textureDict.Add("heartTexture", heartTexture);
            textureDict.Add("gameOverTexture", gameOverTexture);

            startGame();
        }

        public void startGame()
        {
            this.dead = false;
            ballTexture = Content.Load<Texture2D>("img/ball");

            ballSpawner = new BallSpawner(64, 5, ballTexture, 1);
            player = new Player(textureDict["playerTexture"], Vector2.Zero, 3, 30);
            floor = new Floor(textureDict["floorTexture"], textureDict["floorTexture1"], textureDict["floorTexture2"]);
            gui = new GUI(textureDict["heartTexture"], textureDict["gameOverTexture"]);
            gameManager = new GameManager(60 * 5, 2);

            ballSpawner.CollidedWithPlayer += player => player.damage();
            player.PlayerDied += () => { this.dead = true; }; // inline lambda useful for short "functions" without the whole thing
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState ks = Keyboard.GetState();

            if (!dead)
            {
                player.update(ks);
                ballSpawner.update(player);
                gameManager.update(ballSpawner);
            }
            if (ks.IsKeyDown(Keys.Space) && this.dead) startGame();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            floor.draw(_spriteBatch);
            player.draw(_spriteBatch);
            ballSpawner.draw(_spriteBatch);
            gui.draw(_spriteBatch, player, font, dead, gameManager);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
