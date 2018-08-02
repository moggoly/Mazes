using System;
using Algorithms;
using Mazes;

namespace MazeRunner
{
    class Program
    {
        static void Main()
        {
            var p = new Program();
            p.Run();

            Console.WriteLine("Done....");
            //Console.ReadLine();
        }

        private void Run()
        {
            var grid = GenerateMaze(25, 25);
            var maze = ShortestPath(grid);

            //Console.WriteLine(maze);
            var img = maze.ToPng(20);
            img.Save("maze.png");
        }

        private ColoredGrid GenerateMaze(int rows, int columns)
        {
            var grid = new ColoredGrid(rows, columns);
            var maze = Wilsons.GenerateMaze(grid);
            return maze;
        }

        private ColoredGrid ShortestPath(ColoredGrid maze)
        {
            var start = maze.GetCenterCell();
            var distances = start.Distances();
            maze.Distances = distances;
            //maze.Distances = distances.PathTo(maze.GetCell(maze.Rows - 1, maze.Columns - 1));
            return maze;
        }

        private ColoredGrid LongestPath(ColoredGrid maze)
        {
            var start = maze.GetCell(0, 0);
            var distances = start.Distances();

            var (newStart, _) = distances.Max();

            var newDistances = newStart.Distances();
            var (goal, _) = newDistances.Max();

            maze.Distances = newDistances.PathTo(goal);

            return maze;
        }
    }
}
