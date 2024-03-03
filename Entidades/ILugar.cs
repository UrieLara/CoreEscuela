using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreEscuela.Entidades
{
    public interface ILugar
    {
         string Direccion { get; set; }
         void LimpiarLugar();
    }
}