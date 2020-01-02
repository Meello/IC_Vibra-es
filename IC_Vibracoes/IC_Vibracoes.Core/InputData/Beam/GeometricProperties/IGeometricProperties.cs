using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao.InputData.Beam.GeometricProperties
{
    public interface IGeometricProperties
    {
        double Area(double diametro, double espessura);

        double Area(double altura, double largura, double espessura);

        double MomentInertia(double diametro, double espessura);

        double MomentInertia(double altura, double largura, double espessura);
    }
}
