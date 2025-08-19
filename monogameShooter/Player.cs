using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace monogameShooter
{
    internal class Player
    {
        public event Action? PlayerDied;
        private bool dashPressed = false;
        private int maxDashFrame = 5;
        private int currentDashFrame = 0;
        private Vector2 dashDirection = new Vector2(0, 0);
        private int dashSpeed = 50;

        private Sprite sprite;
        private int speed;
        private int iFrameSpeedMult = 2;
        private int maxIFrames;
        private int currentIFrames;

        public int health;
        public int maxHealth;
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

        public Player(Texture2D texture, Vector2 position, int health, int maxIFrames)
        {
            this.sprite = new Sprite(position, texture);
            this.speed = 7;
            this.health = health;
            this.maxHealth = health;
            this.maxIFrames = maxIFrames;
            this.currentIFrames = 0;
        }

        public void update(KeyboardState ks)
        {
            bool dashLF = dashPressed;

            this.currentIFrames -= 1;
            this.currentDashFrame -= 1;
            Vector2 dir = new Vector2(0, 0);

            if (ks.IsKeyDown(Keys.A)) dir.X -= 1; 
            if (ks.IsKeyDown(Keys.D)) dir.X += 1; 
            if (ks.IsKeyDown(Keys.W)) dir.Y -= 1; 
            if (ks.IsKeyDown(Keys.S)) dir.Y += 1;

            dashPressed = ks.IsKeyDown(Keys.LeftShift); // if dash just pressed, not standing still, and can dash
            if (dashPressed == true && dashLF == false && dir != Vector2.Zero && currentDashFrame <= 0) dash(dir);

            this.move_and_collide(dir);
        }

        public void move_and_collide(Vector2 dir)
        {

            Vector2 velocity;

            if (this.currentDashFrame <= 0)
            {
                int s = speed;
                if (this.currentIFrames > 0) s *= this.iFrameSpeedMult;
                velocity = new Vector2((int)dir.X * s, (int)dir.Y * s);
            }
            else
            {
                velocity = new Vector2((int)this.dashDirection.X * this.dashSpeed,
                    (int)this.dashDirection.Y * this.dashSpeed);
            }

            Rectangle nextRect = new Rectangle((int)this.rect.X + (int)velocity.X, (int)this.rect.Y + (int)velocity.Y,
                                                this.rect.Width, this.rect.Height);
            if (nextRect.Left >= 0 && nextRect.Right <= 1280) this.sprite.position.X = nextRect.X;
            if (nextRect.Top >= 0 && nextRect.Bottom <= 720) this.sprite.position.Y = nextRect.Y;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            Color col = Color.GreenYellow;
            if (this.currentIFrames > 0) col = Color.Green;
            if (this.currentDashFrame > 0) col = Color.Blue;

            spriteBatch.Draw(this.sprite.texture, this.rect, col);
        }

        public void dash(Vector2 dir)
        {
            this.dashDirection = dir;
            this.currentDashFrame = this.maxDashFrame;
        }

        public void damage()
        {
            if (this.currentIFrames <= 0 && this.currentDashFrame <= 0)
            {
                this.health -= 1;
                this.manageHealth();
                this.currentIFrames = this.maxIFrames;
            }
        }

        public void manageHealth()
        {
            if (this.health <= 0)
            {
                PlayerDied?.Invoke();
            }
        }
    }
}
