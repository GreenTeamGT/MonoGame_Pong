using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using zUI;

/*
 === Algunas referencias ===
 https://www.monogame.net/webdemo/
 https://github.com/MonoGame/MonoGame.WebDemo
 https://github.com/MonoGame/MonoGame.WebDemo/tree/gh-pages
 https://awesomeopensource.com/project/aloisdeniel/awesome-monogame
 https://github.com/dotnet-ad/Transform
 https://awesomeopensource.com/projects/monogame
 https://awesomeopensource.com/project/riperiperi/FreeSO
 */

namespace MonoGame_Pong
{
    public class Wellknown
    {
        public class Default
        {
            public static readonly int FPS = 60;

            public static readonly int Width = 700;
            public static readonly int Height = 500;
        }

        public class Font
        {
            public static readonly char[,] chars = new char[,]
            {
                { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' },
                { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' },
                { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0' },
                { ',', ':', ';', '?', '.', '!', ' ','\'','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0','\0' }
            };
        }
    }


    public class Game1 : Game
    {
        Jugador jugador;
        Pelota pelota;
        Enemigo enemigo;

        Label gameOverLabel;

        public static GameState gameState;

        public static GraphicsDeviceManager graphicsDeviceManager;
        public static ContentManager contentManager;
        SpriteBatch spriteBatch;

        public Game1()
        {
            // Window
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            graphicsDeviceManager.PreferredBackBufferWidth = Wellknown.Default.Width;
            graphicsDeviceManager.PreferredBackBufferHeight = Wellknown.Default.Height;
            graphicsDeviceManager.ApplyChanges();

            // FPS
            base.IsFixedTimeStep = true;
            base.TargetElapsedTime = TimeSpan.FromSeconds(1d / Wellknown.Default.FPS);

            // Content
            string absolutePath = Path.Combine(Environment.CurrentDirectory, "Content");
            base.Content.RootDirectory = absolutePath;
            Game1.contentManager = base.Content;

            // others
            base.Window.Title = "Pong!";
            base.IsMouseVisible = true;

            // Iniciar assets
            jugador = new Jugador();
            pelota = new Pelota();
            enemigo = new Enemigo();
            gameState = GameState.modo_juego;

            SpriteFont _spriteFont = Tools.Font.GenerateFont(texture2D: Tools.Texture.GetTexture(graphicsDeviceManager.GraphicsDevice, contentManager, "MyFont_PNG_260x56"), chars: Wellknown.Font.chars);

            gameOverLabel = new Label(new Rectangle(0, 0, 700, 250), _spriteFont, "GAME OVER!\nPRESS 'P' TO RESTART", Label.TextAlignment.Midle_Center, Color.Green, lineSpacing: 15);

            // Initialize Game
            base.Initialize();
        }


        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime) {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            switch (gameState)
            {
                case GameState.modo_juego:
                    pelota.Update(enemigo, jugador);
                    enemigo.Update(pelota);
                    jugador.Update();

                    if (pelota.rectangulo.X < jugador.rectangulo.X || pelota.rectangulo.X > enemigo.rectangulo.X)
                    {
                        gameState = GameState.game_over;
                    }

                    break;
                case GameState.game_over:
                    if (Keyboard.GetState().IsKeyDown(Keys.P))
                    {
                        pelota = new Pelota();
                        gameState = GameState.modo_juego;
                    }
                    break;
                case GameState.pausa:
                    break;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            base.GraphicsDevice.Clear(Color.CornflowerBlue);
            this.spriteBatch.Begin();

            switch (gameState)
            {
                case GameState.modo_juego:
                    jugador.Draw(spriteBatch);
                    pelota.Draw(spriteBatch);
                    enemigo.Draw(spriteBatch);
                    break;
                case GameState.game_over:
                    jugador.Draw(spriteBatch);
                    pelota.Draw(spriteBatch);
                    enemigo.Draw(spriteBatch);
                    gameOverLabel.Draw(spriteBatch);
                    break;
                case GameState.pausa:
                    break;
            }

            this.spriteBatch.End();
            base.Draw(gameTime);
        }
    }

    public enum GameState
    {
        modo_juego,
        game_over,
        pausa
    }
}
