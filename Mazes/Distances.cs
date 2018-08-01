using System.Collections.Generic;

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

        public KeyCollection Cells()
        {
            return Keys;
        }

        public void SetDistance(Cell cell, int distance)
        {
            if (!ContainsKey(cell))
                Add(cell, distance);
            else
                this[cell] = distance;
        }
    }
}
