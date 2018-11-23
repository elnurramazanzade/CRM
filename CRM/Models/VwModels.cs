using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Models
{
    class VwCounterparty
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Person { get; set; }
        public string Position { get; set; }
    }

    class VwMission
    {
        public int Id { get; set; }
        public string Counterparty { get; set; }
        public string Employee { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        public string EndTime { get; set; }
        public string Completed { get; set; }
    }

    class VwComment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
    }
}
