using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App.Entities.Events.Listeners;

namespace App.Entities
{
    public class Documento
    {
        public event DespuesCalcular onDespuesCalcular;

        public string Numero { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        //Void es una función que no retorna valor
        //Operación
        public virtual void Calcular()
        {
            //Este método es para que los hijos puedan sobreescribir en los métodos.
            if (onDespuesCalcular != null)
            {
                onDespuesCalcular(this);
            }
        }
    }
}
