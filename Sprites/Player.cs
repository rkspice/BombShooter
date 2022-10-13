using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BombShooter.Sprites
{
    public class Player : Sprite
    {
        public bool HasDied = false;
        public Bullet Bullet;

        public Player(Texture2D texture) : base(texture) { }

        public override void Draw(SpriteBatch spriteBatch) { base.Draw(spriteBatch); }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            _previousKey = _currentKey;
            _currentKey = Keyboard.GetState();

            Move();
            Shoot(sprites);

            foreach (var sprite in sprites)
                if (sprite is Bomb && sprite.Rectangle.Intersects(Rectangle))
                    HasDied = true;

            Position += Velocity;

            // Keep the sprite on the screen 
            Position.X = MathHelper.Clamp(Position.X, 0, Game1.ScreenWidth - Rectangle.Width);

            // Reset the velocity for when the user isn't holding a key. 
            Velocity = Vector2.Zero;
        }

        private void Shoot(List<Sprite> sprites)
        {
            if (_currentKey.IsKeyDown(Keys.Space) && _previousKey.IsKeyUp(Keys.Space))
            {
                var bullet = Bullet.Clone() as Bullet;
                bullet.Speed = 8f;
                bullet.Position = Position;
                bullet.Parent = this;

                sprites.Add(bullet);
            }
        }

        private void Move()
        {
            if (Input == null)
                throw new Exception("Pleas assign a value to 'input'");

            if (_currentKey.IsKeyDown(Input.Left))
                Velocity.X = -Speed;

            if (_currentKey.IsKeyDown(Input.Right))
                Velocity.X = Speed;
        }
    }
}