namespace Regresion.Algorithms;

public static class MyTranspose
{
    public static double[,] CallMyTranspose(double[,] matrix)
    {
        int rows = matrix.GetLength(0);
        int cols = matrix.GetLength(1);
        double[,] result = new double[cols,rows];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                result[j,i] = matrix[i,j];
            }
        }

        return result;
    }
}