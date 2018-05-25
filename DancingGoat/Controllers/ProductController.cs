﻿using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using DancingGoat.Infrastructure;

using KenticoCloud.Delivery;

namespace DancingGoat.Controllers
{
    public class ProductController : ControllerBase
    {
        public ProductController(IProjectContext projectContext) : base(projectContext)
        {
        }

        public async Task<ActionResult> Detail(string urlSlug)
        {
            var item = (await client.GetItemsAsync<object>(new EqualsFilter("elements.url_pattern", urlSlug), new InFilter("system.type", "brewer", "coffee"))).Items.FirstOrDefault();

            if (item == null)
            {
                throw new HttpException(404, "Not found");
            }
            else
            {
                ViewBag.FreeTasteRequested = TempData["formSubmited"] ?? false;
                ViewBag.UrlSlug = urlSlug;
                return View(item.GetType().Name, item);
            }
        }

        /// <summary>
        /// Dummy action; form information is being handed over to Kentico Cloud Engagement management service through JavaScript.
        /// </summary>
        [HttpPost]
        public ActionResult FreeTaste()
        {
            // If needed, put your code here to work with the uploaded data in MVC.
            TempData["formSubmited"] = true;
            return RedirectToAction("Detail", new { urlSlug = Request.Form["product_url_slug"]});
        }
    }
}