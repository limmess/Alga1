using Alga1.Models;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Alga1.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        #region Index Method

        // GET: Employees
        public ActionResult Index(string searchString)
        {
            var names = from v in db.Employee
                        select v;

            if (!String.IsNullOrEmpty(searchString))
            {
                names = names.Where(s => s.Name.Contains(searchString) || s.Surname.Contains(searchString));

            }

            if (User.IsInRole(RoleName.Admin))
                return View("IndexAdmin", names);

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
            if (!ModelState.IsValid) return View(employee);

            if (fileImage != null)
            {
                if (fileImage.ContentLength > 0 && fileImage.ContentLength < 512000)
                {
                    var fileExt = Path.GetExtension(fileImage.FileName);
                    if (fileExt != null && (fileExt.ToLower().EndsWith(".jpg") && IsFileImage(fileImage)))
                    {
                        BinaryReader reader = new BinaryReader(fileImage.InputStream);
                        employee.EmployeePhoto = reader.ReadBytes(fileImage.ContentLength);
                    }
                    else
                    {
                        ViewBag.Error = "Picture is not JPG";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Error = "Picture is bigger than 500kb";
                    return View(employee);
                }
            }



            db.Employee.Add(employee);
            db.SaveChanges();
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
            if (!ModelState.IsValid) return View(employee);


            var empolyeeInDb = db.Employee.Single(c => c.Id == employee.Id);
            employee.EmployeePhoto = empolyeeInDb.EmployeePhoto;

            if (fileImage != null)
            {
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
                        return View();
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

            db.SaveChanges();
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
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employee.Find(id);
            db.Employee.Remove(employee);
            db.SaveChanges();
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
                byte[] imgBytes = employee.EmployeePhoto;
                return File(employee.EmployeePhoto, ".jpg");
            }
            else
            {
                return null;
            }
        }

        #endregion


    }
}
