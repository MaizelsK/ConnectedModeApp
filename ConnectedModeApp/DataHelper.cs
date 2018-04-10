using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ConnectedModeApp
{
    public class DataHelper
    {
        public SqlConnection GetConnection()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();

            builder.Add("Data Source", "makir97");
            builder.Add("Integrated Security", "True");
            builder.Add("Initial Catalog", "MyDB");

            SqlConnection connection = new SqlConnection(builder.ConnectionString);

            return connection;
        }

        public List<Event> GetEvents()
        {
            List<Event> events = new List<Event>();

            using(var connection = GetConnection())
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();

                command.CommandText = "select * from events";
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    events.Add(new Event
                    {
                        Id = (int)reader["id"],
                        EventDate = DateTime.Parse(reader["eventdate"].ToString()),
                        Description = reader["description"].ToString()
                    });
                }
            }

            return events;
        }
    }
}
