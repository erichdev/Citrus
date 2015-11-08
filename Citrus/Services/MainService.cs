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
using System.Web.Configuration;

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
                   e.CategoryId = reader.GetSafeInt32(startingIndex++);
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
                   e.CategoryId = reader.GetSafeInt32(startingIndex++);
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


        public static RestResponse SendSimpleMessage(string name, string email)
        {

            string MailGunKey = WebConfigurationManager.AppSettings["MailGunKey"];

            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                   new HttpBasicAuthenticator("api",
                                              MailGunKey);
            RestRequest request = new RestRequest();
            request.AddParameter("domain",
                                "sandbox0ffd1e1851714d94b8b5f20390a50dd3.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Mailgun Sandbox <postmaster@sandbox0ffd1e1851714d94b8b5f20390a50dd3.mailgun.org>");
            request.AddParameter("to", email);
            request.AddParameter("subject", "A volunteer opportunity for you");
            request.AddParameter("text", "Hi " + name + ", there is a volunteering opportunity that you might be interested in! Log in to volunteerme.dev to find more information.");
            request.Method = Method.POST;
            return (RestResponse)client.Execute(request);
        }

    }
}
