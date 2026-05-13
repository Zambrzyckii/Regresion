using System.Globalization;
using CsvHelper;

namespace Regresion.Loader;

public static class CsvDataLoader
{
    public static (double[] ma30, double[] close) LoadWith(string filePath, int days, string nameX, string nameY)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Read();
        csv.ReadHeader();
        List<double> xList = new List<double>();
        List<double> yList = new List<double>();
        while (csv.Read())
        {
            double valX = csv.GetField<double>(nameX);
            double valY = csv.GetField<double>(nameY);

            xList.Add(valX);
            yList.Add(valY);
        }
        
        double[] X = xList.ToArray();
        double[] Y = yList.ToArray();

        return (X, Y);
    }
}

public class TheAxes
{
    public double AxeX { get; set; }
    public double AxeY { get; set; }
}