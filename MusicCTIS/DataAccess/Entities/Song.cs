﻿#nullable disable
using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Song : Record
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        

        public Artist Artist { get; set; }
        public int? ArtistId { get; set; }

        

        public List<UserSong> UserSong  { get; set; }






    }
}
