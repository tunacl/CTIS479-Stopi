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
    public class UserModel : Record
    {
        #region Entity Properities
        [DisplayName("User Name")]
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(30)]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "{0} is required!")]
        [StringLength(20)]
        public string Password { get; set; }

        [DisplayName("Active")]
        public bool IsActive { get; set; }

        [DisplayName("Birth Date")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [DisplayName("Role")]
        public int? RoleId { get; set; }
        #endregion

        #region Extra Properities
        public RoleModel RoleOutput { get; set; }

        [DisplayName("Active")]
        public string IsActiveOutput { get; set; } //to show active or not active format

        [DisplayName("Birth Date")]
        public string BirthDateOutput { get; set; }
        #endregion



    }
}
