using System;
using System.Collections.Generic;
using System.Text;

namespace IC_Vibracao.InputData.Barra.PropriedadesGeometricas
{
    public interface IPropriedadesGeometricas
    {
        double Area(double diametro, double espessura);

        double Area(double altura, double largura, double espessura);

        double MomentoInercia(double diametro, double espessura);

        double MomentoInercia(double altura, double largura, double espessura);
    }
}
