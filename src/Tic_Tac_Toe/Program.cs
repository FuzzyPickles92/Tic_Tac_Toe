/*HW_TicTacToe_Task1_drusse14
 * DeMario Russell
 * CIS - 285
 * 2/10/2023
 */


using System;

namespace TicTacToe
{
    class Program
    {
        static char[] arr = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static int player = 1;
        static int choice;

        static void Main(string[] args)
        {
            Console.WriteLine("Player 1: X");
            Console.WriteLine("Player 2: O");
            Console.WriteLine("\n");

            do
            {
                Console.Clear();
                if (player % 2 == 0)
                {
                    Console.WriteLine("Player 2 turn");
                }
                else
                {
                    Console.WriteLine("Player 1 turn");
                }
                Console.WriteLine("\n");
                Board();
                choice = int.Parse(Console.ReadLine());

                if (arr[choice] != 'X' && arr[choice] != 'O')
                {
                    if (player % 2 == 0)
                    {
                        arr[choice] = 'O';
                        player++;
                    }
                    else
                    {
                        arr[choice] = 'X';
                        player++;
                    }
                }
                else
                {
                    Console.WriteLine("Sorry the cell {0} is already occupied", choice);
                    Console.WriteLine("\n");
                    Console.WriteLine("Please wait 2 second board is loading again.....");
                    Console.ReadLine();
                }
            } while (!CheckWin());

            Console.Clear();
            Board();
            Console.WriteLine("Player {0} has won!", (player % 2) + 1);
            Console.ReadLine();
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
            Console.WriteLine("  {0}  |  {1}  |  {2}", arr[1], arr[2], arr[3]);
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", arr[4], arr[5], arr[6]);
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", arr[7], arr[8], arr[9]);
            Console.WriteLine("     |     |      ");
        }
    }
}
