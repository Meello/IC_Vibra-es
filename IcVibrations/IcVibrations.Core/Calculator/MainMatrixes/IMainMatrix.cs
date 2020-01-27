using IcVibrations.Core.Models.Piezoelectric;
using IcVibrations.Models.Beam;
using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Calculator.MainMatrixes
{
    public interface IMainMatrix
    {
        double[,] CalculateElementMass(double area, double density, double elementLength);

        double[,] CalculateBeamElementHardness(double momentInertia, double youngModulus, double elementLength);

        double[,] CalculatePiezoelectricElementHardness(double momentInertia, double elasticityToConstantElectricField, double length);

        double[,] CalculatePiezoelectricElementElectromechanicalCoupling(Piezoelectric piezoelectric, double beamHeight);

        double[,] CalculatePiezoelectricElementCapacitance(double area, double length, double heigth, double dielectricConstantsToConstantStrain);

        double[,] CalculateMass(Beam beam, uint degreesFreedomMaximum);

        double[,] CalculateMass(Beam beam, Piezoelectric piezoelectric, uint degreesFreedomMaximum);

        double[,] CalculateHardness(Beam beam, uint degreesFreedomMaximum);

        double[,] CalculateHardness(Beam beam, Piezoelectric piezoelectric, uint degreesFreedomMaximum);

        double[,] CalculateDamping(double[,] mass, double[,] hardness, uint degreesFreedomMaximum);

        double[,] CalculatePiezoelectricElectromechanicalCoupling(double beamHeight, uint elementCount, Piezoelectric piezoelectric, uint degreesFreedomMaximum);

        double[,] CalculatePiezoelectricCapacitance(Piezoelectric piezoelectric, uint elementCount);

        double[,] CalculateEquivalentMass(double[,] mass, uint degreesFreedomMaximum, uint piezoelectricMatrixSize);

        double[,] CalculateEquivalentHardness(double[,] hardness, double[,] piezoelectricElectromechanicalCoupling, double[,] piezoelectricCapacitance, uint degreesFreedomMaximum, uint piezoelectricMatrixSize);

        bool[] CalculateBeamBondaryCondition(Fastening firstFastening, Fastening lastFastening, uint degreesFreedomMaximum);
    }
}
