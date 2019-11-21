using System;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    public class BaseModel
    {
        public BaseModel()
        {
            this.PK_ID = Guid.NewGuid().ToString("N");
        }

        [Key]
        public string PK_ID { get; set; }
    }
}
