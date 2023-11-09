using Microsoft.AspNetCore.Mvc;

namespace Asst.Models
{
	public class QuestionModel
	{
		public string Topic { get; set; }
		public List<string> Questions { get; set; }
	}
}
