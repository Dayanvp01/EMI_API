using EMI_API.Commons.Entities;
using EMI_API.Data.Interfaces;

namespace EMI_API.Data.Repository
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
