using System.Net;
using System.Threading.Tasks;
using Airport_REST_API.Shared.DTO;

namespace Airport_REST_API.Services.Interfaces
{
    public interface ICrewService : IService<CrewDTO>
    {
        Task<bool> LoadDataAsync();
    }
}
