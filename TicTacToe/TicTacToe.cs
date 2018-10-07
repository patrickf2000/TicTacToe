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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TicTacToe.ComputerPlayer;

/*
 * Using the new square id (for checking if a player won):
 *     1 | 2 | 3
 *  -----|---|-----
 *     4 | 5 | 6
 *  -----|---|-----
 *     7 | 8 | 9
 *  -----|---|----- 
 */
namespace TicTacToe
{
	public class TicTacToe : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;
		Texture2D pixelTexture;
        SpriteFont xoFont, gameFont;
        LinkedList<GameSquare> gameSquares, xSquares, oSquares;
        MouseState oldMouseState;
        GameScreen gameScreen;
        PLAYER_ID currentPlayer, winningPlayer;
        int winCoord1, winCoord2, winCoord3;
        string currentMsg;
        Rectangle goButton, resetButton, menuButton, quitButton;
        Robot computerPlayer;
        bool twoPlayer;
        RadioButton onePlayerButton, twoPlayerButton;

		public TicTacToe ()
		{
			graphics = new GraphicsDeviceManager (this);
			this.IsMouseVisible = true;
			Content.RootDirectory = "Content";
		}

		protected override void Initialize ()
		{
			base.Initialize ();

            gameScreen = GameScreen.MENU;
            currentPlayer = PLAYER_ID.X;
            currentMsg = "Current Player: X";

            onePlayerButton = new RadioButton(21, 51, pixelTexture);
            onePlayerButton.SetChecked(true);
            twoPlayerButton = new RadioButton(21, 86, pixelTexture);
            goButton = new Rectangle(20, 130, 70, 25);
            
            gameSquares = GameSquare.initGameSquares(pixelTexture, xoFont);
            
            xSquares = new LinkedList<GameSquare>();
            oSquares = new LinkedList<GameSquare>();

            resetButton = new Rectangle(700, 20, 70, 30);
            menuButton = new Rectangle(700, 60, 70, 30);
            quitButton = new Rectangle(700, 100, 70, 30);

            computerPlayer = new Robot(gameSquares);
            twoPlayer = false;
        }

		protected override void LoadContent ()
		{
			spriteBatch = new SpriteBatch (GraphicsDevice);
			pixelTexture = Content.Load<Texture2D> ("pixel");
            xoFont = Content.Load<SpriteFont>("xoFont");
            gameFont = Content.Load<SpriteFont>("GameFont");
		}

        private bool CheckClick(int x, int y)
        {
            bool found = false;
            foreach (GameSquare sq in gameSquares)
            {
                if (sq.Contains(x, y))
                {
                    sq.SetClicked(currentPlayer);
                    found = true;
                }
            }
            return found;
        }

        private void UpdateLists()
        {
            xSquares.Clear();
            oSquares.Clear();
            foreach (GameSquare sq in gameSquares)
            {
                if (sq.clicked)
                {
                    if (sq.id==PLAYER_ID.X)
                    {
                        xSquares.AddLast(sq);
                    } else
                    {
                        oSquares.AddLast(sq);
                    }
                }
            }
        }

        /*
         * Winning combinations:
         * 1-2-3
         * 4-5-6
         * 7-8-9
         * 1-4-7
         * 2-5-8
         * 3-6-9
         * 1-5-9
         * 3-5-7
         */
        private bool CheckThreeNumbers(LinkedList<int> coords, int no1, int no2, int no3)
        {
            bool found = false;
            if ((coords.Contains(no1))&&(coords.Contains(no2))&&(coords.Contains(no3)))
            {
                winCoord1 = no1;
                winCoord2 = no2;
                winCoord3 = no3;
                found = true;
            }
            return found;
        }

        private bool CheckCoords(LinkedList<int> coords, PLAYER_ID ID)
        {
            winningPlayer = ID;
            if (CheckThreeNumbers(coords,1,2,3))
            {
                return true;
            } else if (CheckThreeNumbers(coords, 4, 5, 6))
            {
                return true;
            } else if (CheckThreeNumbers(coords, 7, 8, 9))
            {
                return true;
            } else if (CheckThreeNumbers(coords, 1, 4, 7))
            {
                return true;
            } else if (CheckThreeNumbers(coords, 2, 5, 8))
            {
                return true;
            } else if (CheckThreeNumbers(coords, 3, 6, 9))
            {
                return true;
            } else if (CheckThreeNumbers(coords, 1, 5, 9))
            {
                return true;
            } else if (CheckThreeNumbers(coords, 3, 5, 7))
            {
                return true;
            }
            return false;
        }

        private bool CheckWin()
        {
            UpdateLists();
            bool found = false;
            LinkedList<int> coords = new LinkedList<int>();
            foreach (GameSquare sq in xSquares)
            {
                coords.AddLast(sq.SquareID);
            }
            if (CheckCoords(coords,PLAYER_ID.X))
            {
                found = true;
                return found;
            }
            coords.Clear();
            foreach (GameSquare sq in oSquares)
            {
                coords.AddLast(sq.SquareID);
            }
            if (CheckCoords(coords,PLAYER_ID.O))
            {
                found = true;
                return found;
            }
            return found;
        }

        //This function must be called after CheckWin
        private bool CheckDraw()
        {
            bool found = false;
            if ((xSquares.Count+oSquares.Count)==9)
            {
                return true;
            }
            return found;
        }

        private void SetWinningSquares(LinkedList<GameSquare> squares)
        {
            foreach (GameSquare sq in squares)
            {
                if ((sq.SquareID==winCoord1)||(sq.SquareID==winCoord2)||(sq.SquareID==winCoord3))
                {
                    sq.SetAsWinningSquare();
                }
            }
        }

        private void Won()
        {
            gameScreen = GameScreen.GAME_OVER;
            if (winningPlayer == PLAYER_ID.X)
            {
                currentMsg = "Player X Won!";
                SetWinningSquares(xSquares);
            }
            else
            {
                currentMsg = "Player O Won!";
                SetWinningSquares(oSquares);
            }
        }

        private void Draw()
        {
            gameScreen = GameScreen.GAME_OVER;
            currentMsg = "Game Ended in a Draw!";
            foreach (GameSquare sq in gameSquares)
            {
                sq.SetAsDraw();
            }
        }

        private void Reset()
        {
            currentPlayer = PLAYER_ID.X;
            currentMsg = "Current Player: X";
            gameScreen = GameScreen.GAME;
            xSquares.Clear();
            oSquares.Clear();
            foreach (GameSquare sq in gameSquares)
            {
                sq.Reset();
            }
        }

        private void PostClick()
        {
            if (CheckWin())
            {
                Won();
            }
            else
            {
                if (CheckDraw())
                {
                    Draw();
                }
                else
                {
                    SwitchPlayer();
                }
            }
        }

        private void MovePlayerO()
        {
            computerPlayer.Update(xSquares, oSquares, gameSquares);
            int square = computerPlayer.Move();
            foreach (GameSquare sq in gameSquares)
            {
                if (sq.SquareID == square)
                {
                    sq.SetClicked(PLAYER_ID.O);
                }
            }
            PostClick();
        }

        private void SwitchPlayer()
        {
            if (currentPlayer == PLAYER_ID.X)
            {
                currentPlayer = PLAYER_ID.O;
                currentMsg = "Current Player: O";
                if (!twoPlayer)
                {
                    MovePlayerO();
                }
            }
            else
            {
                currentPlayer = PLAYER_ID.X;
                currentMsg = "Current Player: X";
            }
        }

        private void UpdateMenuScreen(MouseState currentMouseState)
        {
            if ((oldMouseState.LeftButton == ButtonState.Released) && (currentMouseState.LeftButton == ButtonState.Pressed))
            {
                if (onePlayerButton.HasCoords(currentMouseState.X, currentMouseState.Y))
                {
                    if (onePlayerButton.IsChecked())
                    {
                        onePlayerButton.SetChecked(false);
                        twoPlayerButton.SetChecked(true);
                    }
                    else
                    {
                        onePlayerButton.SetChecked(true);
                        twoPlayerButton.SetChecked(false);
                    }
                }
                else if (twoPlayerButton.HasCoords(currentMouseState.X, currentMouseState.Y))
                {
                    if (twoPlayerButton.IsChecked())
                    {
                        onePlayerButton.SetChecked(true);
                        twoPlayerButton.SetChecked(false);
                    }
                    else
                    {
                        onePlayerButton.SetChecked(false);
                        twoPlayerButton.SetChecked(true);
                    }
                }
                else if (goButton.Contains(currentMouseState.X, currentMouseState.Y))
                {
                    if (onePlayerButton.IsChecked())
                    {
                        twoPlayer = false;
                    }
                    else
                    {
                        twoPlayer = true;
                    }
                    gameScreen = GameScreen.GAME;
                }
            }
        }

        private void UpdateGameScreen(MouseState currentMouseState)
        {
            if ((oldMouseState.LeftButton == ButtonState.Released) && (currentMouseState.LeftButton == ButtonState.Pressed))
            {
                if ((currentPlayer==PLAYER_ID.X)||(twoPlayer))
                {
                    if (CheckClick(currentMouseState.X, currentMouseState.Y))
                    {
                        PostClick();
                    }
                }
            }
        }

        private void UpdateGameOverScreen(MouseState currentMouseState)
        {
            if ((oldMouseState.LeftButton==ButtonState.Released)&&(currentMouseState.LeftButton==ButtonState.Pressed))
            {
                if (quitButton.Contains(currentMouseState.X, currentMouseState.Y))
                {
                    System.Console.WriteLine("Exiting...");
                    this.Exit();
                } else if (menuButton.Contains(currentMouseState.X,currentMouseState.Y)) {
                    Reset();
                    gameScreen = GameScreen.MENU;
                } else if (resetButton.Contains(currentMouseState.X,currentMouseState.Y))
                {
                    Reset();
                }
            }
        }

		protected override void Update (GameTime gameTime)
		{
			#if !__IOS__ &&  !__TVOS__
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState ().IsKeyDown (Keys.Escape))
				Exit ();
            #endif

            MouseState currentMouseState = Mouse.GetState();
            if (oldMouseState==null)
            {
                oldMouseState = currentMouseState;
            }
            if (gameScreen == GameScreen.MENU)
            {
                UpdateMenuScreen(currentMouseState);
            } else if (gameScreen==GameScreen.GAME)
            {
                UpdateGameScreen(currentMouseState);
            } else
            {
                UpdateGameOverScreen(currentMouseState);
            }
            oldMouseState = currentMouseState;
            
			base.Update (gameTime);
		}

        private void DrawMenuScreen()
        {
            spriteBatch.DrawString(gameFont, "Welcome to TicTacToe!", new Vector2(300, 20), Color.CornflowerBlue);

            Rectangle outlineRect = new Rectangle(20, 50, 20, 20);
            spriteBatch.Draw(pixelTexture, outlineRect, Color.Black);
            onePlayerButton.Draw(spriteBatch);
            spriteBatch.DrawString(gameFont, "One Player", new Vector2(45,53), Color.Black);

            outlineRect.Location = new Point(20, 85);
            spriteBatch.Draw(pixelTexture, outlineRect, Color.Black);
            twoPlayerButton.Draw(spriteBatch);
            spriteBatch.DrawString(gameFont, "Two Player", new Vector2(45, 89), Color.Black);

            spriteBatch.Draw(pixelTexture, goButton, Color.OrangeRed);
            spriteBatch.DrawString(gameFont, "Go!", new Vector2(40, 135), Color.GreenYellow);
        }

        private void DrawGameOverScreen()
        {
            spriteBatch.Draw(pixelTexture, resetButton, Color.GreenYellow);
            spriteBatch.Draw(pixelTexture, menuButton, Color.Cyan);
            spriteBatch.Draw(pixelTexture, quitButton, Color.Orange);
            spriteBatch.DrawString(gameFont, "Reset", new Vector2(710, 25), Color.Black);
            spriteBatch.DrawString(gameFont, "Menu", new Vector2(715, 65), Color.Black);
            spriteBatch.DrawString(gameFont, "Quit", new Vector2(720, 105), Color.Black);
        }

        private void DrawGameScreen()
        {
            spriteBatch.Draw(pixelTexture, new Rectangle(100, 50, 340, 340), Color.Black);
            foreach (GameSquare sqr in gameSquares)
            {
                sqr.Draw(spriteBatch);
            }
            spriteBatch.DrawString(gameFont, currentMsg, new Vector2(550, 350), Color.Black);
            if (gameScreen==GameScreen.GAME_OVER)
            {
                DrawGameOverScreen();
            }
        }

		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.GhostWhite);

            spriteBatch.Begin();
            if (gameScreen == GameScreen.MENU)
            {
                DrawMenuScreen();
            }
            else if ((gameScreen == GameScreen.GAME)||(gameScreen==GameScreen.GAME_OVER))
            {
                DrawGameScreen();   
            }
            spriteBatch.End();
            
			base.Draw (gameTime);
		}
	}
}

