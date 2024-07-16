using EMI_API.Commons.Entities;
using EMI_API.Data.Interfaces;

namespace EMI_API.Data.Repository
{
    public class EmployeeProjectRepository : Repository<EmployeeProject>, IEmployeeProjectRepository
    {
        public EmployeeProjectRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
