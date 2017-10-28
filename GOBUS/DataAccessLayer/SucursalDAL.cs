using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataAccessLayer
{
    public class SucursalDAL
    {
        public List<Sucursal> ConsultarSucursales()
        {
            using(GOBUSEntities db=new GOBUSEntities())
            {
                
                return db.Sucursals.ToList();
            }
        }

        public bool ModificarSucursal(Sucursal s)
        {
            using(GOBUSEntities db=new GOBUSEntities())
            {
                db.Sucursals.Attach(s);

                //permite actualzar el registro
                //siempre debe incluir el ID del objeto(primary key)
                db.Entry(s).State = EntityState.Modified;

                //refrescamos modelo de base de datos
                return db.SaveChanges()>0;
            }
        }

        public bool BorrarSucursal(int id)
        {
            using(GOBUSEntities db = new GOBUSEntities())
            {
                var query = db.Sucursals.Where(x => x.SucursalId == id).Single();
                db.Sucursals.Remove(query);
                return db.SaveChanges()>0;
            }
        }
    }
}
