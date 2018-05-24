using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

using DancingGoat.Models;
using DancingGoat.Infrastructure;

using KenticoCloud.Delivery;

namespace DancingGoat.Controllers
{
    public class CafesController : ControllerBase
    {
        public CafesController(IProjectContext projectContext) : base(projectContext)
        {
        }

        public async Task<ActionResult> Index()
        {
            var response = await client.GetItemsAsync<Cafe>(
                new EqualsFilter("system.type", "cafe"),
                new OrderParameter("system.name")
            );
            var cafes = response.Items;

            var viewModel = new CafesViewModel
            {
                CompanyCafes = cafes.Where(c => c.Country == "USA").ToList(),
                PartnerCafes = cafes.Where(c => c.Country != "USA").ToList()
            };

            return View(viewModel);
        }
    }
}