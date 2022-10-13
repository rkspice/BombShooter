using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BombShooter.Models;
using BombShooter.Sprites;

namespace BombShooter;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public static Random Random;

    public static int ScreenWidth;
    public static int ScreenHeight;

    private List<Sprite> _sprites;
    private float _timer;
    private bool _hasStarted = false;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        Random = new Random();

        _graphics.PreferredBackBufferWidth = 720;
        _graphics.PreferredBackBufferHeight = 640;

        ScreenWidth = _graphics.PreferredBackBufferWidth;
        ScreenHeight = _graphics.PreferredBackBufferHeight;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        Restart();
    }

    private void Restart()
    {
        var playerTexture = Content.Load<Texture2D>("UpShip");

        _sprites = new List<Sprite>()
        {
            new Player(playerTexture)
            {
                Bullet = new Bullet(Content.Load<Texture2D>("Bullet")),
                Position = new Vector2((ScreenWidth / 2) - (playerTexture.Width / 2), (ScreenHeight - playerTexture.Height)),
                Input = new Input()
                {
                    Left = Keys.A,
                    Right = Keys.D,
                },
                Speed = 10f,
            }
        };

        _hasStarted = false;
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here
        if (Keyboard.GetState().IsKeyDown(Keys.P))
            _hasStarted = true;

        if (Keyboard.GetState().IsKeyDown(Keys.R))
            Restart();

        if (!_hasStarted)
            return;

        _timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        foreach (var sprite in _sprites.ToArray())
            sprite.Update(gameTime, _sprites);

        if (_timer > 0.25f)
        {
            _timer = 0;
            _sprites.Add(new Bomb(Content.Load<Texture2D>("Bomb")));
        }

        for (int i = 0; i < _sprites.Count; i++)
        {
            var sprite = _sprites[i];

            if (sprite.IsRemoved)
            {
                _sprites.RemoveAt(i);
                i--;
            }

            if (sprite is Player)
            {
                var player = sprite as Player;
                if (player.HasDied)
                    _hasStarted = false;

                // Restart();
            }

        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // TODO: Add your drawing code here
        _spriteBatch.Begin();

        foreach (var sprite in _sprites)
            sprite.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
