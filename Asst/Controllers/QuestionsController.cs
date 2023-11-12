using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Asst.Models;
using System.Diagnostics;
using Asst.DAL;

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


        [HttpPost]
        public ActionResult AskQuestions(string topic)
        {
            QuestionDAL questionDAL = new QuestionDAL();

            List<QuestionModel> questions = questionDAL.GetQuestions();
            
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
    }
}
