using System;
using System.Linq;
using System.Text;

namespace Lab_18_OOP_Danylko
{
    public class SimpleArray
    {
        public int CountPositiveNumbers(double[] array)
        {
            return array.Count(x => x > 0);
        }

        public double SumAfterLastZero(double[] array)
        {
            int lastIndex = Array.LastIndexOf(array, 0);
            if (lastIndex != -1 && lastIndex != array.Length - 1)
            {
                return array.Skip(lastIndex + 1).Sum();
            }
            else
            {
                return 0;
            }
        }

        public double[] TransformArray(double[] array)
        {
            return array.OrderBy(x => Math.Truncate(x) <= 1 ? 0 : 1).ToArray();
        }
    }
    public class HardArray
    {
        public double[] GetFifthRow(double[,] array)
        {
            int rowCount = array.GetLength(0);
            double[] fifthRow = new double[array.GetLength(1)];
            for (int j = 0; j < array.GetLength(1); j++)
            {
                fifthRow[j] = array[4, j];
            }
            return fifthRow;
        }

        public double[] GetColumnValues(double[,] array, int columnIndex)
        {
            int rowCount = array.GetLength(0);
            if (columnIndex >= 0 && columnIndex < array.GetLength(1))
            {
                double[] columnValues = new double[rowCount];
                for (int i = 0; i < rowCount; i++)
                {
                    columnValues[i] = array[i, columnIndex];
                }
                return columnValues;
            }
            else
            {
                throw new IndexOutOfRangeException("Ви ввели недійсний номер стовпця.");
            }
        }

        public string GetArrayOutput(double[,] array)
        {
            StringBuilder sb = new StringBuilder();
            int rowCount = array.GetLength(0);
            int columnCount = array.GetLength(1);
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < columnCount; j++)
                {
                    sb.Append(array[i, j]);
                    if (j < columnCount - 1)
                    {
                        sb.Append(", ");
                    }
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}

