using Regresion.Algorithms;
using Regresion.Loader;
using ScottPlot;

int degree = 3;

Console.WriteLine("Name for X: ");
string inputX = Console.ReadLine();
Console.WriteLine("Name for Y: ");
string inputY = Console.ReadLine();

var (xArr, yArr) = CsvDataLoader.LoadWith("gold_futures_timeseries.csv", 150, inputX, inputY);

//scaling

double minX = xArr.Min();
double maxX = xArr.Max();
for (int i = 0; i < xArr.Length; i++)
{
    xArr[i] = ((xArr[i] - minX) / (maxX - minX)) * 10;
}

double[,] matrixX = BuildMatrix.Build_X_Matrix(xArr, degree);
int n = yArr.Length;
double[,] matrixY = new double[n, 1];
for (int i = 0; i < n; i++)
{
    matrixY[i, 0] = yArr[i];
}

double[,] X_T = MyTranspose.CallMyTranspose(matrixX);
double[,] X_T_X = MultiplyMatrix.Multi(X_T, matrixX);

double[,] inverse = Inverse.InvertMatrix(X_T_X);
if (inverse == null) return;

double[,] X_T_y = MultiplyMatrix.Multi(X_T, matrixY);
double[,] theta = MultiplyMatrix.Multi(inverse, X_T_y);

List<double> predictedCoeff = new List<double>();

for (int i = 0; i < theta.GetLength(0); i++)
{
    predictedCoeff.Add(theta[i,0]);
}

List<double> yPredictied = new List<double>();
foreach (double x in xArr)
{
    double predicatedValue = Evaluate.EvaluatePolynominal(x, predictedCoeff);
    yPredictied.Add(predicatedValue);
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



ScottPlot.Plot plt = new();

var realplot = plt.Add.Scatter(xArr, yArr);
realplot.LineWidth = 0;
realplot.MarkerSize = 5;
realplot.Color = new Color(255, 0, 0);
Array.Sort(xArr,ys_pred);
var predPlot = plt.Add.Scatter(xArr, ys_pred);
predPlot.MarkerSize = 0;
predPlot.LineWidth = 3;
predPlot.LineColor = new Color(0, 255, 255);

plt.Title("Results");
plt.XLabel("MA 30");
plt.YLabel("CLOSE");
plt.SavePng("samples.png",1920,1080);

