using EnglishCenter.DTO;
using EnglishCenter.Request;

namespace EnglishCenter.Repository
{
    public interface ITestRepository 
    {
        List<ShowTestDTO> getAllTest();

        ShowTestDTO getDetailTest(int id);

        void actionTest(int id,bool? status);
        void addTest(AddTestRequest request);

        ShowTestUpdateDTO GetUpdateTest(int id);

        void updateTest(UpdateTestRequest request);
    }
}
