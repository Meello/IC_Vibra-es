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

        double[,] CalculatePiezoelectricElectromechanicalCoupling(double piezoelectricWidth, double piezoelectricHeight, double beamHeight, double piezoelectricLength, double piezoelectricStrain);

        double[,] CalculatePiezoelectricCapacitance(double area, double length, double heigth, double dielectricConstantsToConstantStrain);

        bool[] CalculateBeamBondaryCondition(Fastening fixacao1, Fastening fixacaoN, int degreesFreedomMaximum);
    }
}
