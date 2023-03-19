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
        static int choice;
        static Random rnd = new Random();

        static void Main(string[] args)
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
                        Console.ReadLine();
                        break;
                    case "2":
                        Console.WriteLine();
                        PlayGame(true); // Play against the computer
                        Console.ReadLine();
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
                arr[i] = i.ToString()[0];
            }

            bool isDraw;

            do
            {
                Console.Clear();
                if (currentPlayer == 1)
                {
                    PrintMessage($"Player {currentPlayer}'s turn ({player1Symbol})");
                }
                else
                {
                    PrintMessage($"{(isAgainstComputer ? "Computer" : $"Player {currentPlayer}")}'s turn ({player2Symbol})");
                }
                PrintMessage("\n");
                Board();

                if (!isAgainstComputer || currentPlayer == 1)
                {
                    // Human player's turn
                    // Prompt user to enter a valid move
                    PrintMessage("Enter your move (1-9), or enter 0 to exit the game: ");
                    string? input = Console.ReadLine();

                    // Check if the user wants to exit the game
                    if (input == "0")
                    {
                        PrintMessage("\nExiting the game...");
                        Console.ReadLine();
                        return;
                    }

                    bool isNumeric = int.TryParse(input, out choice);

                    // Check if the input was successfully parsed
                    if (!isNumeric)
                    {
                        PrintMessage("Invalid input. Please enter a number between 1 and 9, or enter 0 to exit the game.");
                        Console.ReadLine();
                        continue;
                    }

                    if (choice < 0 || choice > 9)
                    {
                        PrintMessage("Invalid input. Please enter a number between 1 and 9, or enter 0 to exit the game.");
                        Console.ReadLine();
                        continue;
                    }

                    // Check if the chosen cell is already occupied
                    if (arr[choice] == player1Symbol || arr[choice] == player2Symbol)
                    {

                        PrintMessage($"Sorry, the cell {choice} is already occupied by {arr[choice]}");
                        PrintMessage("\n");
                        PrintMessage("Please wait 2 seconds while the board is loading again.....");
                        System.Threading.Thread.Sleep(2000);
                    }
                    else
                    {
                        // Set the cell to the current player's symbol and switch to the next player
                        if (currentPlayer == 1)
                        {
                            arr[choice] = player1Symbol;
                            currentPlayer = 2;
                        }
                        else
                        {
                            arr[choice] = player2Symbol;
                            currentPlayer = 1;
                        }
                    }
                }
                else
                {
                    // Computer player's turn
                    // Choose a random available move
                    choice = GetComputerMove();

                    // Set the cell to the computer player's symbol and switch to the next player
                    arr[choice] = player2Symbol;
                    currentPlayer = 1;
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
                PrintMessage($"{(isAgainstComputer && currentPlayer == 1 ? "Computer" : $"Player {3 - currentPlayer}")} ({(currentPlayer == 1 ? player2Symbol : player1Symbol)}) has won! Please press enter to return to the selection screen.");
            }
        }

        private static int GetComputerMove()
        {
            // A simple logic for the computer's move: select the first available cell
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] != player1Symbol && arr[i] != player2Symbol)
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

            // Get user preferences for going first and symbol choice
            if (!isAgainstComputer)
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
                if (arr[i] == arr[i + 1] && arr[i + 1] == arr[i + 2]) return true;
            }
            for (int i = 1; i <= 3; i++)
            {
                if (arr[i] == arr[i + 3] && arr[i + 3] == arr[i + 6]) return true;
            }

            // Check for diagonal wins
            if (arr[1] == arr[5] && arr[5] == arr[9]) return true;
            if (arr[3] == arr[5] && arr[5] == arr[7]) return true;

            // Check for a draw
            if (arr.All(x => x == player1Symbol || x == player2Symbol))
            {
                isDraw = true;
            }

            return false;
        }

        private static void Board()
        {
            PrintMessage("     |     |      ");
            PrintMessage($"  {arr[1]}  |  {arr[2]}  |  {arr[3]}");
            PrintMessage("_____|_____|_____ ");
            PrintMessage("     |     |      ");
            PrintMessage($"  {arr[4]}  |  {arr[5]}  |  {arr[6]}");
            PrintMessage("_____|_____|_____ ");
            PrintMessage("     |     |      ");
            PrintMessage($"  {arr[7]}  |  {arr[8]}  |  {arr[9]}");
            PrintMessage("     |     |      ");
        }
    }
}