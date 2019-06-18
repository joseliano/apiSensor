using appSensor.DataModel;
using appSensor.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using static appSensor.Util.Mensagens;

namespace appSensor.Repository
{
    public class SensorValorRepository : IEntity<SensorValorModels>
    {
      
        public SensorValorModels Add(SensorValorModels item)
        {
            try
            {
                using (SensorEntities _entity = new SensorEntities())
                {
                    Sensor_valor sensorValor = new Sensor_valor
                    {
                       Id = item.Id,
                      IdSensor = item.SensorModels.Id,
                      TimesTamp = item.Timestamp,
                      Valor = item.Valor,
                      DataCadastrado = DateTime.Now
                    };
                    _entity.Entry(sensorValor).State = !_entity.Sensor.Any(f => f.Id == item.Id) ? EntityState.Added : EntityState.Modified;
                    _entity.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message, Mensagem.HOUVEUMERRONAEXECUCAO.GetStringValue());
            }
            return item;
        }
        public List<object>  ListAll(Boolean dtTable = true)
        {
           
            return null; //lst.ToArray();
        }
        public long Count()
        {
            int count = 0;
            try
            {
                using (SensorEntities _entity = new SensorEntities())
                {
                    count = _entity.Sensor_valor.Count();
                }

            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message, Mensagem.HOUVEUMERRONAEXECUCAO.GetStringValue());
            }
            return count;
        }

        public SensorValorModels Get(int id)
        {
            return null;
        }

        public SensorValorModels Get(string nome)
        {
            return null;
        }
    }
}