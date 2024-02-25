
using System.Reflection.Metadata.Ecma335;
using CoreEscuela.Entidades;
using System.Linq;
using CoreEscuela.Util;

namespace CoreEscuela
{

    public class EscuelaEngine
    {
        public Escuela Escuela { get; set; }

        public void Inicializar()
        {
            Escuela = new Escuela("Platzi Academy", 2012, TipoEscuela.Primaria,
                                         ciudad: "Bogota", pais: "Colombia");

            CargarCursos();
            CargarAsignaturas();
            CargarEvaluaciones();

        }

        private void CargarCursos()
        {
            Escuela.Cursos = new List<Curso>(){
                new Curso() {Nombre = "101", Jornada = TiposJornada.Mañana},
                new Curso() {Nombre = "201", Jornada = TiposJornada.Mañana},
                new Curso() {Nombre = "301", Jornada = TiposJornada.Mañana},
                new Curso() {Nombre = "401", Jornada = TiposJornada.Tarde},
                new Curso() {Nombre = "501", Jornada = TiposJornada.Tarde}
            };

            foreach (var curso in Escuela.Cursos)
            {
                Random rnd = new Random();
                curso.Alumnos = GenerarAlumnosAlAzar(rnd.Next(5, 20));
            }
        }

        private void CargarEvaluaciones()
        {
            foreach (var curso in Escuela.Cursos)
            {
                foreach (var alumno in curso.Alumnos)
                {
                    var asignaturas = curso.Asignaturas;

                    for (int i = 1; i < 6; i++)
                    {
                        var listaEvaluaciones = from asig in asignaturas
                                                    //from al in alumnos
                                                select new Evaluacion
                                                {
                                                    Nombre = $"Parcial {i}",
                                                    Alumno = alumno,
                                                    Asignatura = asig,
                                                    Nota = GenerarNotaRandom()
                                                };

                        alumno.Evaluaciones.AddRange(listaEvaluaciones.ToList());
                    }
                }
            }
        }

        private float GenerarNotaRandom()
        {
            Random r = new Random();
            float nota = (float)Math.Round(r.NextDouble() * 5, 1);
            return nota;
        }

        private void CargarAsignaturas()
        {
            foreach (var curso in Escuela.Cursos)
            {
                var listaAsignaturas = new List<Asignatura>(){
                    new Asignatura{Nombre="Matematicas"},
                    new Asignatura{Nombre="Educación Fisica"},
                    new Asignatura{Nombre="Castellano"},
                    new Asignatura{Nombre="Ciencias Naturales"}
                };
                curso.Asignaturas = listaAsignaturas;
            }
        }

        private List<Alumno> GenerarAlumnosAlAzar(int cantidad)
        {
            string[] nombre1 = { "Juanita", "Menganita", "Mafalda", "Perenganito", "Juan", "Estebio", "Eluri" };
            string[] nombre2 = { "Margarita", "Flavia", "Susanita", "Picoles", "Luis", "Secundo", "Elara" };
            string[] apellido = { "Lopez", "Urrega", "Quino", "Kokol", "Mandio", "Santos", "Castro" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido
                               select new Alumno { Nombre = $"{n1} {n2} {a1}" };

            return listaAlumnos.OrderBy((al) => al.UniqueId).Take(cantidad).ToList();
        }


        public List<ObjetoEscuelaBase> GetObjetosEscuela()
        {
            var listaObj = new List<ObjetoEscuelaBase>();
            listaObj.Add(Escuela);
            listaObj.AddRange(Escuela.Cursos);
            
            foreach (var curso in Escuela.Cursos)
            {
                listaObj.AddRange(curso.Asignaturas);
                listaObj.AddRange(curso.Alumnos);

                foreach (var alumno in curso.Alumnos)
                {
                    listaObj.AddRange(curso.Asignaturas);
                }
            }

            return listaObj;
        }
    }
}


