using Asst.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data.SqlClient;
using System.Data;


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
                if (type == 1)
                {
                    query = "SELECT Topic, question FROM QuestionTable";
                }
                else
                {
                    query = "SELECT Topic, question FROM QuestionTableChild";
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

            // Open a database connection
            conn.Open();

            try
            {
                // Define the @topic parameter
                cmd.Parameters.AddWithValue("@topic", topic);

                // Check if the topic already exists
                cmd.CommandText = (type == 1)
                    ? "SELECT COUNT(*) FROM QuestionTable WHERE topic = @topic"
                    : "SELECT COUNT(*) FROM QuestionTableChild WHERE topic = @topic";

                int existingTopicCount = (int)cmd.ExecuteScalar();

                // Clear previous parameters
                cmd.Parameters.Clear();

                if (existingTopicCount > 0)
                {
                    // Topic already exists, update the question
                    cmd.CommandText = (type == 1)
                        ? "UPDATE QuestionTable SET question = @question WHERE topic = @topic"
                        : "UPDATE QuestionTableChild SET question = @question WHERE topic = @topic";
                }
                else
                {
                    // Topic does not exist, insert a new row
                    cmd.CommandText = (type == 1)
                        ? "INSERT INTO QuestionTable (topic, question) VALUES (@topic, @question)"
                        : "INSERT INTO QuestionTableChild (topic, question) VALUES (@topic, @question)";
                }

                // Define the @question parameter
                cmd.Parameters.AddWithValue("@question", question);
                cmd.Parameters.AddWithValue("@topic", topic);

                // ExecuteNonQuery is used for UPDATE and INSERT
                cmd.ExecuteNonQuery();
            }
            finally
            {
                // Close the database connection in the finally block to ensure it is always closed
                conn.Close();
            }

            return topic;
        }
        public List<SelectListItem> GetTopic(int type)
        {
            List<SelectListItem> topicList = new List<SelectListItem>();

            try
            {
                string query = "";
                conn.Open();
                if (type == 1)
                {


                    // Assuming the table structure has 'Topic' column
                     query = "SELECT DISTINCT Topic FROM QuestionTable";
                }
                else
                {
                     query = "SELECT DISTINCT Topic FROM QuestionTablechild";

                }
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
        public List<QuizQuestion> GetQuizQuestions()
        {
            List<QuizQuestion> qnList = new List<QuizQuestion>();
            try
            {
                conn.Open();
                string query = "SELECT * FROM QuizQn";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    qnList.Add(new QuizQuestion
                    {
                        id = Convert.ToInt32(reader["qnID"]),
                        content = reader["qnText"].ToString(),
                        image = reader["qnImage"].ToString(),
                        category = reader["qnCat"].ToString(),
                        keywords = reader["qnKeywords"].ToString().Split(",").ToList(),
                        answers = reader["qnAns"].ToString().Split(";").ToList()
                    });
                }
            }
            finally
            {
                conn.Close();
            }
            return qnList;
        }
    }

}
