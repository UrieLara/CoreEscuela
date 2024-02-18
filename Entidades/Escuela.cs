
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;

namespace CoreEscuela.Entidades
{
    public class Escuela
    {
        string nombre;

        //Encapsulamiento
        /*Sirve para poder usar los atributos de la clase y modificar el código para obtener o pedir 
        los valores del atributo sin modificarlo directamente*/
        public string Nombre{
            get{return nombre;}
            set{nombre = value.ToUpper();}
        }

        public string UniqueId = Guid.NewGuid().ToString();

        //Esto es lo mismo pero más cortito
        public int AñoDeCreacion {get;set;}

        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public TipoEscuela TipoEscuela {get;set;}

        public List<Curso> Cursos {get; set;}
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
    }
}