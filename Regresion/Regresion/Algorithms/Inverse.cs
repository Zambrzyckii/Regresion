namespace Regresion.Algorithms;

public static class Inverse
{
    public static double[,]? InvertMatrix(double[,] matrix)
    {
        int n = matrix.GetLength(0);
        double[,] augmented = new double[n, n * 2];

        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                augmented[i, j] = matrix[i, j];
                augmented[i, j + n] = (i == j) ? 1 : 0;
            }
        }

        for (int i = 0; i < n; i++)
        {
            int pivot = i;
            for (int j = i + 1; j < n; j++)
            {
                if (Math.Abs(augmented[j, i]) > Math.Abs(augmented[pivot, i]))
                {
                    pivot = j;
                }
            }

            if (pivot != i)
            {
                for (int k = 0; k < 2 * n; k++)
                {
                    double temp = augmented[i, k];
                    augmented[i, k] = augmented[pivot, k];
                    augmented[pivot, k] = temp;
                }
            }

            if (Math.Abs(augmented[i, i]) < 1e-10)
            {
                return null;
            }

            double p = augmented[i, i];
            for (int j = 0; j < 2 * n; j++)
            {
                augmented[i,j] /= p;
            }

            for (int j = 0; j < n; j++)
            {
                if (j != i)
                {
                    double factor = augmented[j, i];
                    for (int k = 0; k < 2 * n; k++)
                    {
                        augmented[j, k] -= factor * augmented[i, k];
                    }
                }
            }
        }

        double[,] result = new double[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                result[i, j] = augmented[i, j + n];
            }
        }

        return result;
    }
}