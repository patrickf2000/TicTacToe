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
using System;
using System.Collections.Generic;

namespace TicTacToe.ComputerPlayer
{
    public class Robot
    {
        private LinkedList<GameSquare> xSquares;
        private LinkedList<GameSquare> oSquares;
        private LinkedList<GameSquare> allSquares;

        public Robot(LinkedList<GameSquare> allSq)
        {
            allSquares = allSq;
        }

        public void Update(LinkedList<GameSquare> xSq, LinkedList<GameSquare> oSq, LinkedList<GameSquare> allSq)
        {
            xSquares = xSq;
            oSquares = oSq;
            allSquares = allSq;
        }

        private int ChooseRandom()
        {
            int chosen = 0;
            LinkedList<int> availableSquares = new LinkedList<int>();
            foreach (GameSquare sq in allSquares)
            {
                if (!sq.clicked)
                {
                    availableSquares.AddLast(sq.SquareID);
                }
            }
            Random rand = new Random();
            chosen = rand.Next(0,availableSquares.Count);
            int index = 0;
            int no = 0;
            foreach (int i in availableSquares)
            {
                if (index==chosen)
                {
                    no = i;
                }
                index++;
            }
            return no;
        }

        public int Move()
        {
            Defensive defensive = new Defensive(xSquares, oSquares);
            int no = defensive.Move();

            if (no==-1)
            {
                Offensive offensive = new Offensive(xSquares, oSquares);
                no = offensive.Move();
                
                if (no==-1)
                {
                    no = ChooseRandom();
                }
            }
            return no;
        }
    }
}
