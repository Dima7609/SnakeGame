using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Pos
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    class Program
    {
        static char[][] grid = new char[25][];
        static int width = 50;
        static int height = 25;
        static int worm_x = 25;
        static int worm_y = 9;
        static List<Pos> worm = new List<Pos>();
        static bool gameover = false;

        enum direction
        {
            UP = 1,
            DOWN = 2,
            LEFT = 3,
            RIGHT = 4
        };

        static direction current = direction.RIGHT;
        static int wormlenght = 1;
        static int target_x;
        static int target_y;
        static int score = 0;

        static void Main()
        {
            Speed speed = new Speed();
            ColorC cnslcolor = new ColorC();

            cnslcolor.TestFrame();
            InitFrame();
            DrawFrame();
            InitWorm();
            SetTarget();
            WriteMaxScore();

            while (!gameover)
            {
                DrawWormHead();
                if (TargetTaken())
                {
                    SetTarget();
                    UpdateScore();
                }
                speed.IncreaseSpeed();
                ReadKeys();
                DrawWormBodyOnHeadPosition();
                MoveWormHead();
                if (isGameover())
                {
                    gameover = true;
                    TestInitt();
                }
                DeleteWormTail();
            }
            //DrawWormHead();
        }

        static void InitFrame() {

            Console.CursorVisible = false;

            for (int i = 0; i < height; i++)
                grid[i] = new char[width];

            grid[0][0] = '\u2554';
            grid[0][width - 1] = '\u2557';
            grid[height - 1][0] = '\u255A';
            grid[height - 1][width - 1] = '\u255D';

            for (int i = 1; i < height - 1; i++)
            {
                grid[i][0] = '\u2551';
                grid[i][width - 1] = '\u2551';
            }

            for (int i = 1; i < width - 1; i++)
            {
                grid[0][i] = '\u2550';
                grid[height - 1][i] = '\u2550';
            }

            for (int y = 1; y < height - 1; y++)
                for (int x = 1; x < width - 1; x++)
                    grid[y][x] = ' ';
        }

        static void DrawFrame()
        {
            for (int y = 0; y < height; y++)
                for(int x = 0; x < width; x++)
                {
                    Console.SetCursorPosition(x, y);
                    Console.Write(grid[y][x].ToString());
                }
        }

        static void InitWorm()
        {
            worm.Add(new Pos() { X = 25, Y = 9 });
            foreach (Pos pos in worm)
                grid[pos.Y][pos.X] = ' ';
        }

        static void DrawWormBodyOnHeadPosition()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(worm_x, worm_y);
            Console.Write('o');
        }

        static void DrawWormHead()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(worm_x, worm_y);
            Console.Write('O');
        }

        static void MoveWormHead()
        {
            grid[worm_y][worm_x] = 'o';

            switch (current)
            {
                case direction.UP :
                    worm_y--;
                    break;
                case direction.DOWN :
                    worm_y++;
                    break;
                case direction.LEFT :
                    worm_x--;
                    break;
                case direction.RIGHT :
                    worm_x++;
                    break;
            }
            worm.Add(new Pos() { X = worm_x, Y = worm_y });
        }

        static bool isGameover()
        {
            bool value = false;
            if (grid[worm_y][worm_x] != ' ')
                value = true;
            return value;
        }

        static void DeleteWormTail()
        {
            Console.SetCursorPosition(worm[0].X, worm[0].Y);
            Console.Write(' ');
            if (worm.Count != wormlenght)
            {
                grid[worm[0].Y][worm[0].X] = ' ';
                worm.RemoveAt(0);
            }
        }

        static bool TargetTaken()
        {
            return worm_x == target_x && worm_y == target_y;
        }

        static void SetTarget()
        {
            Random rnd = new Random();
            int x = 0;
            int y = 0;

            while (grid[y][x] != ' ')
            {
                x = rnd.Next(1, width - 1);
                y = rnd.Next(1, height - 1);
            }
            target_x = x;
            target_y = y;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(x, y);
            Console.Write("X");
        }

        static void UpdateScore()
        {
            score++;
            wormlenght++;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, 25);
            Console.Write("Score: " + score);
        }

         public int ReturnScore()
        {
            return score;
        }

        static void ReadKeys()
        {
            ConsoleKeyInfo s;
            if(Console.KeyAvailable)
            {
                s = Console.ReadKey(true);
                switch (s.Key)
                {
                    case ConsoleKey.UpArrow :
                        if (current != direction.DOWN)
                        {
                            current = direction.UP;
                        }
                        break;
                    case ConsoleKey.DownArrow :
                        if (current != direction.UP)
                        {
                            current = direction.DOWN;
                        }
                        break;
                    case ConsoleKey.LeftArrow :
                        if (current != direction.RIGHT)
                        {
                            current = direction.LEFT;
                        }
                        break;
                    case ConsoleKey.RightArrow :
                        if (current != direction.LEFT)
                        {
                            current = direction.RIGHT;
                        }
                        break;
                    case ConsoleKey.P:
                        {
                            MaxScore mxscore = new MaxScore();
                            mxscore.TestMaxScore();
                            Environment.Exit(0);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        static void TestInitt()
        {
            Console.SetCursorPosition(0, 26);
            Console.WriteLine("Game Over!");
            MaxScore mxscore = new MaxScore();
            mxscore.TestMaxScore();
            Console.ReadLine();

        }
        static void WriteMaxScore()
        {
            MaxScore mxscore = null;
            mxscore = MaxScore.GetMaxScore();
            int maxscore = mxscore.MyScore;
            Console.SetCursorPosition(51, 1);
            Console.WriteLine("Max score: " + maxscore);
        }
    }
}