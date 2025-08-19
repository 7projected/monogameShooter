using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogameShooter
{
    internal class Floor
    {
        private Texture2D texture;
        private Texture2D texture1;
        private Texture2D texture2;
        private Vector2 textureSize = new Vector2(64, 64);
        private int sizeX = 20;
        private int sizeY = 12;
        private bool randomized = false;
        List<Vector2> alter1 = new List<Vector2>();
        List<Vector2> alter2 = new List<Vector2>();
        private Random rnd = new Random();

        public Floor(Texture2D texture, Texture2D texture1, Texture2D texture2)
        {
            this.texture = texture;
            this.texture1 = texture1;
            this.texture2 = texture2;
        }

        private void randomize_map()
        {
            this.randomized = true;
            
            for (int x = 0; x < this.sizeX; x++)
            {
                for (int y = 0; y < this.sizeY; y++)
                {
                    int randomNum = rnd.Next(1, 20);
                    if (randomNum == 1) alter1.Add(new Vector2(x, y)); // Add modifies list instead of creating new one
                    if (randomNum == 2) alter2.Add(new Vector2(x, y));
                }
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            if (!this.randomized) this.randomize_map();
            for (int x = 0; x < this.sizeX; x++)
            {
                for (int y = 0; y < this.sizeY; y++)
                {
                    Texture2D txt = this.texture;
                    foreach(Vector2 vec in alter1) if (vec.X == x && vec.Y == y) txt = this.texture1;
                    foreach (Vector2 vec in alter2) if (vec.X == x && vec.Y == y) txt = this.texture2;
                  
                        Rectangle drawRect = new Rectangle(x * (int)this.textureSize.X, y * (int)this.textureSize.Y,
                            (int)this.textureSize.X, (int)this.textureSize.Y);
                    spriteBatch.Draw(txt, drawRect, Color.White);
                }
            }
        }
    }
}
