using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreEscuela.Entidades;

namespace CoreEscuela.Entidades
{
    public class Curso
    {
        public string UniqueId { get; private set; }
        public string Nombre { get; set; }
        public TiposJornada Jornada { get; set; }
        public List<Asignatura> Asignaturas{get; set;}
        public List<Alumno> Alumnos { get; set; }
        public List<Evaluaciones> Evaluaciones {get; set;}
        public Curso()=>
            UniqueId = Guid.NewGuid().ToString();
        
    }
}