using System.Linq;
using Common;
using Mazes;

namespace Algorithms
{
    public class HuntAndKill
    {
        public static T GenerateMaze<T>(T grid) where T : Grid
        {
            var current = grid.RandomCell();

            while (current != null)
            {
                var unvisitedNeighbours = current.Neighbours.Where(n => !n.Links().Any());

                if (unvisitedNeighbours.Any())
                {
                    var neighbour = unvisitedNeighbours.Random();
                    current.Link(neighbour);
                    current = neighbour;
                }
                else
                {
                    current = null;

                    foreach (var cell in grid.EachCell())
                    {
                        var visitedNeighbours = cell.Neighbours.Where(n => n.Links().Any());
                        if (!cell.Links().Any() && visitedNeighbours.Any())
                        {
                            current = cell;
                            var neighbour = visitedNeighbours.Random();
                            current.Link(neighbour);
                            break;
                        }
                    }
                }
            }

            return grid;
        }
    }
}