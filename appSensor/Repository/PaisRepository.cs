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
    public class PaisRepository : IEntity<PaisModels>
    {
        PaisModels paisRetorno  ;
        public PaisModels Add(PaisModels item)
        {
            try
            {
                using (SensorEntities _entity = new SensorEntities())
                {
                    Pais pais = new Pais
                    {
                        Id = item.Id,
                        Nome = item.Nome,
                        Ativo = "1"
                    };

                    _entity.Entry(pais).State = !_entity.Pais.Any(f => f.Id == item.Id) ? EntityState.Added : EntityState.Modified;
                    _entity.SaveChanges();
                    item.Id = pais.Id;
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
            DataTable dt = new DataTable();
            if (dtTable)
            {
                
                dt.Columns.Add("Pais", System.Type.GetType("System.String"));
                dt.Columns.Add("Valor", System.Type.GetType("System.Int32"));

            }

            using (SensorEntities _entity = new SensorEntities())
            {
                var lst = (from tp in _entity.Regiao
                           join sn in _entity.Sensor on
                             tp.Id equals sn.idRegiao
                           join pgn in _entity.Pais on
                            sn.idPais equals pgn.Id
                           join snv in _entity.Sensor_valor on
                            sn.Id equals snv.IdSensor                          
                           group tp by pgn.Nome into gr
                           select new { Nome = gr.Key, Valor = gr.Count() })
                           .ToList();

                
                if (dtTable)
                {

                    foreach (var value in lst)
                    {
                        DataRow dr = dt.NewRow();
                        dr["Pais"] = value.Nome;
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
                else
                {
                    iDados.Add(lst);
                }

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
                    count = _entity.Pais.Count();
                }

            }
            catch (Exception e)
            {
                throw new ArgumentNullException(e.Message, Mensagem.HOUVEUMERRONAEXECUCAO.GetStringValue());
            }
            return count;
        }

        public PaisModels Get(int id)
        {
            try
            {
                using (SensorEntities _entity = new SensorEntities())
                {
                    paisRetorno = (from tp in _entity.Pais
                                   select new { tp.Nome, tp.Id, tp.Ativo })
                                   .Select(tp => new PaisModels
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
            return paisRetorno;
        }

        public PaisModels Get(string nome)
        {
            try
            {
                using (SensorEntities _entity = new SensorEntities())
                {
                    paisRetorno = (from tp in _entity.Pais
                                   select new { tp.Nome, tp.Id, tp.Ativo })
                                   .Select(tp => new PaisModels
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
            return paisRetorno;
        }
    }
}