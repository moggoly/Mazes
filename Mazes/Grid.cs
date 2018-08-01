﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Mazes
{
    public class Grid
    {
        public int Rows { get; }
        public int Columns { get; }
        public Cell[][] Cells { get; private set; }

        private readonly Random _rnd = new Random();

        public Grid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;

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
                var body = "   ";
                var corner = "+";

                foreach (var cell in cellRow)
                {
                    top += body + (cell.IsLinked(cell.East) ? " " : "|");
                    bottom += (cell.IsLinked(cell.South) ? "   " : "---") + corner;
                }

                output += top + "\n";
                output += bottom + "\n";
            }

            return output;
        }
    }
}
