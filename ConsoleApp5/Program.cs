using System;
using System.Collections.Generic;

namespace ConsoleApp5
{
    public class Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Visited { get; set; }
        public bool[] Walls { get; set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
            Visited = false;
            Walls = new bool[] { true, true, true, true }; // Top, Right, Bottom, Left
        }
        // Phương thức Display được cải tiến để hiển thị mỗi ô trên ba dòng
        public void DisplayTop()
        {
            Console.Write("█");
            Thread.Sleep(100);
            Console.Write(Walls[0] ? "█" : " ");
            Thread.Sleep(100);
            Console.Write("█");
            Thread.Sleep(100);
        }

        public void DisplayMiddle()
        {
            Console.Write(Walls[3] ? "█" : " ");
            Thread.Sleep(100);
            Console.Write(" ");
            Thread.Sleep(100);
            Console.Write(Walls[1] ? "█" : " ");
            Thread.Sleep(100);
        }

        public void DisplayBottom()
        {
            Console.Write("█");
            Thread.Sleep(100);
            Console.Write(Walls[2] ? "█" : " ");
            Thread.Sleep(100);
            Console.Write("█");
            Thread.Sleep(100);
        }
    }

    public class Maze
    {
        private int width, height;
        private List<Cell> grid;
        private Stack<Cell> stack;

        public Maze(int width, int height)
        {
            this.width = width;
            this.height = height;
            grid = new List<Cell>();
            stack = new Stack<Cell>();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    grid.Add(new Cell(x, y));
                }
            }
        }

        public void Generate()
        {
            Cell current = grid[0];
            current.Visited = true;
            stack.Push(current);

            while (stack.Count > 0)
            {
                current = stack.Pop();
                List<Cell> neighbors = GetUnvisitedNeighbors(current);

                if (neighbors.Count > 0)
                {
                    stack.Push(current);
                    Cell next = neighbors[new Random().Next(neighbors.Count)];
                    RemoveWalls(current, next);
                    next.Visited = true;
                    stack.Push(next);
                }
            }
        }

        private List<Cell> GetUnvisitedNeighbors(Cell cell)
        {
            List<Cell> neighbors = new List<Cell>();

            int[,] deltas = new int[,] { { 0, -1 }, { 1, 0 }, { 0, 1 }, { -1, 0 } };
            for (int i = 0; i < 4; i++)
            {
                int newX = cell.X + deltas[i, 0];
                int newY = cell.Y + deltas[i, 1];

                if (newX >= 0 && newY >= 0 && newX < width && newY < height)
                {
                    Cell neighbor = grid[newX + newY * width];
                    if (!neighbor.Visited)
                    {
                        neighbors.Add(neighbor);
                    }
                }
            }

            return neighbors;
        }

        private void RemoveWalls(Cell a, Cell b)
        {
            int x = b.X - a.X;
            int y = b.Y - a.Y;

            if (x == 1)
            {
                a.Walls[1] = false;
                b.Walls[3] = false;
            }
            else if (x == -1)
            {
                a.Walls[3] = false;
                b.Walls[1] = false;
            }

            if (y == 1)
            {
                a.Walls[2] = false;
                b.Walls[0] = false;
            }
            else if (y == -1)
            {
                a.Walls[0] = false;
                b.Walls[2] = false;
            }
        }

        public void Display()
        {
            for (int y = 0; y < height; y++)
            {
                // Hiển thị phần trên của mỗi ô
                for (int x = 0; x < width; x++)
                {
                    grid[x + y * width].DisplayTop();
                }
                Console.WriteLine();

                // Hiển thị phần giữa của mỗi ô
                for (int x = 0; x < width; x++)
                {
                    grid[x + y * width].DisplayMiddle();
                }
                Console.WriteLine();

                // Hiển thị phần dưới của mỗi ô
                for (int x = 0; x < width; x++)
                {
                    grid[x + y * width].DisplayBottom();
                }
                Console.WriteLine();
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Maze maze = new Maze(8, 8);
            maze.Generate();
            maze.Display();
        }
    }
}
