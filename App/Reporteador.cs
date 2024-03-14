using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreEscuela.Entidades;
using System.Data;
using System.Diagnostics;
using System.ComponentModel.Design;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using static System.Console;
using CoreEscuela.Util;

namespace CoreEscuela.App
{
    public class Reporteador
    {
        Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> _diccionario;
        public Reporteador(Dictionary<LlaveDiccionario, IEnumerable<ObjetoEscuelaBase>> dicObjEsc)
        {

            if (dicObjEsc == null)
                throw new ArgumentNullException(nameof(dicObjEsc));

            _diccionario = dicObjEsc;
        }

        public IEnumerable<Evaluacion> GetListaEvaluaciones()
        {

            if (_diccionario.TryGetValue(LlaveDiccionario.Evaluacion,
                                            out IEnumerable<ObjetoEscuelaBase> lista))
            { return lista.Cast<Evaluacion>(); }
            { return new List<Evaluacion>(); }
        }

        public void ImprimirListaEvaluaciones(int cant = 5)
        {
            var lista = GetListaEvaluaciones();
            int contador = 0;
            foreach (var obj in lista)
            {
                Printer.WriteTitle($"Alumno: {obj.Alumno}, Nota: {obj.Nota}");
                contador++;

                if (contador == cant)
                {
                    return;
                }
            }
        }

        public IEnumerable<string> GetListaAsignaturas()
        {

            return GetListaAsignaturas(out var dummy);
        }

        public IEnumerable<string> GetListaAsignaturas(out IEnumerable<Evaluacion> listaEvaluaciones)
        {
            listaEvaluaciones = GetListaEvaluaciones();

            return (from Evaluacion ev in listaEvaluaciones
                    select ev.Asignatura.Nombre).Distinct();
        }

        public Dictionary<string, IEnumerable<Evaluacion>> GetDicEvXAsig()
        {
            var dicRta = new Dictionary<string, IEnumerable<Evaluacion>>();

            var listAsig = GetListaAsignaturas(out var listaEvaluaciones);

            foreach (var asig in listAsig)
            {
                var evalsAsig = from eval in listaEvaluaciones
                                where eval.Asignatura.Nombre == asig
                                select eval;

                dicRta.Add(asig, evalsAsig);
            }

            return dicRta;
        }

        public Dictionary<string, IEnumerable<AlumnoPromedio>> GetPromeAlumXAsig()
        {
            var respuesta = new Dictionary<string, IEnumerable<AlumnoPromedio>>();
            var dicEvalXAsig = GetDicEvXAsig();

            foreach (var asigConEval in dicEvalXAsig)
            {
                var promedioAlumnos =
                            from eval in asigConEval.Value
                            group eval by new
                            {
                                eval.Alumno.UniqueId,
                                eval.Alumno.Nombre
                            }

                            into grupoEvalsAlumno
                            select new AlumnoPromedio
                            {
                                alumnoId = grupoEvalsAlumno.Key.UniqueId,
                                alumnoNombre = grupoEvalsAlumno.Key.Nombre,
                                promedio = (float)Math.Round(grupoEvalsAlumno.Average(evaluacion => evaluacion.Nota), 2)

                            };
                respuesta.Add(asigConEval.Key, promedioAlumnos);
            }

            return respuesta;
        }


        public Dictionary<string, IEnumerable<AlumnoPromedio>> MejoresPromedios(int top)
        {

            var respuesta = new Dictionary<string, IEnumerable<AlumnoPromedio>>();
            var promediosGenerales = GetPromeAlumXAsig();

            foreach (var asignatura in promediosGenerales)
            {
                var topProm =
                        (from asig in asignatura.Value
                         orderby asig.promedio descending
                         select asig).Take(top);

                respuesta.Add(asignatura.Key, topProm);
            }

            return respuesta;

        }

        public void ImprimirPromedios(Dictionary<string, IEnumerable<AlumnoPromedio>> dic, int cant = 2)
        {
            int contador = 0;
            
            foreach (var obj in dic)
            {   
                Printer.WriteTitle($"{obj.Key.ToString()}");

                foreach (var item in obj.Value)
                {
                    Console.WriteLine($"Id: {item.alumnoId}\n Nombre: {item.alumnoNombre}, Promedio: {item.promedio}\n");
                    
                    contador++;

                    if (contador == cant)
                    {
                        contador = 0;
                        goto Siguiente;
                    }
                    
                }
                
                Siguiente: continue;

            }

        }

        public void ImprimirPromedios(Dictionary<string, IEnumerable<AlumnoPromedio>> dic)
        {

            foreach (var obj in dic)
            {
                Printer.WriteTitle($"{obj.Key.ToString()}");

                foreach (var item in obj.Value)
                {
                    Console.WriteLine($"Id: {item.alumnoId}\n Nombre: {item.alumnoNombre}, Promedio: {item.promedio}\n");
                }
            }

        }

        public void PrimerConsola()
        {
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
                catch (ArgumentOutOfRangeException arge)
                {
                    WriteLine(arge.Message);
                    WriteLine("Saliendo...");

                }
                catch (Exception)
                {
                    WriteLine("El valor de la nota no es un número válido");
                    WriteLine("Saliendo...");
                }
                finally
                {
                    Printer.WriteTitle("Finally");
                }

            }
        }



    }


}