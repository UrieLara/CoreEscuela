using CoreEscuela.Entidades;
using Etapa4.Entidades;
using static System.Console;

namespace CoreEscuela.Util {

    public static class Printer{
        //no permite crear nuevas instancias

        public static void DrawLine(int tam = 10){
            
            WriteLine("".PadLeft(tam, '=')); //Pad = rellenar
        }


        public static void WriteTitle(string titulo){
            
            var tam = titulo.Length+4;
            DrawLine(tam);
            WriteLine($"| {titulo} |");
            DrawLine(tam);
        }

        public static void Beep(int hz=2000, int tiempo=500, int cantidad=1){

            while(cantidad-->0){
                Console.Beep(hz, tiempo);

            }
        }
    }
}