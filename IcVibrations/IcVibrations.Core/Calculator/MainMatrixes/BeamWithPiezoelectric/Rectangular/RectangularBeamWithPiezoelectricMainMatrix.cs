﻿using IcVibrations.Calculator.MainMatrixes;
using IcVibrations.Common.Profiles;
using IcVibrations.Core.Calculator.ArrayOperations;
using IcVibrations.Core.Calculator.MainMatrixes.Beam.Rectangular;
using IcVibrations.Core.Models;
using IcVibrations.Core.Models.Piezoelectric;
using System;
using System.Threading.Tasks;

namespace IcVibrations.Core.Calculator.MainMatrixes.BeamWithPiezoelectric.Rectangular
{
    /// <summary>
    /// It's responsible to calculate the rectangular beam with piezoelectric main matrixes.
    /// </summary>
    public class RectangularBeamWithPiezoelectricMainMatrix : BeamWithPiezoelectricMainMatrix<RectangularProfile>, IRectangularBeamWithPiezoelectricMainMatrix
    {
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="commonMainMatrix"></param>
        /// <param name="beamMainMatrix"></param>
        /// <param name="arrayOperation"></param>
        public RectangularBeamWithPiezoelectricMainMatrix(
            ICommonMainMatrix commonMainMatrix,
            IRectangularBeamMainMatrix beamMainMatrix,
            IArrayOperation arrayOperation)
            : base(commonMainMatrix, beamMainMatrix, arrayOperation)
        {
        }

        public override Task<double[,]> CalculateElementPiezoelectricCapacitance(BeamWithPiezoelectric<RectangularProfile> beamWithPiezoelectric, uint elementIndex)
        {
            double[,] piezoelectricCapacitance = new double[Constants.PiezoelectricElementMatrixSize, Constants.PiezoelectricElementMatrixSize];
            double elementLength = beamWithPiezoelectric.Length / beamWithPiezoelectric.NumberOfElements;

            double constant = -beamWithPiezoelectric.DielectricConstant * beamWithPiezoelectric.GeometricProperty.Area[elementIndex] * elementLength / Math.Pow(beamWithPiezoelectric.Profile.Height, 2);

            piezoelectricCapacitance[0, 0] = constant;
            piezoelectricCapacitance[0, 1] = -constant;
            piezoelectricCapacitance[1, 0] = -constant;
            piezoelectricCapacitance[1, 1] = constant;

            return Task.FromResult(piezoelectricCapacitance);
        }

        public override Task<double[,]> CalculatePiezoelectricElementElectromechanicalCoupling(BeamWithPiezoelectric<RectangularProfile> beamWithPiezoelectric)
        {
            double[,] electromechanicalCoupling = new double[Constants.DegreesFreedomElement, Constants.PiezoelectricElementMatrixSize];
            double elementLength = beamWithPiezoelectric.Length / beamWithPiezoelectric.NumberOfElements;

            double constant = -(beamWithPiezoelectric.DielectricPermissiveness * beamWithPiezoelectric.PiezoelectricProfile.Width * elementLength / 2) * (2 * beamWithPiezoelectric.Profile.Height * beamWithPiezoelectric.PiezoelectricProfile.Height + Math.Pow(beamWithPiezoelectric.PiezoelectricProfile.Height, 2));

            electromechanicalCoupling[0, 0] = 0;
            electromechanicalCoupling[0, 1] = 0;
            electromechanicalCoupling[1, 0] = -elementLength * constant;
            electromechanicalCoupling[1, 1] = elementLength * constant;
            electromechanicalCoupling[2, 0] = 0;
            electromechanicalCoupling[2, 1] = elementLength * constant;
            electromechanicalCoupling[3, 0] = elementLength * constant;
            electromechanicalCoupling[3, 1] = -elementLength * constant;

            return Task.FromResult(electromechanicalCoupling);
        }
    }
}
