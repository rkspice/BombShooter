using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BombShooter.Sprites
{
    public class Bullet : Sprite
    {
        public Bullet(Texture2D texture) : base(texture)
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            // if bullet is at top of screen THEN IsRemoved = true
            if (Rectangle.Bottom <= 0)
                IsRemoved = true;

            // if bullet collides with a bomb THEN remvoe both
            foreach (var sprite in sprites)
            {
                if (sprite is Bomb && sprite.Rectangle.Intersects(Rectangle))
                {
                    sprite.IsRemoved = true;
                    IsRemoved = true;
                }
            }

            // move bullet position
            Position.Y -= Speed;
        }
    }
}