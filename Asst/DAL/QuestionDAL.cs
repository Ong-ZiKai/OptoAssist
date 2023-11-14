using System.Data.SqlClient;
using Asst.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using Newtonsoft.Json;

namespace Asst.DAL
{
    public class QuestionDAL
    {
        private IConfiguration Configuration { get; }
        private SqlConnection conn;

        // Constructor
        public QuestionDAL()
        {
            // Read ConnectionString from appsettings.json file
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
            string strConn = Configuration.GetConnectionString("OptoAssistConnectionString");

            // Instantiate a SqlConnection object with the Connection String read.
            conn = new SqlConnection(strConn);
        }

        public List<QuestionModel> GetQuestions()
        {
            List<QuestionModel> questions = new List<QuestionModel>();

            try
            {
                conn.Open();

                // Assuming the table structure has 'Topic' and 'question' columns
                string query = "SELECT Topic, question FROM QuestionTable";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string topic = reader["Topic"].ToString();
                    string questionsJson = reader["question"].ToString();

                    List<string> questionList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(questionsJson);

                    QuestionModel questionModel = new QuestionModel
                    {
                        Topic = topic,
                        Questions = questionList
                    };

                    questions.Add(questionModel);
                }
            }
            finally
            {
                conn.Close();
            }

            return questions;
        }

        public string Add(string topic,string question )
        {
            string questionlist = "";
            //Create a SqlCommand object from connection object
            SqlCommand cmd = conn.CreateCommand();
            //Specify the SQL statement that selects the right topic and question
            cmd.CommandText = @"SELECT question FROM Member
                                WHERE topic = @selectedtopic";
            //Define the parameter used in SQL statement, value for the
            cmd.Parameters.AddWithValue("@selectedtopic", topic);
            //Open a database connection
            conn.Open();
            //executing sql thru a datareader
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows) 
            {
                //Read the record from database
                while (reader.Read())
                {
                    // Fill question with value from the data reader
                    questionlist = reader.GetString(1);
                }
            }
            //Close DataReader
            reader.Close();
            //Close the database connection
            conn.Close();

            //Specifed an UPDATE SQL statement
            cmd.CommandText = @"UPDATE QuestionTable SET question=@questionlist
                                WHERE topic = @topic";

            //Define the parameters used in SQL statement, value for each parameter
            //is retrieved from respective class's property.
            cmd.Parameters.AddWithValue("@questionlist", questionlist);
            cmd.Parameters.AddWithValue("@topic", topic);

            conn.Open();
            //ExecuteNonQuery is used for UPDATE
            cmd.ExecuteNonQuery();
            conn.Close() ;
            return "successful!";
        }

    }

}
