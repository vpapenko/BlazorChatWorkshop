using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorChat.ViewModels
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public bool UserNameOk => !string.IsNullOrEmpty(Name);
    }
}
