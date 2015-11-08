using Citrus.Data;
using Citrus.Models.Domain;
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
    }
}
