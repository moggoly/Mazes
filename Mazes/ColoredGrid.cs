using System;
using System.Drawing;
using System.Linq;

namespace Mazes
{
    public class ColoredGrid : Grid
    {
        private int? maximum;

        public Distances Distances { get; set; }

        public ColoredGrid(int rows, int columns) : base(rows, columns)
        {
            ShowBackgrounds = true;
        }

        public override Color BackgroundColorFor(Cell cell)
        {
            if (!maximum.HasValue) maximum = Distances.Max().Item2;
            if (Distances != null && Distances.ContainsKey(cell))
            {
                var distance = Distances[cell];
                var intensity = (maximum - (float) distance) / maximum;
                var dark = Convert.ToInt32(255 * intensity);
                var bright = 128 + Convert.ToInt32(127 * intensity);
                return Color.FromArgb(dark, bright, dark);
            }

            return Color.White;
        }
    }
}