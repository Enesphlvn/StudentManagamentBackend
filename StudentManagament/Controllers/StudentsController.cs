using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentManagament.DomainModels;
using StudentManagament.Repositories;

namespace StudentManagament.Controllers
{
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();

            return Ok(_mapper.Map<List<Student>>(students));
        }
    }
}
