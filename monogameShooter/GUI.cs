using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monogameShooter
{
    internal class GUI
    {
        private Texture2D heartTexture;
        private Texture2D gameOverTexture;
        private Vector2 heartTextureSize = new Vector2(64, 64);

        public GUI (Texture2D heartTexture, Texture2D gameOverTexture)
        {
            this.heartTexture = heartTexture;
            this.gameOverTexture = gameOverTexture;
        }

        public void draw(SpriteBatch spriteBatch, Player player)
        {
            for (int i = 0; i < player.maxHealth; i++)
            {
                Color col = Color.Black;
                if (i < player.health) col = Color.Red;

                spriteBatch.Draw(heartTexture, new Rectangle(i * (int)this.heartTextureSize.X, 0,
                                        (int)this.heartTextureSize.X, (int)this.heartTextureSize.Y), col);
            }
        }
    }
}
