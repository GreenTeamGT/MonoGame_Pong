using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;

/*
 https://www.monogame.net/webdemo/
https://github.com/MonoGame/MonoGame.WebDemo
https://github.com/MonoGame/MonoGame.WebDemo/tree/gh-pages
HTMLCanvasElement((
From eli to Everyone:  02:04 PM
https://awesomeopensource.com/project/aloisdeniel/awesome-monogame
https://github.com/dotnet-ad/Transform
https://awesomeopensource.com/projects/monogame
https://awesomeopensource.com/project/riperiperi/FreeSO

 */

namespace MonoGame_Pong
{
    public class Game1 : Game
    {

        Jugador _jugador;
        Pelota _pelota;
        Enemigo _enemigo;

        public static GraphicsDeviceManager _graphics;
        public static ContentManager contentManager;
        SpriteBatch _spriteBatch;

        public Game1() {
            // Window
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 700;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();

            // FPS
            base.IsFixedTimeStep = true;
            base.TargetElapsedTime = TimeSpan.FromSeconds(1d / 60);


            // Content
            string absolutePath = Path.Combine(Environment.CurrentDirectory, "Content");
            base.Content.RootDirectory = absolutePath;
            Game1.contentManager = base.Content;


            // others
            if (true)
            {
                // base.Window.IsBorderless = true;
                Rectangle gameWindow = base.Window.ClientBounds;
                base.Window.Title = "Pong!";
                base.IsMouseVisible = true;
            }

            _jugador = new Jugador();
            _pelota = new Pelota();
            _enemigo = new Enemigo();


            base.Initialize();

        }


        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            


            _pelota.Update(_enemigo, _jugador);
            _enemigo.Update(_pelota);
            _jugador.Update();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            base.GraphicsDevice.Clear(Color.CornflowerBlue);
            this._spriteBatch.Begin();

            // ToDo: Code
            _jugador.Draw(_spriteBatch);
            _pelota.Draw(_spriteBatch);
            _enemigo.Draw(_spriteBatch);

            this._spriteBatch.End();
            base.Draw(gameTime);
        }

        public static Texture2D GetTexture(GraphicsDevice graphicsDevice/*, ContentManager contentManager, string imageName, string folder = ""*/) {
            // string absolutePath = new DirectoryInfo(Path.Combine(Path.Combine(contentManager.RootDirectory, folder), $"{imageName}.png")).ToString();
            string absolutePath = new DirectoryInfo(@"C:\Users\lpena\Desktop\test2.png").ToString();

            FileStream fileStream = new FileStream(absolutePath, FileMode.Open);

            var result = Texture2D.FromStream(graphicsDevice, fileStream);
            fileStream.Dispose();

            return result;
        }
    }

}
