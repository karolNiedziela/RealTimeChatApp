using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.ViewModels
{
    public class CreateRoomViewModel
    {
        [DisplayName("Room name:")]
        [Required]
        [StringLength(30, ErrorMessage = "Name can contain at least 5 characters and maximum 30 characters", MinimumLength = 5)]
        public string Name { get; set; }
    }
}
