using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KillerAppV2ASP.Models;

namespace KillerAppV2ASP.ViewModels
{
    public class EventsViewModel
    {
        public Character SelectedCharacter { get; set; }
        public EventSystem _EventSystem { get; set; }

        public UserLoginViewModel UserLoginViewModel { get; set; }
        public CharacterViewModel CharacterViewModel { get; set; }
    }
}