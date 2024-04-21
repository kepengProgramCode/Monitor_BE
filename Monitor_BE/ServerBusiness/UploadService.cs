using Monitor_BE.Entity;
using Monitor_BE.Repository;
using SqlSugar;
using System.Security.Policy;
using static System.Net.WebRequestMethods;

namespace Monitor_BE.ServerBusiness
{
    public class UploadService : AccessClient<tb_image>
    {
        public async Task<string> SaveUploadfile(IFormFile file)
        {
            // 服务器上图片保存的相对路径，此处需要确保appsettings.json中配置了正确的路径或在此直接具体定义
            string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadedImages");

            if (!Directory.Exists(savePath))
            {
                Directory.CreateDirectory(savePath);
            }

            string fileName = Path.GetFileNameWithoutExtension(file.FileName);
            string extension = Path.GetExtension(file.FileName);
            string uniqueFileName = $"{fileName}-{Guid.NewGuid()}{extension}";
            string filePath = Path.Combine(savePath, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // 构建文件的URL地址，确保applicationUrl在你的launchSettings.json文件中已经设置好
            var baseUrl = $"https://localhost:7189";
            var fileUrl = $"{baseUrl}/UploadedImages/{uniqueFileName}";

            return fileUrl;
        }
    }
}
