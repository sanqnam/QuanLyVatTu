using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLVT_BE.Data;

namespace QLVT_BE.Service
{
    public interface IWriteFileService
    {
        Task<JsonResult> WriteFiles(List<IFormFile> files, string folder);
        Task<JsonResult> WriteFile(IFormFile file, string folder);
    }
    public class WriteFileService : IWriteFileService
    {
        private readonly QuanLyVatTuContext _context;

        public WriteFileService(QuanLyVatTuContext context)
        {
            _context = context;
        }
        // upLoad nhiều file
        public async Task<JsonResult> WriteFiles(List<IFormFile> files, string folder)
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
        // upload một file
        public async Task<JsonResult> WriteFile(IFormFile file, string folder)
        {
            string local;
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
                var result = "Upload/" + local + "/" + folder + "/" + file.FileName;
                return new JsonResult(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

}
