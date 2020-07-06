using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using DemoApp.Models;

namespace DemoApp.Controllers
{
    public class SubscriberController : ApiController
    {
        private SubscribeContext db = new SubscribeContext();
        // POST: Subscriber
        public HttpResponseMessage Post(SubscriberDetail subscriberDetail)
        {
            HttpResponseMessage result;
            if (ModelState.IsValid)
            {
                db.SubscriberDetail.Add(subscriberDetail);
                db.SaveChanges();
                result = Request.CreateResponse(HttpStatusCode.Created, subscriberDetail);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest, "Validation failed!");
            }
            return result;
        }
    }
}