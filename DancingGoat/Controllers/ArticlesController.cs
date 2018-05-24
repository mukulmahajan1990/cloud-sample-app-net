using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using DancingGoat.Models;
using DancingGoat.Infrastructure;

using KenticoCloud.Delivery;

namespace DancingGoat.Controllers
{
    public class ArticlesController : ControllerBase
    {
        public ArticlesController(IProjectContext projectContext) : base(projectContext)
        {
        }

        public async Task<ActionResult> Index()
        {
            var response = await client.GetItemsAsync<Article>(
                new EqualsFilter("system.type", "article"),
                new OrderParameter("elements.post_date", SortOrder.Descending),
                new ElementsParameter("teaser_image", "post_date", "summary", "url_pattern", "title")
            );

            return View(response.Items);
        }

        public async Task<ActionResult> Show(string urlSlug)
        {
            var response = await client.GetItemsAsync<Article>(
                new EqualsFilter("elements.url_pattern", urlSlug),
                new EqualsFilter("system.type", "article"),
                new DepthParameter(1)
            );

            if (response.Items.Count == 0)
            {
                throw new HttpException(404, "Not found");
            }
            else
            {
                return View(response.Items[0]);
            }
        }
    }
}