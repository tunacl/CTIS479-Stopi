﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Playlist
    {
        public int PlaylistId { get; set; }

        public string PlaylistName { get; set; }

        public User User { get; set; }
    }
}
