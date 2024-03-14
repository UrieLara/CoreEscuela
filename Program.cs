using CoreEscuela.App;
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
            //AppDomain.CurrentDomain.ProcessExit += AccionDelEvento; //se ejecuta cada vez que se termina el programa

            var engine = new EscuelaEngine();

            engine.Inicializar();

            var reporteador = new Reporteador(engine.GetDiccionarioObjetos());


            #region Interfaz
            Printer.WriteTitle("Bienvenidos a la Escuela");
            // Printer.Beep();
            ImprimirCursosEscuela(engine.Escuela);

            Printer.WriteTitle("MENÚ");
            WriteLine("Escriba el número del reporte que desea imprimir");
            WriteLine("1: Lista de Evaluaciones \n2: Lista de Promedios de Alumnos por Asignatura\n 3: Lista de mejores promedios");
            Printer.PresioneENTER();

            string opcion = ReadLine();
            string aux;

            switch (int.Parse(opcion))
            {
                case 1:
                    WriteLine("\nEscribe el número de evaluaciones que quieres ver");
                    aux = ReadLine();
                    reporteador.ImprimirListaEvaluaciones(int.Parse(aux));
                    
                    break;
                case 2:
                WriteLine("\nEscribe el número de promedios que quieres ver");
                    aux = ReadLine();
                    reporteador.ImprimirPromedios(reporteador.GetPromeAlumXAsig(), int.Parse(aux));
                    break;
                case 3:
                WriteLine("\nEscribe la cantidad de promedios que quieres ver");
                    aux = ReadLine();
                    reporteador.ImprimirPromedios(reporteador.MejoresPromedios(int.Parse(aux)));
                    break;
                default:
                    WriteLine("opción no válida");
                    break;
            }


            /*var diccionario = engine.GetDiccionarioObjetos();
                        engine.ImprimirDiccionario(diccionario);*/



            #endregion

            // engine.Escuela.LimpiarLugar();

            /*filtrar
            var listaObjetos = engine.GetObjetosEscuela();
            var listaLugar = from obj in listaObjetos
                             where obj is ILugar
                             select (ILugar)obj;*/
        }

        private static void ImprimirCursosEscuela(Escuela escuela)
        {
            Printer.WriteTitle("Cursos de la Escuela");

            foreach (var curso in escuela.Cursos)
            {
                WriteLine($"Nombre: {curso.Nombre}, Id: {curso.UniqueId}");
            }
        }

        private static void AccionDelEvento(object? sender, EventArgs e)
        {
            Printer.WriteTitle("Saliendo");
            Printer.Beep(3000, 1000, 2);
            Printer.WriteTitle("Chao!");
        }

    }
}



