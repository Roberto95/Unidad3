using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppMVC.Models
{
    public class UnionCitaSucursalCliente
    {
        [Key]
        public int CitaId { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidosCliente { get; set; }
        public string NombreSucursal { get; set; }
        public DateTime? FechaCita { get; set; }
        public string Placas { get; set; }
    }
}