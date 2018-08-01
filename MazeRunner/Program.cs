﻿using System;
using Algorithms;
using Mazes;

namespace MazeRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            var grid = new DistanceGrid(8, 8);
            var maze = Sidewinder.GenerateMaze(grid);

            var start = maze.GetCell(0, 0);
            var distances = start.Distances();
            maze.Distances = distances;

            Console.WriteLine(maze);

            var img = maze.ToPng(20);
            img.Save("maze.png");

            Console.WriteLine("Done....");
            Console.ReadLine();
        }
    }
}
