using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Calculator.GeometricProperties
{
    public interface IGeometricProperties
    {
        double Area(double diameter, double thickness);

        double Area(double height, double width, double thickness);

        double MomentInertia(double diameter, double thickness);

        double MomentInertia(double height, double width, double thickness);

        //double StaticalMomentArea(double diameter, double thickness);
    }
}
