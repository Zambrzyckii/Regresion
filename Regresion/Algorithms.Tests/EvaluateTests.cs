using Xunit;
using Regresion.Algorithms;
using System.Collections.Generic;

namespace Regresion.Tests;

public class EvaluateTests
{
    [Fact]
    public void EvaluatePolynominal_ShouldCalculateCorrectValue_ForX()
    {
        double x = 2.0;
        List<double> coefficients = new List<double> { 3.0, 4.0, 5.0 };

        double result = Evaluate.EvaluatePolynominal(x, coefficients);

        Assert.Equal(31.0, result);
    }

    [Fact]
    public void EvaluatePolynominal_WithZeroCoefficients_ShouldReturnZero()
    {
        double x = 5.0;
        List<double> coefficients = new List<double> { 0.0, 0.0, 0.0 };

        double result = Evaluate.EvaluatePolynominal(x, coefficients);

        Assert.Equal(0.0, result);
    }

    [Fact]
    public void EvaluateMultiple_ShouldCalculateCorrectPrediction_SoloValue()
    {
        double[] rowFeatures = { 2.0, 3.0 };
        List<double> theta = new List<double> { 1.5, 2.0, 4.0 };

        double result = Evaluate.EvaluateMultiple(rowFeatures, theta);

        Assert.Equal(17.5, result);
    }

    [Fact]
    public void EvaluateMultiple_WithZerFeatures_ReturnOnlySoloValue()
    {
        double[] rowFeatures = new double[0];
        List<double> theta = new List<double> { 5.0 };

        double result = Evaluate.EvaluateMultiple(rowFeatures, theta);

        Assert.Equal(5.0, result);
    }
}