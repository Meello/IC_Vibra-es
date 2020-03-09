﻿using System;

namespace IcVibrations.Core.Models.BeamWithPiezoelectric
{
    public enum PiezoelectricPosition
    {
        Up = 1,
        Down = -1,
        UpAndDown = 2,
        Around = 4
    }

    /// <summary>
    /// It's responsible to manipulate a PiezoelectricPosition object based in a string.
    /// </summary>
    public class PiezoelectricPositionFactory
    {
        /// <summary>
        /// It's responsible to create a PiezoelectricPosition object based in a string.
        /// </summary>
        public static uint Create(string piezoelectricPosition)
        {
            switch ((PiezoelectricPosition)Enum.Parse(typeof(PiezoelectricPosition), piezoelectricPosition, ignoreCase: true))
            {
                case PiezoelectricPosition.Up: return 1;
                case PiezoelectricPosition.Down: return 1;
                case PiezoelectricPosition.UpAndDown: return 2;
                case PiezoelectricPosition.Around: return 4;
                default: break;
            }

            throw new Exception($"Invalid material: {piezoelectricPosition}.");
        }
    }
}
