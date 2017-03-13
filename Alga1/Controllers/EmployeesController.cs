using Alga1.Models;
using Microsoft.AspNet.Identity;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Alga1.Controllers
{


    [Authorize(Roles = RoleName.Admin + "," + RoleName.Employee + "," + RoleName.Manager)]
    public class EmployeesController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(EmployeesController));

        private ApplicationDbContext db = new ApplicationDbContext();

        #region Index Method

        // GET: Employees
        public ActionResult Index()
        {
            log.Debug("Action Index has been fired.");
            var names = db.Employee.ToList();

            log.Debug("Eployee list been read from database");

            if (User.IsInRole(RoleName.Admin))
            {
                log.Debug("Check if user role is Admin");
                return View("IndexAdmin", names);
            }


            if (User.IsInRole(RoleName.Employee))
            {
                log.Debug("Check if user role is Employee");
                var loggedEmployeeId = db.Users.Find(User.Identity.GetUserId()).Employee.Id;
                var employee = db.Employee.Where(c => c.Id == loggedEmployeeId).ToList();
                log.Debug("Got current logged EmployeeId" + loggedEmployeeId.ToString());
                return View("IndexGuest", employee);
            }


            return View("IndexGuest", names);
        }
        #endregion


        #region GetDetailsMethod

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole(RoleName.Admin))
                return View(employee);

            return View("DetailsReadOnly", employee);
        }

        #endregion


        #region CreatePage
        // GET: Employees/Create
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Create()
        {
            return View();
        }
        #endregion


        #region CreateMethod

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Create(
            [Bind(Include = "Id,Name,Surname,SalaryNet,ChildrenNo,RaisesChildrenAlone,SalaryGross", Exclude = "EmployeePhoto")]
        Employee employee, HttpPostedFileBase fileImage)

        {
            log.Info("Request to create new user from " + User.Identity.GetUserId());
            if (!ModelState.IsValid) return View(employee);

            if (fileImage != null)
            {
                log.Info("File is attached to create request");
                if (fileImage.ContentLength > 0 && fileImage.ContentLength < 512000)
                {
                    log.Info("User uploaded file: " + fileImage.FileName);
                    var fileExt = Path.GetExtension(fileImage.FileName);
                    if (fileExt != null && (fileExt.ToLower().EndsWith(".jpg") && IsFileImage(fileImage)))
                    {
                        BinaryReader reader = new BinaryReader(fileImage.InputStream);
                        employee.EmployeePhoto = reader.ReadBytes(fileImage.ContentLength);
                    }
                    else
                    {
                        ViewBag.Error = "Picture is not JPG";
                        return View(employee);
                    }
                }
                else
                {
                    ViewBag.Error = "Picture is bigger than 500kb";
                    return View(employee);
                }
            }

            db.Employee.Add(employee);
            log.Info("Updating database ...");
            db.SaveChanges();
            log.Info("New employee id=" + employee.Id + " saved to database");
            return RedirectToAction("Index");
        }
        #endregion


        #region EditPage

        // GET: Employees/Edit/5
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        #endregion


        #region EditMethod

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Edit([Bind(Include = "Id,Name,Surname,SalaryNet,ChildrenNo,RaisesChildrenAlone,SalaryGross", Exclude = "EmployeePhoto")] Employee employee, HttpPostedFileBase fileImage)
        {
            log.Info("Request to update user Id " + employee.Id + " " + employee.Name + " " + employee.Surname);
            if (!ModelState.IsValid) return View(employee);

            var empolyeeInDb = db.Employee.Single(c => c.Id == employee.Id);
            employee.EmployeePhoto = empolyeeInDb.EmployeePhoto;

            if (fileImage != null)
            {
                log.Info("File is attached: " + fileImage.FileName);
                if (fileImage.ContentLength > 0 && fileImage.ContentLength < 512000)
                {
                    var fileExt = Path.GetExtension(fileImage.FileName);
                    if (fileExt != null && fileExt.ToLower().EndsWith(".jpg") && IsFileImage(fileImage))
                    {
                        BinaryReader reader = new BinaryReader(fileImage.InputStream);
                        employee.EmployeePhoto = reader.ReadBytes(fileImage.ContentLength);
                        empolyeeInDb.EmployeePhoto = employee.EmployeePhoto;
                    }
                    else
                    {
                        ViewBag.Error = "Picture is not JPG";
                        return View(employee);
                    }
                }
                else
                {
                    ViewBag.Error = "Picture is bigger than 500kb";
                    return View(employee);
                }
            }

            empolyeeInDb.Name = employee.Name;
            empolyeeInDb.Surname = employee.Surname;
            empolyeeInDb.SalaryNet = employee.SalaryNet;
            empolyeeInDb.ChildrenNo = employee.ChildrenNo;
            empolyeeInDb.RaisesChildrenAlone = employee.RaisesChildrenAlone;

            log.Info("Updating database ...");
            db.SaveChanges();
            log.Info("Employee new info saved to database");
            return RedirectToAction("Index");


        }

        #endregion

        #region DeletePage

        // GET: Employees/Delete/5
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        #endregion

        #region DeleteMethod

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult DeleteConfirmed(int? id)
        {
            log.Info("Employee id=" + id + " delete request from " + User.Identity.GetUserId());

            //check if link is with Id
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);


            var applicationUser = db.Users.Where(c => c.Employee.Id == id.Value).SingleOrDefault(e => e.Employee.Id == id);
            Employee employee = db.Employee.Find(id);

            if (applicationUser != null)
            {
                var applicationUserId = db.Users.Find(applicationUser.Id);
                db.Users.Remove(applicationUserId);
                db.Employee.Remove(employee);
            }

            else
            {
                if (employee == null) return HttpNotFound();

                db.Employee.Remove(employee);
            }

            log.Info("Updating database ...");
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                log.Error("Can not delete user. Error: ", ex);
                return View(employee);
            }

            log.Info("Employee id=" + id + " deleted from database");
            return RedirectToAction("Index");
        }

        #endregion




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        #region Helpers

        public bool IsFileImage(HttpPostedFileBase file) //Check's if uploaded file is an image
        {
            bool result;
            try
            {
                System.Drawing.Image imgInput = System.Drawing.Image.FromStream(file.InputStream);
                System.Drawing.Graphics gInput = System.Drawing.Graphics.FromImage(imgInput);
                System.Drawing.Imaging.ImageFormat thisFormat = imgInput.RawFormat;
                file.InputStream.Position = 0; //reset InputStream  reader position to 0
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }

        public FileContentResult GetImage(int id)
        {
            Employee employee = db.Employee.Find(id);
            if (employee != null)
            {
                if (employee.EmployeePhoto != null)
                {
                    return File(employee.EmployeePhoto, ".jpg");
                }

            }
            return null;

        }

        #endregion


    }
}
