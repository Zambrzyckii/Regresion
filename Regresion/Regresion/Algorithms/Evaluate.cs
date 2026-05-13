namespace Regresion.Algorithms;

public static class Evaluate
{
    public static double EvaluatePolynominal(double x, List<double> coefficients)
    {
        double result = 0, current_power = 1.0;

        foreach (var coeff in coefficients)
        {
            result += coeff *  current_power;
            current_power *= x;
        }
        
        return result;
    }

    public static double EvaluateMultiple(double[] rowFeatures, List<double> theta)
    {
        double result = theta[0];
        for (int i = 0; i < rowFeatures.Length; i++)
        {
            result += theta[i + 1] * rowFeatures[i];
        }

        return result;
    }
}