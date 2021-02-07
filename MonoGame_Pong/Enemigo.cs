using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Pong
{
    class Enemigo
    {
        Texture2D _textura;
        Point _posicion;
        public Rectangle _rectangulo { get => new Rectangle(_posicion.X - (_textura.Width / 2), _posicion.Y - (_textura.Height / 2), _textura.Width, _textura.Height); }

        public Enemigo()
        {
            _textura = Tools.Texture.CreateColorTexture(Game1._graphics.GraphicsDevice, Color.Red, 20, 50);
            _posicion = new Point(650, 250);
        }
        
        public void Update(Pelota rpelota)
        {
            _posicion.Y = rpelota._rectangulo.Center.Y;
        }

        public void Draw(SpriteBatch rspriteBatch)
        {
            rspriteBatch.Draw(_textura, _rectangulo, Color.White);
        }
    }
}
