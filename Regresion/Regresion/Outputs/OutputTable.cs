using Spectre.Console;

namespace Regresion.Outputs;

public static class OutputTable
{
    public static void CreateOutputTable(double[] yArray, double[] yPredictedValues)
    {
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn(new TableColumn("Expected value").Centered());
        table.AddColumn(new TableColumn("Prediction").Centered());
        table.AddColumn("Status");
        for (int i = 0; i < yPredictedValues.Length; i++)
        {
            double expected = yArray[i];
            double predicted = yPredictedValues[i];
            double difference = Math.Abs(predicted - expected);

            string status = difference switch
            {
                > 100 => "[red bold] Anomaly [/]",
                > 50 => "[blue] Not Bad [/]",
                _ => "[green] Success [/]"
            };

            table.AddRow(
                i.ToString(),
                expected.ToString("F2"),
                predicted.ToString("F2"),
                status
            );
        }

        AnsiConsole.Write(table);
    }
}