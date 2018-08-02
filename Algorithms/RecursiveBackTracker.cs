using System.Collections.Generic;
using System.Linq;
using Common;
using Mazes;

namespace Algorithms
{
    public class RecursiveBackTracker
    {
        public static T GenerateMaze<T>(T grid) where T : Grid
        {
            var start = grid.RandomCell();

            var stack = new Stack<Cell>();
            stack.Push(start);
            while (stack.Any())
            {
                var current = stack.Peek();
                var neighbours = current.Neighbours.Where(n => !n.Links().Any());

                if (neighbours.Any())
                {
                    var neighbour = neighbours.Random();
                    current.Link(neighbour);
                    stack.Push(neighbour);
                }
                else
                {
                    stack.Pop();
                }
            }

            return grid;
        }
    }
}