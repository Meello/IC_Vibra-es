namespace IcVibrations.Methods.AuxiliarOperations
{
    public interface IAuxiliarOperation
    {
        double[,] AplyBondaryConditions(double[,] matrix, bool[] bondaryConditions, int trueBondaryConditionCount);

        double[] AplyBondaryConditions(double[] matrix, bool[] bondaryConditions, int trueBondaryConditionCount);

        uint CalculateDegreesFreedomMaximum(uint numberOfElements);

        //void WriteInFile(string path, NewmarkMethodOutput output);
    }
}