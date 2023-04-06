/*HW_TicTacToe_Task3_drusse14
 * DeMario Russell
 * CIS - 285
 * 3/13/2023
 */

using System;
using System.Linq;

namespace TicTacToe
{
    class Program
    {
        static char[] arr = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static char player1Symbol = 'X';
        static char player2Symbol = 'O';
        static int currentPlayer = 1;
        static Random rnd = new Random();

        static void Main(string[] args)
        {
            MainMenu();
        }

        static void MainMenu()
        {
            bool exitGame = false;

            do
            {
                Console.Clear();
                PrintMessage("Welcome to Tic-Tac-Toe!\n\n1. Play against a friend\n2. Play against the computer\n3. Exit\n\nPlease enter your choice: ");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine();
                        PlayGame(false); // Play against a friend
                        break;
                    case "2":
                        Console.WriteLine();
                        PlayGame(true); // Play against the computer
                        break;
                    case "3":
                        exitGame = true;
                        break;
                    default:
                        PrintMessage("\nInvalid input. Please enter 1, 2, or 3.");
                        Console.ReadLine();
                        break;
                }
            } while (!exitGame);

            PrintMessage("Press any key to exit...");
            Console.ReadLine();
        }

        static void PlayGame(bool isAgainstComputer)
        {
            GetPlayerPreferences(isAgainstComputer, out currentPlayer, out player1Symbol, out player2Symbol);

            // Reset the game board
            for (int i = 1; i < arr.Length; i++)
            {
                arr[i] = ' ';
            }

            bool isDraw;

            int move;
            bool isValidMove;

            do
            {
                Console.Clear();
                Board();

                if (isAgainstComputer && currentPlayer == 2)
                {
                    move = GetComputerMove();
                }
                else
                {
                    PrintMessage($"\nPlayer {currentPlayer}, enter your move (1-9) or 'q' to quit: ");
                    string? moveInput = Console.ReadLine();

                    if (moveInput?.ToLower() == "q")
                    {
                        MainMenu();
                        return;
                    }

                    int.TryParse(moveInput, out move);
                }

                isValidMove = move >= 1 && move <= 9 && arr[move] == ' ';

                if (isValidMove)
                {
                    arr[move] = currentPlayer == 1 ? player1Symbol : player2Symbol;
                    currentPlayer = 3 - currentPlayer;
                }
                else
                {
                    PrintMessage("Invalid move, please try again.");
                    Console.ReadKey();
                }

            } while (!CheckWin(out isDraw));

            Console.Clear();
            Board();

            if (isDraw)
            {
                PrintMessage("It's a draw!");
            }
            else
            {
                PrintMessage($"{(isAgainstComputer && currentPlayer == 1 ? "Computer" : $"Player {3 - currentPlayer}")} ({(currentPlayer == 1 ? player2Symbol : player1Symbol)}) has won!");
            }

            PrintMessage("\n1. Restart the match\n2. Change symbols\n3. Return to main menu\n4. Exit\n\nPlease enter your choice: ");
            string? playInput = Console.ReadLine();

            switch (playInput)
            {
                case "1":
                    PlayGame(isAgainstComputer);
                    break;
                case "2":
                    GetPlayerPreferences(isAgainstComputer, out currentPlayer, out player1Symbol, out player2Symbol);
                    PlayGame(isAgainstComputer);
                    break;
                case "3":
                    MainMenu();
                    break;
                case "4":
                    PrintMessage("Press any key to exit...");
                    Console.ReadLine();
                    Environment.Exit(0);
                    break;
                default:
                    PrintMessage("\nInvalid input. Please enter 1, 2, 3, or 4.");
                    Console.ReadLine();
                    break;
            }
        }

        private static int GetComputerMove()
        {
            // A simple logic for the computer's move: select the first available cell
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] == ' ')
                {
                    return i;
                }
            }

            return 1; // Default fallback move
        }

        static void GetPlayerPreferences(bool isAgainstComputer, out int startingPlayer, out char player1Symbol, out char player2Symbol)
        {
            // Set default values
            startingPlayer = 1;
            player1Symbol = 'X';
            player2Symbol = 'O';

            if (isAgainstComputer)
            {
                PrintMessage("Do you want to go first? (y/n): ");
                string? firstPlayerInput = Console.ReadLine()?.ToLower();

                if (firstPlayerInput == "n")
                {
                    startingPlayer = 2;
                }
            }
            else
            {
                PrintMessage("Player 1, do you want to go first? (y/n): ");
                string? firstPlayerInput = Console.ReadLine()?.ToLower();

                if (firstPlayerInput == "n")
                {
                    startingPlayer = 2;
                }
            }

            PrintMessage($"Player {startingPlayer}, do you want to use X or O? (x/o): ");
            string? symbolInput = Console.ReadLine()?.ToLower();

            if (symbolInput == "o")
            {
                player1Symbol = 'O';
                player2Symbol = 'X';
            }

            PrintMessage($"\nPlayer {startingPlayer} is {player1Symbol} and will go first.");
            PrintMessage($"Player {(startingPlayer == 1 ? 2 : 1)} is {player2Symbol} and will go second.");

            PrintMessage("\nPress any key to start the game...");
            Console.ReadKey();
        }

        private static void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        private static bool CheckWin(out bool isDraw)
        {
            isDraw = false;

            // Check for horizontal and vertical wins
            for (int i = 1; i <= 7; i += 3)
            {
                if (arr[i] == arr[i + 1] && arr[i + 1] == arr[i + 2] && arr[i] != ' ') return true;
            }
            for (int i = 1; i <= 3; i++)
            {
                if (arr[i] == arr[i + 3] && arr[i + 3] == arr[i + 6] && arr[i] != ' ') return true;
            }

            // Check for diagonal wins
            if (arr[1] == arr[5] && arr[5] == arr[9] && arr[1] != ' ') return true;
            if (arr[3] == arr[5] && arr[5] == arr[7] && arr[3] != ' ') return true;

            // Check for a draw
            if (arr.All(x => x == 'X' || x == 'O'))
            {
                isDraw = true;
                return true;
            }

            return false;
        }

        static void Board()
        {
            Console.WriteLine(" {0} | {1} | {2}", arr[1], arr[2], arr[3]);
            Console.WriteLine("---|---|---");
            Console.WriteLine(" {0} | {1} | {2}", arr[4], arr[5], arr[6]);
            Console.WriteLine("---|---|---");
            Console.WriteLine(" {0} | {1} | {2}", arr[7], arr[8], arr[9]);
        }
    }
}