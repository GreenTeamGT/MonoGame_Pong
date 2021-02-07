using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1() {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize() {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            base.GraphicsDevice.Clear(Color.CornflowerBlue);
            this._spriteBatch.Begin();

            // ToDo: Code

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
