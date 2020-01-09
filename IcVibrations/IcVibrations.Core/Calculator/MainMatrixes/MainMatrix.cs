using IcVibrations.Core.Models;
using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IcVibrations.Calculator.MainMatrixes
{
    public class MainMatrix : IMainMatrix
	{
		public double[,] MassElement(double area, double density, double length)
		{
			double[,] massElement = new double[Constants.DegreesFreedomElement, Constants.DegreesFreedomElement];

			massElement[0, 0] = 156 * ((area * density * length) / 420);
			massElement[0, 1] = 22 * length * ((area * density * length) / 420);
			massElement[0, 2] = 54 * ((area * density * length) / 420);
			massElement[0, 3] = -13 * length * ((area * density * length) / 420);
			massElement[1, 0] = 22 * length * ((area * density * length) / 420);
			massElement[1, 1] = 4 * length * length * ((area * density * length) / 420);
			massElement[1, 2] = 13 * length * ((area * density * length) / 420);
			massElement[1, 3] = -3 * length * length * ((area * density * length) / 420);
			massElement[2, 0] = 54 * ((area * density * length) / 420);
			massElement[2, 1] = 13 * length * ((area * density * length) / 420);
			massElement[2, 2] = 156 * ((area * density * length) / 420);
			massElement[2, 3] = -22 * length * ((area * density * length) / 420);
			massElement[3, 0] = -13 * length * ((area * density * length) / 420);
			massElement[3, 1] = -3 * length * length * ((area * density * length) / 420);
			massElement[3, 2] = -22 * length * ((area * density * length) / 420);
			massElement[3, 3] = 4 * length * length * ((area * density * length) / 420);

			return massElement;
		}

		public double[,] HardnessElement(double momentInertia, double youngModulus, double length)
		{
			double[,] hardnessElement = new double[Constants.DegreesFreedomElement, Constants.DegreesFreedomElement];

			hardnessElement[0, 0] = 12 * momentInertia * youngModulus / Math.Pow(length, 3);
			hardnessElement[0, 1] = 6 * momentInertia  * youngModulus  / Math.Pow(length, 2);
			hardnessElement[0, 2] = -12 * momentInertia * youngModulus / Math.Pow(length, 3);
			hardnessElement[0, 3] = 6 * momentInertia * youngModulus / Math.Pow(length, 2);
			hardnessElement[1, 0] = 6 * momentInertia * youngModulus / Math.Pow(length, 2);
			hardnessElement[1, 1] = 4 * momentInertia * youngModulus / length;
			hardnessElement[1, 2] = -(6 * momentInertia * youngModulus / Math.Pow(length, 2));
			hardnessElement[1, 3] = (4 * momentInertia * youngModulus / length) / 2;
			hardnessElement[2, 0] = -(12 * momentInertia * youngModulus / Math.Pow(length, 3));
			hardnessElement[2, 1] = -(6 * momentInertia * youngModulus / Math.Pow(length, 2));
			hardnessElement[2, 2] = 12 * momentInertia * youngModulus / Math.Pow(length, 3);
			hardnessElement[2, 3] = -(6 * momentInertia * youngModulus / Math.Pow(length, 2));
			hardnessElement[3, 0] = 6 * momentInertia * youngModulus / Math.Pow(length, 2);
			hardnessElement[3, 1] = 4 * momentInertia * youngModulus / length / 2;
			hardnessElement[3, 2] = -(6 * momentInertia * youngModulus / Math.Pow(length, 2));
			hardnessElement[3, 3] = 4 * momentInertia * youngModulus / length;

			return hardnessElement;
		}

		public double[,] Mass(int degreesFreedomMaximum, int elements, double[,] massElement)
        {
			int[,] elementsCoordinate = this.ElementsCoordinate(elements);
			double[,] mass = new double[degreesFreedomMaximum, degreesFreedomMaximum];
			int p, q, r, s;

			for (int i = 0; i < degreesFreedomMaximum; i++)
			{
				for (int j = 0; j < degreesFreedomMaximum; j++)
				{
					mass[i, j] = 0;
				}
			}

			for (int n = 0; n < elements; n++)
			{
				p = elementsCoordinate[n,0];
				q = elementsCoordinate[n,1];
				r = elementsCoordinate[n,2];
				s = elementsCoordinate[n,3];

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

		public double[,] Hardness(int degreesFreedomMaximum, int elements, double[,] hardnessElement)
		{
			double[,] hardness = new double[degreesFreedomMaximum, degreesFreedomMaximum];
			int[,] elementsCoordinate = this.ElementsCoordinate(elements);
			int p, q, r, s;

			for (int n = 0; n < elements; n++)
			{
				p = elementsCoordinate[n, 0];
				q = elementsCoordinate[n, 1];
				r = elementsCoordinate[n, 2];
				s = elementsCoordinate[n, 3];

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

		public double[,] Damping(double[,] mass, double[,] hardness, double mi, int degreesFreedomMaximum)
		{
			double[,] damping = new double[degreesFreedomMaximum, degreesFreedomMaximum];

			for (int i = 0; i < degreesFreedomMaximum; i++)
			{
				for (int j = 0; j < degreesFreedomMaximum; j++)
				{
					damping[i, j] = mi * (mass[i, j] + hardness[i, j]);
				}
			}

			return damping;
		}
		
		public double[] Area(double area, int elements)
		{
			double[] matrizArea = new double[elements];

			for (int i = 0; i < elements; i++)
			{
				matrizArea[i] = area;
			}

			return matrizArea;
		}

		public double[] MomentInertia(double momentoInercia, int elements)
		{
			double[] matrizMomentoInercia = new double[elements];

			for (int i = 0; i < elements; i++)
			{
				matrizMomentoInercia[i] = momentoInercia;
			}

			return matrizMomentoInercia;
		}

		public double[] YoungModulus(double moduloElasticidade, int elements)
		{
			double[] moduloEl = new double[elements];

			for (int i = 0; i < elements; i++)
			{
				moduloEl[i] = moduloElasticidade;
			}

			return moduloEl;
		}

		public double[] Force(double[] forceValues, int[] forcePosition, int degreesFreedomMaximum)
		{
			double[] force = new double[degreesFreedomMaximum];
			
			for (int i = 0; i < force.Count(); i++)
			{
				force[forcePosition[i]] = forceValues[i];
			}

			return force;
		}

		public bool[] BondaryCondition(Fastening fixacao1, Fastening fixacaoN, int degreesFreedomMaximum)
		{
			bool[] condicoesContorno = new bool[degreesFreedomMaximum];

			for (int i = 0; i < degreesFreedomMaximum; i++)
			{
				if (i == 0)
				{
					condicoesContorno[i] = fixacao1.Displacement;
					condicoesContorno[i + 1] = fixacao1.Angle;
				}
				else if (i == degreesFreedomMaximum - 1)
				{
					condicoesContorno[i - 1] = fixacaoN.Displacement;
					condicoesContorno[i] = fixacaoN.Angle;
				}
				else
				{
					condicoesContorno[i] = true;
				}
			}

			return condicoesContorno;
		}

		private int[,] ElementsCoordinate(int elements)
		{
			int[,] coordenadaElementos = new int[elements, Constants.Dimensions];

			for (int i = 0; i < elements; i++)
			{
				for (int j = 0; j < Constants.Dimensions; j++)
				{
					coordenadaElementos[i, j] = i + j + 1;
				}
			}

			return coordenadaElementos;
		}
	}
}
