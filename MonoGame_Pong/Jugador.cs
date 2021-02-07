using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame_Pong
{
    class Jugador
    {

        int _veocidad = 3;
        Texture2D _textura;
        Point _posicion;
        public Rectangle _rectangulo { get => new Rectangle(_posicion.X - (_textura.Width / 2), _posicion.Y - (_textura.Height / 2), _textura.Width, _textura.Height); }

        public Jugador() {
            _textura = Tools.Texture.CreateColorTexture(Game1._graphics.GraphicsDevice, Color.Red, 20, 50);
            _posicion = new Point(50, 250);
        }

        public void Draw(SpriteBatch rspriteBatch) {
            rspriteBatch.Draw(_textura, _rectangulo, Color.White);
        }
        public void Update() {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.Up) && _posicion.Y >= 0)
            {
                _posicion.Y -= _veocidad;
            }
            if (keyboardState.IsKeyDown(Keys.Down) && _posicion.Y <= 500)
            {
                _posicion.Y += _veocidad;
            }
        }

    }
}
