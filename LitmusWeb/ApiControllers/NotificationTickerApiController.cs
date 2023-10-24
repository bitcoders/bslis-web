using DataAccess;
using DataAccess.Repositories;
using LitmusWeb.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace LitmusWeb.ApiControllers
{
    public class NotificationTickerApiController : ApiController
    {
        readonly NotificationTickerRepository tickerRepository = new NotificationTickerRepository();
        [HttpPost]
        [Route("api/NotificationTickerApi/PostNotificationTicker")]
        [ActionName("PostNotificationTicker")]
        public async Task<HttpResponseMessage> PostNotificationTicker()
        {
            List<NotificationsTicker> ticker = new List<NotificationsTicker>();
            ticker = await Task.FromResult(tickerRepository.GetNotificationTickerList());
            if (ticker.Count <= 0)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, "No Content");
            }
            List<NotificationTickerModel> Model = new List<NotificationTickerModel>();
            foreach (var item in ticker)
            {
                NotificationTickerModel temp = new NotificationTickerModel()
                {
                    ValidUnits = item.ValidUnits,
                    DisplayText = item.DisplayText,
                    ValidFrom = item.ValidFrom,
                    ValidTill = item.ValidTill,
                    IsActive = item.IsActive,
                    CreatedBy = item.CreatedBy,
                    CreatedAt = item.CreatedAt
                };
                Model.Add(temp);
            }
            var Notifications = new { Model };
            return Request.CreateResponse(HttpStatusCode.OK, Notifications);
        }
    }
}
