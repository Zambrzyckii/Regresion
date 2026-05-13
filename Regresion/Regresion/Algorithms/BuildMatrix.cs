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
    
    public static double[,] Build_Multiple_X_Matrix(double[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        double[,] result = new double[rows,  cols + 1];

        for(int i = 0; i < rows; i++)
        {
            result[i, 0] = 1.0;
            for (int j = 0; j < cols; j++)
            {
                result[i, j + 1] = matrix[i, j];
            }
        }

        return result;
    }
}