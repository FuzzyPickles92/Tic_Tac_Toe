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
        }



        static void PlayGame(bool isAgainstComputer)
        {
            // Reset the game board
            arr = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            currentPlayer = 1;

            // Prompt the players to select their symbols
            PrintMessage("Player 1, choose your symbol (X or O): ");
            player1Symbol = (Console.ReadLine()?.Trim().ToUpper().FirstOrDefault() == 'X') ? 'X' : 'O';
            player2Symbol = (player1Symbol == 'X') ? 'O' : 'X'; // Set player 2's symbol to the opposite of player 1's
            PrintMessage($"Player 1: {player1Symbol}");

            if (isAgainstComputer)
            {
                PrintMessage($"You are playing against the computer ({player2Symbol})");
            }
            else
            {
                PrintMessage($"Player 2: {player2Symbol}");
            }

            PrintMessage("\n");

            // Prompt the players to press enter to start the game
            PrintMessage("Press enter to start the game.");
            Console.ReadLine();

            // Prompt the player to choose who goes first
            PrintMessage($"Who would you like to go first? (1 for Player 1, 2 for the computer): ");

            // If playing against the computer, set the computer as the second player
            int maxPlayer = (isAgainstComputer) ? 2 : 1;

            string? input = Console.ReadLine();
            bool isNumeric = int.TryParse(input, out int firstPlayer);

            // Check if the input was successfully parsed and is valid
            if (!isNumeric || firstPlayer < 1 || firstPlayer > maxPlayer)
            {
                PrintMessage($"Invalid input. Player 1 will go first against {(isAgainstComputer ? "the computer" : "Player 2")}.");
                Console.ReadLine();
                firstPlayer = 1;
            }

            // Set the current player to the chosen player
            currentPlayer = firstPlayer;

            do
            {
                Console.Clear();
                if (currentPlayer == 1)
                {
                    PrintMessage($"Player {currentPlayer}'s turn ({player1Symbol})");
                }
                else
                {
                    PrintMessage($"Player {currentPlayer}'s turn ({player2Symbol})");
                }
                PrintMessage("\n");
                Board();

                // Prompt user to enter a valid move
                PrintMessage("Enter your move (1-9), or enter 0 to exit the game: ");
                input = Console.ReadLine();

                // Check if the user wants to exit the game
                if (input == "0")
                {
                    PrintMessage("\nExiting the game...");
                    Console.ReadLine();
                    return;
                }

                isNumeric = int.TryParse(input, out choice);

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
                    Console.ReadLine();
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
            } while (!CheckWin());

            Console.Clear();
            Board();
            PrintMessage($"Player {currentPlayer} ({(currentPlayer == 1 ? player1Symbol : player2Symbol)}) has won!");
        }

        private static void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        private static bool CheckWin()
        {
            // check the win conditions here
            // (not included in this example for brevity)
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