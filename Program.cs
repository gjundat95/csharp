using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BaiTap
{
    class Program
    {
        static void Main(string[] args)
        {
            int columnCount = 7;
            int rowCount = 6;

            int[,] a = new int[,] {

                { 0, 2, 3, 4, 0, 0, 0 },
                { 0, 0, 0, 0, 1, 0, 0 },
                { 0, 0, 0, 0, 2, 3, 0 },
                { 0, 0, 0, 0, 0, 4, 0 },
                { 0, 0, 0, 0, 0, 0, 5 },
                { 0, 0, 0, 0, 0, 0, 3 }
            };

            //for (int col = rowCount-1; col >= 0; col--)
            //{
            //    for (int row = columnCount-1; row >= 0; row--)
            //    {
            //        System.Console.Write("-" + a[col, row]+"/");
            //    }
            //}
            //for (int row = rowCount-1; row >= 0; row--)
            //{
            //    for (int col = columnCount; col >= 0; col--)
            //    {
            //        System.Console.Write(minOneRow(a, rowCount, columnCount, row));
            //    }
            //}

            //int[] listLS = getLS(a, rowCount, columnCount);
            //System.Console.WriteLine();
            //for (int i = rowCount - 1; i >= 0; --i) {
            //    System.Console.WriteLine(listLS[i]);
            //}

            //int[] listEO =getEO(a, rowCount, columnCount);
            //foreach (int i in listEO)
            //{
            //    System.Console.Write(i);
            //}

            int[,] eo = getEO(a, rowCount, columnCount);
            List<Point> positionCriticalPath = getPositionCriticalPath(eo,rowCount,columnCount);
            foreach (int position in eo) {
                System.Console.Write("-{0}-",position);
            }
            Console.WriteLine();
           


            System.Console.ReadLine();
        }
        /// <summary>
        /// Return position CriticalPath
        /// </summary>
        /// <param name="a">Param Array EO type int[,] </param>
        /// <param name="rowCount">Row count</param>
        /// <param name="columnCount">Column count</param>
        /// <returns>Return List<Point> position of criticalPath</returns>
        public static List<Point> getPositionCriticalPath(int[,] a, int rowCount, int columnCount)
        {
            List<Point> position = new List<Point>();
            int temp = columnCount - 1;
            for (int col = columnCount - 1; col >= 0; col--)
            {
                int maxCol = maxOneColumn(a, rowCount, columnCount, col);
                for (int row = rowCount - 1; row >= 0; row--)
                {
                    if (maxCol == a[row, col] && temp == col)
                    {
                        Point point = new Point(row, col);
                        position.Add(point);
                        temp = row;
                    }
                }
            }
            return position;
        }

        /// <summary>
        /// Processing EO,result one Array MaxEO
        /// </summary>
        /// <param name="a">Array type int[,]</param>
        /// <param name="rowCount">row</param>
        /// <param name="columCount">column</param>
        /// <returns>Araay MaxEO type int[]</returns>
        public static int[] getEOMax(int[,] a, int rowCount, int columCount)
        {
            int[,] eo = new int[rowCount, columCount];
            int[] eoMax = new int[columCount];
            eoMax[0] = 0;
            int temp = 0;

            for (int col = 0; col < columCount; col++)
            {
                for (int row = 0; row < rowCount; row++)
                {
                    temp = eoMax[row];
                    if (a[row, col] != 0)
                    {
                        eo[row, col] = a[row, col] + temp;
                        eoMax[col] = maxOneColumn(eo, rowCount, columCount, col);
                    }
                }
            }
            return eoMax;
        }
        /// <summary>
        /// Processing EO, result multi array EO
        /// </summary>
        /// <param name="a">Array type int[,]</param>
        /// <param name="rowCount">Row count</param>
        /// <param name="columCount">Column count</param>
        /// <returns>Return multi array EO</returns>
        public static int[,] getEO(int[,] a, int rowCount, int columCount)
        {
            int[,] eo = new int[rowCount, columCount];
            int[] eoMax = new int[columCount];
            eoMax[0] = 0;
            int temp = 0;

            for (int col = 0; col < columCount; col++)
            {
                for (int row = 0; row < rowCount; row++)
                {
                    temp = eoMax[row];
                    if (a[row, col] != 0)
                    {
                        eo[row, col] = a[row, col] + temp;
                        eoMax[col] = maxOneColumn(eo, rowCount, columCount, col);
                    }
                }
            }
            return eo;
        }
        /// <summary>
        /// Processing LS
        /// </summary>
        /// <param name="a">a Array int[,]</param>
        /// <param name="rowCount">row of array</param>
        /// <param name="columnCount">column of array</param>
        /// <returns>array LS type int[]</returns>
        public static int[] getLS(int[,] a, int rowCount, int columnCount)
        {
            int[,] ls = new int[rowCount, columnCount];
            int[] lsMin = new int[rowCount + 1];
            lsMin[rowCount] = 11;
            int temp = 0;
            for (int row = rowCount - 1; row >= 0; row--)
            {
                for (int col = columnCount - 1; col >= 0; col--)
                {
                    temp = lsMin[col];
                    if (a[row, col] != 0)
                    {
                        ls[row, col] = temp - a[row, col];
                        if (col > 1)
                        {
                            lsMin[row] = minOneRow(ls, rowCount, columnCount, row);
                        }
                        else
                        {
                            lsMin[row] = minOneRowZero(ls, rowCount, columnCount, row);
                        }
                        // System.Console.Write("/"+minOneRow(ls, rowCount, columnCount, row)+"/");
                    }
                }
            }
            return lsMin;
        }

        public static int maxOneColumn(int[,] a, int rowCount, int columnCount, int indexColumn)
        {
            int maxColumn = a[0, indexColumn];
            for (int row = 0; row < rowCount; row++)
            {
                if (maxColumn < a[row, indexColumn])
                    maxColumn = a[row, indexColumn];
            }
            return maxColumn;
        }

        public static int minOneRow(int[,] a, int rowCount, int columnCount, int indexRow)
        {

            int minRow = a[indexRow, 0];
            for (int i = 0; i < columnCount; i++)
            {
                if (a[indexRow, i] != 0)
                {
                    minRow = a[indexRow, i];
                    break;
                }
            }
            for (int col = 0; col < columnCount; col++)
            {
                if (minRow > a[indexRow, col] && a[indexRow, col] != 0)
                    minRow = a[indexRow, col];
            }
            return minRow;
        }

        public static int minOneRowZero(int[,] a, int rowCount, int columnCount, int indexRow)
        {
            int minRow = a[indexRow, 0];
            for (int col = 0; col < columnCount; col++)
            {
                if (minRow > a[indexRow, col] && a[indexRow, col] != 0)
                    minRow = a[indexRow, col];
            }
            return minRow;
        }

        public static int[] maxMatrixColumns(int[,] a, int columnCount, int rowCount)
        {
            int[] columns = new int[columnCount];
            for (int i = 0; i < columnCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    int max = a[i, 0];
                    if (max < a[i, j])
                        max = a[i, j];
                    columns[i] = max;
                }
            }
            return columns;
        }

    }

    public class Point
    {
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int x { get; set; }
        public int y { get; set; }
    }

}
