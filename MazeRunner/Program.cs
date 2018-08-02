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
            Console.ReadLine();
        }

        private void Run()
        {
            var grid = GenerateMaze(15, 15);
            var maze = ShortestPath(grid);

            Console.WriteLine(maze);
            //var img = maze.ToPng(20);
            //img.Save("maze.png");
        }

        private DistanceGrid GenerateMaze(int rows, int columns)
        {
            var grid = new DistanceGrid(rows, columns);
            var maze = Sidewinder.GenerateMaze(grid);
            return maze;
        }

        private Grid ShortestPath(DistanceGrid maze)
        {
            var start = maze.GetCell(0, 0);
            var distances = start.Distances();

            maze.Distances = distances.PathTo(maze.GetCell(maze.Rows - 1, maze.Columns - 1));
            return maze;
        }

        private Grid LongestPath(DistanceGrid maze)
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
