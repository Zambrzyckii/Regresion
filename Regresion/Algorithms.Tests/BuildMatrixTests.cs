using Xunit;
using Regresion.Algorithms;

namespace Regresion.Tests;

public class BuildMatrixTests
{
    [Fact]
    public void BuildXMatrix_ShouldGeneratePowersUpToDegree()
    {
        double[] input = {2.0, 3.0};
        int degree = 3;

        double[,] result = BuildMatrix.Build_X_Matrix(input, degree);

        Assert.Equal(2, result.GetLength(0));
        Assert.Equal(4, result.GetLength(1));
        Assert.Equal(1.0, result[0, 0]);
        Assert.Equal(2.0, result[0, 1]);
        Assert.Equal(4.0, result[0, 2]);
        Assert.Equal(8.0, result[0, 3]);
        Assert.Equal(1.0, result[1, 0]);
        Assert.Equal(27.0, result[1, 3]);
    }

    [Fact]
    public void BuildMultipleXMatrix_ShouldPrependSoloColumn()
    {
        double[,] input ={{50.0, 5.0}, {80.0, 10.0}};

        double[,] result = BuildMatrix.Build_Multiple_X_Matrix(input);

        Assert.Equal(2, result.GetLength(0));
        Assert.Equal(3, result.GetLength(1));
        Assert.Equal(1.0, result[0, 0]);
        Assert.Equal(50.0, result[0, 1]);
        Assert.Equal(5.0, result[0, 2]);
        Assert.Equal(1.0, result[1, 0]);
        Assert.Equal(10.0, result[1, 2]);
    }

    [Fact]
    public void Build_MultipleXMatrix_WithEmptyArray_ShouldReturnEmptyWithSoloColumn()
    {
        double[,] emptyInput = new double[0, 5];

        double[,] result = BuildMatrix.Build_Multiple_X_Matrix(emptyInput);

        Assert.Equal(0, result.GetLength(0));
        Assert.Equal(6, result.GetLength(1));
    }
}