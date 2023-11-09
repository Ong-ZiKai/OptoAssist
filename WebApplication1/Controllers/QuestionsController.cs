﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Asst.Controllers
{
    public class QuestionsController : Controller
    {
        private Dictionary<string, List<string>> questions = new Dictionary<string, List<string>>
        {
            { "chief complaint", new List<string> { "What brings you in for an eye examination today?", "Can you describe the main issue or concern you have with your eyes?" } },
            { "medical history", new List<string> { "Do you have any existing medical conditions?", "Are you currently taking any medications or eye drops?", "Do you have any known allergies to medications or substances?", "Have you ever had eye surgery or any eye-related procedures in the past?" } },
            { "symptoms", new List<string> { "Could you describe any symptoms you're experiencing, such as pain, redness, or discomfort?", "When did these symptoms start?", "Are the symptoms constant or intermittent?", "Have you noticed any triggers or patterns related to the symptoms?", "Do you have any associated symptoms?" } },
            { "pain", new List<string> { "Which eye had the pain?", "What is the severity of the pain between 1 to 10, 10 being the most painful?", "How long have you had the pain?", "How would you describe the pain?", "Is there any other issues that you think are related to the pain? e.g. redness?", "Have you had this kind of pain before?", "Each time you had the pain, how long did it last?" } },
            { "history taking", new List<string> { "Can you describe the patient's chief complaint or reason for the visit?", "Ask about the onset and duration of symptoms.", "Inquire about any relevant medical history or pre-existing conditions.", "Ask about the family history of eye diseases.", "Explore lifestyle factors like smoking, allergies, and occupation.", "Encourage the patient to share any other symptoms or concerns." } }
            // Add more topics and questions here...
        };

        public ActionResult AskQuestions()
        {
            ViewBag.Topics = questions.Keys;
            return View();
        }

        [HttpPost]
        public ActionResult ShowQuestions(string topic)
        {
            if (questions.ContainsKey(topic))
            {
                ViewBag.Topic = topic;
                ViewBag.Questions = questions[topic];
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid topic or questions not found.";
            }

            return View("AskQuestions");
        }
    }
}
    