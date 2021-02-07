using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame_Pong
{
    class Jugador
    {
        Texture2D _textura;
        Point _posicion;
        public Rectangle _rectangulo { get => new Rectangle(_posicion.X - (_textura.Width / 2), _posicion.Y - (_textura.Height / 2), _textura.Width, _textura.Height); }

        public Jugador() {
            _textura = Tools.Texture.CreateColorTexture(Game1._graphics.GraphicsDevice, Color.Red, 20, 50);
            _posicion = new Point(0, 50);
        }

        public void Draw(SpriteBatch rspriteBatch) {
            rspriteBatch.Draw(_textura, _rectangulo, Color.White);
        }
        public void Update() {

        }

    }
}
