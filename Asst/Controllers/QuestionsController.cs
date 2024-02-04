using Asst.DAL;
using Asst.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Humanizer;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

namespace Asst.Controllers
{
    public class QuestionsController : Controller
    {

        public ActionResult AskQuestions()
        {
            string msgTextList = " " + HttpContext.Session.GetString("MsgList");
            string[] msgArray = msgTextList.Split("%&%");
            List<Message> msgList = new List<Message>();
            msgList.Add(new Message { Content = "Welcome to OptoAssist! \n " +
                "A training tool to help you become a history taking expert! \n" +
                "\n to introduce you to history taking, this is the main structure that you would want to know as an optometrist for history taking: \n" +
                "\n OLDCARTS:\n Onset - Acute vs gradual\n Location \n Duration\n Characteristics\n Aggravating factors \n Relieving factors\n Treatments\n Severity" +
                "\n\nFor more enquiries, just ask us based on the type of symptom you would like to ask! \n\n" +
                "How can I help you today?" });
            for (int i = 0; i < msgArray.Length - 1; i++)
            {
                msgList.Add(new Message { Content = msgArray[i] });
            }
            return View(msgList);
        }
        public ActionResult AskQuestionschild()
        {
            string msgTextList = " " + HttpContext.Session.GetString("MsgListChild");
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
            List<SelectListItem> topics = questionDAL.GetTopic(1);

            // Use 'topics' as needed, pass it to the view, etc.
            ViewData["CitiesList"] = topics;
            return View();

        }
        [HttpPost]
        public ActionResult DeleteQuestion(string topic, string question)
        {
            QuestionDAL questionDAL = new QuestionDAL();

            List<QuestionModel> questions = questionDAL.GetQuestions(1);

            // Assuming questions is a Dictionary<string, List<string>> where the key is the topic
            QuestionModel topicQuestions = questions.FirstOrDefault(q => q.Topic.Equals(topic));
            if (topicQuestions != null)
            {
                // Remove the question from the list
                topicQuestions.Questions.Remove(question);

                // Update the list in the database
                questionDAL.Add(1,topic, Newtonsoft.Json.JsonConvert.SerializeObject(topicQuestions.Questions));
            }

            // Return a JSON response to indicate success (if needed)
            return Json(new { success = true });
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
            List<Message> msgList = new List<Message>();

            string msgTextList = HttpContext.Session.GetString("MsgList");
            msgTextList += topic + "%&%";
            bool keyWordFound = false;
            foreach (var a in questions)
            {
                if (topic.ToLower().Contains(a.Topic.ToLower()))
                {
                    if (keyWordFound)
                    {
                        msgTextList += "\n\n";
                    }
                    keyWordFound = true;
                    QuestionModel topicQuestions = questions.FirstOrDefault(q => q.Topic.Equals(a.Topic));
                    topic = topic.Replace(a.Topic, string.Empty);
                    if (a.Topic == "history taking")
                    {
                        msgTextList += "Here are some steps you could follow during history taking:";
                    }
                    else
                    {
                        msgTextList += "Here are some questions you could ask regarding the topic of " + a.Topic + ":";
                    }
                    for (int i = 0; i < topicQuestions.Questions.Count; i++)
                    {
                        msgTextList += "\n" + (i + 1).ToString() + ". " + topicQuestions.Questions[i];
                    }
                }
            }
            if (!keyWordFound)
            {
                msgTextList += "Sorry, I couldn't find any topics relating to your sentence.";
            }
            msgTextList += "%&%";
            HttpContext.Session.SetString("MsgList", msgTextList);
            string[] msgArray = msgTextList.Split("%&%");

            msgList.Add(new Message { Content = "Welcome to OptoAssist! How can I help you today?" });
            for (int i = 0; i < msgArray.Length - 1; i++)
            {
                msgList.Add(new Message { Content = msgArray[i] });
            }

            return View(msgList);

            // Handle the case where none of the topics match
            // ... your existing code for handling a single topic without spaces

            return View();
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

       

        [HttpPost]
        public ActionResult AskQuestionschild(string topic)
        {
            QuestionDAL questionDAL = new QuestionDAL();

            List<QuestionModel> questions = questionDAL.GetQuestions(2);
            List<Message> msgList = new List<Message>();

            string msgTextList = HttpContext.Session.GetString("MsgListChild");
            msgTextList += topic + "%&%";
            bool keyWordFound = false;
            foreach (var a in questions)
            {
                if (topic.ToLower().Contains(a.Topic.ToLower()))
                {
                    if (keyWordFound)
                    {
                        msgTextList += "\n\n";
                    }
                    keyWordFound = true;
                    QuestionModel topicQuestions = questions.FirstOrDefault(q => q.Topic.Equals(a.Topic));
                    topic = topic.Replace(a.Topic, string.Empty);
                    if (a.Topic == "history taking")
                    {
                        msgTextList += "Here are some steps you could follow during history taking:";
                    }
                    else
                    {
                        msgTextList += "Here are some questions you could ask regarding the topic of " + a.Topic + ":";
                    }
                    for (int i = 0; i < topicQuestions.Questions.Count; i++)
                    {
                        msgTextList += "\n" + (i + 1).ToString() + ". " + topicQuestions.Questions[i];
                    }
                }
            }
            if (!keyWordFound)
            {
                msgTextList += "Sorry, I couldn't find any topics relating to your sentence.";
            }
            msgTextList += "%&%";
            HttpContext.Session.SetString("MsgListChild", msgTextList);
            string[] msgArray = msgTextList.Split("%&%");

            msgList.Add(new Message { Content = "Welcome to OptoAssist! How can I help you today?" });
            for (int i = 0; i < msgArray.Length - 1; i++)
            {
                msgList.Add(new Message { Content = msgArray[i] });
            }

            return View(msgList);

            // Handle the case where none of the topics match
            // ... your existing code for handling a single topic without spaces

            return View();
        }
        public ActionResult DeleteQuestionschild()
        {
            QuestionDAL questionDAL = new QuestionDAL();
            List<SelectListItem> topics = questionDAL.GetTopic(2);

            // Use 'topics' as needed, pass it to the view, etc.
            ViewData["CitiesList"] = topics;
            return View();

        }
        [HttpPost]
        public ActionResult DeleteQuestionchild(string topic, string question)
        {
            QuestionDAL questionDAL = new QuestionDAL();

            List<QuestionModel> questions = questionDAL.GetQuestions(2);

            // Assuming questions is a Dictionary<string, List<string>> where the key is the topic
            QuestionModel topicQuestions = questions.FirstOrDefault(q => q.Topic.Equals(topic));
            if (topicQuestions != null)
            {
                // Remove the question from the list
                topicQuestions.Questions.Remove(question);

                // Update the list in the database
                questionDAL.Add(2, topic, Newtonsoft.Json.JsonConvert.SerializeObject(topicQuestions.Questions));
            }

            // Return a JSON response to indicate success (if needed)
            return Json(new { success = true });
        }





        [HttpGet]
        public ActionResult GetQuestionsByTopicchild(string topic)
        {
            QuestionDAL questionDAL = new QuestionDAL();
            List<QuestionModel> questions = questionDAL.GetQuestions(2);
            QuestionModel topicQuestions = questions.FirstOrDefault(q => q.Topic.Equals(topic));

            // Return only the questions for the selected topic
            return PartialView("_QuestionsPartialchild", topicQuestions?.Questions);
        }

        public ActionResult QuizGame()
        {
            QuestionDAL questionDAL = new QuestionDAL();
            List<QuizQuestion> quizqns = questionDAL.GetQuizQuestions();
            return View(quizqns);
        }
    }
}
