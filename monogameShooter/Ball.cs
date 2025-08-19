using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;


namespace monogameShooter
{
    internal class Ball
    {
        private Vector2 center;
        private Vector2 velocity;
        private int size;
        private int speed;
        private bool isColl = false;
        public Rectangle rect
        {
            get
            {
                return new Rectangle((int)center.X - (size / 2), (int)center.Y - (size / 2), size, size);
            }
        }

        public Ball(Vector2 center, int size, Random rnd, int speed)
        {
            this.speed = speed;
            this.center = center;
            this.size = size;
            this.velocity = new Vector2(0, 0);
            while (this.velocity.X == 0) this.velocity.X = rnd.Next(1, 4) - 2;
            while (this.velocity.Y == 0) this.velocity.Y = rnd.Next(1, 4) - 2;
        }

        public bool update(Player player)
        {
            Rectangle nextRect = new Rectangle((int)rect.X + ((int)this.velocity.X * this.speed),
                                               (int)rect.Y + ((int)this.velocity.Y * this.speed),
                                               rect.Width, rect.Height);

            if (nextRect.Left <= 0 || nextRect.Right >= 1280) this.velocity.X *= -1;
            if (nextRect.Top <= 0 || nextRect.Bottom >= 720) this.velocity.Y *= -1;

            this.center.X += this.velocity.X * this.speed;
            this.center.Y += this.velocity.Y * this.speed;

            // Check collision with every ball and player each Update Frame
            return player.rect.Intersects(this.rect);
        }

        public void draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            Color col = Color.White;
            if (this.isColl) col = Color.Red;

            spriteBatch.Draw(texture, this.rect, col);
        }
    }

    internal class BallSpawner
    {
        private Random rng = new Random();
        public int ballsAlive;
        public List<Ball> ballList = new List<Ball>();
        public event Action<Player>? CollidedWithPlayer;

        private int ballSize;
        private int ballSpeed;
        private Texture2D ballTexture;

        public BallSpawner(int ballSize, int ballSpeed, Texture2D ballTexture, int ballsAlive)
        {
            this.ballSize = ballSize;
            this.ballSpeed = ballSpeed;
            this.ballTexture = ballTexture;
            this.ballsAlive = ballsAlive;
        }

        public void spawn_ball()
        {
            int rngX = this.rng.Next(100, 1200);
            int rngY = this.rng.Next(100, 600);
            ballList.Add(new Ball(new Vector2(rngX, rngY), this.ballSize, this.rng, this.ballSpeed));
        }

        public void update(Player player)
        {
            if (ballList.Count < ballsAlive) spawn_ball();
            foreach (Ball ball in ballList)
            {
                if (ball.update(player) == true){
                    CollidedWithPlayer?.Invoke(player);
                }
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            foreach (Ball ball in ballList)
            {
                ball.draw(spriteBatch, this.ballTexture);
            }
        }

    }
}
