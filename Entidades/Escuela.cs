
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using CoreEscuela.Util;

namespace CoreEscuela.Entidades
{
    public class Escuela: ObjetoEscuelaBase, ILugar
    {
        public int AñoDeCreacion {get;set;}

        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public TipoEscuela TipoEscuela {get;set;}
        public List<Curso> Cursos {get; set;}

        public string Direccion { get; set; }

        public override string ToString()
        {
            return $"Nombre: \"{Nombre}\", Tipo: {TipoEscuela} \n Pais: {Pais}, Ciudad: {Ciudad}";
            //para colocar caracteres se puede usar \" \" <- dibuja las comillas
            // {System.Enviroment.NewLine} es lo mismo que \n
        }

        /*
        //Esto es un constructor: 
        public Escuela(string nombre, int año){

            this.nombre = nombre;
            AñoDeCreacion = año;
        }*/

        //Esto es otra manera de escribir un constructor con duplas
        //public Escuela(string nombre, int año) => (Nombre, AñoDeCreacion) = (nombre, año);

        //para definir un parámetro opcional se debe inicializar en el constructor
        public Escuela(string nombre, int año, 
                        TipoEscuela tipo, 
                        string pais="", string ciudad="") {

                    (Nombre, AñoDeCreacion) = (nombre, año);
                    Pais = pais;
                    Ciudad = ciudad;
                        } 

        public void LimpiarLugar(){

            Printer.DrawLine();
            Console.WriteLine("Limpiando Escuela...");
            foreach(var curso in Cursos){
                curso.LimpiarLugar();
            }
            Console.WriteLine($"Escuela limpia");
            
        }
    }
}