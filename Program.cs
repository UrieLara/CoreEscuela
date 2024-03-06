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



            var diccionario = engine.GetDiccionarioObjetos();
            engine.ImprimirDiccionario(diccionario);
            
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



