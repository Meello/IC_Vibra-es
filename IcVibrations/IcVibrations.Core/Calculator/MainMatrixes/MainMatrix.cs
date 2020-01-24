using IcVibrations.Core.Models;
using IcVibrations.Models.Beam;
using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IcVibrations.Calculator.MainMatrixes
{
    public class MainMatrix : IMainMatrix
	{
		public double[,] CalculateMassElement(double area, double specificMass, double length)
		{
			double[,] massElement = new double[Constants.DegreesFreedomElement, Constants.DegreesFreedomElement];

			double constant = area * specificMass * length / 420;

			massElement[0, 0] = 156 * constant;
			massElement[0, 1] = 22 * length * constant;
			massElement[0, 2] = 54 * constant;
			massElement[0, 3] = -13 * length * constant;
			massElement[1, 0] = 22 * length * constant;
			massElement[1, 1] = 4 * length * length * constant;
			massElement[1, 2] = 13 * length * constant;
			massElement[1, 3] = -3 * length * length * constant;
			massElement[2, 0] = 54 * constant;
			massElement[2, 1] = 13 * length * constant;
			massElement[2, 2] = 156 * constant;
			massElement[2, 3] = -22 * length * constant;
			massElement[3, 0] = -13 * length * constant;
			massElement[3, 1] = -3 * length * length * constant;
			massElement[3, 2] = -22 * length * constant;
			massElement[3, 3] = 4 * length * length * constant;

			return massElement;
		}

		//public double[,] CalculatePiezoelectricElementMass(double area, double specificMass, double length)
		//{
		//	double[,] massElement = new double[Constants.DegreesFreedomElement, Constants.DegreesFreedomElement];

		//	double constant = area * specificMass * length / 420;

		//	massElement[0, 0] = 156 * constant;
		//	massElement[0, 1] = 22 * length * constant;
		//	massElement[0, 2] = 54 * constant;
		//	massElement[0, 3] = -13 * length * constant;
		//	massElement[1, 0] = 22 * length * constant;
		//	massElement[1, 1] = 4 * length * length * constant;
		//	massElement[1, 2] = 13 * length * constant;
		//	massElement[1, 3] = -3 * length * length * constant;
		//	massElement[2, 0] = 54 * constant;
		//	massElement[2, 1] = 13 * length * constant;
		//	massElement[2, 2] = 156 * constant;
		//	massElement[2, 3] = -22 * length * constant;
		//	massElement[3, 0] = -13 * length * constant;
		//	massElement[3, 1] = -3 * length * length * constant;
		//	massElement[3, 2] = -22 * length * constant;
		//	massElement[3, 3] = 4 * length * length * constant;

		//	return massElement;
		//}

		public double[,] CalculateBeamHardnessElement(double momentInertia, double youngModulus, double length)
		{
			double[,] hardnessElement = new double[Constants.DegreesFreedomElement, Constants.DegreesFreedomElement];

			double constant = momentInertia * youngModulus / Math.Pow(length, 3);

			hardnessElement[0, 0] = 12 * constant;
			hardnessElement[0, 1] = 6 * length * constant;
			hardnessElement[0, 2] = -12 * constant;
			hardnessElement[0, 3] = 6 * length * constant;
			hardnessElement[1, 0] = 6 * length * constant;
			hardnessElement[1, 1] = 4 * Math.Pow(length, 2) * constant;
			hardnessElement[1, 2] = -(6 * length * constant);
			hardnessElement[1, 3] = 2 * Math.Pow(length, 2) * constant;
			hardnessElement[2, 0] = -(12 * constant);
			hardnessElement[2, 1] = -(6 * length * constant);
			hardnessElement[2, 2] = 12 * constant;
			hardnessElement[2, 3] = -(6 * length * constant);
			hardnessElement[3, 0] = 6 * length * constant;
			hardnessElement[3, 1] = 2 * Math.Pow(length, 2) * constant;
			hardnessElement[3, 2] = -(6 * length * constant);
			hardnessElement[3, 3] = 4 * Math.Pow(length, 2) * constant;

			return hardnessElement;
		}

		public double[,] CalculatePiezoelectricElementHardness(double elasticityToConstantElectricField, double momentInertia, double length)
		{
			double[,] hardnessElement = new double[Constants.DegreesFreedomElement, Constants.DegreesFreedomElement];

			double constant = momentInertia * elasticityToConstantElectricField / Math.Pow(length, 3);

			hardnessElement[0, 0] = 12 * constant;
			hardnessElement[0, 1] = 6 * length * constant;
			hardnessElement[0, 2] = -12 * constant;
			hardnessElement[0, 3] = 6 * length * constant;
			hardnessElement[1, 0] = 6 * length * constant;
			hardnessElement[1, 1] = 4 * Math.Pow(length, 2) * constant;
			hardnessElement[1, 2] = -(6 * length * constant);
			hardnessElement[1, 3] = 2 * Math.Pow(length, 2) * constant;
			hardnessElement[2, 0] = -(12 * constant);
			hardnessElement[2, 1] = -(6 * length * constant);
			hardnessElement[2, 2] = 12 * constant;
			hardnessElement[2, 3] = -(6 * length * constant);
			hardnessElement[3, 0] = 6 * length * constant;
			hardnessElement[3, 1] = 2 * Math.Pow(length, 2) * constant;
			hardnessElement[3, 2] = -(6 * length * constant);
			hardnessElement[3, 3] = 4 * Math.Pow(length, 2) * constant;

			return hardnessElement;
		}

		public double[,] CalculateBeamMass(Beam beam, int degreesFreedomMaximum)
        {
			int elements = beam.ElementCount;

			int[,] nodeCoordinates = this.NodeCoordinates(elements);
			
			int p, q, r, s;
			
			double[,] mass = new double[degreesFreedomMaximum, degreesFreedomMaximum];
			
			double length = beam.Length / elements;

			for (int n = 0; n < elements; n++)
			{
				double[,] massElement = this.CalculateMassElement(beam.Profile.Area[n], beam.Material.SpecificMass, length);

				p = nodeCoordinates[n,0];
				q = nodeCoordinates[n,1];
				r = nodeCoordinates[n,2];
				s = nodeCoordinates[n,3];

				mass[p, p] += massElement[0, 0];
				mass[p, q] += massElement[0, 1];
				mass[p, r] += massElement[0, 2];
				mass[p, s] += massElement[0, 3];
				mass[q, p] += massElement[1, 0];
				mass[q, q] += massElement[1, 1];
				mass[q, r] += massElement[1, 2];
				mass[q, s] += massElement[1, 3];
				mass[r, p] += massElement[2, 0];
				mass[r, q] += massElement[2, 1];
				mass[r, r] += massElement[2, 2];
				mass[r, s] += massElement[2, 3];
				mass[s, p] += massElement[3, 0];
				mass[s, q] += massElement[3, 1];
				mass[s, r] += massElement[3, 2];
				mass[s, s] += massElement[3, 3];
			}

			return mass;
		}

		public double[,] CalculateBeamHardness(Beam beam, int degreesFreedomMaximum)
		{
			int elements = beam.ElementCount;

			int[,] nodeCoordinates = this.NodeCoordinates(elements);
			
			int p, q, r, s;

			double[,] hardness = new double[degreesFreedomMaximum, degreesFreedomMaximum];

			double length = beam.Length / elements;

			for (int n = 0; n < elements; n++)
			{
		
				double[,] hardnessElement = this.CalculateBeamHardnessElement(beam.Profile.MomentInertia[n], beam.Material.YoungModulus, length);

				p = nodeCoordinates[n, 0];
				q = nodeCoordinates[n, 1];
				r = nodeCoordinates[n, 2];
				s = nodeCoordinates[n, 3];

				hardness[p, p] += hardnessElement[0, 0];
				hardness[p, q] += hardnessElement[0, 1];
				hardness[p, r] += hardnessElement[0, 2];
				hardness[p, s] += hardnessElement[0, 3];
				hardness[q, p] += hardnessElement[1, 0];
				hardness[q, q] += hardnessElement[1, 1];
				hardness[q, r] += hardnessElement[1, 2];
				hardness[q, s] += hardnessElement[1, 3];
				hardness[r, p] += hardnessElement[2, 0];
				hardness[r, q] += hardnessElement[2, 1];
				hardness[r, r] += hardnessElement[2, 2];
				hardness[r, s] += hardnessElement[2, 3];
				hardness[s, p] += hardnessElement[3, 0];
				hardness[s, q] += hardnessElement[3, 1];
				hardness[s, r] += hardnessElement[3, 2];
				hardness[s, s] += hardnessElement[3, 3];
			}

			return hardness;
		}

		public double[,] CalculateBeamDamping(double[,] mass, double[,] hardness, int degreesFreedomMaximum)
		{
			double[,] damping = new double[degreesFreedomMaximum, degreesFreedomMaximum];

			for (int i = 0; i < degreesFreedomMaximum; i++)
			{
				for (int j = 0; j < degreesFreedomMaximum; j++)
				{
					damping[i, j] = Constants.Mi * (mass[i, j] + hardness[i, j]);
				}
			}

			return damping;
		}

		public bool[] CalculateBeamBondaryCondition(Fastening firstFastening, Fastening lastFastening, int degreesFreedomMaximum)
		{
			bool[] bondaryCondition = new bool[degreesFreedomMaximum];

			for (int i = 0; i < degreesFreedomMaximum; i++)
			{
				if(i == 0 || i == degreesFreedomMaximum - 2)
				{
					bondaryCondition[i] = firstFastening.Displacement;
				}
				else if(i == 1 || i == degreesFreedomMaximum - 1)
				{
					bondaryCondition[i] = firstFastening.Angle;
				}
				else
				{
					bondaryCondition[i] = true;
				}
			}

			return bondaryCondition;
		}

		private int[,] NodeCoordinates(int elements)
		{
			int[,] nodeCoordinates = new int[elements + 1, Constants.DegreesFreedomElement];

			for (int i = 0; i < elements + 1; i++)
			{
				for (int j = 0; j < Constants.DegreesFreedomElement; j++)
				{
					nodeCoordinates[i, j] = 2 * i + j;
				}
			}

			return nodeCoordinates;
		}

		public double[,] CalculatePiezoelectricElectromechanicalCoupling(double piezoelectricWidth, double piezoelectricHeight, double beamHeight, double piezoelectricLength, double piezoelectricStrain)
		{
			double[,] electromechanicalCoupling = new double[Constants.DegreesFreedomElement, Constants.PiezoelectricCapacitanceMatrixSize];

			double constant = -(piezoelectricStrain * piezoelectricWidth * piezoelectricLength / 2) * (2 * beamHeight * piezoelectricHeight + Math.Pow(piezoelectricHeight, 2));

			electromechanicalCoupling[0, 0] = 0;
			electromechanicalCoupling[0, 1] = 0;
			electromechanicalCoupling[1, 0] = -piezoelectricLength * constant;
			electromechanicalCoupling[1, 1] = piezoelectricLength * constant;
			electromechanicalCoupling[2, 0] = 0;
			electromechanicalCoupling[2, 1] = piezoelectricLength * constant;
			electromechanicalCoupling[3, 0] = piezoelectricLength * constant;
			electromechanicalCoupling[3, 1] = -piezoelectricLength * constant;

			return electromechanicalCoupling;
		}

		public double[,] CalculatePiezoelectricCapacitance(double area, double length, double heigth, double dielectricConstantsToConstantStrain)
		{
			double[,] piezoelectricCapacitance = new double[Constants.PiezoelectricCapacitanceMatrixSize, Constants.PiezoelectricCapacitanceMatrixSize];

			double constant = -dielectricConstantsToConstantStrain * area * length / Math.Pow(heigth, 2);

			piezoelectricCapacitance[0, 0] = constant;
			piezoelectricCapacitance[0, 1] = -constant;
			piezoelectricCapacitance[1, 0] = -constant;
			piezoelectricCapacitance[1, 1] = constant;

			return piezoelectricCapacitance;
		}
	}
}
