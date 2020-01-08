using IcVibrations.Models.Beam.Characteristics;
using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Calculator.MainMatrixes
{
    public class MainMatrix : IMainMatrix
	{
        public double[,] Mass(int degreesFreedomPerElemenent, int degreesFreedomMaximum, int elements, double[] area, int[,] elementsCoordinate, double density, double length)
        {
			double[,] elementMass = new double[degreesFreedomPerElemenent, degreesFreedomPerElemenent];
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
				elementMass[0, 0] = 156 * ((area[n] * density * length) / 420);
				elementMass[0, 1] = 22 * length * ((area[n] * density * length) / 420);
				elementMass[0, 2] = 54 * ((area[n] * density * length) / 420);
				elementMass[0, 3] = -13 * length * ((area[n] * density * length) / 420);
				elementMass[1, 0] = 22 * length * ((area[n] * density * length) / 420);
				elementMass[1, 1] = 4 * length * length * ((area[n] * density * length) / 420);
				elementMass[1, 2] = 13 * length * ((area[n] * density * length) / 420);
				elementMass[1, 3] = -3 * length * length * ((area[n] * density * length) / 420);
				elementMass[2, 0] = 54 * ((area[n] * density * length) / 420);
				elementMass[2, 1] = 13 * length * ((area[n] * density * length) / 420);
				elementMass[2, 2] = 156 * ((area[n] * density * length) / 420);
				elementMass[2, 3] = -22 * length * ((area[n] * density * length) / 420);
				elementMass[3, 0] = -13 * length * ((area[n] * density * length) / 420);
				elementMass[3, 1] = -3 * length * length * ((area[n] * density * length) / 420);
				elementMass[3, 2] = -22 * length * ((area[n] * density * length) / 420);
				elementMass[3, 3] = 4 * length * length * ((area[n] * density * length) / 420);

				p = elementsCoordinate[n,0];
				q = elementsCoordinate[n,1];
				r = elementsCoordinate[n,2];
				s = elementsCoordinate[n,3];

				mass[p,p] = elementMass[0, 0] + mass[p,p];
				mass[p,q] = elementMass[0, 1] + mass[p,q];
				mass[p,r] = elementMass[0, 2] + mass[p,r];
				mass[p,s] = elementMass[0, 3] + mass[p,s];
				mass[q,p] = elementMass[1, 0] + mass[q,p];
				mass[q,q] = elementMass[1, 1] + mass[q,q];
				mass[q,r] = elementMass[1, 2] + mass[q,r];
				mass[q,s] = elementMass[1, 3] + mass[q,s];
				mass[r,p] = elementMass[2, 0] + mass[r,p];
				mass[r,q] = elementMass[2, 1] + mass[r,q];
				mass[r,r] = elementMass[2, 2] + mass[r,r];
				mass[r,s] = elementMass[2, 3] + mass[r,s];
				mass[s,p] = elementMass[3, 0] + mass[s,p];
				mass[s,q] = elementMass[3, 1] + mass[s,q];
				mass[s,r] = elementMass[3, 2] + mass[s,r];
				mass[s,s] = elementMass[3, 3] + mass[s,s];
			}

			return mass;
		}

		public double[,] Hardness(int degreesFreedomPerElemenent, int degreesFreedomMaximum, int elements, int[,] elementsCoordinate, double[] momentInertia, double[] youngModulus, double length)
		{
			double[,] elementHardness = new double[degreesFreedomPerElemenent, degreesFreedomPerElemenent];
			double[,] hardness = new double[degreesFreedomMaximum, degreesFreedomMaximum];
			int p, q, r, s;

			for (int n = 0; n < elements; n++)
			{
				elementHardness[0, 0] = (12 * momentInertia[n] * youngModulus[n] / Math.Pow(length, 3));
				elementHardness[0, 1] = (6 * momentInertia[n] * youngModulus[n] / Math.Pow(length, 2));
				elementHardness[0, 2] = -(12 * momentInertia[n] * youngModulus[n] / Math.Pow(length, 3));
				elementHardness[0, 3] = (6 * momentInertia[n] * youngModulus[n] / Math.Pow(length, 2));
				elementHardness[1, 0] = (6 * momentInertia[n] * youngModulus[n] / Math.Pow(length, 2));
				elementHardness[1, 1] = (4 * momentInertia[n] * youngModulus[n] / length);
				elementHardness[1, 2] = -(6 * momentInertia[n] * youngModulus[n] / Math.Pow(length, 2));
				elementHardness[1, 3] = (4 * momentInertia[n] * youngModulus[n] / length) / 2;
				elementHardness[2, 0] = -(12 * momentInertia[n] * youngModulus[n] / Math.Pow(length, 3));
				elementHardness[2, 1] = -(6 * momentInertia[n] * youngModulus[n] / Math.Pow(length, 2));
				elementHardness[2, 2] = (12 * momentInertia[n] * youngModulus[n] / Math.Pow(length, 3));
				elementHardness[2, 3] = -(6 * momentInertia[n] * youngModulus[n] / Math.Pow(length, 2));
				elementHardness[3, 0] = (6 * momentInertia[n] * youngModulus[n] / Math.Pow(length, 2));
				elementHardness[3, 1] = (4 * momentInertia[n] * youngModulus[n] / length) / 2;
				elementHardness[3, 2] = -(6 * momentInertia[n] * youngModulus[n] / Math.Pow(length, 2));
				elementHardness[3, 3] = (4 * momentInertia[n] * youngModulus[n] / length);

				p = elementsCoordinate[n, 0];
				q = elementsCoordinate[n, 1];
				r = elementsCoordinate[n, 2];
				s = elementsCoordinate[n, 3];

				hardness[p, p] = elementHardness[0, 0] + hardness[p, p];
				hardness[p, q] = elementHardness[0, 1] + hardness[p, q];
				hardness[p, r] = elementHardness[0, 2] + hardness[p, r];
				hardness[p, s] = elementHardness[0, 3] + hardness[p, s];
				hardness[q, p] = elementHardness[1, 0] + hardness[q, p];
				hardness[q, q] = elementHardness[1, 1] + hardness[q, q];
				hardness[q, r] = elementHardness[1, 2] + hardness[q, r];
				hardness[q, s] = elementHardness[1, 3] + hardness[q, s];
				hardness[r, p] = elementHardness[2, 0] + hardness[r, p];
				hardness[r, q] = elementHardness[2, 1] + hardness[r, q];
				hardness[r, r] = elementHardness[2, 2] + hardness[r, r];
				hardness[r, s] = elementHardness[2, 3] + hardness[r, s];
				hardness[s, p] = elementHardness[3, 0] + hardness[s, p];
				hardness[s, q] = elementHardness[3, 1] + hardness[s, q];
				hardness[s, r] = elementHardness[3, 2] + hardness[s, r];
				hardness[s, s] = elementHardness[3, 3] + hardness[s, s];
			}

			return hardness;
		}

		public double[,] Damping(double[,] mass, double[,] hardness, double mi, int degreesFreedomMaximum)
		{
			double[,] amortecimento = new double[degreesFreedomMaximum, degreesFreedomMaximum];

			for (int i = 0; i < degreesFreedomMaximum; i++)
			{
				for (int j = 0; j < degreesFreedomMaximum; j++)
				{
					amortecimento[i, j] = mi * (mass[i, j] + hardness[i, j]);
				}
			}

			return amortecimento;
		}
		public int[,] ElementsCoordinate(int elements, int dimensions)
		{
			int[,] coordenadaElementos = new int[elements, dimensions];

			for (int i = 0; i < elements; i++)
			{
				for (int j = 0; j < dimensions; j++)
				{
					coordenadaElementos[i, j] = i + j + 1;
				}
			}

			return coordenadaElementos;
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

		public double[] Force(double forca, int posicaoForca, int degreesFreedomMaximum)
		{
			double[] forcamento = new double[degreesFreedomMaximum];

			for (int i = 0; i < degreesFreedomMaximum; i++)
			{
				if (i == posicaoForca)
				{
					forcamento[i] = forca;
				}
				else
				{
					forcamento[i] = 0;
				}
			}

			return forcamento;
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
	}
}
