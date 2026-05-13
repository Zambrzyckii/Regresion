using System.Globalization;
using CsvHelper;

namespace Regresion.Loader;

public static class CsvDataLoader
{
    public static (double[] ma30, double[] close) LoadWith(string filePath, int days)
    {
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var records = csv.GetRecords<Gold>().TakeLast(days).ToList();

        double[] X = records.Select(r => r.ma_30).ToArray();
        double[] Y = records.Select(r => r.close).ToArray();

        return (X, Y);
    }
}

public class Gold
{
    public double ma_30 { get; set; }
    public double close { get; set; }
}