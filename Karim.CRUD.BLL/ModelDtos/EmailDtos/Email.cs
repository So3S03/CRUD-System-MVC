using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.BLL.ModelDtos.EmailDtos
{
	public class Email
	{
        public required string To { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }
    }
}

