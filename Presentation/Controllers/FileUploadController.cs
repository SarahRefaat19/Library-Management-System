using LibrarySystem.BusnissLogic.Dtos.FileUpload;
using LibrarySystem.BusnissLogic.ServicesClasses;
using LibrarySystem.BusnissLogic.ServicesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Presentation.Controllers
{
    [Authorize(Roles = "Admin,Librarian")]
    [ApiController]
    [Route("api/[controller]")]
    public class FileUploadController : ControllerBase
    {
        private readonly IFileUploadsService _uploadsService;

        public FileUploadController(IFileUploadsService uploadsService)
        {
            _uploadsService = uploadsService;
        }


        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Upload([FromForm] FileUploadDto FileDto)
        {
            if (FileDto == null) return BadRequest();
            return Ok(new { FileName = FileDto.File.FileName });
        }        





        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromForm] FileUpdateDto fileDto)
        {
            if (fileDto == null)
            {
                return BadRequest("Image you want to Update is Not Found ");
            }
            var filename = await _uploadsService.UpdateImageAsync(fileDto.File, fileDto.OldFileName);

            return Ok(new { FileName = filename });






                }



         [HttpDelete("Delete")]
        public async Task<IActionResult> Delete( string oldImage)
        {
            if (oldImage == null)
            {
                return BadRequest("Image you want to Delete is Not Found ");
            }
            var filename = await _uploadsService.DeleteImageAsync(oldImage);

            return Ok(new { FileName = filename });


        }
    }
}
