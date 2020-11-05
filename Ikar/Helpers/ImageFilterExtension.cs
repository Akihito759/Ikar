using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataServer.Helpers
{
    public static class ImageFilterExtension
    {
        public static double[] ApplyFilterOnData(this double[] temperature, double[,] filter, bool doFillFrame = false)
        {
            var data = ConvertTo2DWithFrame(temperature,doFillFrame);
            var final = data.Clone() as double[,];

            for (var row = 1; row < 9; ++row)
            {
                for (var column = 1; column < 9; ++column)
                {
                    var window = GetWindow(data, row, column);
                    final[row, column] = GetFilterdValue(window, filter);
                }
            }

            return Convert2DToOriginalSize(final);
        }

        public static double[] ApplyMedianFilter(this double[] temperature, bool doFillFrame = false)
        {
            var temperatureWithFrame = ConvertTo2DWithFrame(temperature,doFillFrame);
            for (var row = 1; row < 9; ++row)
            {
                for (var column = 1; column < 9; ++column)
                {
                    var window = GetWindow(temperatureWithFrame, row, column);
                    var medianValue = FlatArrayToList(window).OrderBy(x => x).ElementAt(4);
                    temperatureWithFrame[row, column] = medianValue;
                }
            }
            return Convert2DToOriginalSize(temperatureWithFrame);
        }

        public static double[] ApplyBinarisation(this double[] temperature, double threshold)
        {
            var temp = new double[64];
            for (int i = 0; i < temperature.Length; ++i)
            {
                temp[i] =
                    temperature[i] <= threshold ? 0 : 1;
            }
            return temp;
        }

        private static double[,] ConvertTo2DWithFrame(double[] temperature, bool doFrame = false)
        {
            var temp = new double[10, 10];
            var index = 0;
            for (var row = 1; row < 9; ++row)
            {
                for (var column = 1; column < 9; ++column)
                {
                    temp[row, column] = temperature[index++];
                }
            }

            if (doFrame)
            {
                FillFrame(temp);
            }


            return temp;
        }

        private static void FillFrame(double[,] dataWithFrame)
        {
            for (var row = 0; row < 10; ++row)
            {
                if (row == 0)
                {
                    dataWithFrame[row, 0] = dataWithFrame[row + 1, 1];
                    dataWithFrame[row, 9] = dataWithFrame[row + 1, 8];
                    for (var j=1; j < 9; ++j)
                    {
                        dataWithFrame[row, j] = dataWithFrame[row + 1, j];
                    }
                }
                else if(row == 9)
                {
                    dataWithFrame[row, 0] = dataWithFrame[row - 1, 1];
                    dataWithFrame[row, 9] = dataWithFrame[row - 1, 8];
                    for (var j = 1; j < 9; ++j)
                    {
                        dataWithFrame[row, j] = dataWithFrame[row - 1, j];
                    }
                }
                else
                {
                    dataWithFrame[row, 0] = dataWithFrame[row, 1];
                    dataWithFrame[row, 9] = dataWithFrame[row, 8];
                }
            }
        }

        private static double[] Convert2DToOriginalSize(double[,] frame)
        {
            var temp = new double[64];
            var index = 0;
            for (var row = 1; row < 9; ++row)
            {
                for (var column = 1; column < 9; ++column)
                {
                    temp[index++] = frame[row, column];
                }
            }
            return temp;
        }

        private static double[,] GetWindow(double[,] frame, int row, int column)
        {
            if (row < 1 || row > 8 || column < 1 || column > 8)
            {
                throw new Exception();
            }

            var window = new double[3, 3];
            for (var i = 0; i < 3; ++i)
            {
                for (var j = 0; j < 3; ++j)
                {
                    window[i, j] = frame[row + i - 1, column + j - 1];
                }
            }
            return window;
        }

        private static double GetFilterdValue(double[,] window, double[,] filter)
        {
            double value = 0;

            for (var i = 0; i < 3; ++i)
            {
                for (var j = 0; j < 3; ++j)
                {
                    value += window[i, j] * filter[i, j];
                }
            }
            return FlatArrayToList(filter).Sum() != 0 ? value / FlatArrayToList(filter).Sum() : throw new Exception();
        }

        private static List<double> FlatArrayToList(double[,] array2D)
        {
            var temp = new List<double>();
            for (var i = 0; i < 3; ++i)
            {
                for (var j = 0; j < 3; ++j)
                {
                    temp.Add(array2D[i, j]);
                }
            }
            return temp;
        }
    }
}
