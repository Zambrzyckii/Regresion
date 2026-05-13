using System.Diagnostics;
using CommandLine;
using Regresion.Algorithms;
using Regresion.Inputs;
using Regresion.Loader;
using ScottPlot;
using Spectre.Console;

string? filePath = string.Empty;
IEnumerable<string?> AxisX = new List<string?>();
string? AxisY = string.Empty;
int degree = 6;

Parser.Default.ParseArguments<InputOptions>(args).WithParsed<InputOptions>(options =>
{
    filePath = options.csvFilePath;
    AxisX = options.axisX;
    AxisY = options.axisY;
});

if (string.IsNullOrEmpty(filePath) || !AxisX.Any() || string.IsNullOrEmpty(AxisY)) return;

var (xArr, yArr) = CsvDataLoader.LoadWith(filePath, 50, AxisX, AxisY);

//scaling


double[,] matrixX = BuildMatrix.Build_Multiple_X_Matrix(xArr);
int n = yArr.Length;
double[,] matrixY = new double[n, 1];
for (int i = 0; i < n; i++)
{
    matrixY[i, 0] = yArr[i];
}

double[,] X_T = MyTranspose.CallMyTranspose(matrixX);
double[,] X_T_X = MultiplyMatrix.Multi(X_T, matrixX);

double[,]? inverse = Inverse.InvertMatrix(X_T_X);
if (inverse == null) return;

double[,] X_T_y = MultiplyMatrix.Multi(X_T, matrixY);
double[,] theta = MultiplyMatrix.Multi(inverse, X_T_y);

List<double> predictedCoeff = new List<double>();

for (int i = 0; i < theta.GetLength(0); i++)
{
    predictedCoeff.Add(theta[i,0]);
}

List<double> yPredictied = new List<double>();
int rowsCount = xArr.GetLength(0);
int colsCount = xArr.GetLength(1);

for (int i = 0; i < rowsCount; i++)
{
    double[] rowFeatues = new double[colsCount];
    for (int j = 0; j < colsCount; j++)
    {
        rowFeatues[j] = xArr[i, j];
    }

    double pred = Evaluate.EvaluateMultiple(rowFeatues, predictedCoeff);
    yPredictied.Add(pred);
}

double mse = 0;
for (int i = 0; i < n; i++)
{
    double error = yArr[i] - yPredictied[i];
    mse += error * error;
}

mse /= n;

Console.WriteLine($"Predicted coefficients: {string.Join(", ", predictedCoeff)}");
Console.WriteLine($"MSE: {mse}");

double[] ys_pred = yPredictied.ToArray();

var table = new Table();
table.AddColumn("ID");
table.AddColumn(new TableColumn("Wartość oczekiwana").Centered());
table.AddColumn(new TableColumn("Predykcja").Centered());
table.AddColumn("status");
for (int i = 0; i < ys_pred.Length; i++)
{
    double expected = yArr[i];
    double predicted = ys_pred[i];
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
ScottPlot.Plot plt = new();

var realplot = plt.Add.Scatter(yArr, ys_pred);
realplot.LineWidth = 0;
realplot.MarkerSize = 5;

double minVal = yArr.Min();
double maxVal = yArr.Max();
var perfectLine = plt.Add.Line(minVal, minVal, maxVal, maxVal);
perfectLine.Color = new ScottPlot.Color(255, 0, 0);
perfectLine.LinePattern = ScottPlot.LinePattern.Dashed;

plt.Title("Actual vs Predicted");
plt.XLabel("Real price");
plt.YLabel("Predicted price");
plt.SavePng("samples.png",1920,1080);

Process.Start(new ProcessStartInfo
{
    FileName = "samples.png",
    UseShellExecute = true 
});

