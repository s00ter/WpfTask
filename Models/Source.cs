using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Models
{
    public class Source
    {
        public int outage_id { get; set; }
        public DateTime outage_start { get; set; }
        public DateTime outage_end { get; set; }
        public string outage_status { get; set; }
        public string created_by { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public List<AffectedArea> affected_areas { get; set; }
    }
}
