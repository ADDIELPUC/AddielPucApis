using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parcial2Listas
{
    public class Alumno
    {
        public string name;
        public double calif1;
        public double calif2;
        public double calif3;
        public double promedio;

        public Alumno(string Name, double Calif1, double Calif2, double Calif3, double Promedio) 
        {
            name = Name;
            calif1 = Calif1;
            calif2 = Calif2;
            calif3 = Calif3;
            promedio = Promedio;
        }
    }
}