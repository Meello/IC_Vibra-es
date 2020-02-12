using IcVibrations.Core.Models;

namespace IcVibrations.Methods.AuxiliarOperations
{
    public class AuxiliarOperation : IAuxiliarOperation
    {
        public double[,] ApplyBondaryConditions(double[,] matrix, bool[] bondaryConditions, uint size)
        {
            int i, j, count1, count2;

            int n = matrix.GetLength(0);

            double[,] matrixBC = new double[size, size];

            count2 = 0;

            for (i = 0; i < n; i++)
            {
                count1 = 0;

                for (j = 0; j < n; j++)
                {
                    if (bondaryConditions[i] == true && bondaryConditions[j] == true)
                    {
                        matrixBC[count1, count2] = matrix[j, i];

                        count1 += 1;
                    }
                }

                if (bondaryConditions[i] == true)
                {
                    count2 += 1;
                }
            }

            return matrixBC;
        }

        public double[] ApplyBondaryConditions(double[] matrix, bool[] bondaryConditions, uint size)
        {
            int i, count1 = 0;

            int n = matrix.GetLength(0);

            double[] matrixCC = new double[size];

            for (i = 0; i < n; i++)
            {
                if (bondaryConditions[i] == true)
                {
                    matrixCC[count1] = matrix[i];
                    count1 += 1;
                }
            }

            return matrixCC;
        }

        public uint CalculateDegreesFreedomMaximum(uint numberOfElements)
        {
            return (numberOfElements + 1) * Constants.NodesPerElement;
        }

        //public void WriteInFile(string path, NewmarkMethodOutput output)
        //{
        //    StreamWriter streamWriter = new StreamWriter(path);

        //    try
        //    {
        //        using (StreamWriter sw = streamWriter)
        //        {
        //            sw.WriteLine(string.Format("{0},{1},{2},{3}", w, time, y, vel, acel, input.Force));

        //            sw.Close();
        //        }
        //    }
        //    catch
        //    {
        //        // Não quero que pare, só avise que deu erro.
        //        throw new Exception("Couldn't open file.");
        //    }
        //}
    }
}
