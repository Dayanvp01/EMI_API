using EMI_API.Commons.Entities;
using EMI_API.Data.Interfaces;

namespace EMI_API.Data.Repository
{
    public class PositionHistoryRepository : Repository<PositionHistory>, IPositionHistoryRepository
    {
        public PositionHistoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
