﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGame_Pong
{
    class Pelota
    {
        int _direccionX = 1;
        int _velocidad = 3;

        Texture2D _textura;
        Point _posicion;
        Rectangle _rectangulo { get => new Rectangle(_posicion.X - (_textura.Width / 2), _posicion.Y - (_textura.Height / 2), _textura.Width, _textura.Height); }

        public Pelota() {
            _textura = Tools.Texture.CreateColorTexture(Game1._graphics.GraphicsDevice, Color.Blue, 10, 10);
            _posicion = new Point(50, 50);
        }

        public void Draw(SpriteBatch rspriteBatch) {
            rspriteBatch.Draw(_textura, _rectangulo, Color.White);
        }
        public void Update(Enemigo renemigo, Jugador rjugador) {
            if (this._rectangulo.Intersects(renemigo._rectangulo))
            {
                _direccionX = -1;
            }
            if (this._rectangulo.Intersects(rjugador._rectangulo))
            {
                _direccionX = 1;
            }
            _posicion.X += _direccionX * _velocidad;
        }

    }
}
