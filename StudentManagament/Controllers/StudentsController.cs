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
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public StudentsController(IStudentRepository studentRepository, IMapper mapper, IImageRepository imageRepository)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _imageRepository = imageRepository;
        }

        [HttpGet]
        [Route("[controller]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var students = await _studentRepository.GetAllAsync();

            return Ok(_mapper.Map<List<Student>>(students));
        }

        [HttpGet]
        [Route("[controller]/{studentId:guid}"), ActionName("GetByIdAsync")]
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

        [HttpDelete]
        [Route("[controller]/{studentId:guid}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid studentId)
        {
            if (await _studentRepository.Exists(studentId))
            {
                var deletedStudent = await _studentRepository.DeleteAsync(studentId);
                if (deletedStudent != null)
                {
                    return Ok(_mapper.Map<Student>(deletedStudent));
                }
            }
            return NotFound();
        }

        [HttpPost]
        [Route("[controller]/add")]
        public async Task<IActionResult> AddAsync([FromBody] AddStudentRequest request)
        {
            var student = await _studentRepository.AddAsync(_mapper.Map<DataModels.Student>(request));
            return CreatedAtAction(nameof(GetByIdAsync), new { studentId = student.Id }, _mapper.Map<Student>(student));
        }

        [HttpPost]
        [Route("[controller]/{studentId:guid}/upload-image")]
        public async Task<IActionResult> UpdateProfileImage([FromRoute] Guid studentId, IFormFile profileImage)
        {
            var validExtensions = new List<string>()
            {
                ".jpeg",
                ".png",
                ".gif",
                ".jpg"
            };

            if(profileImage != null && profileImage.Length > 0)
            {
                var extension = Path.GetExtension(profileImage.FileName);
                if (validExtensions.Contains(extension))
                {
                    if(await _studentRepository.Exists(studentId))
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(profileImage.FileName);

                        var fileImagePath = await _imageRepository.Upload(profileImage, fileName);

                        if(await _studentRepository.UpdateProfileImage(studentId, fileImagePath))
                        {
                            return Ok(fileImagePath);
                        }

                        return StatusCode(StatusCodes.Status500InternalServerError, "Error uploading image");
                    }
                }

                return BadRequest("This is not a valid Image format");
            }

            return NotFound();
        }
    }
}
