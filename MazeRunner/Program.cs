using System;
using Algorithms;
using Mazes;

namespace MazeRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = new Grid(15, 15);
            var maze = Sidewinder.GenerateMaze(grid);

            Console.WriteLine(maze);

            var img = maze.ToPng(20);
            img.Save("maze.png");

            Console.WriteLine("Done....");
            Console.ReadLine();
        }
    }
}
