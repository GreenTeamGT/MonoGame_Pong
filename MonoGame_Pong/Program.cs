/*
 
 
 
 */

using System;

namespace MonoGame_Pong
{
    class Program
    {
        static void Main() {
            using (var game = new Game1())
                game.Run();
        }
    }
}
