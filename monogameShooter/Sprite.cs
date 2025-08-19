using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace monogameShooter
{
    internal class Sprite
    {
        public Vector2 position;
        public Texture2D texture;

        public Sprite(Vector2 position, Texture2D texture)
        {
            this.position = position;
            this.texture = texture;
        }
    }
}
