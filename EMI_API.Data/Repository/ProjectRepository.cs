using EMI_API.Commons.Entities;
using EMI_API.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EMI_API.Data.Repository
{
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        private readonly ApplicationDbContext context;
        public ProjectRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<List<int>> Exist(List<int> ids)
        {
            return await context.Projects.Where(x=> ids.Contains(x.Id)).Select(y=> y.Id).ToListAsync();
        }
    }
}
