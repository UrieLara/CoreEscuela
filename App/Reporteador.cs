using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreEscuela.Entidades;
using System.Data;

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

        public Dictionary<string, IEnumerable<Object>> GetPromeAlumXAsig()
        {
            var respuesta = new Dictionary<string, IEnumerable<Object>>();
            var dicEvalXAsig = GetDicEvXAsig();

            foreach (var asigConEval in dicEvalXAsig)
            {
                var promedioAlumnos = 
                            from eval in asigConEval.Value
                            group eval by new {
                                eval.Alumno.UniqueId,
                                eval.Alumno.Nombre
                                }

                            into grupoEvalsAlumno
                            select new AlumnoPromedio
                            {
                                alumnoId = grupoEvalsAlumno.Key.UniqueId,
                                alumnoNombre = grupoEvalsAlumno.Key.Nombre,
                                promedio = grupoEvalsAlumno.Average(evaluacion => evaluacion.Nota)
                            };
                respuesta.Add(asigConEval.Key, promedioAlumnos);
            }

            return respuesta;
        }
    }
}