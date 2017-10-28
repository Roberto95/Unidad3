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
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult ConsultarCliente(int id=0)
        {
            Cliente c = new Cliente();
            using(GOBUSEntities db=new GOBUSEntities())
            {
                c=db.Cliente.Find(id);
                return View(c);
            }
        }

        public ActionResult ConsultarClientes()
        {
            List<Cliente> lc = new List<Cliente>();
            using(GOBUSEntities db=new GOBUSEntities())
            {
                lc = db.Cliente.ToList();
            }
            return View(lc);
        }

        public ActionResult AgregarCliente(Cliente c)
        {
            if (ModelState.IsValid)
            {


                try
                {
                    ModelState.Clear();
                    using (GOBUSEntities db = new GOBUSEntities())
                    {
                        db.Cliente.Add(c);
                        db.SaveChanges();
                        ViewBag.exitoso = true;

                    }

                }
                catch (SqlException ex)
                {
                    ViewBag.Message = ex.Message;
                }
            }
            return View();
            
        }

        public ActionResult EliminarCliente(int? id)
        {
            using(GOBUSEntities db= new GOBUSEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cliente c = db.Cliente.Find(id);
                if (c == null)
                {
                    return HttpNotFound();
                }
                return View(c);
            }
        }

        [HttpPost, ActionName("EliminarCliente")]
        [ValidateAntiForgeryToken]
        public ActionResult EliminarClienteConfirmar(int id)
        {
            try
            {
                using (GOBUSEntities db = new GOBUSEntities())
                {

                    var q = db.Cliente.Find(id);
                    db.Cliente.Remove(q);
                    db.SaveChanges();
                }
            }
            catch (SqlException ex)
            {
                ViewBag.Message = ex.Message;
            }
            return RedirectToAction("ConsultarClientes");
            
        }

        public ActionResult EditarCliente(int? id)
        {
            using(GOBUSEntities db=new GOBUSEntities())
            {
                Cliente movie = db.Cliente.Find(id);
                if (movie == null)
                {
                    return HttpNotFound();
                }
                return View(movie);
            }
            
        }

        [HttpPost]
        public ActionResult EditarCliente(Cliente c)
        {
            using (GOBUSEntities db = new GOBUSEntities())
            {
                if (ModelState.IsValid)
                {
                    db.Entry(c).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("ConsultarClientes");
                }
                return View(c);
            }

        }

    }
}