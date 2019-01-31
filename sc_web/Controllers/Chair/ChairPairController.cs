using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using sc_web.DataLayer;
using sc_web.Models;
using sc_web.Models.Chair;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sc_web.Controllers
{
    [Authorize]
    public class ChairPairController : Controller
    {
        /// <summary>
        /// Application DB context
        /// </summary>
        protected ApplicationDbContext ApplicationDbContext { get; set; }

        /// <summary>
        /// User manager - attached to application DB context
        /// </summary>
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public ChairPairController()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
        }

        //// GET: Chair/Pair
        //[HttpGet]
        //public ActionResult Pair()
        //{
        //    return View();
        //}

        // GET: Chair/Pair
        [HttpGet]
        public ActionResult Pair(string pairingCode)
        {
            var prepopulatedModel = new Models.PairingRequest();
            prepopulatedModel.PairingCode = pairingCode;

            return View(prepopulatedModel);
        }

        // POST: Chair/Pair
        [HttpPost]
        public ActionResult Pair(PairingRequest model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = UserManager.FindById(User.Identity.GetUserId());
            var authKey = Guid.NewGuid().ToString();
            var chair = new SmartChairModel()
            {
                AuthKey = authKey,
                Name = model.ChairName
            };

            // update the current pending status with the new AuthKey
            var pairingdb = new PairingOperationsContext();
            var pairingRequest = pairingdb.PairingOperations.SingleOrDefault(e => e.ID == model.PairingCode);

            pairingdb.Entry(pairingRequest).State = System.Data.Entity.EntityState.Modified;
            pairingRequest.AuthKey = authKey;
            pairingdb.SaveChanges();

            user.PairedChairs.Add(chair);

            UserManager.Update(user);

            return RedirectToAction("Index", "Home");
        }
    }
}