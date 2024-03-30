using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentManagament.DomainModels;
using StudentManagament.Repositories;

namespace StudentManagament.Controllers
{
    [ApiController]
    public class GendersController : Controller
    {
        private readonly IGenderRepository _genderRepository;
        private readonly IMapper _mapper;

        public GendersController(IGenderRepository genderRepository, IMapper mapper)
        {
            _genderRepository = genderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var genders = await _genderRepository.GetAllAsync();

            return Ok(_mapper.Map<List<Gender>>(genders));
        }
    }
}
