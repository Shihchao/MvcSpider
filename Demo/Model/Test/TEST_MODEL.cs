using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Test
{
    [Table("TEST", Schema = "PT")]
    public class TEST_MODEL : BaseModel
    {

        public string Name { get; set; }

        public int Sex { get; set; }

        public int Age { get; set; }

        public DateTime Birthday { get; set; }

        public string Language { get; set; }
    }
}
