namespace IcVibrations.Core.Calculator.ArrayOperations
{
    public interface IArrayOperation
    {
        double[,] InverseMatrix(double[,] matrix, string matrixName);

        double[,] InverseMatrix(double[,] matrix, int size, string matrixName);

        double[,] Multiply(double[,] matrix1, double[,] matrix2, string matrixesName);

        double[] Multiply(double[,] matrix, double[] array, string arraysName);

        double[] Multiply(double[] array, double[,] matrix, string arraysName);

        double[,] Subtract(double[,] matrix1, double[,] matrix2, string matrixesName);
        
        double[] Subtract(double[] array1, double[] array2, string arraysName);

        double[] Subtract(double[] array1, double[] array2, double[] array3, string arraysName);

        double[,] Sum(double[,] matrix1, double[,] matrix2, string matrixesName);

        double[] Sum(double[] array1, double[] array2, double[] array3, string matrixesName);

        double[] Sum(double[] array1, double[] array2, string arraysName);
        
        double[] Create(double value, uint size);

        double[] Create(double value, uint size, uint[] positions, string arrayName);

        double[,] TransposeMatrix(double[,] matrix);

        double[,] AddValue(double[,] matrixToAdd, double[] values, uint[] valueNodePositions, string matrixName);
    }
}
