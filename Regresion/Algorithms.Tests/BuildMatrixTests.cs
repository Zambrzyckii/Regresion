namespace Algorithms.Tests;
using Regresion.Algorithms;
public class BuildMatrixTests
{
    [Fact]
    public void BuildXMatrix_ProperCalculations_True()
    {
        double[] testTemp = new double[] { 1, 2, 3, 4, 5, 6, 7 };
        var result = BuildMatrix.Build_X_Matrix(testTemp, 10);
        var expectedResult = new double[]
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 2, 4, 8, 16, 32, 64, 128, 256, 512,
            1, 3, 9, 27, 81, 243, 729, 2187, 6561, 19683,
            1, 4, 16, 64, 256, 1024, 4096, 16384, 65536, 262144,
            1, 5, 25, 125, 625, 3125, 15625, 78125, 390625, 1953125,
            1, 6, 36, 216, 1296, 7776, 46656, 279936, 1679616, 10077696,
            1, 7, 49, 343, 2401, 16807, 117649, 823543, 5764801, 40353607
        };
        //Assert.Equal(expectedResult, result);
    }
    
    [Fact]
    public void BuildXMatrix_InProperCalculations_False()
    {
        double[] testTemp = new double[] { 1, 2, 3, 4, 5, 6, 7 };
        var result = BuildMatrix.Build_X_Matrix(testTemp, 10);
        var expectedResult = new double[] 
        { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
            1, 2, 4, 8, 16, 32, 64, 128, 256, 512,
            1, 3, 9, 27, 81, 243, 729, 2187, 6561, 19683,
            1, 4, 16, 64, 256, 1024, 4096, 16384, 65536, 262144,
            1, 5, 25, 125, 625, 3125, 15625, 78125, 390625, 1953125,
            1, 6, 36, 216, 1296, 7776, 46656, 279936, 1679616, 10077696,
            2, 7, 49, 343, 2401, 16807, 117649, 823543, 5764801, 40353607
        };
        //Assert.NotEqual(expectedResult, result);
    }
}