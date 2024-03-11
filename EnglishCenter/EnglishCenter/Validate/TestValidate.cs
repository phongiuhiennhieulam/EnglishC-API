using EnglishCenter.Request;

namespace EnglishCenter.Validate
{
    public class TestValidate
    {

        public List<string> validateAddTest(AddTestRequest request)
        {
            List<string> result = new List<string>();
            if(request.listQuestion.Count == 0)
            {
                result.Add("Test must have at least 1 question");
            }
            if (String.IsNullOrEmpty(request.name) || request.name.Length >= 50)
            {
                result.Add("Test title must not empty and not over 50 characters");
            }
            if (request.time <= 0)
            {
                result.Add("Time must > 0");
            }
            return result;
        }
    }
}
