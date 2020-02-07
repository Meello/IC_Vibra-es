using IcVibrations.Core.Models.Piezoelectric;
using IcVibrations.Models.Beam;
using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IcVibrations.Calculator.MainMatrixes
{
    public interface IMainMatrix
    {
        Task<double[,]> CalculateElementMass(double area, double density, double elementLength);

        Task<double[,]> CalculateBeamElementHardness(double momentInertia, double youngModulus, double elementLength);

        Task<double[,]> CalculatePiezoelectricElementHardness(double momentInertia, double elasticityToConstantElectricField, double length);

        Task<double[,]> CalculatePiezoelectricElementElectromechanicalCoupling(RectangularPiezoelectric piezoelectric, double beamHeight);

        Task<double[,]> CalculatePiezoelectricElementCapacitance(double area, double length, double heigth, double dielectricConstantsToConstantStrain);

        Task<double[,]> CalculateMass(Beam beam, uint degreesFreedomMaximum);

        Task<double[,]> CalculateMass(Beam beam, Piezoelectric piezoelectric, uint degreesFreedomMaximum);

        Task<double[,]> CalculateMassWithDva(double[,] beamMass, double[] dvaMasses, uint[] dvaNodePositions);

        Task<double[,]> CalculateBeamHardness(Beam beam, uint degreesFreedomMaximum);

        Task<double[,]> CalculateBeamHardnessWithDva(double[,] beamHardness, double[] dvaHardness, uint[] dvaNodePositions);

        Task<double[,]> CalculateHardness(Beam beam, Piezoelectric piezoelectric, uint degreesFreedomMaximum);

        Task<double[,]> CalculateDamping(double[,] mass, double[,] hardness, uint degreesFreedomMaximum);
        
        Task<double[,]> CalculatePiezoelectricElectromechanicalCoupling(double beamHeight, uint elementCount, RectangularPiezoelectric piezoelectric, uint degreesFreedomMaximum);
        
        Task<double[,]> CalculatePiezoelectricCapacitance(RectangularPiezoelectric piezoelectric, uint elementCount);
        
        Task<double[,]> CalculateEquivalentMass(double[,] mass, uint degreesFreedomMaximum, uint piezoelectricDegreesFreedomMaximum);
        
        Task<double[,]> CalculateEquivalentHardness(double[,] hardness, double[,] piezoelectricElectromechanicalCoupling, double[,] piezoelectricCapacitance, uint degreesFreedomMaximum, uint piezoelectricDegreesFreedomMaximum);

        Task<bool[]> CalculateBeamBondaryCondition(Fastening firstFastening, Fastening lastFastening, uint degreesFreedomMaximum);
    }
}
