using System;
using System.Collections.Generic;
using Mazes;

namespace Algorithms
{
    public class BinaryTree
    {
        public static Grid GenerateMaze(Grid grid)
        {
            var rnd = new Random();

            foreach (var cell in grid.EachCell())
            {
                var neighbours = new List<Cell>();

                if (cell.North != null)
                    neighbours.Add(cell.North);
                if (cell.East != null)
                    neighbours.Add(cell.East);

                var index = rnd.Next(neighbours.Count);
                if (index < neighbours.Count)
                {
                    var neighbour = neighbours[index];
                    cell.Link(neighbour);
                }
            }

            return grid;
        }
    }
}
