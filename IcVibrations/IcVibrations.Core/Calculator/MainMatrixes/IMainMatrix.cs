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

        double[,] CalculatePiezoelectricElementElectromechanicalCoupling(RectangularPiezoelectric piezoelectric, double beamHeight);

        double[,] CalculatePiezoelectricElementCapacitance(double area, double length, double heigth, double dielectricConstantsToConstantStrain);

        double[,] CalculateMass(Beam beam, uint degreesFreedomMaximum);

        double[,] CalculateMass(Beam beam, Piezoelectric piezoelectric, uint degreesFreedomMaximum);

        double[,] CalculateMassWithDva(double[,] beamMass, double[] dvaMasses, double[] dvaNodePositions);

        double[,] CalculateBeamHardness(Beam beam, uint degreesFreedomMaximum);

        double[,] CalculateBeamHardnessWithDva(double[,] beamHardness, double[] dvaHardness, double[] dvaNodePositions);

        double[,] CalculateHardness(Beam beam, Piezoelectric piezoelectric, uint degreesFreedomMaximum);

        double[,] CalculateDamping(double[,] mass, double[,] hardness, uint degreesFreedomMaximum);

        double[,] CalculatePiezoelectricElectromechanicalCoupling(double beamHeight, uint elementCount, RectangularPiezoelectric piezoelectric, uint degreesFreedomMaximum);

        double[,] CalculatePiezoelectricCapacitance(RectangularPiezoelectric piezoelectric, uint elementCount);

        double[,] CalculateEquivalentMass(double[,] mass, uint degreesFreedomMaximum, uint piezoelectricDegreesFreedomMaximum);

        double[,] CalculateEquivalentHardness(double[,] hardness, double[,] piezoelectricElectromechanicalCoupling, double[,] piezoelectricCapacitance, uint degreesFreedomMaximum, uint piezoelectricDegreesFreedomMaximum);

        bool[] CalculateBeamBondaryCondition(Fastening firstFastening, Fastening lastFastening, uint degreesFreedomMaximum);
    }
}
