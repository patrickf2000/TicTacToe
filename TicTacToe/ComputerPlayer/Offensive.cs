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
using System.Collections.Generic;

namespace TicTacToe.ComputerPlayer
{
    public class Offensive
    {
        private LinkedList<GameSquare> xSquares;
        private LinkedList<GameSquare> oSquares;

        public Offensive(LinkedList<GameSquare> xSq, LinkedList<GameSquare> oSq)
        {
            xSquares = xSq;
            oSquares = oSq;
        }

        private int CheckAndReturnNo(int x1, int x2, int x3)
        {
            if (RobotUtils.HasTwo(oSquares, x1, x2, x3))
            {
                int no = RobotUtils.MissingNumber(oSquares, x1, x2, x3);
                if (!RobotUtils.HasSquare(xSquares, no))
                {
                    return no;
                }
            }
            return -1;
        }

        public int Move()
        {
            int square = CheckAndReturnNo(1, 2, 3);
            if (square != -1)
            {
                return square;
            }
            square = CheckAndReturnNo(4, 5, 6);
            if (square != -1)
            {
                return square;
            }
            square = CheckAndReturnNo(7, 8, 9);
            if (square != -1)
            {
                return square;
            }
            square = CheckAndReturnNo(1, 4, 7);
            if (square != -1)
            {
                return square;
            }
            square = CheckAndReturnNo(2, 5, 8);
            if (square != -1)
            {
                return square;
            }
            square = CheckAndReturnNo(3, 6, 9);
            if (square != -1)
            {
                return square;
            }
            square = CheckAndReturnNo(1, 5, 9);
            if (square != -1)
            {
                return square;
            }
            square = CheckAndReturnNo(3, 5, 7);
            if (square != -1)
            {
                return square;
            }
            square = -1;
            return square;
        }
    }
}
