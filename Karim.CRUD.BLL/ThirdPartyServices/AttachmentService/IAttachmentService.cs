using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.BLL.ThirdPartyServices.AttachmentService
{
    public interface IAttachmentService
    {
        Task<string> UploadImages(IFormFile file, string folderName);
        bool DeleteImages(string PictuereUrl);
    }
}
