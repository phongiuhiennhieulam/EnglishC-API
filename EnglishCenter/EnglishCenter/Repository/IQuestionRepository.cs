using EnglishCenter.DTO;
using EnglishCenter.Request;

namespace EnglishCenter.Repository
{
    public interface IQuestionRepository
    {
        List<ShowQuestionDTO> getAllQuestion();

        void deleteQuestion(int id);

        void addQuestion(AddQuestionRequest request);
    }
}
