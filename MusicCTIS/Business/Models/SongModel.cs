#nullable disable
using DataAccess.Records.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class SongModel : Record
    {
        #region entity properities
        [DisplayName("Song Name")]
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Artist")]
        public int? ArtistId { get; set; }

        #endregion

        #region extra prop
        
        public ArtistModel ArtistChoice { get; set; }
        #endregion
    }

}
