using IcVibrations.Models.Beam;
using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Calculator.MainMatrixes
{
    public interface IMainMatrix
    {
        double[,] CalculateBeamMass(Beam beam, int degreesFreedomMaximum);

        double[,] CalculateBeamHardness(Beam beam, int degreesFreedomMaximum);

        double[,] CalculateBeamDamping(double[,] mass, double[,] hardness, int degreesFreedomMaximum);

        double[,] CalculateBeamMassElement(double area, double density, double elementLength);

        double[,] CalculateBeamHardnessElement(double momentInertia, double youngModulus, double elementLength);

        double[,] CalculatePiezoelectricElementMass(double area, double density, double elementLength);

        double[,] CalculatePiezoelectricElementHardness(double elasticityToConstantElectricField, double MomentInertia, double length);

        double[,] CalculatePiezoelectricElectromechanicalCoupling(double v1, double v2, double v3, double v4, double v5);

        double[,] CalculatePiezoelectricPiezoelectricCapacitance();

        bool[] CalculateBeamBondaryCondition(Fastening fixacao1, Fastening fixacaoN, int degreesFreedomMaximum);
    }
}
