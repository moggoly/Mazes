using System.Collections.Generic;
using System.Linq;

namespace Mazes
{
    public class Distances : Dictionary<Cell, int>
    {
        private Cell _root;

        public Distances(Cell root) : base()
        {
            _root = root;
            this[root] = 0;
        }

        public List<Cell> Cells()
        {
            return Keys.ToList();
        }

        public void SetDistance(Cell cell, int distance)
        {
            if (!ContainsKey(cell))
                Add(cell, distance);
            else
                this[cell] = distance;
        }

        public Distances PathTo(Cell goal)
        {
            Cell current = goal;

            var breadcrumbs = new Distances(_root) {[current] = this[current]};

            while (current != _root)
            {
                foreach (var neighbour in current.Links())
                {
                    if (this[neighbour] < this[current])
                    {
                        if (!breadcrumbs.ContainsKey(neighbour))
                            breadcrumbs.Add(neighbour, this[neighbour]);
                        else
                            breadcrumbs[neighbour] = this[neighbour];
                        current = neighbour;
                        break;
                    }
                }
            }

            return breadcrumbs;
        }
    }
}
