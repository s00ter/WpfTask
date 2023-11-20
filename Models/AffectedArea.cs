using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class AffectedArea
    {
        public string area_id { get; set; }
        public string area_name { get; set; }
        public int total_customers { get; set; }
        public int affected_customers { get; set; }
        public DateTime estimated_recovery_time { get; set; }
        public string reason { get; set; }

        public Source source { get; set; }

    }
}
