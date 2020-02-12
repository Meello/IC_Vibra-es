using IcVibrations.Common.Profiles;
using IcVibrations.Core.Models.Beam;
using IcVibrations.Core.Models.Piezoelectric;
using IcVibrations.Models.Beam.Characteristics;
using System.Threading.Tasks;

namespace IcVibrations.Calculator.MainMatrixes
{
    /// <summary>
    /// It's responsible to build the main matrixes in the analysis.
    /// </summary>
    public interface ICommonMainMatrix
    {
        Task<double[,]> CalculateElementMass(double area, double specificMass, double elementLength);

        Task<double[,]> CalculateDamping(double[,] mass, double[,] hardness, uint degreesFreedomMaximum);
        
        Task<bool[]> CalculateBeamBondaryCondition(Fastening firstFastening, Fastening lastFastening, uint degreesFreedomMaximum);
    }
}
