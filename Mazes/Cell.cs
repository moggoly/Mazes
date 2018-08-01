using System.Collections.Generic;
using System.Linq;

namespace Mazes
{
    public class Cell
    {
        private readonly Dictionary<Cell, bool> _links;

        public int Row { get; }
        public int Column { get; }

        public Cell North { get; set; }
        public Cell South { get; set; }
        public Cell East { get; set; }
        public Cell West { get; set; }

        public List<Cell> Neighbours
        {
            get
            {
                var list = new List<Cell>();
                if (North != null) list.Add(North);
                if (South != null) list.Add(South);
                if (East != null) list.Add(East);
                if (West != null) list.Add(West);
                return list;
            }
        }

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
            _links = new Dictionary<Cell, bool>();
        }

        public Cell Link(Cell cell, bool biDirectional = true)
        {
            _links[cell] = true;
            if (biDirectional)
                cell.Link(this, false);
            return this;
        }

        public Cell Unlink(Cell cell, bool biDirectional = true)
        {
            _links.Remove(cell);
            if (biDirectional)
                cell.Unlink(this, false);
            return this;
        }

        public List<Cell> Links()
        {
            return _links.Keys.ToList();
        }

        public bool IsLinked(Cell cell)
        {
            return cell != null && _links.ContainsKey(cell);
        }

        public Distances Distances()
        {
            var distances = new Distances(this);
            var frontier = new List<Cell> {this};

            while (frontier.Any())
            {
                var newFrontier = new List<Cell>();

                foreach (var cell in frontier)
                {
                    foreach (var linked in cell.Links())
                    {
                        if (distances.ContainsKey(linked)) continue;
                        distances.Add(linked, distances[cell] + 1);
                        newFrontier.Add(linked);
                    }
                }
                frontier = newFrontier;
            }

            return distances;
        }
    }
}
