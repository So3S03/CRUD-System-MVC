using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karim.CRUD.DAL.Entities
{
    public class BaseModel
    {
        //Most Of The Property Are Adminstration Data
        public int Id { get; set; } //PK For The Entity In The Database
        public DateTime CreatedOn { get; set; } //When The Record is Created [By Date And Time]
        public int CreatedBy { get; set; } //Who Create The Record
        public DateTime LastModifiedOn { get; set; } //When The Record Is Update [By Date And Time]
        public int LastModifiedBy { get; set; } //Who Update The Record
        public bool IsDeleted { get; set; } //For Record Soft Delete 
    }
}
