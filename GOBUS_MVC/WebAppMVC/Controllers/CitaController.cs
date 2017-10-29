using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class CitaController : Controller
    {
        // GET: Cita

        public ActionResult AgregarCita()
        {
            using (GOBUSEntities db = new GOBUSEntities())
            {
                ViewBag.SucursalId = new SelectList(db.Sucursal, "SucursalId", "Nombre").ToList();
                ViewBag.ClienteId = new SelectList(db.Cliente, "ClienteId", "Nombre").ToList();
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgregarCita(Cita c)
        {
            //[Bind(Include = "CitaId,PlacaNumero,FechaCita,ClienteId,SucursalId")] 
            using (GOBUSEntities db= new GOBUSEntities())
            {
                
                if (ModelState.IsValid)
                {
                    db.Cita.Add(c);
                    db.SaveChanges();
                    ModelState.Clear();
                    return RedirectToAction("ConsultarCitas");
                }
                ViewBag.SucursalId = new SelectList(db.Sucursal, "SucursalId", "Nombre", c.SucursalId).ToList();
                ViewBag.ClienteId = new SelectList(db.Cliente, "ClienteId", "Nombre", c.ClienteId).ToList();
                return View(c);
            }
        }

        public ActionResult ConsultarCitas()
        {
            
            using(GOBUSEntities db=new GOBUSEntities())
            {
                var q = (from cita in db.Cita
                         join cliente in db.Cliente on cita.ClienteId equals cliente.ClienteId
                         join sucursal in db.Sucursal on cita.SucursalId equals sucursal.SucursalId
                         select new UnionCitaSucursalCliente()
                         {
                             CitaId=cita.CitaId,
                             NombreCliente = cliente.Nombre,
                             ApellidosCliente = cliente.Apellidos,
                             NombreSucursal = sucursal.Nombre,
                             FechaCita = cita.FechaCita,
                             Placas=cita.PlacaNumero

                         }).ToList();
              
                           return View(q);
            }
        }

        public ActionResult ConsultarCita(int id=0)
        {
            Cita c = new Cita();
            using(GOBUSEntities db=new GOBUSEntities())
            {
                c = db.Cita.Find(id);
                return View(c);
               
            }
        }

        public ActionResult ModificarCita(int? id)
        {
            using (GOBUSEntities db = new GOBUSEntities())
            {
                Cita c = db.Cita.Find(id);
                if (c == null)
                {
                    return HttpNotFound();
                }
                ViewBag.SucursalId = new SelectList(db.Sucursal, "SucursalId", "Nombre", c.SucursalId).ToList();
                ViewBag.ClienteId = new SelectList(db.Cliente, "ClienteId", "Nombre", c.ClienteId).ToList();

                return View(c);
            }

        }

        [HttpPost]
        public ActionResult ModificarCita(Cita c)
        {
            using(GOBUSEntities db=new GOBUSEntities())
            {
                    ModelState.Clear();
                if (ModelState.IsValid)
                {
                    db.Cita.Attach(c);
                    db.Entry(c).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ConsultarCitas");
                }
                ViewBag.SucursalId = new SelectList(db.Sucursal, "SucursalId", "Nombre", c.SucursalId).ToList();
                ViewBag.ClienteId = new SelectList(db.Cliente, "ClienteId", "Nombre", c.ClienteId).ToList();
                return View(c);
            }
        }

        public ActionResult EliminarCita(int? id)
        {
            using (GOBUSEntities db = new GOBUSEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cita c = db.Cita.Find(id);
                if (c == null)
                {
                    return HttpNotFound();
                }
                return View(c);
            }
        }

        [HttpPost, ActionName("EliminarCita")]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarCitaConfirmar(int id)
        {
            try
            {
                using (GOBUSEntities db = new GOBUSEntities())
                {

                    var q = db.Cita.Find(id);
                    db.Cita.Remove(q);
                    db.SaveChanges();
                }
            }
            catch (SqlException ex)
            {
                ViewBag.Message = ex.Message;
            }
            return RedirectToAction("ConsultarCitas");

        }

    }
}