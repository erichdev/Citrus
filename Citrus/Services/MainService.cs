using Citrus.Data;
using Citrus.Models.Domain;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Twilio;

namespace Citrus.Services
{
    public class MainService : BaseService
    {
        public static Volunteer GetVolunteerById(int Id)
        {
            Volunteer v = new Volunteer();

            DataProvider.ExecuteCmd(GetConnection, "dbo.Volunteer_SelectById"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)

               { paramCollection.AddWithValue("@Id", Id); }

               , map: delegate (IDataReader reader, short set)
               {
                   int startingIndex = 0;

               

                   v.Id = reader.GetSafeInt32(startingIndex++);
                   v.Name = reader.GetSafeString(startingIndex++);
                   v.Address = reader.GetSafeString(startingIndex++);
                   v.Email = reader.GetSafeString(startingIndex++);
                   v.Description = reader.GetSafeString(startingIndex++);

               }
               );

            return v;
        }

        public static List<Event> GetSubscribedEvents(int Id)
        {
            List<Event> list = new List<Event>();

            DataProvider.ExecuteCmd(GetConnection, "dbo.Event_SelectByUserSubscribed"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)

               { paramCollection.AddWithValue("@Id", Id); }

               , map: delegate (IDataReader reader, short set)
               {
                   int startingIndex = 0;
                   Event e = new Event();

                   e.Id = reader.GetSafeInt32(startingIndex++);
                   e.Name = reader.GetSafeString(startingIndex++);
                   e.Organization = reader.GetSafeString(startingIndex++);
                   e.Category = reader.GetSafeInt32(startingIndex++);
                   e.Address = reader.GetSafeString(startingIndex++);
                   e.Description = reader.GetSafeString(startingIndex++);

                   if (list == null)
                   {
                       list = new List<Event>();
                   }

                   list.Add(e);

               }
               );

            return list;
        }


        // ****************************
        // **THIS CURRENTLY GETS ALL EVENTS FROM DB. WILL NEED TO UPDATE IN FRONT END TO GET EVENTS NEAR USER.

        public static List<Event> GetNearbyEvents(int Id)
        {
            List<Event> list = new List<Event>();

            DataProvider.ExecuteCmd(GetConnection, "dbo.Event_Select"
               , inputParamMapper: delegate (SqlParameterCollection paramCollection)

               {  }

               , map: delegate (IDataReader reader, short set)
               {
                   int startingIndex = 0;
                   Event e = new Event();

                   e.Id = reader.GetSafeInt32(startingIndex++);
                   e.Name = reader.GetSafeString(startingIndex++);
                   e.Organization = reader.GetSafeString(startingIndex++);
                   e.Category = reader.GetSafeInt32(startingIndex++);
                   e.Address = reader.GetSafeString(startingIndex++);
                   e.Description = reader.GetSafeString(startingIndex++);

                   if (list == null)
                   {
                       list = new List<Event>();
                   }

                   list.Add(e);

               }
               );

            return list;
        }


        public static RestResponse SendSimpleMessage()
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                   new HttpBasicAuthenticator("api",
                                              "key-0ef427ccc8ef1bcdfe54742e742df2b2");
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                "sandbox0ffd1e1851714d94b8b5f20390a50dd3.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Mailgun Sandbox <postmaster@sandbox0ffd1e1851714d94b8b5f20390a50dd3.mailgun.org>");
            request.AddParameter("to", "Erich <a.erich@gmail.com>");
            request.AddParameter("subject", "Hello Erich");
            request.AddParameter("text", "Congratulations Erich, you just sent an email with Mailgun!  You are truly awesome!  You can see a record of this email in your logs: https://mailgun.com/cp/log .  You can send up to 300 emails/day from this sandbox server.  Next, you should add your own domain so you can send 10,000 emails/month for free.");
            request.Method = Method.POST;
            return (RestResponse)client.Execute(request);
        }

    }
}
