using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGame_Pong
{
    public class Tools
    {
        public class Texture
        {
            /// <summary>
            /// Generate a new texture from a PNG file
            /// </summary>
            public static Texture2D GetTexture(GraphicsDevice graphicsDevice, ContentManager contentManager, string imageName, string folder = "")
            {
                string absolutePath = new DirectoryInfo(Path.Combine(Path.Combine(contentManager.RootDirectory, folder), $"{imageName}.png")).ToString();

                FileStream fileStream = new FileStream(absolutePath, FileMode.Open);

                var result = Texture2D.FromStream(graphicsDevice, fileStream);
                fileStream.Dispose();

                return result;
            }

            /// <summary>
            /// Get a new Texture2D from a bigger Texture2D
            /// </summary>
            public static Texture2D CropTexture(GraphicsDevice graphicsDevice, Texture2D originalTexture2D, Rectangle extractRectangle)
            {
                Texture2D subtexture = new Texture2D(graphicsDevice, extractRectangle.Width, extractRectangle.Height);
                int count = extractRectangle.Width * extractRectangle.Height;
                Color[] data = new Color[count];

                originalTexture2D.GetData(0, new Rectangle(extractRectangle.X, extractRectangle.Y, extractRectangle.Width, extractRectangle.Height), data, 0, count);
                subtexture.SetData(data);

                return subtexture;
            }

            /// <summary>
            /// Create a new Texture2D from a Color
            /// </summary>
            public static Texture2D CreateColorTexture(GraphicsDevice graphicsDevice, Color color, int Width = 1, int Height = 1)
            {
                Texture2D texture2D = new Texture2D(graphicsDevice, Width, Height, false, SurfaceFormat.Color);
                Color[] colors = new Color[Width * Height];

                // Set each pixel to color
                colors = colors
                            .Select(x => x = color)
                            .ToArray();

                texture2D.SetData(colors);

                return texture2D;
            }

            /// <summary>
			/// CreateCircleTexture
			/// </summary>
            public static Texture2D CreateCircleTexture(GraphicsDevice graphicsDevice, Color color, int radius = 1)
            {
                // Implementation
                {
                    List<Color> circle = new List<Color>();
                    for (int y = (radius * -1); y < radius; y++)
                    {
                        for (int x = (radius * -1); x < radius; x++)
                        {
                            float pitagoras = Pitagoras(x, y);

                            if (pitagoras <= radius)
                                circle.Add(color);
                            else
                                circle.Add(Color.Transparent);
                        }
                    }

                    Texture2D texture2D = new Texture2D(graphicsDevice, radius * 2, radius * 2, false, SurfaceFormat.Color);
                    texture2D.SetData(circle.ToArray());

                    return texture2D;
                }

                // Helpers
                float Pitagoras(int x, int y)
                {
                    // r = (x^2 + y^2)^(1/2)
                    return (float)Math.Sqrt(((x * x) + (y * y)));
                }
            }

            internal static Texture2D CreateTriangle(GraphicsDevice graphicsDevice, Color color, int Width, int Height)
            {
                List<Color> colors = new List<Color>();
                
                Point p1 = new Point(0,0); // top
                Point p2 = new Point(Width, Height/2); // middle
                Point p3 = new Point(0, Height); // down

                float m1 = Tools.MyMath.M(p1.ToVector2(), p2.ToVector2());
                float m2 = Tools.MyMath.M(p3.ToVector2(), p2.ToVector2());

                for (int h = 0; h < Height; h++)
                {
                    for (int w = 0; w < Width; w++)
                    {
                        if(h < Height / 2)
                        {
                            int result = (int)(m1 * w + p1.Y);

                            if (result<=h)
                                colors.Add(color);
                            else
                                colors.Add(Color.Transparent);
                        }
                        else
                        {
                            int result = (int)(m2 * w + p3.Y);

                            if (result >= h)
                                colors.Add(color);
                            else
                                colors.Add(Color.Transparent);
                        }
                        
                    }
                }



                Texture2D texture2D = new Texture2D(graphicsDevice, Width, Height, false, SurfaceFormat.Color);
                texture2D.SetData(colors.ToArray());

                return texture2D;
            }
        }


        public class Font
        {
            /// <summary>
            /// Generate a new font from a Texture2D
            /// </summary>
            public static SpriteFont GenerateFont(Texture2D texture2D, char[,] chars)
            {
                int charWidth = texture2D.Width / chars.GetLength(1);
                int charHigh = texture2D.Height / chars.GetLength(0);

                // ===== Implementation =====
                {
                    List<FontChar> fontChars = GetFontChar(chars);

                    // The line spacing (the distance from baseline to baseline) of the font
                    List<char> characters = fontChars.Select(x => x._char).ToList();

                    // The rectangles in the font texture containing letters
                    List<Rectangle> glyphBounds = fontChars.Select(x => x.glyphBound).ToList();

                    // The cropping rectangles, which are applied to the corresponding glyphBounds to calculate the bounds of the actual character
                    List<Rectangle> cropping = fontChars.Select(x => x.cropping).ToList();

                    // The line spacing (the distance from baseline to baseline) of the font
                    int lineSpacing = charHigh + 2;

                    // The spacing (tracking) between characters in the font
                    float spacing = 0f;

                    // The letters kernings(X - left side bearing, Y - width and Z - right side bearing)
                    List<Vector3> kerning = fontChars.Select(x => x.kerning).ToList();

                    // The character that will be substituted when a given character is not included in the font
                    char defaultCharacter = '?';

                    SpriteFont spriteFont = new SpriteFont(texture2D, glyphBounds, cropping, characters, lineSpacing, spacing, kerning, defaultCharacter);

                    return spriteFont;
                }

                // ===== Helpers =====
                List<FontChar> GetFontChar(char[,] chars)
                {
                    List<FontChar> fontChars = new List<FontChar>();
                    for (int column = 0; column < chars.GetLength(0); column++)
                    {
                        for (int element = 0; element < chars.GetLength(1); element++)
                        {
                            fontChars.Add(new FontChar(
                                                    chars[column, element],
                                                    new Rectangle(element * charWidth, column * charHigh, charWidth, charHigh)));
                        }
                    }
                    return fontChars.Where(x => x._char != '\0').OrderBy(x => x._char).ToList();
                }
            }

            class FontChar
            {
                public char _char { get; }
                public Rectangle glyphBound { get; }
                public Rectangle cropping { get; }
                public Vector3 kerning { get; }

                public FontChar(char c, Rectangle glyphBound)
                {
                    this._char = c;
                    this.glyphBound = glyphBound;
                    this.cropping = new Rectangle(0, 0, 0, 0);
                    this.kerning = new Vector3(0, glyphBound.Width, glyphBound.Width / 3);
                }
            }

            /// <summary>
            /// Get a SpriteFont from ContentManager
            /// </summary>
            public static SpriteFont GetFont(ContentManager contentManager, string fontName, string folder = "")
            {
                return contentManager.Load<SpriteFont>(Path.Combine(folder, fontName));
            }

        }


        public class Sound
        {
            /// <summary>
            /// Get SoundEffect from WAV file
            /// </summary>
            public static SoundEffect GetSoundEffect(GraphicsDevice graphicsDevice, ContentManager contentManager, string soundName, string folder = "")
            {
                string absolutePath = new DirectoryInfo(Path.Combine(Path.Combine(contentManager.RootDirectory, folder), $"{soundName}.wav")).ToString();

                FileStream fileStream = new FileStream(absolutePath, FileMode.Open);

                SoundEffect result = SoundEffect.FromStream(fileStream);

                fileStream.Dispose();

                return result;
            }
        }

        public class MyMath
        {
            /// <summary>
            /// Calculate inclination
            /// </summary>
            public static float M(Vector2 start, Vector2 direction)
            {
                float y = direction.Y - start.Y;
                float x = direction.X - start.X;

                if (x == 0f)
                    return 0;
                else
                    return y / x;
            }

            public static float B(float x, float y, float m)
            {
                return y - (m * x);
            }

            public static double DegreeToRadian(double degree)
            {
                return ((Math.PI / 180) * degree);
            }

            public static double RadianToDegree(double radian)
            {
                return radian / (Math.PI / 180);
            }


            /// <summary>
            /// Return the angle between vector p11 --> p12 and p21 --> p22.
            /// Angles less than zero are to the left. Angles greater than
            /// zero are to the right.
            /// </summary>
            public static double GetAngleInRadians(Point Point1_Start, Point Point_1_End, Point Point2_Start, Point Pount2_End)
            {
                // Code thanks to: http://csharphelper.com/blog/2020/06/find-the-angle-between-two-vectors-in-c/

                // Find the vectors.
                Point v1 = new Point(Point_1_End.X - Point1_Start.X, Point_1_End.Y - Point1_Start.Y);
                Point v2 = new Point(Pount2_End.X - Point2_Start.X, Pount2_End.Y - Point2_Start.Y);

                // Calculate the vector lengths.
                double len1 = Math.Sqrt(v1.X * v1.X + v1.Y * v1.Y);
                double len2 = Math.Sqrt(v2.X * v2.X + v2.Y * v2.Y);

                // Use the dot product to get the cosine.
                double dot_product = v1.X * v2.X + v1.Y * v2.Y;
                double cos = dot_product / len1 / len2;

                // Use the cross product to get the sine.
                double cross_product = v1.X * v2.Y - v1.Y * v2.X;
                double sin = cross_product / len1 / len2;

                // Find the angle.
                double angle = Math.Acos(cos);

                if (sin < 0) angle = -angle;
                return angle;
            }
        }
    }
}
