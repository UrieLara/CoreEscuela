
using System.Reflection.Metadata.Ecma335;
using CoreEscuela.Entidades;
using System.Linq;
using CoreEscuela.Util;

namespace CoreEscuela.App
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

        #region Metodos de Carga
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
        #endregion

        #region Listados
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

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoEvaluaciones,
            out int conteoAlumnos,
            out int conteoAsignaturas,
            out int conteoCursos,
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traeAsignaturas = true,
            bool traeCursos = true
            )
        {
            conteoCursos = Escuela.Cursos.Count;
            conteoEvaluaciones = conteoAsignaturas = conteoAlumnos = 0;

            var listaObj = new List<ObjetoEscuelaBase>();
            listaObj.Add(Escuela);

            if (traeCursos)
            {
                listaObj.AddRange(Escuela.Cursos);
            }

            foreach (var curso in Escuela.Cursos)
            {

                conteoAsignaturas += curso.Asignaturas.Count;
                conteoAlumnos += curso.Alumnos.Count;

                if (traeAsignaturas) { listaObj.AddRange(curso.Asignaturas); }
                if (traeAlumnos) { listaObj.AddRange(curso.Alumnos); }

                if (traeEvaluaciones)
                {
                    foreach (var alumno in curso.Alumnos)
                    {
                        listaObj.AddRange(alumno.Evaluaciones);
                        conteoEvaluaciones += alumno.Evaluaciones.Count;
                    }
                }
            }

            return listaObj;
        }


        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
                    bool traeEvaluaciones = true,
                    bool traeAlumnos = true,
                    bool traeAsignaturas = true,
                    bool traeCursos = true
                    )
        {

            return GetObjetosEscuela(out int dummy, out dummy, out dummy, out dummy);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
                    out int conteoEvaluaciones,
                    bool traeEvaluaciones = true,
                    bool traeAlumnos = true,
                    bool traeAsignaturas = true,
                    bool traeCursos = true
                    )
        {
            return GetObjetosEscuela(out conteoEvaluaciones, out int dummy, out dummy, out dummy);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoEvaluaciones,
            out int conteoAlumnos,
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traeAsignaturas = true,
            bool traeCursos = true
            )
        {
            return GetObjetosEscuela(out conteoEvaluaciones, out conteoAlumnos, out int dummy, out dummy);
        }

        public IReadOnlyList<ObjetoEscuelaBase> GetObjetosEscuela(
            out int conteoEvaluaciones,
            out int conteoAlumnos,
            out int conteoAsignaturas,
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traeAsignaturas = true,
            bool traeCursos = true
            )
        {
            return GetObjetosEscuela(out conteoEvaluaciones, out conteoAlumnos, out conteoAsignaturas, out int dummy);
        }

        #endregion

        public Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> GetDiccionarioObjetos()
        {

            var diccionario = new Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>>();

            diccionario.Add(LlaveDiccionario.Escuela, new[] {Escuela});
            diccionario.Add(LlaveDiccionario.Curso, Escuela.Cursos.Cast<ObjetoEscuelaBase>());

            var listatempAsig = new List<Asignatura>();
            var listatempAl = new List<Alumno>();
            var listatempEv = new List<Evaluacion>();

            foreach (var curso in Escuela.Cursos)
            {
                listatempAsig.AddRange(curso.Asignaturas);
                listatempAl.AddRange(curso.Alumnos);

                foreach (var alumno in curso.Alumnos)
                {
                    listatempEv.AddRange(alumno.Evaluaciones);
                }
            }

            diccionario.Add(LlaveDiccionario.Asignatura, listatempAsig.Cast<ObjetoEscuelaBase>());
            diccionario.Add(LlaveDiccionario.Alumno, listatempAl.Cast<ObjetoEscuelaBase>());
            diccionario.Add(LlaveDiccionario.Evaluacion, listatempEv.Cast<ObjetoEscuelaBase>());

            return diccionario;
        }

        public void ImprimirDiccionario(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dic)
        {
            bool ImprimirCur = true,
                ImprimirAl = false,
                ImprimirAsig = false,
                ImprimirEv = false;

            foreach (var obj in dic)
            {


                if (obj.Key == LlaveDiccionario.Curso && !ImprimirCur)
                {
                    continue;
                }
                
                if (obj.Key == LlaveDiccionario.Alumno && !ImprimirAl)
                {
                    continue;
                }

                if (obj.Key == LlaveDiccionario.Asignatura && !ImprimirAsig)
                {
                    continue;
                }

                if (obj.Key == LlaveDiccionario.Evaluacion && !ImprimirEv)
                {
                    continue;
                }

                int contador = obj.Value.Count();
                Printer.WriteTitle($"{obj.Key.ToString()}: {contador}");

                foreach (var item in obj.Value)
                {
                    Console.WriteLine(item);

                    if(ImprimirCur){
                        Console.WriteLine($"{item}"); 
                    }
                }
            }

        }
    }

}



