#nullable disable
using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Playlist //bu record olmayacak mı?
    {
        public int PlaylistId { get; set; }

        public string PlaylistName { get; set; }

        public List<Song> Songs { get; set; }

        public int UserId { get; set; }
    }
}
