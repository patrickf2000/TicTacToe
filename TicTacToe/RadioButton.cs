/*
 * MIT License
 * 
 * Copyright (c) 2018 Patrick Flynn
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE. 
*/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TicTacToe
{
    public class RadioButton
    {
        private Rectangle rect;
        private Texture2D rectTexture;
        private Point position;
        private bool selected;
        private Color squareColor;

        public RadioButton(int x, int y, Texture2D texture)
        {
            rectTexture = texture;
            position = new Point(x, y);
            rect = new Rectangle(position, new Point(18));
            selected = false;
            squareColor = Color.White;
        }

        public bool HasCoords(int x, int y)
        {
            if (rect.Contains(x, y))
            {
                return true;
            }
            return false;
        }

        public void SetChecked(bool c)
        {
            selected = c;
            if (selected)
            {
                squareColor = Color.DarkBlue;
            }
            else
            {
                squareColor = Color.White;
            }
        }

        public bool IsChecked()
        {
            return selected;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(rectTexture, rect, squareColor);
        }
    }
}

