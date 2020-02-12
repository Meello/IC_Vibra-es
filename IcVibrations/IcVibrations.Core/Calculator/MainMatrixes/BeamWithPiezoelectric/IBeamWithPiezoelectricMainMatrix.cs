using IcVibrations.Common.Profiles;
using IcVibrations.Core.Models.Piezoelectric;
using System.Threading.Tasks;

namespace IcVibrations.Core.Calculator.MainMatrixes.BeamWithPiezoelectric
{
    /// <summary>
    /// It's responsible to calculate the beam with piezoelectric main matrixes.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public interface IBeamWithPiezoelectricMainMatrix<TProfile>
        where TProfile : Profile, new()
    {
        /// <summary>
        /// It's responsible to calculate piezoelectric mass matrix.
        /// </summary>
        /// <param name="momentInertia"></param>
        /// <param name="elasticityToConstantElectricField"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        Task<double[,]> CalculateMass(BeamWithPiezoelectric<TProfile> piezoelectric, uint degreesFreedomMaximum);

        /// <summary>
        /// It's responsible to calculate piezoelectric hardness matrix.
        /// </summary>
        /// <param name="momentInertia"></param>
        /// <param name="elasticityToConstantElectricField"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        Task<double[,]> CalculateHardness(BeamWithPiezoelectric<TProfile> beamWithPiezoelectric, uint degreesFreedomMaximum);
        
        /// <summary>
        /// It's responsible to calculate piezoelectric element hardness matrix.
        /// </summary>
        /// <param name="momentInertia"></param>
        /// <param name="elasticityToConstantElectricField"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        Task<double[,]> CalculatePiezoelectricElementHardness(double momentInertia, double elasticityToConstantElectricField, double length);

        /// <summary>
        /// It's responsible to calculate piezoelectric electromechanical coupling matrix.
        /// </summary>
        /// <param name="momentInertia"></param>
        /// <param name="elasticityToConstantElectricField"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        Task<double[,]> CalculatePiezoelectricElectromechanicalCoupling(BeamWithPiezoelectric<TProfile> beamWithPiezoelectric, uint degreesFreedomMaximum);

        /// <summary>
        /// It's responsible to calculate piezoelectric element electromechanical coupling matrix.
        /// </summary>
        /// <param name="momentInertia"></param>
        /// <param name="elasticityToConstantElectricField"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        Task<double[,]> CalculatePiezoelectricElementElectromechanicalCoupling(BeamWithPiezoelectric<TProfile> piezoelectric);

        /// <summary>
        /// It's responsible to calculate piezoelectric capacitance matrix.
        /// </summary>
        /// <param name="momentInertia"></param>
        /// <param name="elasticityToConstantElectricField"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        Task<double[,]> CalculatePiezoelectricCapacitance(BeamWithPiezoelectric<TProfile> beamWithPiezoelectric, uint elementCount);

        /// <summary>
        /// It's responsible to calculate element piezoelectric capacitance matrix.
        /// </summary>
        /// <param name="momentInertia"></param>
        /// <param name="elasticityToConstantElectricField"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        Task<double[,]> CalculateElementPiezoelectricCapacitance(BeamWithPiezoelectric<TProfile> piezoelectric);

        /// <summary>
        /// It's responsible to calculate equivalent mass matrix.
        /// </summary>
        /// <param name="momentInertia"></param>
        /// <param name="elasticityToConstantElectricField"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        Task<double[,]> CalculateEquivalentMass(double[,] mass, uint degreesFreedomMaximum, uint piezoelectricDegreesFreedomMaximum);

        /// <summary>
        /// It's responsible to calculate equivalent hardness matrix.
        /// </summary>
        /// <param name="momentInertia"></param>
        /// <param name="elasticityToConstantElectricField"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        Task<double[,]> CalculateEquivalentHardness(double[,] hardness, double[,] piezoelectricElectromechanicalCoupling, double[,] piezoelectricCapacitance, uint degreesFreedomMaximum, uint piezoelectricDegreesFreedomMaximum);
    }
}
