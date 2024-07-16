using EMI_API.Commons.Entities;

namespace EMI_API.Data.Interfaces
{
    public interface IProjectRepository : IRepository<Project>
    {
        Task<List<int>> Exist(List<int> ids);
    }
}
