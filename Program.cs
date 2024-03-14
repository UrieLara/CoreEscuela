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
            var evaluacionesLista = reporteador.GetListaEvaluaciones();

            //Printer.WriteTitle("Bienvenidos a la Escuela");
            // Printer.Beep();
            // ImprimirCursosEscuela(engine.Escuela);

            // engine.Escuela.LimpiarLugar();

            var listaObjetos = engine.GetObjetosEscuela();

            //filtrar
            var listaLugar = from obj in listaObjetos
                             where obj is ILugar
                             select (ILugar)obj;



            //var diccionario = engine.GetDiccionarioObjetos();
            //engine.ImprimirDiccionario(diccionario);

            var listaPromXAsig = reporteador.GetPromeAlumXAsig();
            var alumnosMejoresPromedios = reporteador.MejoresPromedios(2);
            //reporteador.ImprimirMejoresPromedios(alumnosMejoresPromedios);

            #region Interfaz

            Printer.WriteTitle("Captura de una Evaluación por Consola");
            var newEval = new Evaluacion();
            string nombre, notastring;

            WriteLine("Ingrese el nombre de la Evaluacion,");
            Printer.PresioneENTER();
            nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                WriteLine("El valor del nombre no debe estar vacío");
                WriteLine("Saliendo...");
            }
            else
            {
                newEval.Nombre = nombre.ToLower();
                WriteLine("Nombre guardado correctamente\n");
            }


            WriteLine("Ingrese la nota de la Evaluacion,");
            Printer.PresioneENTER();
            notastring = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(notastring))
            {
                WriteLine("El valor de la nota no debe estar vacío");
                WriteLine("Saliendo...");
            }
            else
            {
                try
                {
                    newEval.Nota = float.Parse(notastring);

                    if (newEval.Nota < 0 || newEval.Nota > 5)
                    {
                        throw new ArgumentOutOfRangeException("La nota debe estar entre 0 y 5");
                    }

                    WriteLine("Nota guardada correctamente");
                }
                catch (ArgumentOutOfRangeException arge){
                    WriteLine(arge.Message);
                    WriteLine("Saliendo...");

                }
                catch(Exception)
                {
                    WriteLine("El valor de la nota no es un número válido");
                    WriteLine("Saliendo...");
                }
                finally{
                    Printer.WriteTitle("Finally");
                }

            }

            #endregion

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



