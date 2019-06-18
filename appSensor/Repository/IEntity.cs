using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appSensor.Repository
{
    public interface IEntity<T>
    {
        T Get(int id);//  recupera pelo id do registro
        T Get(String nome); //  recupera pelo nome do registro
        T Add(T item); //  adiciona um novo registor
        List<object> ListAll(Boolean dtTable = true);
        long Count();//  total de registro

        /*
         *'Não adicionei porque a rotina no momento só adiciona registro 
         T Remove(T item);
        T Update(T item);
        
        */
    }
}