﻿using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

using DancingGoat.Models;
using DancingGoat.Infrastructure;

using KenticoCloud.Delivery;

namespace DancingGoat.Controllers
{
    public class ContactsController : ControllerBase
    {
        public ContactsController(IProjectContext projectContext) : base(projectContext)
        {
        }

        public async Task<ActionResult> Index()
        {
            var response = await client.GetItemsAsync<Cafe>(
                new EqualsFilter("system.type", "cafe"),
                new EqualsFilter("elements.country", "USA")
            );
            var cafes = response.Items;

            var viewModel = new ContactsViewModel
            {
                Roastery = cafes.FirstOrDefault(),
                Cafes = cafes
            };

            return View(viewModel);
        }
    }
}