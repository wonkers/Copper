using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Copper
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private GraphicsDevice device;
        private SpriteBatch _spriteBatch;
        private int ScreenWidth = 320;
        private int ScreenHeight = 256;
        private Texture2D backgroundTexture;
        private Vector2 backgoundPosition = Vector2.Zero;
        private int interval = 1;
        private int horizontalOffset = 0;
        private int minOffset = -96;
        private int maxOffset = 96;
        Color[] data;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            device = _graphics.GraphicsDevice;
            _graphics.PreferredBackBufferWidth = ScreenWidth;
            _graphics.PreferredBackBufferHeight = ScreenHeight;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            backgroundTexture = new Texture2D(device, ScreenWidth, ScreenHeight);
            data = new Color[ScreenWidth * ScreenHeight];

            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            int z = 0;
            int c = -15;
            bool d = false;
            for (int y = 0; y < ScreenHeight; y++)
            {
                for (int x = 0; x < ScreenWidth; x++)
                {
                    data[z] = Color.Transparent;
                    if (y > 128 + c + horizontalOffset)
                    {
                        d = true;
                        uint intensity = (uint)Math.Abs(Math.Abs(c) - 15);

                        //ABGR
                        uint color = 0xff000000 | ((intensity << 20)) | (intensity << 4);
                        data[z] = new Color(color);

                    }
                    if (y > 128 + 15 + horizontalOffset)
                    {
                        data[z] = Color.Transparent;
                    }

                    z++;
                }
                if (c < 15 && d == true)
                    c++;

            }
            backgroundTexture.SetData(data);

            horizontalOffset += interval;
            if (horizontalOffset <= minOffset || horizontalOffset >= maxOffset)
                interval *= -1; 

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(backgroundTexture, backgoundPosition, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
