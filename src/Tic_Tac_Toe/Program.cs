/*HW_TicTacToe_Task4_drusse14
 * DeMario Russell
 * CIS - 285
 * 04/08/2023
 */

using System;
using System.Diagnostics;
using System.Linq;

namespace TicTacToe
{
    // The Program class implements a simple console-based Tic-Tac-Toe game that allows players to play against each other or against the computer
    class Program
    {
        static char[] arr = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static char player1Symbol = 'X';
        static char player2Symbol = 'O';
        static int currentPlayer = 1;
        static Random rnd = new Random();

        // Add new fields to store game statistics
        static int gamesWon = 0;
        static int gamesLost = 0;
        static int gamesDraw = 0;
        static TimeSpan totalTimePlayed = TimeSpan.Zero;

        static void Main(string[] args)
        {
            MainMenu();
        }

        // The MainMenu method displays the main menu and handles user input for selecting game mode or exiting the game.
        static void MainMenu()
        {
            bool exitGame = false;

            do // Loop until the user decides to exit the game
            {
                Console.Clear();
                PrintMessage("Welcome to Tic-Tac-Toe!\n\n1. Play against a friend\n2. Play against the computer\n3. Exit Application\n\nPlease enter your choice: ");
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
                        exitGame = true; // Exit game
                        break;
                    default:
                        PrintMessage("\nInvalid input. Please enter 1, 2, or 3."); // Invalid input display
                        Console.ReadLine();
                        break;
                }
            } while (!exitGame); //This loop displays the main menu and handles user input until the user decides to exit the game. It will exit prematurely if the user inputs "3" for exitGame.

            // Display game statistics when exiting
            PrintMessage("\nGame statistics:");
            PrintMessage($"Games won: {gamesWon}");
            PrintMessage($"Games lost: {gamesLost}");
            PrintMessage($"Games draw: {gamesDraw}");
            PrintMessage($"Total time played: {totalTimePlayed}");
            if (gamesWon + gamesLost + gamesDraw > 0)
            {
                PrintMessage($"Average time per game: {TimeSpan.FromSeconds(totalTimePlayed.TotalSeconds / (gamesWon + gamesLost + gamesDraw))}");
            }
            PrintMessage("\nPress any key to exit...");
            Console.ReadLine();
        }

        // The PlayGame method handles the game loop for playing against a friend or the computer.
        static void PlayGame(bool isAgainstComputer)
        {
            // Get the player preferences (starting player and symbols) before the game starts
            GetPlayerPreferences(isAgainstComputer, out currentPlayer, out player1Symbol, out player2Symbol);

            // Reset the game board
            for (int i = 1; i < arr.Length; i++) //This loop initializes the game board by setting each cell in the arr array to an empty space ' '. The loop runs from index 1 to 9.
            {
                arr[i] = ' ';
            }

            bool isDraw;

            int move;
            bool isValidMove;

            // Add a Stopwatch to measure the time played for each game
            Stopwatch gameTimer = new Stopwatch();
            gameTimer.Start();

            // Main game loop: continues until there is a winner or a draw
            do
            {
                Console.Clear();
                Board();

                if (isAgainstComputer && currentPlayer == 2)
                {
                    // If playing against the computer and it's the computer's turn, get the computer's move
                    move = GetComputerMove();
                }
                else
                {
                    // If it's a human player's turn, ask for their move
                    PrintMessage($"\nPlayer {currentPlayer}, enter your move (1-9) or 'q' to quit: ");
                    string? moveInput = Console.ReadLine();

                    if (moveInput?.ToLower() == "q")
                    {
                        MainMenu();
                        return;
                    }

                    int.TryParse(moveInput, out move);
                }

                // Check if the move is valid
                isValidMove = move >= 1 && move <= 9 && arr[move] == ' ';

                if (isValidMove)
                {
                    // If the move is valid, update the game board and switch the current player
                    arr[move] = currentPlayer == 1 ? player1Symbol : player2Symbol;

                    currentPlayer = 3 - currentPlayer;
                }
                else
                {
                    // If the move is not valid, display an error message and wait for the user to press a key
                    PrintMessage("Invalid move, please try again.");
                    Console.ReadKey();
                }

            } while (!CheckWin(out isDraw)); //This loop handles the game loop, where players take turns making moves on the board. The loop continues until there's a winner or a draw. It will exit prematurely if a player wins or the game results in a draw.

            // Stop the game timer and add the elapsed time to the total time played
            gameTimer.Stop();
            totalTimePlayed += gameTimer.Elapsed;

            Console.Clear();
            Board();

            if (isDraw) // Decision tree to handle the end of the game: draw, win or loss
            {
                PrintMessage("It's a draw!");
                gamesDraw++;
            }
            else
            {
                int winner = 3 - currentPlayer;
                PrintMessage($"{(isAgainstComputer && currentPlayer == 1 ? "Computer" : $"Player {winner}")} ({(currentPlayer == 1 ? player2Symbol : player1Symbol)}) has won!");

                // Update the gamesWon and gamesLost counters
                if (isAgainstComputer)
                {
                    if (currentPlayer == 1)
                    {
                        gamesLost++;
                    }
                    else
                    {
                        gamesWon++;
                    }
                }
                else
                {
                    if (winner == 1)
                    {
                        gamesWon++;
                    }
                    else
                    {
                        gamesLost++;
                    }
                }
            }

            // Display game statistics after each match
            DisplayGameStatistics(); // Decision tree to handle user options after the game ends

            PrintMessage("\n1. Restart the match\n2. Change symbols\n3. Return to main menu\n4. Exit\n\nPlease enter your choice: ");
            string? playInput = Console.ReadLine();

            switch (playInput)
            {
                case "1":// Restart the match
                    PlayGame(isAgainstComputer);
                    break;
                case "2":// Change symbols and restart the match
                    GetPlayerPreferences(isAgainstComputer, out currentPlayer, out player1Symbol, out player2Symbol);
                    PlayGame(isAgainstComputer);
                    break;
                case "3":// Return to main menu
                    MainMenu();
                    break;
                case "4":// Exit the application
                    PrintMessage("Press any key to exit...");
                    Console.ReadLine();
                    Environment.Exit(0);
                    break;
                case "5":
                    Shutdown();
                    break;
                default:// If the input is invalid, display an error message and wait for the user to press a key
                    PrintMessage("\nInvalid input. Please enter 1, 2, 3, 4, or 5.");
                    Console.ReadLine();
                    break;
            }
        }

        // The GetComputerMove method provides a simple AI for the computer to make a move.
        private static int GetComputerMove()
        {
            // Initialize the move variable with the default fallback move
            int move = 1;

            // This loop finds the first available cell for the computer's move
            // by checking if the cell in the arr array is empty (' ').
            // The loop runs from index 1 to the length of the array - 1.
            // It will exit prematurely when the first available cell is found.
            for (int i = 1; i < arr.Length; i++)
            {
                if (arr[i] == ' ')
                {
                    move = i; // Set the move to the index of the first available cell
                    break;    // Exit the loop once the first available cell is found
                }
            }

            // Return the computed move
            return move;
        }


        // The GetPlayerPreferences method gets user input for the starting player and their symbol (X or O).
        static void GetPlayerPreferences(bool isAgainstComputer, out int startingPlayer, out char player1Symbol, out char player2Symbol)
        {
            // Set default values
            startingPlayer = 1;
            player1Symbol = 'X';
            player2Symbol = 'O';

            if (isAgainstComputer) // Decision tree to determine who will go first
            {
                // Get input for the starting player when playing against the computer
                PrintMessage("Do you want to go first? (y/n): ");
                string? firstPlayerInput = Console.ReadLine()?.ToLower();

                if (firstPlayerInput == "n")
                {
                    startingPlayer = 2;
                }
            }
            else
            {
                // Get input for the starting player when playing against a friend
                PrintMessage("Player 1, do you want to go first? (y/n): ");
                string? firstPlayerInput = Console.ReadLine()?.ToLower();

                if (firstPlayerInput == "n")
                {
                    startingPlayer = 2;
                }
            }
            // Decision tree to determine the symbols for the players
            PrintMessage($"Player {startingPlayer}, do you want to use X or O? (x/o): ");
            string? symbolInput = Console.ReadLine()?.ToLower();

            if (symbolInput == "o")
            {
                player1Symbol = 'O';
                player2Symbol = 'X';
            }
            // Display the selected preferences to the user
            PrintMessage($"\nPlayer {startingPlayer} is {player1Symbol} and will go first.");
            PrintMessage($"Player {(startingPlayer == 1 ? 2 : 1)} is {player2Symbol} and will go second.");

            // Prompt the user to start the game
            PrintMessage("\nPress any key to start the game...");
            Console.ReadKey();
        }

        //The PrintMessage method writes the provided message to the console.
        private static void PrintMessage(string message) 
        {
            Console.WriteLine(message);
        }

        // The CheckWin method checks if there is a winner or a draw.
        private static bool CheckWin(out bool isDraw) // Decision tree to check for wins
        {
            isDraw = false;

            bool isWin = false;

            // Check for horizontal and vertical wins
            for (int i = 1; i <= 7 && !isWin; i += 3) //This loop checks for horizontal wins on the game board. The loop runs from index 1 to 7 with an increment of 3. It will exit prematurely if a horizontal win is detected.
            {
                if (arr[i] == arr[i + 1] && arr[i + 1] == arr[i + 2] && arr[i] != ' ')
                {
                    isWin = true;
                }
            }
            for (int i = 1; i <= 3 && !isWin; i++) //This loop checks for vertical wins on the game board. The loop runs from index 1 to 3. It will exit prematurely if a vertical win is detected.
            {
                if (arr[i] == arr[i + 3] && arr[i + 3] == arr[i + 6] && arr[i] != ' ')
                {
                    isWin = true;
                }
            }

            // Check for diagonal wins
            if (!isWin && arr[1] == arr[5] && arr[5] == arr[9] && arr[1] != ' ')
            {
                isWin = true;
            }
            if (!isWin && arr[3] == arr[5] && arr[5] == arr[7] && arr[3] != ' ')
            {
                isWin = true;
            }

            // Check for a draw by verifying if all cells are filled with either 'X' or 'O'
            if (!isWin && arr.All(x => x == 'X' || x == 'O'))
            {
                isDraw = true;
            }
            // Return true if there is a winner or a draw, otherwise return false
            return isWin || isDraw;
        }

        // The Board method displays the game board on the console.
        static void Board()
        {
            for (int i = 1; i <= 9; i += 3) //This loop displays the game board on the console, showing the rows and columns of the board. The loop runs from index 1 to 9 with an increment of 3. There's no premature exit in this loop.
            {
                Console.WriteLine(" {0} | {1} | {2}", DisplayCell(i), DisplayCell(i + 1), DisplayCell(i + 2));

                // If the current row is not the last row, print a horizontal line to separate the rows
                if (i < 7)
                {
                    Console.WriteLine("---|---|---");
                }
            }
        }

        static string DisplayCell(int cellIndex)
        {
            // Check if the cell at the given index is empty (' ')
            // If it's empty, return the index as a string to display the cell number
            // Otherwise, return the symbol (either 'X' or 'O') at that cell as a string
            return arr[cellIndex] == ' ' ? cellIndex.ToString() : arr[cellIndex].ToString();
        }


        static void DisplayGameStatistics() // Decision tree to calculate and display average time per game if there were any games played
        {
            PrintMessage("\nGame statistics:");// Print the game statistics header
            PrintMessage($"Games won: {gamesWon}");// Display the number of games won
            PrintMessage($"Games lost: {gamesLost}");// Display the number of games lost
            PrintMessage($"Games draw: {gamesDraw}");// Display the number of games that ended in a draw
            PrintMessage($"Total time played: {totalTimePlayed}");// Display the total time played
            if (gamesWon + gamesLost + gamesDraw > 0)// Calculate and display the average time per game if any games have been played
            {
                PrintMessage($"Average time per game: {TimeSpan.FromSeconds(totalTimePlayed.TotalSeconds / (gamesWon + gamesLost + gamesDraw))}");
            }
        }


        static void Shutdown() //// Decision tree to confirm computer shutdown
        {
            // Prompt the user to confirm if they want to shut down the computer
            PrintMessage("\nAre you sure you want to shut down the computer? (y/n): ");
            string? shutdownInput = Console.ReadLine()?.ToLower();

            // Decision tree based on user input
            if (shutdownInput == "y")
            {
                // If the user confirms shutdown, create a process to shut down the computer
                var psi = new ProcessStartInfo("shutdown", "/s /t 0")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false
                };
                Process.Start(psi);
            }
            else
            {
                // If the user cancels the shutdown, return to the game
                PrintMessage("Shutdown canceled. Press any key to return to the game...");
                Console.ReadKey();
                MainMenu();
            }
        }
    }
}