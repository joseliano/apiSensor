using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace appSensor.Models
{
    public class SensorModels 
    {
        public SensorModels() { }
        public SensorModels(int pId, String pNome)
        {
            this.Id = pId;
            this.Nome = pNome;
        }
        public SensorModels(String pNome, PaisModels pPaisModels, RegiaoModels pRegiaoModels)
        {
            this.Nome = pNome;
            this.PaisModels = pPaisModels;
            this.RegiaoModels = pRegiaoModels;
        }
        public int Id { get; set; }
        public String Nome { get; set; }

        public virtual PaisModels PaisModels { get; set; }
        public virtual RegiaoModels RegiaoModels{ get; set; }
    }


    public class SensorValorModels 
    {
        public int Id { get; set; }
        public SensorModels SensorModels{ get; set; }
        public long Timestamp { get; set; }
        public long Valor { get; set; }
    }


    public class RegiaoModels 
    {
        public RegiaoModels() { }
        public RegiaoModels(int pId, String pNome)
        {
            this.Id = pId;
            this.Nome = pNome;
        }
        public RegiaoModels(String pNome)
        {
            this.Nome = pNome;
        }
        public int Id { get; set; }
        public String Nome { get; set; }
        public string Ativo { get; set; }
    }

    public class PaisModels
    {
        public PaisModels() { }
        public PaisModels(int pId, String pNome)
        {
            this.Id = pId;
            this.Nome = pNome;
        }
        public PaisModels(String pNome)
        {
            this.Nome = pNome;
        }
        public int Id { get; set; }
        public string Nome { get; set; }
           public string Ativo { get; set; }
    }
}