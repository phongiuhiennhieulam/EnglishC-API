using EnglishCenter.DTO;

namespace EnglishCenter.Repository
{
    public interface ITestRepository 
    {
        List<ShowTestDTO> getAllTest();

        ShowTestDTO getDetailTest(int id);
    }
}
