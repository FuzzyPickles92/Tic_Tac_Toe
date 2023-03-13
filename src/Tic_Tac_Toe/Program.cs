/*HW_TicTacToe_Task3_drusse14
 * DeMario Russell
 * CIS - 285
 * 3/13/2023
 */

using System;

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
                Console.WriteLine("Welcome to Tic-Tac-Toe!");
                Console.WriteLine("\n");
                Console.WriteLine("1. Start Game");
                Console.WriteLine("2. Exit");
                Console.WriteLine("\n");

                Console.Write("Please enter your choice: ");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Console.WriteLine("\n");
                        StartGame();
                        Console.ReadLine();
                        break;
                    case "2":
                        exitGame = true;
                        break;
                    default:
                        Console.WriteLine("\n");
                        Console.WriteLine("Invalid input. Please enter 1 or 2.");
                        Console.ReadLine();
                        break;
                }
            } while (!exitGame);
        }

        static void StartGame()
        {
            // Reset the game board
            arr = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            currentPlayer = 1;

            // Prompt the players to select their symbols
            Console.WriteLine("Player 1, choose your symbol (X or O):");
            player1Symbol = Console.ReadLine()?.ToUpper()?.FirstOrDefault() ?? 'X';
            player2Symbol = player1Symbol == 'X' ? 'O' : 'X'; // Set player 2's symbol to the opposite of player 1's

            Console.WriteLine($"Player 1: {player1Symbol}");
            Console.WriteLine($"Player 2: {player2Symbol}");
            Console.WriteLine("\n");

            // Prompt the players to press enter to start the game
            Console.WriteLine("Press enter to start the game.");
            Console.ReadLine();

            // Prompt the player to choose who goes first
            Console.Write("Who would you like to go first? (1 for Player 1, 2 for Player 2): ");
            string? input = Console.ReadLine();
            bool isNumeric = int.TryParse(input, out int firstPlayer);

            // Check if the input was successfully parsed and is valid
            if (!isNumeric || firstPlayer < 1 || firstPlayer > 2)
            {
                Console.WriteLine("Invalid input. Player 1 will go first.");
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
                    Console.WriteLine($"Player {currentPlayer}'s turn ({player1Symbol})");
                }
                else
                {
                    Console.WriteLine($"Player {currentPlayer}'s turn ({player2Symbol})");
                }
                Console.WriteLine("\n");
                Board();

                // Prompt user to enter a valid move
                Console.WriteLine("Enter your move (1-9), or enter 0 to exit the game: ");
                input = Console.ReadLine();

                // Check if the user wants to exit the game
                if (input == "0")
                {
                    Console.WriteLine("\nExiting the game...");
                    Console.ReadLine();
                    return;
                }

                isNumeric = int.TryParse(input, out choice);

                // Check if the input was successfully parsed
                if (!isNumeric)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 9, or enter 0 to exit the game.");
                    Console.ReadLine();
                    continue;
                }

                if (choice < 0 || choice > 9)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 9, or enter 0 to exit the game.");
                    Console.ReadLine();
                    continue;
                }

                // Check if the chosen cell is already occupied
                if (arr[choice] == player1Symbol || arr[choice] == player2Symbol)
                {
                    Console.WriteLine($"Sorry, the cell {choice} is already occupied by {arr[choice]}");
                    Console.WriteLine("\n");
                    Console.WriteLine("Please wait 2 seconds while the board is loading again.....");
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
            Console.WriteLine($"Player {currentPlayer} ({(currentPlayer == 1 ? player1Symbol : player2Symbol)}) has won!");
        }

        private static bool CheckWin()
        {
            // check the win conditions here
            // (not included in this example for brevity)
            return false;
        }

        private static void Board()
        {
            Console.WriteLine("     |     |      ");
            Console.WriteLine($"  {arr[1]}  |  {arr[2]}  |  {arr[3]}");
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine($"  {arr[4]}  |  {arr[5]}  |  {arr[6]}");
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine($"  {arr[7]}  |  {arr[8]}  |  {arr[9]}");
            Console.WriteLine("     |     |      ");
        }
    }
}