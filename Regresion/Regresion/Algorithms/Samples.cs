namespace Regresion.Algorithms;

public static class Samples
{
    public static SamplesResults GenerateSamples(List<double> coefficients, double number_of_points, double step)
    {
        SamplesResults results = new SamplesResults();

        double currentX = -number_of_points/step;

        for (int i = 0; i < number_of_points; i++)
        {
            double temp = Evaluate.EvaluatePolynominal(currentX, coefficients);
            results.X.Add(currentX);
            results.Y.Add(temp);
            currentX += step;
        }
        
        return results;
    }
}