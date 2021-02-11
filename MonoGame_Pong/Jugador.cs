using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame_Pong
{
    class Jugador
    {
        int _veocidad = 3;
        Texture2D _textura;
        Point _posicion;
        public Rectangle rectangulo { get => new Rectangle(_posicion.X - (_textura.Width / 2), _posicion.Y - (_textura.Height / 2), _textura.Width, _textura.Height); }

        public Jugador()
        {
            _textura = Tools.Texture.CreateColorTexture(Game1.graphicsDeviceManager.GraphicsDevice, Color.Red, 20, 50);
            _posicion = new Point(50, 250);
        }


        public void Update()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Up) && _posicion.Y >= 0)
            {
                _posicion.Y -= _veocidad;
            }
            else if (keyboardState.IsKeyDown(Keys.Down) && _posicion.Y <= 500)
            {
                _posicion.Y += _veocidad;
            }
        }

        public void Draw(SpriteBatch rspriteBatch)
        {
            rspriteBatch.Draw(_textura, rectangulo, Color.White);
        }
    }
}
