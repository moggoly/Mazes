using System;
using Algorithms;
using Mazes;

namespace MazeRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = new DistanceGrid(10, 10);
            var maze = Sidewinder.GenerateMaze(grid);

            var start = maze.GetCell(0, 0);
            var distances = start.Distances();
            maze.Distances = distances.PathTo(grid.GetCell(grid.Rows - 1, grid.Columns - 1));

            Console.WriteLine(maze);

            var img = maze.ToPng(20);
            img.Save("maze.png");

            Console.WriteLine("Done....");
            Console.ReadLine();
        }
    }
}
