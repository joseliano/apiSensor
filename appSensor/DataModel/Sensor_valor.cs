//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace appSensor.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sensor_valor
    {
        public int Id { get; set; }
        public Nullable<int> IdSensor { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public Nullable<decimal> TimesTamp { get; set; }
        public Nullable<System.DateTime> DataCadastrado { get; set; }
    
        public virtual Sensor Sensor { get; set; }
    }
}
