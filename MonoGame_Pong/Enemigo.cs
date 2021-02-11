using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Pong
{
    class Enemigo
    {
        Texture2D textura;
        Point posicion;
        public Rectangle rectangulo { get => new Rectangle(posicion.X - (textura.Width / 2), posicion.Y - (textura.Height / 2), textura.Width, textura.Height); }

        public Enemigo()
        {
            textura = Tools.Texture.CreateColorTexture(Game1.graphicsDeviceManager.GraphicsDevice, Color.Red, 20, 50);
            posicion = new Point(650, 250);
        }
        
        public void Update(Pelota pelota)
        {
            this.posicion.Y = pelota.rectangulo.Center.Y;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textura, rectangulo, Color.White);
        }
    }
}
