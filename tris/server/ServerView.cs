using System;
using System.Text;

namespace server
{
    class ServerView
    {
        public static void ClearConsole()
        {
            Console.Clear();
        }

        public static void DisplayGameBoard(char[] board)
        {
            ClearConsole();

            // Display player names at the top
            //Console.WriteLine($"Player 1: {ServerModel.Player1Name}  |  Player 2: {ServerModel.Player2Name}\n");
            Console.WriteLine($"Player 1: Player1  |  Player 2: Player 2\n");

            // Create a larger game board
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    char cell = board[row * 3 + col];
                    string cellValue = cell == ' ' ? " " : cell.ToString();
                    Console.Write($"  {cellValue}  ");
                    if (col < 2)
                        Console.Write("|");
                }
                Console.WriteLine();
                if (row < 2)
                {
                    Console.WriteLine("-----|-----|-----");
                }
            }
            Console.WriteLine();
        }

        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
