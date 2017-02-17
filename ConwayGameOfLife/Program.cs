using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConwayGameOfLife
{
    class Program 
    {
        static void Main(string[] args)
        {
            Program game = new Program();
            Console.Read();
        }

        private bool [,] currentBoard;
        private bool[,] nextBoard;
        private readonly string ALIVE;
        private readonly string DEAD;
        //        private ThreadStart displayThreadStart;
        //        private Thread displayThread;
        //        private TimeSpan interval;
        //            displayThreadStart = new ThreadStart(DisplayCurrentBoard);
        //            displayThread = new Thread(displayThreadStart);
        //            interval = new TimeSpan(0, 0, 2);

        public Program()
        {
            ALIVE = "\u25A0";
            DEAD = "X";
            currentBoard = new bool[20, 20];
            nextBoard = new bool[20, 20];
            FillBoardWithDead(currentBoard);
            FillBoardWithDead(nextBoard);
            //AskUserForShape();
            //AskUserForAutoOrManual();
            //SeedBoardWithShape();
            SeedBothBoardsWithBlinker();
            PlayGame();
        }

        private void PlayGame()
        {
            int num = 0;
            DisplayCurrentBoard();
            while (true)
            {
                DecideWhoWillLiveAndWhoWillDie();
                Thread.Sleep(1000);
              //  Console.Clear();
                Console.Write("----------the next board { " + num + " } ---------------");
                Console.WriteLine();
                DisplayCurrentBoard();
                num++;
            }
        }

        private void AskUserForAutoOrManual()
        {
            throw new NotImplementedException();
        }

        private void AskUserForShape()
        {
            throw new NotImplementedException();
        }

        private void SeedBoardWithShape()
        {
            throw new NotImplementedException();
        }

        public void SeedBothBoardsWithBlinker()
        {
            currentBoard[9, 9] = true;
            currentBoard[9, 10] = true;
            currentBoard[9, 11] = true;

            nextBoard[9, 9] = true;
            nextBoard[9, 10] = true;
            nextBoard[9, 11] = true;
        }

        private void DisplayCurrentBoard()
        {
            string s;
            for (int i = 0; i < currentBoard.GetLength(0); i++)
            {
                for (int j = 0; j < currentBoard.GetLength(1); j++)
                {
                    s = (currentBoard[i, j]) ? ALIVE : DEAD;
                    Console.Write(s + " ");
                }
                Console.Write("\n");
            }

        }

        private void FillBoardWithDead(bool [,] board) 
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = false;
                }
            }
        }

        public void DecideWhoWillLiveAndWhoWillDie()
        {
            IterateThroughBoard();
        }

        public void IterateThroughBoard()
        {

            for (int row = 0; row < currentBoard.GetLength(0); row++)
            {
                for (int col = 0; col < currentBoard.GetLength(1); col++)
                {

                       DecideNextCellForCurrentCell(countNeighbors(row, col), row, col);
                }
            }
            UpdateCurrentBoardToBeNextBoard();
        }

        public int countNeighbors(int row, int col)
        {
            int counter = 0;
            
            if (row - 1 >= 0 && currentBoard[row - 1, col])
            {
                counter++;
            }
            if (row - 1 >= 0 && col - 1 >= 0 && currentBoard[row - 1, col - 1])
            {
                counter++;
            }
            if (col - 1 >= 0 && currentBoard[row, col - 1])
            {
                counter++;
            }

            if (row + 1 < currentBoard.GetLength(0) && col - 1 >= 0 && currentBoard[row + 1, col - 1])
            {
                counter++;
            }
            if (row - 1 >= 0 && col + 1 < currentBoard.GetLength(1) && currentBoard[row - 1, col + 1])
            {
                counter++;
            }

            if (row + 1 < currentBoard.GetLength(0) && currentBoard[row + 1, col])
            {
                counter++;
            }
            if (row + 1 < currentBoard.GetLength(0) && col + 1 < currentBoard.GetLength(1) && currentBoard[row + 1, col + 1])
            {
                counter++;
            }
            if (col + 1 < currentBoard.GetLength(1) && currentBoard[row, col + 1])
            {
                counter++;
            }
            if (currentBoard[row, col])
            {
                counter++;
            }
            return counter;
        }

        public void DecideNextCellForCurrentCell(int number, int row, int col)
        {
            if (number == 3)
            {
                //live
                nextBoard[row, col] = true;
            }
            else if (number == 4)
            {
                //stay
                nextBoard[row, col] = currentBoard[row, col];
            }
            else
            {
                //die
                nextBoard[row, col] = false;
            }
        }

        public void UpdateCurrentBoardToBeNextBoard()
        {
            currentBoard = nextBoard;
            FillBoardWithDead(nextBoard);
        }

    }
}


