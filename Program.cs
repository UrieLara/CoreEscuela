using CoreEscuela.Entidades;
using CoreEscuela.Util;
using static System.Console;

//Alt-Shif+F - Formatear
namespace CoreEscuela
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new EscuelaEngine();
            engine.Inicializar();
            
            Printer.WriteTitle("Bienvenidos a la Escuela");
            Printer.Beep();
            ImprimirCursosEscuela(engine.Escuela);

        }

        private static void ImprimirCursosEscuela(Escuela escuela)
        {
            Printer.WriteTitle("Cursos de la Escuela");

            foreach (var curso in escuela.Cursos)
            {
                WriteLine($"Nombre: {curso.Nombre}, Id: {curso.UniqueId}");
            }

            
            foreach (var curso in escuela.Cursos)
            {   
                foreach (var ev in curso.Evaluaciones)
                {
                    if(curso.Nombre == "101"){
                        Printer.DibujarLinea(20);
                        WriteLine($"Asignatura: {ev.Asignatura.Nombre}, Nombre: {ev.Nombre}, Alumno: {ev.Alumno.Nombre}, Nota: {ev.Nota}");
                    }
                   
                }
            }
        }
    }
}



