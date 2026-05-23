using System;
using System.Threading;

namespace TicTacToe
{
    class Program
    {
        static char[] board = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        static int player = 1;
        static int choice;
        static int flag = 0;

        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                Console.WriteLine("=======================================");
                Console.WriteLine("         TICTACTOE");
                Console.WriteLine("=======================================");
                Console.WriteLine("Player 1: X (Blue)");
                Console.WriteLine("Player 2: O (Yellow)");
                Console.WriteLine("---------------------------------------");

                if (player % 2 == 1)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Giliran Player 1 (X)..");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Giliran Player 2 (O)..");
                }
                Console.ResetColor();
                Console.WriteLine();

                DrawBoard();

                Console.Write("\nMasukkan nomor pilihan Anda (1-9): ");
                string? input = Console.ReadLine();
                bool isNumber = int.TryParse(input, out choice);

                if (isNumber && choice >= 1 && choice <= 9)
                {
                    if (board[choice] != 'X' && board[choice] != 'O')
                    {
                        if (player % 2 == 1)
                        {
                            board[choice] = 'X';
                            player++;
                        }
                        else
                        {
                            board[choice] = 'O';
                            player++;
                        }

                        DrawBoard();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"\nMaaf, {choice} sudah diisi!");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("\nInput ga valid! masukkan angka antara 1 sampai 9.");
                    Console.ResetColor();
                }

                flag = CheckWin();

            } while (flag == 0);
            Console.Clear();
            Console.WriteLine("=======================================");
            Console.WriteLine("         TICTACTOE SELESAI");
            Console.WriteLine("=======================================");
            Console.WriteLine();
            DrawBoard();
            Console.WriteLine();

            if (flag == 1)
            {
                if (player % 2 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("Player 1 (X) menang!");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Player 2 (O) menang!");
                }
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Permainan berakhir seri!");
                Console.ResetColor();
            }

            Console.WriteLine("\nTekan tombol ENTER untuk keluar");
            Console.ReadLine();
        }

        static void DrawBoard()
        {
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", board[1], board[2], board[3]);
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", board[4], board[5], board[6]);
            Console.WriteLine("_____|_____|_____ ");
            Console.WriteLine("     |     |      ");
            Console.WriteLine("  {0}  |  {1}  |  {2}", board[7], board[8], board[9]);
            Console.WriteLine("     |     |      ");
        }
        static int CheckWin()
        {
            if (board[1] == board[2] && board[2] == board[3])
            {
                return 1;
            }
            else if (board[4] == board[5] && board[5] == board[6])
            {
                return 1;
            }
            else if (board[7] == board[8] && board[8] == board[9])
            {
                return 1;
            }
            else if (board[1] == board[4] && board[4] == board[7])
            {
                return 1;
            }
            else if (board[2] == board[5] && board[5] == board[8])
            {
                return 1;
            }
            else if (board[3] == board[6] && board[6] == board[9])
            {
                return 1;
            }
            else if (board[1] == board[5] && board[5] == board[9])
            {
                return 1;
            }
            else if (board[3] == board[5] && board[5] == board[7])
            {
                return 1;
            }
            else if (board[1] != '1' && board[2] != '2' && board[3] != '3' &&
                     board[4] != '4' && board[5] != '5' && board[6] != '6' &&
                     board[7] != '7' && board[8] != '8' && board[9] != '9')
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
