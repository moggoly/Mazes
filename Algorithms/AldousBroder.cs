using System;
using Mazes;

namespace Algorithms
{
    public class AldousBroder
    {
        public static T GenerateMaze<T>(T grid) where T : Grid
        {
            var rnd = new Random();

            var cell = grid.RandomCell();
            var unvisited = grid.Size - 1;

            while (unvisited > 0)
            {
                var index = rnd.Next(cell.Neighbours.Count);
                var neighbour = cell.Neighbours[index];

                if (neighbour.Links().Count == 0)
                {
                    cell.Link(neighbour);
                    unvisited -= 1;
                }

                cell = neighbour;
            }

            return grid;
        }
    }
}