using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Title : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}
