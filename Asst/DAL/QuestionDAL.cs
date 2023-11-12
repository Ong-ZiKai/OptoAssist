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
    }

}
