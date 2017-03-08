using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IGATAPCODE.ViewModels
{
    public class LoginViewModel
    {
        public string UserName { get; set; }


        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
