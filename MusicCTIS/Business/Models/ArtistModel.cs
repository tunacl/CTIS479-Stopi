#nullable disable
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class ArtistModel : Artist
    {
        #region Entity Properities
        [DisplayName("Artist Name")]
        [Required(ErrorMessage = "{0} is required!")]  
        [StringLength(20)]
        public string Name { get; set; }
        #endregion

        #region Extra Properities
        [DisplayName("Artist's Total Songs")]
        public int TotalSong { get; set; }

        [DisplayName("Artist's Songs")]
        public string Songs { get; set; }
        #endregion
    }
}
