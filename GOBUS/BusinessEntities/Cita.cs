//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BusinessEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cita
    {
        public int CitaId { get; set; }
        public int ClienteId { get; set; }
        public string PlacaNumero { get; set; }
        public Nullable<System.DateTime> FechaCita { get; set; }
        public int SucursalId { get; set; }
    
        public virtual Cliente Cliente { get; set; }
        public virtual Sucursal Sucursal { get; set; }
    }
}