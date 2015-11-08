using Citrus.Models;
using Citrus.Models.Domain;
using Citrus.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Citrus.Controllers
{
    [RoutePrefix("api")]
    public class MainApiController : ApiController
    {
        [Route("volunteer/{Id:int}"), HttpGet]
        public HttpResponseMessage GetVolunteerById(int Id)
        {
            ItemResponse<Volunteer> response = new ItemResponse<Volunteer>();
            response.Item = MainService.GetVolunteerById(Id);
            return Request.CreateResponse(response);
        }

        [Route("event/{Id:int}"), HttpGet]
        public HttpResponseMessage GetEventById(int Id)
        {
            ItemResponse<Event> response = new ItemResponse<Event>();
            response.Item = MainService.GetEventById(Id);
            return Request.CreateResponse(response);
        }


        [Route("volunteer/{Id:int}/events/subscribed"), HttpGet]
        public HttpResponseMessage GetSubscribedEvents(int Id)
        {
            ItemsResponse<Event> response = new ItemsResponse<Event>();
            response.Items = MainService.GetSubscribedEvents(Id);
            return Request.CreateResponse(response);
        }

        [Route("volunteer/{Id:int}/events/nearby"), HttpGet]
        public HttpResponseMessage GetNearbyEvents(int Id)
        {
            ItemsResponse<Event> response = new ItemsResponse<Event>();
            response.Items = MainService.GetNearbyEvents(Id);
            return Request.CreateResponse(response);
        }

    }
}
