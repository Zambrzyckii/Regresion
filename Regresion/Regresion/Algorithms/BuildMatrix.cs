namespace Regresion.Algorithms;

public static class BuildMatrix
{
    public static double[,] Build_X_Matrix(double[] matrix, int degree)
    {
        int rows = matrix.GetLength(0);
        double[,] result = new double[rows, degree + 1];

        for(int j = 0; j < rows; j++)
        {
            double power = 1;
            for (int i = 0; i < degree + 1; i++)
            {
                result[j, i] = power;
                power *= matrix[j];
            }
        }

        return result;
    }
}