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
    public class RoleModel : Record
    {
        #region Entity Properities
        [DisplayName("Role Name")]
        [Required(ErrorMessage ="{0} is required!")] //default hatayı almamak için modifiye ediyoruz 
        [StringLength(40)]
        public string Name { get; set; }
        #endregion

        

        #region Extra Properities
        [DisplayName("Total User")] //burada dinamik bir şekilde rolü nasıl gösterebiliriz?

        public int UserCount { get; set; }

        [DisplayName("Users")]
        
        public string UserRoles { get; set; }

        #endregion



    }

}
