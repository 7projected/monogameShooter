using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace monogameShooter
{
    internal class Ball
    {
        private Vector2 center;
        private Vector2 velocity;
        private int size;
        private int speed;
        private Rectangle rect
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

        public void update()
        {
            Rectangle nextRect = new Rectangle((int)rect.X + ((int)this.velocity.X * this.speed),
                                               (int)rect.Y + ((int)this.velocity.Y * this.speed),
                                               rect.Width, rect.Height);

            if (nextRect.Left <= 0 || nextRect.Right >= 1280) this.velocity.X *= -1;
            if (nextRect.Top <= 0 || nextRect.Bottom >= 720) this.velocity.Y *= -1;

            this.center.X += this.velocity.X * this.speed;
            this.center.Y += this.velocity.Y * this.speed;
        }

        public void draw(SpriteBatch spriteBatch, Texture2D texture)
        {
            spriteBatch.Draw(texture, this.rect, Color.Blue);
        }


    }
}
