using System;

namespace client
{
    class ClientView
    {
        public static void ClearConsole()
        {
            Console.Clear();
        }

        public static void DisplayGameBoard(char[] board)
        {
            ClearConsole();

            // Display the game board
            Console.WriteLine("Tic-Tac-Toe Game\n");
            Console.WriteLine("  1 | 2 | 3 ");
            Console.WriteLine(" ---|---|---");
            Console.WriteLine($"  4 | 5 | 6 ");
            Console.WriteLine(" ---|---|---");
            Console.WriteLine($"  7 | 8 | 9 ");
            Console.WriteLine("\nCurrent Game Board:");

            for (int i = 0; i < 9; i++)
            {
                Console.Write($" {board[i]} ");
                if (i % 3 == 2)
                    Console.WriteLine(i == 8 ? "\n" : "\n ---|---|---");
                else
                    Console.Write("|");
            }
        }

        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        public static string GetUserMove()
        {
            Console.Write("Enter your move (1-9): ");
            return Console.ReadLine();
        }
    }
}
