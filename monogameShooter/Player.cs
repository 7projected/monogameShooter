using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace monogameShooter
{
    internal class Player
    {
        public Sprite sprite;
        public int speed;
        public Rectangle rect
        {
            get
            { 
    /* Getter that returns a rectangle using sprite position and 64, 64
       If im not mistaken, 
        !!!   its basically a function called whenever this.rect is referenced. so it returns a new rect*/
                return new Rectangle((int)sprite.position.X, (int)sprite.position.Y, 64, 64);
            }
        }

        public Player(Texture2D texture, Vector2 position)
        {
            this.sprite = new Sprite(position, texture);
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

            if (nextRect.Left >= 0 && nextRect.Right <= 1280) this.sprite.position.X = nextRect.X;
            if (nextRect.Top >= 0 && nextRect.Bottom <= 720) this.sprite.position.Y = nextRect.Y;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.sprite.texture, this.rect, Color.GreenYellow);
        }
    }
}
