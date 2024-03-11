using EnglishCenter.DTO;
using EnglishCenter.Model;
using Microsoft.EntityFrameworkCore;

namespace EnglishCenter.Repository
{
    public class TestRepository : ITestRepository
    {
        private readonly AssginPRN231Context _context;
        public TestRepository(AssginPRN231Context context)
        {
            _context = context;

        }
        public List<ShowTestDTO> getAllTest()
        {
            var list = _context.Tests.ToList();
            List<ShowTestDTO> returnList = new();
            try
            {

                foreach (var item in list)
                {
                    ShowTestDTO show = new ShowTestDTO()
                    {
                       id = item.Id,
                       name = item.Title,
                       time = item.Time,
                       date = item.CreateDate,
                       status = item.Status
                    };
                    returnList.Add(show);
                }

            }
            catch (Exception ex)
            {

            }
            return returnList;
        }
    }
}
