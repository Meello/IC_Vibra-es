﻿using IcVibrations.Common.Classes;
using IcVibrations.Common.Profiles;
using System.Collections.Generic;

namespace IcVibrations.DataContracts.CalculateVibration
{
    /// <summary>
    /// It represents the 'data' content of beam request operation.
    /// </summary>
    /// <typeparam name="TProfile"></typeparam>
    public abstract class CalculateVibrationRequestData<TProfile>
        where TProfile : Profile
    {
        /// <summary>
        /// Number of elements in the beam.
        /// </summary>
        public uint NumberOfElements { get; set; }

        /// <summary>
        /// Beam material.
        /// </summary>
        public string Material { get; set; }

        /// <summary>
        /// Beam first fastening.
        /// </summary>
        public string FirstFastening { get; set; }

        /// <summary>
        /// Beam last fastening.
        /// </summary>
        public string LastFastening { get; set; }

        /// <summary>
        /// Beam length.
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// Applied forces in the beam.
        /// </summary>
        public List<Force> Forces { get; set; }

        /// <summary>
        /// Beam profile.
        /// </summary>
        public TProfile Profile { get; set; }
    }
}