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
        public async Task<IActionResult> GetAllAsync()
        {
            var students = await _studentRepository.GetAllAsync();

            return Ok(_mapper.Map<List<Student>>(students));
        }

        [HttpGet]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid studentId)
        {
            var student = await _studentRepository.GetByIdAsync(studentId);

            if(student == null)
            { 
                return NotFound();
            }
            return Ok(_mapper.Map<Student>(student));
            
        }

        [HttpPut]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid studentId, [FromBody] UpdateStudentRequest request)
        {
            var mappedStudent = _mapper.Map<DataModels.Student>(request);

            if (await _studentRepository.Exists(studentId))
            {
                var updatedStudent = await _studentRepository.UpdateAsync(studentId, mappedStudent);
                if(updatedStudent != null)
                {
                    return Ok(_mapper.Map<Student>(updatedStudent));
                }
            }
            return NotFound();
        }
    }
}
