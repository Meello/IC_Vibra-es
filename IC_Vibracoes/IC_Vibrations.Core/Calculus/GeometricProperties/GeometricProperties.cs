using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibration.Calculus.GeometricProperties
{
    public class GeometricProperties : IGeometricProperties
    {
        public double Area(double diametro, double espessura)
        {
            double d = diametro;
            double t = espessura;
        
            double area = (Math.PI / 4) * (Math.Pow(d, 2) - Math.Pow((d - 2 * t), 2));
            
            return area;
        }
        
        public double Area(double altura, double largura, double espessura)
        {
            double a = altura;
            double b = largura;
            double t = espessura;
            
            double area = (a * b) - ((a - 2 * t) * (a - 2 * t));
        
            return area;
        }
        
        public double MomentInertia(double diametro, double espessura)
        {
            double d = diametro;
            double t = espessura;
        
            double momentoInercia = (Math.PI / 64) * (Math.Pow(d, 4) - Math.Pow((d - 2 * t), 4));
        
            return momentoInercia;
        }
        
        public double MomentInertia(double altura, double largura, double espessura)
        {
            double a = altura;
            double b = largura;
            double t = espessura;
        
            double momentoInercia = (Math.Pow(a, 3) * b - (Math.Pow((a - 2 * t), 3) * (b - 2 * t))) / 12;
        
            return momentoInercia;
        }
    }
}
