using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Mazes
{
    public class Grid
    {
        public int Rows { get; }
        public int Columns { get; }
        public int Size { get; }
        public Cell[][] Cells { get; private set; }

        private readonly Random _rnd = new Random();

        protected bool ShowBackgrounds = false;

        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            Size = Rows * Columns;

            PrepareGrid();
            ConfigureCells();
        }

        public void PrepareGrid()
        {
            Cells = new Cell[Rows][];
            for (var i = 0; i < Rows; i++)
            {
                Cells[i] = new Cell[Columns];
                for (var j = 0; j < Columns; j++)
                {
                    Cells[i][j] = new Cell(i, j);
                }
            }
        }

        public void ConfigureCells()
        {
            foreach (var cell in EachCell())
            {
                var row = cell.Row;
                var col = cell.Column;

                if (row - 1 >= 0)
                    cell.North = Cells[row - 1][col];
                if (row + 1 < Rows)
                    cell.South = Cells[row + 1][col];
                if (col - 1 >= 0)
                    cell.West = Cells[row][col - 1];
                if (col + 1 < Columns)
                    cell.East = Cells[row][col + 1];
            }
        }

        public Cell GetCell(int row, int column)
        {
            return Cells[row][column];
        }

        public Cell RandomCell()
        {
            var row = _rnd.Next(Rows);
            var column = _rnd.Next(Columns);
            return GetCell(row, column);
        }

        public IEnumerable<Cell[]> EachRow()
        {
            for (var i = 0; i < Rows; i++)
            {
                yield return Cells[i];
            }
        }

        public IEnumerable<Cell> EachCell()
        {
            return Cells.SelectMany(cellRow => cellRow);
        }

        public virtual string ContentsOf(Cell cell)
        {
            return " ";
        }

        public virtual Color BackgroundColorFor(Cell cell)
        {
            return Color.White;
        }

        public override string ToString()
        {
            var output = "+";
            for (var i = 0; i < Columns; i++)
            {
                output += "---+";
            }
            output += "\n";

            foreach (var cellRow in EachRow())
            {
                var top = "|";
                var bottom = "+";
                var corner = "+";

                foreach (var cell in cellRow)
                {
                    var body = $" {ContentsOf(cell)} ";
                    top += body + (cell.IsLinked(cell.East) ? " " : "|");
                    bottom += (cell.IsLinked(cell.South) ? "   " : "---") + corner;
                }

                output += top + "\n";
                output += bottom + "\n";
            }

            return output;
        }

        public Bitmap ToPng(int cellSize = 10)
        {
            var imageWidth = cellSize * Columns;
            var imageHeight = cellSize * Rows;

            var background = Brushes.White;
            var wall = Pens.Black;

            var image = new Bitmap(imageWidth, imageHeight);

            using (var g = Graphics.FromImage(image))
            {
                g.FillRectangle(background, 0, 0, imageWidth, imageHeight);

                if (ShowBackgrounds)
                    foreach (var cell in EachCell())
                    {
                        var x1 = cell.Column * cellSize;
                        var y1 = cell.Row * cellSize;
                        var x2 = (cell.Column + 1) * cellSize;
                        var y2 = (cell.Row + 1) * cellSize;

                        var color = BackgroundColorFor(cell);
                        var brush = new SolidBrush(color);
                        g.FillRectangle(brush, x1, y1, (x2 - x1), (y2 - y1));
                    }

                foreach (var cell in EachCell())
                {
                    var x1 = cell.Column * cellSize;
                    var y1 = cell.Row * cellSize;
                    var x2 = (cell.Column + 1) * cellSize;
                    var y2 = (cell.Row + 1) * cellSize;

                    if (cell.North == null)
                        g.DrawLine(wall, x1, y1, x2, y1);
                    if (cell.West == null)
                        g.DrawLine(wall, x1, y1, x1, y2);

                    if (!cell.IsLinked(cell.East))
                        g.DrawLine(wall, x2, y1, x2, y2);
                    if (!cell.IsLinked(cell.South))
                        g.DrawLine(wall, x1, y2, x2, y2);
                }
            }

            return image;
        }
    }
}
