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

        private bool[,] currentBoard;
        private bool[,] nextBoard;
        private readonly string ALIVE;
        private readonly string DEAD;
        int mode = 0;
        int seed = 0;

        public Program()
        {
            ALIVE = "\u25A0";
            DEAD = "X";
            currentBoard = new bool[20, 20];
            nextBoard = new bool[20, 20];
            FillBoardWithDead(currentBoard);
            FillBoardWithDead(nextBoard);
            //int shape = AskUserForShape();
            //AskUserForAutoOrManual();
            //SeedBoardWithShape(shape);
            SeedBothBoardsWithBlinker();
            PlayGame();
        }

        private void PlayGame()
        {
            int num = 0;
            int key = 1;
            DisplayCurrentBoard();
            
            while (key != 0)
            {

                DecideWhoWillLiveAndWhoWillDie();
                Thread.Sleep(1000);
                //  Console.Clear();
                Console.Write("----------the next board { " + num + " } ---------------");
                Console.WriteLine();
                DisplayCurrentBoard();
                num++;
                if (mode == 1)
                {
                    Console.WriteLine("Enter any key to continue or 0 to end");
                    key = int.Parse(Console.ReadLine());
                }
            }
        }

        private void AskUserForAutoOrManual()
        {
            
            do
            {
                Console.WriteLine("press 1 for auto 2 for manual");

                int.TryParse(Console.ReadLine(), out mode);

            } while (mode < 1 || mode > 2);

        }

        private int AskUserForShape()
        {
            
            bool result;

            do
            {
                Console.WriteLine("choose a pattern 1-5: Random= 0, Blinker=1,Toad=2,Beacon=3,Pulsar=4,Pentadeclathon=5");

                result = int.TryParse(Console.ReadLine(), out seed);

            } while (seed < 0 || seed > 5 || !result);

            return seed;
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

        private void FillBoardWithDead(bool[,] board)
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
                   // Console.Write(" deciding  =-= -=- =- =-= - =-= -= --=  -= ");
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
            if (row + 1 < currentBoard.GetLength(0) && col + 1 < currentBoard.GetLength(1) &&
                currentBoard[row + 1, col + 1])
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
            Console.Write("the counter:   " + counter + "     =-=-=-=-=-=--==-=--=-==-==--");
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

        private void SeedBoardWithShape(int seed)
        {
            switch (seed)
            {
                case 1:
                    InitializeBlinker();
                    break;
                case 2:
                    InitializeToad();
                    break;
                case 3:
                    InitializeBeacon();
                    break;
                case 4:
                    InitializePulsar();
                    break;
                case 5:
                    InitializePentadecathlon();
                    break;
                default:
                    Console.WriteLine("that is not a valid input");
                    break;
            }
        }

        private void InitializeBlinker()
            {
                Console.Title = "Blinker (period 2)";

                currentBoard[9, 9] = true;
                currentBoard[9, 10] = true;
                currentBoard[9, 11] = true;
            }

            private void InitializeToad()
            {
                Console.Title = "Toad (period 2)";

            currentBoard[9, 9] = true;
            currentBoard[9, 10] = true;
            currentBoard[9, 11] = true;
            currentBoard[10, 8] = true;
            currentBoard[10, 9] = true;
            currentBoard[10, 10] = true;
            }

            private void InitializeBeacon()
            {
                Console.Title = "Beacon (period 2)";

            currentBoard[9, 8] = true;
            currentBoard[9, 9] = true;
            currentBoard[10, 8] = true;
            currentBoard[10, 9] = true;
            currentBoard[11, 10] = true;
            currentBoard[11, 11] = true;
            currentBoard[12, 10] = true;
            currentBoard[12, 11] = true;
            }

            private void InitializePulsar()
            {
                Console.Title = "Pulsar (period 3)";

            currentBoard[4, 5] = true;
            currentBoard[4, 6] = true;
            currentBoard[4, 7] = true;
            currentBoard[4, 11] = true;
            currentBoard[4, 12] = true;
            currentBoard[4, 13] = true;
            currentBoard[6, 3] = true;
            currentBoard[6, 8] = true;
            currentBoard[6, 10] = true;
            currentBoard[6, 15] = true;
            currentBoard[7, 3] = true;
            currentBoard[7, 8] = true;
            currentBoard[7, 10] = true;
            currentBoard[7, 15] = true;
            currentBoard[8, 3] = true;
            currentBoard[8, 8] = true;
            currentBoard[8, 10] = true;
            currentBoard[8, 15] = true;
            currentBoard[9, 5] = true;
            currentBoard[9, 6] = true;
            currentBoard[9, 7] = true;
            currentBoard[9, 11] = true;
            currentBoard[9, 12] = true;
            currentBoard[9, 13] = true;
            currentBoard[11, 5] = true;
            currentBoard[11, 6] = true;
            currentBoard[11, 7] = true;
            currentBoard[11, 11] = true;
            currentBoard[11, 12] = true;
            currentBoard[11, 13] = true;
            currentBoard[12, 3] = true;
            currentBoard[12, 8] = true;
            currentBoard[12, 10] = true;
            currentBoard[12, 15] = true;
            currentBoard[13, 3] = true;
            currentBoard[13, 8] = true;
            currentBoard[13, 10] = true;
            currentBoard[13, 15] = true;
            currentBoard[14, 3] = true;
            currentBoard[14, 8] = true;
            currentBoard[14, 10] = true;
            currentBoard[14, 15] = true;
            currentBoard[16, 5] = true;
            currentBoard[16, 6] = true;
            currentBoard[16, 7] = true;
            currentBoard[16, 11] = true;
            currentBoard[16, 12] = true;
            currentBoard[16, 13] = true;
            }

            private void InitializePentadecathlon()
            {
                Console.Title = "Pentadecathlon (period 15)";

            currentBoard[8, 7] = true;
            currentBoard[8, 12] = true;
            currentBoard[9, 5] = true;
            currentBoard[9, 8] = true;
            currentBoard[9, 9] = true;
            currentBoard[9, 10] = true;
            currentBoard[9, 11] = true;
            currentBoard[9, 13] = true;
            currentBoard[9, 14] = true;
            currentBoard[10, 7] = true;
            currentBoard[10, 12] = true;
            }
        }

    }



