using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BombShooter.Sprites
{
    public class Bomb : Sprite
    {

        public Bomb(Texture2D texture) : base(texture)
        {
            Position = new Vector2(Game1.Random.Next(0, Game1.ScreenWidth - _texture.Width), -_texture.Height);
            Speed = Game1.Random.Next(3, 10);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Position.Y += Speed;

            // Bomb hit the bottom.
            if (Rectangle.Bottom >= Game1.ScreenHeight)
                IsRemoved = true;
        }
    }
}