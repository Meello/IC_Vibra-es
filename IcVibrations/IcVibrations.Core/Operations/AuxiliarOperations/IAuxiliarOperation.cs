namespace IcVibrations.Methods.AuxiliarOperations
{
    public interface IAuxiliarOperation
    {
        double[,] AplyBondaryConditions(double[,] matrix, bool[] bondaryConditions, uint numberOfBondaryConditionsTrue);

        double[] AplyBondaryConditions(double[] matrix, bool[] bondaryConditions, uint numberOfBondaryConditionsTrue);

        uint CalculateDegreesFreedomMaximum(uint numberOfElements);

        //void WriteInFile(string path, NewmarkMethodOutput output);
    }
}