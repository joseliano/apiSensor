using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleSensor
{
    class Program
    {
        static readonly GenericClient<Sensor> genericClient = new GenericClient<Sensor>();
        private static int   valorAelatorio(int min, int max) {
            Random randNum = new Random();
            return randNum.Next(min, max);
        }
        public static String GetTimestamp()
        { 
            Int32 unixTimestamp = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
             return Convert.ToString(unixTimestamp);
        }

        static void Main(string[] args)
        {

            string[] arPais = { "BR", "USA", "AF", "ZA", "AL", "DE", "AD", "AO", "AI", "AQ", "AG", "AN", "SA", "DZ", "AR", "AM", "AW", "AU", "AT", "AZ", "BS", "BD", "BB", "BH", "BY", "BE", "BZ", "BX", "BJ", "BM", "BO", "BA", "BW", "BG", "BN", "BF", "BI", "BT", "CV", "CM", "KH", "CA", "QA", "KZ", "TD", "GW", "GQ", "HT", "NL", "HN", "HK", "HU", "YE", "BV", "IM", "CX", "NF", "KY", "CC", "CK", "GG", "FO", "HM", "FK", "MP", "MH", "UM", "SB", "TC", "VG", "VI", "WF", "IN", "ID", "IR", "IQ", "IE", "IS", "IL", "ID", "IR", "IQ", "IE", "IS", "IL", "IT", "JM", "JP", "JE", "PW", "PA", "PG", "PK", "PY", "PE", "PN", "PF", "PL", "PR", "PT" };
            string[] arRegiao = { "sudeste", "norte", "nordeste", "sul", "centroOeste" };

            while (true)
            {
                int vlPais = valorAelatorio(1, 99);
                int vlRegiao = valorAelatorio(1, 5);
                int vlSensor = valorAelatorio(1, 10);
                int vlValor = valorAelatorio(1, 900);

                Sensor sensor = new Sensor
                {
                    TimesTamp = GetTimestamp(),
                    Tag = arPais[vlPais] + "." + arRegiao[vlRegiao] + ".sensor" + vlSensor, //"BR.sudeste.sensor01";
                    Valor = Convert.ToString(vlValor)
                };
                genericClient._Cadastrar(sensor);

                Console.WriteLine("Item Cadastrado" );
                Thread.Sleep(TimeSpan.FromSeconds(5));
            }; 

           

           
             

        }
      
    }
}
