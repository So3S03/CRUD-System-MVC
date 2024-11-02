using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.BLL.ThirdPartyServices.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        public async Task<string> UploadImages(IFormFile file /*The File Itself*/, string folderName /*The FolderName That The File Will be Saved On*/)
        {
            //1. Get Folder Path
            // we here compain the project path with the wwwroot path with the folder i will save the file on path
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images", folderName); //ProjectPath + wwwrooot + images + folder to be ssaved on

            //2. Make The FileName Uniqe
            // this is for avoiding the name repeating
            var fileName = $"{Guid.NewGuid()}{file.FileName}";

            //3. Get The Full Path Of The File
            var fullPath = Path.Combine(folderPath, fileName); //ProjectPath + wwwrooot + images + folder to be ssaved on + fileName

            //4. Open The File As Stream With Choosing The Mode
            var fileStream = new FileStream(fullPath, FileMode.Create); // Save File while creating the record that have this file

            //5. Coping The File To The Stream ( The Path I Have Set )
            await file.CopyToAsync(fileStream);

            return Path.Combine($"images\\{folderName}", fileName);
        }

        public bool DeleteImages(string PictuereUrl)
        {
            //1. Check If The File Exist Or Not 
            if (PictuereUrl != "N/A")
            {
                if (File.Exists(PictuereUrl))
                {
                    // if exist delete it
                    File.Delete(PictuereUrl);
                    return true;
                } 
            }
            return false;
        }

    }
}
