using EnglishCenter.Request;

namespace EnglishCenter.Validate
{
    public class QuestionValidate
    {
        public List<string> validateAddQuestion(AddQuestionRequest request)
        {
            List<string> error = new List<string>();
            if (String.IsNullOrEmpty(request.questionContent))
            {
                error.Add("Question must not empty");
            }
            if (request.list.Count < 2)
            {
                error.Add("Question must have at least 2 answer");
            }
            if (String.IsNullOrEmpty(request.questionContent) || request.questionContent.Length >= 50)
            {
                error.Add("Question title must not empty and not over 50 characters");
            }
            return error;
        }
        public List<string> validateUpdateQuestion(UpdateQuestionRequest request)
        {
            List<string> error = new List<string>();
            if (String.IsNullOrEmpty(request.questionContent))
            {
                error.Add("Question must not empty");
            }
            if (String.IsNullOrEmpty(request.questionContent) || request.questionContent.Length >= 50)
            {
                error.Add("Question title must not empty and not over 50 characters");
            }
            if (request.answerQuestions.Count < 2)
            {
                error.Add("Question must have at least 2 answer");
            }
            return error;
        }
    }
}
