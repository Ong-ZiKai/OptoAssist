using Asst.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;


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

        public List<QuestionModel> GetQuestions(int type)
        {
            List<QuestionModel> questions = new List<QuestionModel>();

            try
            {
                conn.Open();
                string query = "";
                // Assuming the table structure has 'Topic' and 'question' columns
                if (type == 2)
                {
                    query = "SELECT Topic, question FROM QuestionTable";
                }
                else
                {
                    query = "SELECT Topic, question FROM QuestionTable";
                }
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

        public string Add(int type, string topic, string question)
        {
            // Create a SqlCommand object from the connection object
            SqlCommand cmd = conn.CreateCommand();
            if (type == 1)
            {
                cmd.CommandText = "SELECT COUNT(*) FROM QuestionTableChild WHERE topic = @topic";
            }
            else
            {
                cmd.CommandText = "SELECT COUNT(*) FROM QuestionTable WHERE topic = @topic";
            }
            cmd.Parameters.AddWithValue("@topic", topic);
            // Open a database connection
            conn.Open();

            int existingTopicCount = (int)cmd.ExecuteScalar();

            // Clear previous parameters
            cmd.Parameters.Clear();

            if (existingTopicCount > 0)
            {
                cmd.CommandText = "UPDATE QuestionTable SET question = @question WHERE topic = @topic";
            }
            else
            {
                cmd.CommandText = "INSERT INTO QuestionTable (topic, question) VALUES (@topic, @question)";
            }
            // Define the parameters used in the SQL statement
            cmd.Parameters.Clear(); // Clear previous parameters
            cmd.Parameters.AddWithValue("@question", question);
            cmd.Parameters.AddWithValue("@topic", topic);


            // ExecuteNonQuery is used for UPDATE
            cmd.ExecuteNonQuery();

            // Close the database connection
            conn.Close();
            return topic;
        }
        public List<SelectListItem> GetTopic()
        {
            List<SelectListItem> topicList = new List<SelectListItem>();

            try
            {
                conn.Open();

                // Assuming the table structure has 'Topic' column
                string query = "SELECT DISTINCT Topic FROM QuestionTable";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string topic = reader["Topic"].ToString();
                    topicList.Add(new SelectListItem { Text = topic, Value = topic });
                }
            }
            finally
            {
                conn.Close();
            }

            return topicList;
        }


    }

}
