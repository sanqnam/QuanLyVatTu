using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using QLVT_BE.Service;

namespace QLVT_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WriteFileController : ControllerBase
    {
        private readonly IWriteFileService _writeFile;
        public WriteFileController(IWriteFileService writeFile)
        {
            _writeFile = writeFile;
        }
        private async Task<JsonResult> WriteFiles(List<IFormFile> files, string folder)
        {
            string local;
                List<string> results = new List<string>();
                foreach (var file in files)
                {
                    try
                    {
                        var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
                        if (extension == ".jpg" || extension == ".jpge" || extension == ".png")
                        {
                            local = "Images";
                        }
                        else
                        {
                            local = "Files";
                        }
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "UpLoad\\" + local + "\\" + folder + "");
                        if (!Directory.Exists(filePath))
                        {   
                            Directory.CreateDirectory(filePath);
                        }
                        var exactpath = Path.Combine(Directory.GetCurrentDirectory(), "UpLoad\\" + local + "\\" + folder + "", file.FileName);
                        using (var stream = new FileStream(exactpath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        string result = "Upload/" + local + "/" + folder + "/" + file.FileName;
                        results.Add(result);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                return new JsonResult(results);
        }

        [HttpPost("UploadFiles/{folder}")]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files, string folder)
        {
            try
            {
                var result = await WriteFiles(files, folder);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("UploadFile/{folder}")]
        public async Task<IActionResult> UploadFile(IFormFile file, string folder)
        {
            try
            {
                var result = await _writeFile.WriteFile(file, folder);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
