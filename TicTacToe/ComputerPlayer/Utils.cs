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
    public class RobotUtils
    {
        private static bool HasANumber(LinkedList<GameSquare> squares, int no)
        {
            bool found = false;
            foreach (GameSquare sq in squares)
            {
                if (sq.SquareID==no)
                {
                    found = true;
                }
            }
            return found;
        }

        public static bool HasTwo(LinkedList<GameSquare> squares, int x1, int x2, int x3)
        {
            LinkedList<bool> foundNos = new LinkedList<bool>();
            if (HasANumber(squares,x1))
            {
                foundNos.AddLast(true);
            }
            if (HasANumber(squares,x2))
            {
                foundNos.AddLast(true);
            }
            if (HasANumber(squares,x3))
            {
                foundNos.AddLast(true);
            }
            if (foundNos.Count==2)
            {
                return true;
            }
            return false;
        }

        //Returns the value of the number that the player does not have
        public static int MissingNumber(LinkedList<GameSquare> squares, int x1, int x2, int x3)
        {
            int no = -1;
            LinkedList<int> availableIDs = new LinkedList<int>();
            foreach (GameSquare sq in squares)
            {
                availableIDs.AddLast(sq.SquareID);
            }
            if (!availableIDs.Contains(x1))
            {
                no = x1;
            } else if (!availableIDs.Contains(x2))
            {
                no = x2;
            } else if (!availableIDs.Contains(x3))
            {
                no = x3;
            }
            return no;
        }

        public static bool HasSquare(LinkedList<GameSquare> squares, int no)
        {
            bool found = false;
            foreach(GameSquare sq in squares)
            {
                if (sq.SquareID==no)
                {
                    found = true;
                }
            }
            return found;
        }
    }
}
