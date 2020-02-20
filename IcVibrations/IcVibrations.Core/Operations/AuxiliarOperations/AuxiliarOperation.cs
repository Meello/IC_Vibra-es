using IcVibrations.Common.Classes;
using IcVibrations.Core.Models;
using System;
using System.IO;

namespace IcVibrations.Methods.AuxiliarOperations
{
    public class AuxiliarOperation : IAuxiliarOperation
    {
        public double[,] ApplyBondaryConditions(double[,] matrix, bool[] bondaryConditions, uint size)
        {
            int i, j, count1, count2;

            int n = matrix.GetLength(0);

            double[,] matrixBC = new double[size, size];
            
            count1 = 0;
            
            for (i = 0; i < n; i++)
            {
                count2 = 0;
                
                if (bondaryConditions[i] == true)
                {
                    for (j = 0; j < n; j++)
                    {
                        if (bondaryConditions[j] == true)
                        {
                            matrixBC[count1, count2] = matrix[i, j];

                            count2 += 1;
                        }
                    }

                    count1 += 1;
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

        //public void WriteInFile(string path, Result result)
        //{
        //    StreamWriter streamWriter = new StreamWriter(path, true);

        //    try
        //    {
        //        using (StreamWriter sw = streamWriter)
        //        {
        //            sw.Write(sw.NewLine);

        //            sw.Write(string.Format("{0}, ", result.Time));

        //            for (int i = 0; i < result.Displacements.Length; i++)
        //            {
        //                sw.Write(string.Format("{0}, ", result.Displacements[i]));
        //            }

        //            for (int i = 0; i < result.Velocities.Length; i++)
        //            {
        //                sw.Write(string.Format("{0}, ", result.Velocities[i]));
        //            }

        //            for (int i = 0; i < result.Accelerations.Length; i++)
        //            {
        //                sw.Write(string.Format("{0}, ", result.Accelerations[i]));
        //            }

        //            for (int i = 0; i < result.Forces.Length; i++)
        //            {
        //                sw.Write(string.Format("{0}, ", result.Forces[i]));
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        throw new Exception("Couldn't open file.");
        //    }
        //}

        public void WriteInFile(string path, string message)
        {
            StreamWriter streamWriter = new StreamWriter(path);

            try
            {
                using (StreamWriter sw = streamWriter)
                {
                    sw.WriteLine(message);

                    sw.Close();
                }
            }
            catch
            {
                throw new Exception("Couldn't open file.");
            }
        }
    }
}