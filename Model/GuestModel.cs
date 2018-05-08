using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class GuestModel
    {
        public string IP { get; set; }
        public int Id { get; set; }
        public int? Status { get; set; }
        public DateTime? FirstVisitedTime { get; set; }
        public string ContactInfo { get; set; }
    }
}
