﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao.InputData.Beam.Properties
{
    public abstract class Material
    {
        // Unidade: Pa
        public abstract double YoungModulus { get; }

        // Unidade: Pa
        public abstract double YieldStrenght { get; }

        // Unidade: kg/m³
        public abstract double SpecificMass { get; }
    }

    public class Steel1020 : Material
    {
        public override double YoungModulus => 205e9;

        public override double YieldStrenght => 350e6;

        public override double SpecificMass => 7850;
    }

    public class Steel4130 : Material
    {
        public override double YoungModulus => 200e9;

        public override double YieldStrenght => 460e6;

        public override double SpecificMass => 7850;
    }

    public class Aluminium : Material
    {
        public override double YoungModulus => 70e9;

        public override double YieldStrenght => 300e6;

        public override double SpecificMass => 2700;
    }
}
