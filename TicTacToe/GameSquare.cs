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
using System.Collections.Generic;

namespace TicTacToe
{
    public class GameSquare
    {
        public PLAYER_ID id = PLAYER_ID.X;
        public int SquareID = 1;
        public bool clicked = false;
        public bool isWinningSquare = false;

        private Texture2D texture;
        private SpriteFont font;
        private Rectangle boundingRect;
        private Color textColor = Color.Red;
        int x = 0;
        int y = 0;

        public GameSquare(Texture2D sqrTexture, SpriteFont sqrFont, int sqID)
        {
            texture = sqrTexture;
            font = sqrFont;
            SquareID = sqID;
            boundingRect = new Rectangle(x, y, 100, 100);
        }

        public void SetPosition(Point pos)
        {
            x = pos.X;
            y = pos.Y;
            boundingRect.X = x;
            boundingRect.Y = y;
        }

        public bool Contains(int x, int y)
        {
            return boundingRect.Contains(x, y);
        }

        public void SetClicked(PLAYER_ID ID)
        {
            id = ID;
            clicked = true;
        }

        public void SetAsWinningSquare()
        {
            isWinningSquare = true;
            textColor = Color.Green;
        }

        public void SetAsDraw()
        {
            textColor = Color.Blue;
        }

        public void Reset()
        {
            clicked = false;
            textColor = Color.Red;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, boundingRect, Color.GhostWhite);
            string str = "X";
            if (id == PLAYER_ID.O)
            {
                str = "O";
            }
            if (clicked)
            {
                spriteBatch.DrawString(font, str, new Vector2(boundingRect.Center.X, boundingRect.Center.Y), textColor);
            }
        }

        public static LinkedList<GameSquare> initGameSquares(Texture2D pixelTexture, SpriteFont xoFont)
        {
            GameSquare sq1 = new GameSquare(pixelTexture, xoFont, 1);
            sq1.SetPosition(new Point(100, 50));

            GameSquare sq2 = new GameSquare(pixelTexture, xoFont, 2);
            sq2.SetPosition(new Point(220, 50));

            GameSquare sq3 = new GameSquare(pixelTexture, xoFont, 3);
            sq3.SetPosition(new Point(340, 50));

            GameSquare sq4 = new GameSquare(pixelTexture, xoFont, 4);
            sq4.SetPosition(new Point(100, 170));

            GameSquare sq5 = new GameSquare(pixelTexture, xoFont, 5);
            sq5.SetPosition(new Point(220, 170));

            GameSquare sq6 = new GameSquare(pixelTexture, xoFont, 6);
            sq6.SetPosition(new Point(340, 170));

            GameSquare sq7 = new GameSquare(pixelTexture, xoFont, 7);
            sq7.SetPosition(new Point(100, 290));

            GameSquare sq8 = new GameSquare(pixelTexture, xoFont, 8);
            sq8.SetPosition(new Point(220, 290));

            GameSquare sq9 = new GameSquare(pixelTexture, xoFont, 9);
            sq9.SetPosition(new Point(340, 290));

            LinkedList<GameSquare> gameSquares = new LinkedList<GameSquare>();
            gameSquares.AddLast(sq1);
            gameSquares.AddLast(sq2);
            gameSquares.AddLast(sq3);
            gameSquares.AddLast(sq4);
            gameSquares.AddLast(sq5);
            gameSquares.AddLast(sq6);
            gameSquares.AddLast(sq7);
            gameSquares.AddLast(sq8);
            gameSquares.AddLast(sq9);

            return gameSquares;
        }
    }
}
