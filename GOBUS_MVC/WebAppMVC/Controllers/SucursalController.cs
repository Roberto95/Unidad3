using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class SucursalController : Controller
    {
        // GET: Sucursal
        public ActionResult ConsultarSucursal()
        {
            List<Sucursal> ls = new List<Sucursal>();
            using(GOBUSEntities db= new GOBUSEntities())
            {
                ls = db.Sucursal.ToList();
            }
            return View(ls);
        }

        public ActionResult RegistrarSucursal(Sucursal s)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ModelState.Clear();
                    using (GOBUSEntities db = new GOBUSEntities())
                    {
                        db.Sucursal.Add(s);
                        db.SaveChanges();
                        ViewBag.exitoso = true;
                        
                    }

                }catch(SqlException ex)
                {
                    ViewBag.Message = ex.Message;
                }
            }
            return View();
        }
        public ActionResult BorrarSucursal(int? id)
        {
            using(GOBUSEntities db=new GOBUSEntities())
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Sucursal s = db.Sucursal.Find(id);
                if (s == null)
                {
                    return HttpNotFound();
                }
            return View(s);

            }
        }

        [HttpPost,ActionName("BorrarSucursal")]
        [ValidateAntiForgeryToken]
        public ActionResult BorrarSucursalConfirmar(int id)
        {
            try
            {
                using (GOBUSEntities db = new GOBUSEntities())
                {

                    var q=db.Sucursal.Find(id);
                    db.Sucursal.Remove(q);
                    db.SaveChanges();
                }
            }catch(SqlException ex)
            {
                ViewBag.Message = ex.Message;
            }
            return RedirectToAction("ConsultarSucursal");
        }
    }
}