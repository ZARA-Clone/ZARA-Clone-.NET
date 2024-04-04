using AutoMapper;
using E_CommerceProject.Business.Dashborad.Data;
using E_CommerceProject.Business.Dashborad.Data.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceProject.WebAPI.Controllers
{
    [Route("dashboard/api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly IDataService _dataService;
        private readonly ILogger<DataController> _logger;
        private readonly IMapper _mapper;

        public DataController(IDataService dataService
            , ILogger<DataController> logger
            , IMapper mapper)
        {
            _dataService = dataService;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<DataReadDto>> GetData()
        {
            var  result = await _dataService.GetDataDashboard();
            return result;
        }
    }
}
