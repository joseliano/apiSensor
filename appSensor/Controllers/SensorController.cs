using appSensor.Models;
using appSensor.Repository;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace appSensor.Controllers
{
   // [ApiController]
  //  public class SensorController : ControllerBase
    public class SensorController : ApiController
    { 
        static readonly IEntity<SensorValorModels> sSensorValor = new SensorValorRepository();
        static readonly IEntity<SensorModels> sSensor = new SensorRepository();
        static readonly IEntity<PaisModels> sPais = new PaisRepository();
        static readonly IEntity<RegiaoModels> sRegiao = new RegiaoRepository();

        // GET: api/Sensor
        public IEnumerable<string> Get()
        {
            PaisModels pais = new PaisModels();
            pais.Nome = "USA";
            sPais.Add(pais);
            return new string[] { "value1", "value2" };
        } 

        // GET: api/Sensor/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Sensor
        [HttpPost]
        [Route("api/Sensor/AddSensor")]
        public void Post([FromBody]  appSensor.Util.Sensor form)
        {
            char[] delimiterChars = { '.' };

            string[] arTag = form.Tag.Split(delimiterChars);
            if (arTag.Length < 3 || arTag.Length > 3)
                throw new Exception("Tag inválida");

            if (form.TimesTamp == null || form.TimesTamp == "")
                throw new Exception("Tag inválida");

            if (form.Valor == null || form.Valor == "")
                throw new Exception("Tag inválida");

            PaisModels pais;
            pais = sPais.Get(arTag[0]);
            if (pais == null)
                pais = sPais.Add(new PaisModels(arTag[0]));

            RegiaoModels  regiao;
            regiao = sRegiao.Get(arTag[1]);
            if (regiao == null)
                regiao = sRegiao.Add(new RegiaoModels(arTag[1]));

            SensorModels  sensor;
            sensor = sSensor.Get(arTag[2]);
            if (sensor == null)
                sensor = sSensor.Add(new SensorModels(arTag[2], pais, regiao));

            SensorValorModels sensorValorModels = new SensorValorModels
            {
                Timestamp = (long)Convert.ToDouble(form.TimesTamp),
                Valor = Convert.ToInt32(form.Valor) ,
                SensorModels = sensor
            };
            var retorno = sSensorValor.Add(sensorValorModels);


        }

        // PUT: api/Sensor/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Sensor/5
        public void Delete(int id)
        {
        } 
    }
}
