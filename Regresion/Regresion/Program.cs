using System.Diagnostics;
using CommandLine;
using Regresion.Algorithms;
using Regresion.Inputs;
using Regresion.Loader;
using Regresion.Outputs;

string? filePath = string.Empty;
IEnumerable<string?>? AxisX = new List<string?>();
string? AxisY = string.Empty;
string encodedDataCsv = "encoded_data.csv";
string pythonScriptName = "encode.py";
string localPythonScriptPath = $"../Encoding/{pythonScriptName}";
string dockerPythonScriptPath = pythonScriptName;
string finalPythonScriptPath = File.Exists(localPythonScriptPath) ? localPythonScriptPath : dockerPythonScriptPath;

Parser.Default.ParseArguments<InputOptions>(args).WithParsed<InputOptions>(options =>
{
    filePath = options.csvFilePath;
    AxisX = options.axisX;
    AxisY = options.axisY;
});

if (string.IsNullOrEmpty(filePath) || !AxisX.Any() || string.IsNullOrEmpty(AxisY)) return;

var processInfo = new ProcessStartInfo
{
    FileName = "python3",
    Arguments = $"{finalPythonScriptPath} {filePath} {encodedDataCsv}",
    UseShellExecute = false
};

using (var process = Process.Start(processInfo))
{
    process!.WaitForExit();
    if (process.ExitCode != 0)
    {
        Console.WriteLine("Something gone wrong with python parsing");
        return;
    }
}

var (xArr, yArr) = CsvDataLoader.LoadWith(encodedDataCsv, 50, AxisX!, AxisY);

double[,] matrixX = BuildMatrix.Build_Multiple_X_Matrix(xArr);
double[,] matrixY = new double[yArr.Length, 1];
FillMatrixY(ref matrixY, yArr);

double[,] X_T = MyTranspose.CallMyTranspose(matrixX);
double[,] X_T_X = MultiplyMatrix.Multi(X_T, matrixX);

double[,]? inverse = Inverse.InvertMatrix(X_T_X);
if (inverse == null) return;

double[,] X_T_y = MultiplyMatrix.Multi(X_T, matrixY);
double[,] theta = MultiplyMatrix.Multi(inverse, X_T_y);

List<double> predictedCoeff = new List<double>();

for (int i = 0; i < theta.GetLength(0); i++)
{
    predictedCoeff.Add(theta[i, 0]);
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
for (int i = 0; i < yArr.Length; i++)
{
    double error = yArr[i] - yPredictied[i];
    mse += error * error;
}

mse /= yArr.Length;

Console.WriteLine($"Predicted coefficients: {string.Join(", ", predictedCoeff)}");
Console.WriteLine($"MSE: {mse}");

double[] ys_pred = yPredictied.ToArray();

OutputTable.CreateOutputTable(yArr, ys_pred);
OutputGraph.CreateResultGraph(yArr, ys_pred);


void FillMatrixY(ref double[,] matrixY, double[] yArr)
{
    int n = yArr.Length;
    for (int i = 0; i < yArr.Length; i++)
    {
        matrixY[i, 0] = yArr[i];
    }
}