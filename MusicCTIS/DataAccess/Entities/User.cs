using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class User : Record
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }

        public Role Role { get; set; }
        public int RoleId { get; set; }

        public List<UserSong> UserSongs { get; set; }

        public List<Playlist> Playlists { get; set; }


    }
}
