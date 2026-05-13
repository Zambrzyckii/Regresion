using System.Globalization;
using CsvHelper;

namespace Regresion.Loader;

public static class CsvDataLoader
{
    public static (double[,] AxeX, double[] AxeY) LoadWith(string filePath, int days, IEnumerable<string> nameX, string nameY)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Read();
        csv.ReadHeader();
        List<List<double>> xList = new List<List<double>>();
        List<double> yList = new List<double>();
        while (csv.Read())
        {
            List<double> valX = new List<double>();
            foreach (string name in nameX)
            {
                double tempValX = csv.GetField<double>(name);
                valX.Add(tempValX);
            }
            double valY = csv.GetField<double>(nameY);

            xList.Add(valX);
            yList.Add(valY);
        }

        xList = xList.TakeLast(days).ToList();
        yList = yList.TakeLast(days).ToList();
        double[,] X = new double[xList.Count,xList[0].Count];
        for (int i = 0; i < xList.Count; i++)
        {
            for (int j = 0; j < xList[i].Count; j++)
            {
                X[i, j] = xList[i][j];
            }
        }
        double[] Y = yList.ToArray();

        return (X, Y);
    }
}

