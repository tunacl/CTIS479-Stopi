using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Artist : Record
    {
        public string Name { get; set; }

        public List<Song> Songs { get; set; }
    }
}
