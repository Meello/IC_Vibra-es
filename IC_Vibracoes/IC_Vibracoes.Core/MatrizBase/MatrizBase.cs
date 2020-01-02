using IC_Vibracao.InputData.Beam;
using System;
using System.Collections.Generic;
using System.Text;
using static IC_Vibracao.InputData.Constantes;

namespace IC_Vibracao.MatrizBase
{
    public class MatrizBase : IMatrizBase
	{
		private readonly IMatrizes _matrizes;

		public MatrizBase(IMatrizes matrizes)
		{
			this._matrizes = matrizes;
		}

        public double[,] Massa(Beam barra)
        {
			double[,] massaElemento = new double[Variaveis.GrausLiberdadeElemenento,Variaveis.GrausLiberdadeElemenento];
			double[,] massa = new double[Variaveis.GrausLiberdadeMaximo,Variaveis.GrausLiberdadeMaximo];
			int[,] coorEl = this._matrizes.CoordenadaElementos();
			double[] area = this._matrizes.Area(barra.Perfil.Area);
			double densidade = barra.Material.SpecificMass;
			double l = barra.Comprimento;
			int p, q, r, s;

			for (int i = 0; i < Variaveis.GrausLiberdadeMaximo; i++)
			{
				for (int j = 0; j < Variaveis.GrausLiberdadeMaximo; j++)
				{
					massa[i, j] = 0;
				}
			}

			for (int n = 0; n < Variaveis.NumeroElementos; n++)
			{
				massaElemento[0, 0] = 156 * ((area[n] * densidade * l) / 420);
				massaElemento[0, 1] = 22 * l * ((area[n] * densidade * l) / 420);
				massaElemento[0, 2] = 54 * ((area[n] * densidade * l) / 420);
				massaElemento[0, 3] = -13 * l * ((area[n] * densidade * l) / 420);
				massaElemento[1, 0] = 22 * l * ((area[n] * densidade * l) / 420);
				massaElemento[1, 1] = 4 * l * l * ((area[n] * densidade * l) / 420);
				massaElemento[1, 2] = 13 * l * ((area[n] * densidade * l) / 420);
				massaElemento[1, 3] = -3 * l * l * ((area[n] * densidade * l) / 420);
				massaElemento[2, 0] = 54 * ((area[n] * densidade * l) / 420);
				massaElemento[2, 1] = 13 * l * ((area[n] * densidade * l) / 420);
				massaElemento[2, 2] = 156 * ((area[n] * densidade * l) / 420);
				massaElemento[2, 3] = -22 * l * ((area[n] * densidade * l) / 420);
				massaElemento[3, 0] = -13 * l * ((area[n] * densidade * l) / 420);
				massaElemento[3, 1] = -3 * l * l * ((area[n] * densidade * l) / 420);
				massaElemento[3, 2] = -22 * l * ((area[n] * densidade * l) / 420);
				massaElemento[3, 3] = 4 * l * l * ((area[n] * densidade * l) / 420);

				p = coorEl[n,0];
				q = coorEl[n,1];
				r = coorEl[n,2];
				s = coorEl[n,3];

				massa[p,p] = massaElemento[0, 0] + massa[p,p];
				massa[p,q] = massaElemento[0, 1] + massa[p,q];
				massa[p,r] = massaElemento[0, 2] + massa[p,r];
				massa[p,s] = massaElemento[0, 3] + massa[p,s];
				massa[q,p] = massaElemento[1, 0] + massa[q,p];
				massa[q,q] = massaElemento[1, 1] + massa[q,q];
				massa[q,r] = massaElemento[1, 2] + massa[q,r];
				massa[q,s] = massaElemento[1, 3] + massa[q,s];
				massa[r,p] = massaElemento[2, 0] + massa[r,p];
				massa[r,q] = massaElemento[2, 1] + massa[r,q];
				massa[r,r] = massaElemento[2, 2] + massa[r,r];
				massa[r,s] = massaElemento[2, 3] + massa[r,s];
				massa[s,p] = massaElemento[3, 0] + massa[s,p];
				massa[s,q] = massaElemento[3, 1] + massa[s,q];
				massa[s,r] = massaElemento[3, 2] + massa[s,r];
				massa[s,s] = massaElemento[3, 3] + massa[s,s];
			}

			return massa;
		}

		public double[,] Rigidez(Beam barra)
		{
			double[,] rigidezElemento = new double[Variaveis.GrausLiberdadeElemenento, Variaveis.GrausLiberdadeElemenento];
			double[,] rigidez = new double[Variaveis.GrausLiberdadeMaximo, Variaveis.GrausLiberdadeMaximo];
			int[,] coorEl = this._matrizes.CoordenadaElementos();
			double[] moI = this._matrizes.MomentoInercia(barra.Perfil.MomentoInercia);
			double[] moEl = this._matrizes.ModuloElasticade(barra.Material.YoungModulus);
			double l = barra.Comprimento;
			int p, q, r, s;

			for (int n = 0; n < Variaveis.NumeroElementos; n++)
			{
				rigidezElemento[0, 0] = (12 * moI[n] * moEl[n] / Math.Pow(l, 3));
				rigidezElemento[0, 1] = (6 * moI[n] * moEl[n] / Math.Pow(l, 2));
				rigidezElemento[0, 2] = -(12 * moI[n] * moEl[n] / Math.Pow(l, 3));
				rigidezElemento[0, 3] = (6 * moI[n] * moEl[n] / Math.Pow(l, 2));
				rigidezElemento[1, 0] = (6 * moI[n] * moEl[n] / Math.Pow(l, 2));
				rigidezElemento[1, 1] = (4 * moI[n] * moEl[n] / l);
				rigidezElemento[1, 2] = -(6 * moI[n] * moEl[n] / Math.Pow(l, 2));
				rigidezElemento[1, 3] = (4 * moI[n] * moEl[n] / l) / 2;
				rigidezElemento[2, 0] = -(12 * moI[n] * moEl[n] / Math.Pow(l, 3));
				rigidezElemento[2, 1] = -(6 * moI[n] * moEl[n] / Math.Pow(l, 2));
				rigidezElemento[2, 2] = (12 * moI[n] * moEl[n] / Math.Pow(l, 3));
				rigidezElemento[2, 3] = -(6 * moI[n] * moEl[n] / Math.Pow(l, 2));
				rigidezElemento[3, 0] = (6 * moI[n] * moEl[n] / Math.Pow(l, 2));
				rigidezElemento[3, 1] = (4 * moI[n] * moEl[n] / l) / 2;
				rigidezElemento[3, 2] = -(6 * moI[n] * moEl[n] / Math.Pow(l, 2));
				rigidezElemento[3, 3] = (4 * moI[n] * moEl[n] / l);

				p = coorEl[n, 0];
				q = coorEl[n, 1];
				r = coorEl[n, 2];
				s = coorEl[n, 3];

				rigidez[p, p] = rigidezElemento[0, 0] + rigidez[p, p];
				rigidez[p, q] = rigidezElemento[0, 1] + rigidez[p, q];
				rigidez[p, r] = rigidezElemento[0, 2] + rigidez[p, r];
				rigidez[p, s] = rigidezElemento[0, 3] + rigidez[p, s];
				rigidez[q, p] = rigidezElemento[1, 0] + rigidez[q, p];
				rigidez[q, q] = rigidezElemento[1, 1] + rigidez[q, q];
				rigidez[q, r] = rigidezElemento[1, 2] + rigidez[q, r];
				rigidez[q, s] = rigidezElemento[1, 3] + rigidez[q, s];
				rigidez[r, p] = rigidezElemento[2, 0] + rigidez[r, p];
				rigidez[r, q] = rigidezElemento[2, 1] + rigidez[r, q];
				rigidez[r, r] = rigidezElemento[2, 2] + rigidez[r, r];
				rigidez[r, s] = rigidezElemento[2, 3] + rigidez[r, s];
				rigidez[s, p] = rigidezElemento[3, 0] + rigidez[s, p];
				rigidez[s, q] = rigidezElemento[3, 1] + rigidez[s, q];
				rigidez[s, r] = rigidezElemento[3, 2] + rigidez[s, r];
				rigidez[s, s] = rigidezElemento[3, 3] + rigidez[s, s];
			}

			return rigidez;
		}

		public double[,] Amortecimento(double[,] massa, double[,] rigidez, double mi)
		{
			double[,] amortecimento = new double[Variaveis.GrausLiberdadeMaximo, Variaveis.GrausLiberdadeMaximo];

			for (int i = 0; i < Variaveis.GrausLiberdadeMaximo; i++)
			{
				for (int j = 0; j < Variaveis.GrausLiberdadeMaximo; j++)
				{
					amortecimento[i, j] = mi * (massa[i, j] + rigidez[i, j]);
				}
			}

			return amortecimento;
		}
    }
}
