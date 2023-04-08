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
            GetPlayerPreferences(isAgainstComputer, out currentPlayer, out player1Symbol, out player2Symbol);

            // Reset the game board
            for (int i = 1; i < arr.Length; i++)
            {
                arr[i] = ' ';
            }

            bool isDraw;

            int move;
            bool isValidMove;

            // Add a Stopwatch to measure the time played for each game
            Stopwatch gameTimer = new Stopwatch();
            gameTimer.Start();

            // Loop until there is a winner or a draw
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

                // Check if the move is valid
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

            // Stop the game timer and add the elapsed time to the total time played
            gameTimer.Stop();
            totalTimePlayed += gameTimer.Elapsed;

            Console.Clear();
            Board();

            if (isDraw)
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
            DisplayGameStatistics();

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
                case "5":
                    Shutdown();
                    break;
                default:
                    PrintMessage("\nInvalid input. Please enter 1, 2, 3, 4, or 5.");
                    Console.ReadLine();
                    break;
            }
        }

        // The GetComputerMove method provides a simple AI for the computer to make a move.
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

        // The GetPlayerPreferences method gets user input for the starting player and their symbol (X or O).
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

        // The CheckWin method checks if there is a winner or a draw.
        private static bool CheckWin(out bool isDraw)
        {
            isDraw = false;

            bool isWin = false;

            // Check for horizontal and vertical wins
            for (int i = 1; i <= 7 && !isWin; i += 3)
            {
                if (arr[i] == arr[i + 1] && arr[i + 1] == arr[i + 2] && arr[i] != ' ')
                {
                    isWin = true;
                }
            }
            for (int i = 1; i <= 3 && !isWin; i++)
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

            // Check for a draw
            if (!isWin && arr.All(x => x == 'X' || x == 'O'))
            {
                isDraw = true;
            }

            return isWin || isDraw;
        }

        // The Board method displays the game board on the console.
        static void Board()
        {
            for (int i = 1; i <= 9; i += 3)
            {
                Console.WriteLine(" {0} | {1} | {2}", DisplayCell(i), DisplayCell(i + 1), DisplayCell(i + 2));
                if (i < 7)
                {
                    Console.WriteLine("---|---|---");
                }
            }
        }

        static string DisplayCell(int cellIndex)
        {
            return arr[cellIndex] == ' ' ? cellIndex.ToString() : arr[cellIndex].ToString();
        }


        static void DisplayGameStatistics()
        {
            PrintMessage("\nGame statistics:");
            PrintMessage($"Games won: {gamesWon}");
            PrintMessage($"Games lost: {gamesLost}");
            PrintMessage($"Games draw: {gamesDraw}");
            PrintMessage($"Total time played: {totalTimePlayed}");
            if (gamesWon + gamesLost + gamesDraw > 0)
            {
                PrintMessage($"Average time per game: {TimeSpan.FromSeconds(totalTimePlayed.TotalSeconds / (gamesWon + gamesLost + gamesDraw))}");
            }
        }


        static void Shutdown()
        {
            PrintMessage("\nAre you sure you want to shut down the computer? (y/n): ");
            string? shutdownInput = Console.ReadLine()?.ToLower();

            if (shutdownInput == "y")
            {
                var psi = new ProcessStartInfo("shutdown", "/s /t 0")
                {
                    CreateNoWindow = true,
                    UseShellExecute = false
                };
                Process.Start(psi);
            }
            else
            {
                PrintMessage("Shutdown canceled. Press any key to return to the game...");
                Console.ReadKey();
                MainMenu();
            }
        }
    }
}
