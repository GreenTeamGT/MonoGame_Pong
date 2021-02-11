using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Pong
{
    class Pelota
    {
        int direccionX = 1;
        int direccionY = 1;
        int velocidad = 3;

        Texture2D textura;
        Point posicion;
        public Rectangle rectangulo { get => new Rectangle(posicion.X - (textura.Width / 2), posicion.Y - (textura.Height / 2), textura.Width, textura.Height); }

        public Pelota()
        {
            textura = Tools.Texture.CreateColorTexture(Game1.graphicsDeviceManager.GraphicsDevice, Color.Blue, 10, 10);
            posicion = new Point(350, 250);
        }


        public void Update(Enemigo enemigo, Jugador jugador)
        {
            // check collision
            {
                if (this.rectangulo.Intersects(enemigo.rectangulo))
                {
                    direccionX = -1;
                }
                if (this.rectangulo.Intersects(jugador.rectangulo))
                {
                    direccionX = 1;
                }
                posicion.X += direccionX * velocidad;
            }

            // update position
            {
                if (posicion.Y >= 500)
                {
                    direccionY = -1;
                }
                if (posicion.Y <= 0)
                {
                    direccionY = 1;
                }
                posicion.Y += direccionY * velocidad;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, rectangulo, Color.White);
        }
    }
}