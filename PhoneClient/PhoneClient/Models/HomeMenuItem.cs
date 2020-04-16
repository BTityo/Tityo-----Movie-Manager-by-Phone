using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneClient.Models
{
    public enum MenuItemType
    {
        Connection,
        MoviesList,
        Command,
        Log
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
