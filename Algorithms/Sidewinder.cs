using System;
using System.Collections.Generic;
using Mazes;

namespace Algorithms
{
    public class Sidewinder
    {
        public static T GenerateMaze<T>(T grid) where T: Grid
        {
            var rnd = new Random();

            foreach (var cellRow in grid.EachRow())
            {
                var run = new List<Cell>();

                foreach (var cell in cellRow)
                {
                    run.Add(cell);

                    var atEasternBoundary = cell.East == null;
                    var atNorthernBoundary = cell.North == null;

                    var shouldCloseOut = atEasternBoundary || !atNorthernBoundary && rnd.Next(2) == 0;

                    if (shouldCloseOut)
                    {
                        var member = run[rnd.Next(run.Count - 1)];
                        if (member.North != null)
                        {
                            member.Link(member.North);
                        }
                        run.Clear();
                    }
                    else
                    {
                        cell.Link(cell.East);
                    }
                }
            }

            return grid;
        }
    }
}