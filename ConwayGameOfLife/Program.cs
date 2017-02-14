using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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

        bool [,] board;
        private readonly string ALIVE;
        private readonly string DEAD;

        public Program()
        {
            ALIVE = "\u25A0";
            DEAD = "X";
            board = new bool[20, 20];
            FillBlankBoard();
            //AskUserForShape();
            //AskUserForAutoOrManual();
            //SeedBoardWithShape();
            SeedBoardWithBlinker();
            PlayGame();
        }

        private void PlayGame()
        {
            while (true)
            {
                int liveOrDie = CountTheAllField();
                DecideNextGen(liveOrDie);
                Console.Clear();

                DisplayBoard();
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

        public void SeedBoardWithBlinker()
        {
            board[9, 9] = true;
            board[9, 10] = true;
            board[9, 11] = true;
            DisplayBoard();
        }

        private void DisplayBoard()
        {
            string s;
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    s = (board[i, j]) ? ALIVE : DEAD;
                    Console.Write(s + " ");
                }
                Console.Write("\n");
            }
        }

        private void FillBlankBoard() 
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    board[i, j] = false;
                }
            }
            DisplayBoard();
        }
        
        public int CountTheAllField()
        {
            return 0;
        }

        public void DecideNextGen(int number)
        {
            if (number == 3)
            {
                //live
            }
            else if (number == 4)
            {
                //stay
            }
            else
            {
                //die
            }
        }

       
    }

}
