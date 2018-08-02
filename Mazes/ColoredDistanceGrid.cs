using Common;

namespace Mazes
{
    public class ColoredDistanceGrid : ColoredGrid
    {
        public ColoredDistanceGrid(int rows, int columns) : base(rows, columns)
        {
        }

        public override string ContentsOf(Cell cell)
        {
            if (Distances != null && Distances.ContainsKey(cell))
            {
                return Base36.Encode(Distances[cell]);
            }

            return " ";
        }
    }
}