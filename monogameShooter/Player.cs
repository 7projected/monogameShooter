using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace monogameShooter
{
    internal class Player
    {
        public Sprite sprite;
        public Rectangle rect;
        public int speed;

        public Player(Texture2D texture, Vector2 position)
        {
            this.sprite = new Sprite(position, texture);
            this.rect = new Rectangle((int)position.X, (int)position.Y, 64, 64);
            this.speed = 10;
        }

        public void update(KeyboardState ks)
        {
            Vector2 dir = new Vector2(0, 0);

            if (ks.IsKeyDown(Keys.A)) dir.X -= 1; 
            if (ks.IsKeyDown(Keys.D)) dir.X += 1; 
            if (ks.IsKeyDown(Keys.W)) dir.Y -= 1; 
            if (ks.IsKeyDown(Keys.S)) dir.Y += 1;

            this.move_and_collide(dir);
        }

        public void move_and_collide(Vector2 dir)
        {
            Vector2 velocity = new Vector2((int)dir.X * this.speed, (int)dir.Y * this.speed);
            Rectangle nextRect = new Rectangle((int)this.rect.X + (int)velocity.X, (int)this.rect.Y + (int)velocity.Y,
                                                this.rect.Width, this.rect.Height);

            if (nextRect.Left >= 0 && nextRect.Right <= 1280) this.rect.X = nextRect.X;
            if (nextRect.Top >= 0 && nextRect.Bottom <= 720) this.rect.Y = nextRect.Y;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.sprite.texture, this.rect, Color.GreenYellow);
        }
    }
}
