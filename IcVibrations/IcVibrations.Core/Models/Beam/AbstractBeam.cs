﻿using IcVibrations.Common.Profiles;
using IcVibrations.Models.Beam.Characteristics;

namespace IcVibrations.Core.Models.Beam
{
    /// <summary>
    /// It represents the analyzed beam.
    /// </summary>
    public abstract class AbstractBeam<TProfile>
        where TProfile : Profile, new()
    {
        /// <summary>
        /// Number of elements.
        /// </summary>
        public uint NumberOfElements { get; set; }

        /// <summary>
        /// Material. Can be: Steel 1020, Steel 4130, Aluminium.
        /// </summary>
        public Material Material { get; set; }

        /// <summary>
        /// First fastening. Can be: fixed, pinned and simple.
        /// </summary>
        public Fastening FirstFastening { get; set; }

        /// <summary>
        /// Last fastening. Can be: fixed, pinned and simple.
        /// </summary>
        public Fastening LastFastening { get; set; }

        /// <summary>
        /// Length.
        /// </summary>
        public double Length { get; set; }

        /// <summary>
        /// Force matrix. Matrix size: degrees freedom maximum.
        /// </summary>
        public double[] Forces { get; set; }

        /// <summary>
        /// Geometric properties: Area and Moment of Inertia.
        /// </summary>
        public GeometricProperty GeometricProperty { get; set; }

        /// <summary>
        /// Beam profile.
        /// </summary>
        public TProfile Profile { get; set; }
    }
}
