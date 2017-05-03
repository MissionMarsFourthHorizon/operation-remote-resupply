using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using DroneLander.Service.Models;
using DroneLander.Service.DataObjects;

namespace DroneLander.Service.Controllers
{
    [Authorize]
    public class ActivityItemController : TableController<ActivityItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            DroneLanderServiceContext context = new DroneLanderServiceContext();
            DomainManager = new EntityDomainManager<ActivityItem>(context, Request);
        }

        // GET tables/ActivityItem
        public IQueryable<ActivityItem> GetAllActivityItems()
        {
            return Query();
        }

        // GET tables/ActivityItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<ActivityItem> GetActivityItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/ActivityItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<ActivityItem> PatchActivityItem(string id, Delta<ActivityItem> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/ActivityItem
        public async Task<IHttpActionResult> PostActivityItem(ActivityItem item)
        {
            ActivityItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/ActivityItem/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteActivityItem(string id)
        {
            return DeleteAsync(id);
        }
    }
}