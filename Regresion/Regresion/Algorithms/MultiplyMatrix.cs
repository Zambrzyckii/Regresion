namespace Regresion.Algorithms;

public static class MultiplyMatrix
{
    public static double[,] Multi(double[,] X, double[,] Y)
    {
        int rowsA =  X.GetLength(0);
        int rowsB = Y.GetLength(1);
        int colsA = X.GetLength(1);
        double[,] result = new double[rowsA, rowsB];

        for (int i = 0; i < rowsA; i++)
        {
            for (int j = 0; j < rowsB; j++)
            {
                double sum = 0;

                for (int k = 0; k < colsA; k++)
                {
                    sum += X[i, k] * Y[k, j];
                }
                result[i, j] = sum;
            }
        }
        
        return result;
    }
}