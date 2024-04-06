using E_CommerceProject.Business.Dashborad.Data.Dtos;

namespace E_CommerceProject.Business.Dashborad.Data
{
    public interface IDataService
    {
        Task<DataReadDto> GetDataDashboard();
    }
}
