namespace Regresion.Outputs;

public static class OutputGraph
{
    public static void CreateResultGraph(double[] yArray, double[] yPredictedValues)
    {
        ScottPlot.Plot plt = new();

        var realValues = plt.Add.Scatter(yArray, yPredictedValues);
        realValues.LineWidth = 0;
        realValues.MarkerSize = 5;

        double minVal = yArray.Min();
        double maxVal = yArray.Max();
        var perfectLine = plt.Add.Line(minVal, minVal, maxVal, maxVal);
        perfectLine.Color = new ScottPlot.Color(255, 0, 0);
        perfectLine.LinePattern = ScottPlot.LinePattern.Dashed;

        plt.Title("Actual vs Predicted");
        plt.XLabel("Real price");
        plt.YLabel("Predicted price");
        plt.SavePng("samples.png", 1920, 1080);

        string outputFolder = Directory.Exists("/data") ? "/data" : ".";
        string imagePath = Path.Combine(outputFolder, "samples.png");
        plt.SavePng(imagePath, 1920, 1080);
        Console.WriteLine("\nResult graph -> samples.png");
    }
}