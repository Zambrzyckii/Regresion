using Xunit;
using Regresion.Algorithms;

namespace Regresion.Tests;

public class MultiplyMatrixTests
{
    [Fact]
    public void Multi_Should_Multiply_Two_Square_Matrices()
    {
        double[,] X = {{1.0, 2.0}, {3.0, 4.0}};
        double[,] Y = {{5.0,6.0}, {7.0, 8.0}};

        double[,] result = MultiplyMatrix.Multi(X, Y);

        Assert.Equal(2, result.GetLength(0));
        Assert.Equal(2, result.GetLength(1));
        Assert.Equal(19.0, result[0, 0]);
        Assert.Equal(22.0, result[0, 1]);
        Assert.Equal(43.0, result[1, 0]);
        Assert.Equal(50.0, result[1, 1]);
    }

    [Fact]
    public void Multi_Should_Multiply_Rectangular_Matrices()
    {
        double[,] X = {{ 1.0, 2.0, 3.0}, {4.0, 5.0, 6.0} };
        double[,] Y = {{7.0, 8.0},{9.0, 10.0},{11.0, 12.0}};
        double[,] result = MultiplyMatrix.Multi(X, Y);

        Assert.Equal(2, result.GetLength(0));
        Assert.Equal(2, result.GetLength(1));
        Assert.Equal(58.0, result[0, 0]);
        Assert.Equal(64.0, result[0, 1]);
        Assert.Equal(139.0, result[1, 0]);
        Assert.Equal(154.0, result[1, 1]);
    }

    [Fact]
    public void Multi_Should_Multiply_Matrix_By_Column_Vector()
    {
        double[,] X = {{1.0, 2.0}, {3.0, 4.0},  {5.0, 6.0}};
        double[,] Y = {{7.0}, {8.0}};

        double[,] result = MultiplyMatrix.Multi(X, Y);

        Assert.Equal(3, result.GetLength(0));
        Assert.Equal(1, result.GetLength(1));
        Assert.Equal(23.0, result[0, 0]);
        Assert.Equal(53.0, result[1, 0]);
        Assert.Equal(83.0, result[2, 0]);
    }

    [Fact]
    public void Multi_With_Zero_Matrix_Should_Return_Zero_Matrix()
    {
        double[,] X = {{1.0, 2.0}, {3.0, 4.0}};
        double[,] Y = {{0.0, 0.0},{0.0, 0.0}};

        double[,] result = MultiplyMatrix.Multi(X, Y);

        Assert.Equal(2, result.GetLength(0));
        Assert.Equal(2, result.GetLength(1));
        Assert.Equal(0.0, result[0, 0]);
        Assert.Equal(0.0, result[0, 1]);
        Assert.Equal(0.0, result[1, 0]);
        Assert.Equal(0.0, result[1, 1]);
    }
}