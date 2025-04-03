
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Services.AttachmentService
{
    public interface IAttachmentService
    {
        bool Delete(string folderName);

        string Upload(IFormFile file, string folderName);
    }
}
