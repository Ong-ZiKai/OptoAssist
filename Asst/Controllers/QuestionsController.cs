using Asst.DAL;
using Asst.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asst.Controllers
{
    public class QuestionsController : Controller
    {




        public ActionResult AskQuestions()
        {
            string msgTextList = " " + HttpContext.Session.GetString("MsgList");
            string[] msgArray = msgTextList.Split("%&%");
            List<Message> msgList = new List<Message>();
            msgList.Add(new Message { Content = "Welcome to OptoAssist! How can I help you today?" });
            for (int i = 0; i < msgArray.Length - 1; i++)
            {
                msgList.Add(new Message { Content = msgArray[i] });
            }
            return View(msgList);
        }
        public ActionResult DeleteQuestions()
        {

            QuestionDAL questionDAL = new QuestionDAL();
            List<SelectListItem> topics = questionDAL.GetTopic();

            // Use 'topics' as needed, pass it to the view, etc.
            ViewData["CitiesList"] = topics;
            return View();

        }
        [HttpGet]
        public ActionResult GetQuestionsByTopic(string topic)
        {
            QuestionDAL questionDAL = new QuestionDAL();
            List<QuestionModel> questions = questionDAL.GetQuestions(1);
            QuestionModel topicQuestions = questions.FirstOrDefault(q => q.Topic.Equals(topic));

            // Return only the questions for the selected topic
            return PartialView("_QuestionsPartial", topicQuestions?.Questions);
        }

        public ActionResult AddQuestions()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AskQuestions(string topic)
        {
            QuestionDAL questionDAL = new QuestionDAL();

            List<QuestionModel> questions = questionDAL.GetQuestions(1);

            string msgTextList = HttpContext.Session.GetString("MsgList");
            msgTextList += topic + "%&%";

            // Assuming questions is a Dictionary<string, List<string>> where the key is the topic
            QuestionModel topicQuestions = questions.FirstOrDefault(q => q.Topic.Equals(topic));

            if (topicQuestions != null)
            {
                msgTextList += "Here are some questions you could ask regarding the topic of " + topic + ":";
                for (int i = 0; i < topicQuestions.Questions.Count; i++)
                {
                    msgTextList += "\n" + (i + 1).ToString() + ". " + topicQuestions.Questions[i];
                }
                msgTextList += "%&%";
            }
            else
            {
                msgTextList += "Invalid topic or questions not found.%&%";
            }

            HttpContext.Session.SetString("MsgList", msgTextList);
            string[] msgArray = msgTextList.Split("%&%");
            List<Message> msgList = new List<Message>();
            msgList.Add(new Message { Content = "Welcome to OptoAssist! How can I help you today?" });
            for (int i = 0; i < msgArray.Length - 1; i++)
            {
                msgList.Add(new Message { Content = msgArray[i] });
            }

            return View(msgList);
        }



        [HttpPost]
        public ActionResult AddQuestions(AddQnModel result)
        {
            string question = result.Question;
            string topic = result.Topic;
            int type = result.Type;
            QuestionDAL questionDAL = new QuestionDAL();

            List<QuestionModel> questions = questionDAL.GetQuestions(type);

            // Assuming questions is a Dictionary<string, List<string>> where the key is the topic
            QuestionModel topicQuestions = questions.FirstOrDefault(q => q.Topic.Equals(topic));
            if (topicQuestions != null)
            {
                topicQuestions.Questions.Add(question);

                // Update the list in the database
                questionDAL.Add(type, topic, Newtonsoft.Json.JsonConvert.SerializeObject(topicQuestions.Questions));
            }
            else
            {
                // Create a new topic and question entry
                List<string> newQuestions = new List<string> { question };
                questionDAL.Add(type, topic, Newtonsoft.Json.JsonConvert.SerializeObject(newQuestions));
            }


            // Optionally, you can handle the result and provide feedback to the user
            return RedirectToAction("AddQuestions");
        }



    }
}
