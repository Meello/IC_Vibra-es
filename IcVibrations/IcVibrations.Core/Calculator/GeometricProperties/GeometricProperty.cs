using System;
using System.Collections.Generic;
using System.Text;

namespace IcVibrations.Calculator.GeometricProperties
{
    public class GeometricProperty : IGeometricProperty
    {
        public double Area(double diameter, double thickness)
        {
            double d = diameter;
            double t = thickness;
        
            double area = (Math.PI / 4) * (Math.Pow(d, 2) - Math.Pow((d - 2 * t), 2));
            
            return area;
        }
        
        public double Area(double height, double width, double thickness)
        {
            double a = height;
            double b = width;
            double t = thickness;
            
            double area = (a * b) - ((a - 2 * t) * (a - 2 * t));
        
            return area;
        }
        
        public double MomentInertia(double diameter, double thickness)
        {
            double d = diameter;
            double t = thickness;
        
            double momentoInercia = (Math.PI / 64) * (Math.Pow(d, 4) - Math.Pow((d - 2 * t), 4));
        
            return momentoInercia;
        }
        
        public double MomentInertia(double height, double width, double thickness)
        {
            double a = height;
            double b = width;
            double t = thickness;
        
            double momentoInercia = (Math.Pow(a, 3) * b - (Math.Pow((a - 2 * t), 3) * (b - 2 * t))) / 12;
        
            return momentoInercia;
        }
    }
}
