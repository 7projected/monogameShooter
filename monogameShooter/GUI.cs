using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monogameShooter
{
    internal class GUI
    {
        private Texture2D heartTexture;
        private Texture2D gameOverTexture;
        private Vector2 heartTextureSize = new Vector2(64, 64);

        //Manages score as well.

        public GUI (Texture2D heartTexture, Texture2D gameOverTexture)
        {
            this.heartTexture = heartTexture;
            this.gameOverTexture = gameOverTexture;
        }

        public void draw(SpriteBatch spriteBatch, Player player, SpriteFont font, bool dead, GameManager gm)
        {
            if (!dead)
            {
                for (int i = 0; i < player.maxHealth; i++)
                {
                    Color col = Color.Black;
                    if (i < player.health) col = Color.Red;

                    spriteBatch.Draw(heartTexture, new Rectangle(i * (int)this.heartTextureSize.X, 0,
                                            (int)this.heartTextureSize.X, (int)this.heartTextureSize.Y), col);
                    spriteBatch.DrawString(font, $"Score: {gm.score}", new Vector2(0, 720 - 32), Color.White);
                }
            }
            else
            {
                spriteBatch.Draw(this.gameOverTexture, Vector2.Zero, Color.White);

                string score = $"Score: {gm.score}";
                Vector2 textSize = font.MeasureString(score);
                Vector2 screenCenter = new Vector2(1280 / 2, 4 * (720 / 5));
                Vector2 position = screenCenter - textSize / 2;

                spriteBatch.DrawString(font, score, position, Color.White);

            }
            
        }
    }
}
