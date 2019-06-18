using appSensor.DataModel;
using appSensor.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using static appSensor.Util.Mensagens;

namespace appSensor.Repository
{
    public class RegiaoRepository : IEntity<RegiaoModels>
    {
        RegiaoModels regiaoRetorno;

        public List<object> ListAll(Boolean dtTable = true)
        {
            List<object> iDados = new List<object>();

            DataTable dt = new DataTable();
            dt.Columns.Add("Regiao", System.Type.GetType("System.String"));
            dt.Columns.Add("Valor", System.Type.GetType("System.Int32"));

            using (SensorEntities _entity = new SensorEntities())
            {
                var lst = (from tp in _entity.Regiao
                           join sn in _entity.Sensor on
                             tp.Id equals sn.idRegiao
                           join snv in _entity.Sensor_valor on
                            sn.Id equals snv.IdSensor
                           group tp by tp.Nome into gr
                           select new { Nome = gr.Key, Valor = gr.Count() })
                           .ToList();

                foreach (var value in lst)
                {
                    DataRow dr = dt.NewRow();
                    dr["Regiao"] =  value.Nome;
                    dr["Valor"] = value.Valor;
                    dt.Rows.Add(dr);
                }

                foreach (DataColumn dc in dt.Columns)
                {
                    List<object> x = new List<object>();
                    x = (from DataRow drr in dt.Rows select drr[dc.ColumnName]).ToList();
                    iDados.Add(x);
                }


            }


            return iDados;
        }
        public RegiaoModels Add(RegiaoModels item)
        {
            try
            {
                using (SensorEntities _entity = new SensorEntities())
                {
                    Regiao regiao = new Regiao
                    {
                        Id = item.Id,
                        Nome = item.Nome,
                        Ativo = "1"
                    };

                    _entity.Entry(regiao).State = !_entity.Regiao.Any(f => f.Id == item.Id) ? EntityState.Added : EntityState.Modified;
                    _entity.SaveChanges();
                    item.Id = regiao.Id;
                }
            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message, Mensagem.HOUVEUMERRONAEXECUCAO.GetStringValue());
            }
            return item;
        }

        public long Count()
        {
            int count = 0;
            try
            {
                using (SensorEntities _entity = new SensorEntities())
                {
                    count = _entity.Regiao.Count();
                }

            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message, Mensagem.HOUVEUMERRONAEXECUCAO.GetStringValue());
            }
            return count;
        }

        public RegiaoModels Get(int id)
        {
            try
            {
                using (SensorEntities _entity = new SensorEntities())
                {
                    regiaoRetorno = (from tp in _entity.Regiao
                                     select new { tp.Nome, tp.Id, tp.Ativo })
                                   .Select(tp => new RegiaoModels
                                   {
                                       Id = tp.Id,
                                       Nome = tp.Nome
                                   })
                                  .Where(wh => wh.Id == id)
                                  .FirstOrDefault();
                }

            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message, Mensagem.HOUVEUMERRONAEXECUCAO.GetStringValue());
            }
            return regiaoRetorno;
        }

        public RegiaoModels Get(string nome)
        {
            try
            {
                using (SensorEntities _entity = new SensorEntities())
                {
                    regiaoRetorno = (from tp in _entity.Regiao
                                     select new { tp.Nome, tp.Id, tp.Ativo })
                                   .Select(tp => new RegiaoModels
                                   {
                                       Id = tp.Id,
                                       Nome = tp.Nome
                                   })
                                   .Where(wh => wh.Nome.Contains(nome == "" ? wh.Nome : nome))
                                   .FirstOrDefault();
                }

            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message, Mensagem.HOUVEUMERRONAEXECUCAO.GetStringValue());
            }
            return regiaoRetorno;
        }
    }
}