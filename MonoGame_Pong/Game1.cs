using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using zUI;

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
    public enum _GameState
    {
        modo_juego,
        game_over,
        pausa
    }
    public class Game1 : Game
    {

        Jugador _jugador;
        Pelota _pelota;
        Enemigo _enemigo;
        public static _GameState _gameState;
        Label _gameOverLabel;

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
            _gameState = _GameState.modo_juego;
            Texture2D _fontTexture = Tools.Texture.GetTexture(_graphics.GraphicsDevice, contentManager, "MyFont_PNG_260x56");
            char[,] _chars = {
                { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' },
                { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' },
                { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0' },
                { ',', ':', ';', '?', '.', '!', ' ','\'','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0' }
            };
            SpriteFont _spriteFont = Tools.Font.GenerateFont(_fontTexture, _chars);
            _gameOverLabel = new Label(new Rectangle(0, 0, 700, 250), _spriteFont, "GAME OVER!\nPRESS 'P' TO RESTART", Label.TextAlignment.Midle_Center, Color.Green, lineSpacing:15 );


            base.Initialize();

        }


        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            KeyboardState _keyboardState = Keyboard.GetState();

            if (_gameState == _GameState.modo_juego )
            {
                _pelota.Update(_enemigo, _jugador);
                _enemigo.Update(_pelota);
                _jugador.Update();
            } else if ( _gameState == _GameState.game_over ) { 
                if (_keyboardState.IsKeyDown(Keys.P))
                {
                    _pelota = new Pelota();
                    _gameState = _GameState.modo_juego;
                }
            }

            if (_pelota._rectangulo.X < _jugador._rectangulo.X || _pelota._rectangulo.X > _enemigo._rectangulo.X)
            {
                _gameState = _GameState.game_over;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            base.GraphicsDevice.Clear(Color.CornflowerBlue);
            this._spriteBatch.Begin();

            // ToDo: Code
            _jugador.Draw(_spriteBatch);
            _pelota.Draw(_spriteBatch);
            _enemigo.Draw(_spriteBatch);

            if (_gameState == _GameState.game_over)
            {
                _gameOverLabel.Draw(_spriteBatch);
            }

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
