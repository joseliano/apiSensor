using appSensor.DataModel;
using appSensor.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using static appSensor.Util.Mensagens;

namespace appSensor.Repository
{
    public class SensorRepository : IEntity<SensorModels>
    {
        SensorModels sensorRetorno;
        public SensorModels Add(SensorModels item)
        {
            try
            {
                using (SensorEntities _entity = new SensorEntities())
                {
                    Sensor sensor = new Sensor
                    {
                        Id = item.Id,
                        Nome = item.Nome,
                        idPais = item.PaisModels.Id,
                        idRegiao = item.RegiaoModels.Id
                    };

                    _entity.Entry(sensor).State = !_entity.Sensor.Any(f => f.Id == item.Id) ? EntityState.Added : EntityState.Modified;
                    _entity.SaveChanges();
                    item.Id = sensor.Id;
                }
            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message, Mensagem.HOUVEUMERRONAEXECUCAO.GetStringValue());
            }
            return item;
        }

        public List<object> ListAll(Boolean dtTable = true)
        {
            List<object> iDados = new List<object>();
         
            using (SensorEntities _entity = new SensorEntities())
            {
                var lst = (from sn in _entity.Sensor 
                           join snv in _entity.Sensor_valor on
                            sn.Id equals snv.IdSensor
                           group sn by sn.Nome into gr
                           select new { Nome = gr.Key, Valor = gr.Sum(s=> s.Sensor_valor.Sum(x=>x.Valor))})
                          .Select( gr=> new 
                            {
                              Nome =  gr.Nome,
                              Valor = gr.Valor
                            })
                           .ToList();

                iDados.Add(lst);
            }
          return iDados;
        }
        public long Count()
        {
            int count = 0;
            try
            {
                using (SensorEntities _entity = new SensorEntities())
                {
                    count = _entity.Sensor.Count();
                }

            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message, Mensagem.HOUVEUMERRONAEXECUCAO.GetStringValue());
            }
            return count;
        }

        public SensorModels Get(int id)
        {
            try
            {
                using (SensorEntities _entity = new SensorEntities())
                {
                    sensorRetorno = (from tp in _entity.Sensor
                                     join rg in _entity.Regiao on
                                        tp.idRegiao equals rg.Id
                                     join pg in _entity.Pais on
                                        tp.idPais equals pg.Id
                                     where (tp.Id == id)
                                     select new
                                     {
                                         valSensor = tp,
                                         valRegiao = rg,
                                         valPais = pg
                                     })
                                    .AsEnumerable()
                                    .Select(tp => new SensorModels
                                    {
                                        Id = tp.valSensor.Id,
                                        Nome = tp.valSensor.Nome,
                                        PaisModels = new PaisModels(tp.valPais.Id, tp.valPais.Nome),
                                        RegiaoModels = new RegiaoModels(tp.valRegiao.Id, tp.valRegiao.Nome)
                                    })
                                    .FirstOrDefault();
                }

            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message, Mensagem.HOUVEUMERRONAEXECUCAO.GetStringValue());
            }
            return sensorRetorno;
        }

        public SensorModels Get(string nome)
        {
            try
            {
                using (SensorEntities _entity = new SensorEntities())
                {

                    sensorRetorno = (from tp in _entity.Sensor
                                     join rg in _entity.Regiao on
                                        tp.idRegiao equals rg.Id
                                     join pg in _entity.Pais on
                                        tp.idPais equals pg.Id
                                     where (tp.Nome.Contains(nome == "" ? tp.Nome : nome))
                                     select new
                                     {
                                         valSensor = tp,
                                         valRegiao = rg,
                                         valPais = pg
                                     })
                                   .AsEnumerable()
                                   .Select(tp => new SensorModels
                                   {
                                       Id = tp.valSensor.Id,
                                       Nome = tp.valSensor.Nome,
                                       PaisModels = new PaisModels(tp.valPais.Id, tp.valPais.Nome),
                                       RegiaoModels = new RegiaoModels(tp.valRegiao.Id, tp.valRegiao.Nome)
                                   }) 
                                   .FirstOrDefault();
                }

            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message, Mensagem.HOUVEUMERRONAEXECUCAO.GetStringValue());
            }
            return sensorRetorno;
        }
    }
}