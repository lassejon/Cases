using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CsvHelper.Configuration.Attributes;

namespace Password
{
    public class User
    {
        // index er property attributer tilhørende csvhelper bilioteket. Det sørger for at de rette
        // værdier i csv filen matcher klasse strukturen
        [Index(0)]
        public string Name { get; set; }

        [Index(1)]
        public string Password { get; set; }

        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public override string ToString()
        {
            return $"Name: {Name} password: {Password}";
        }
    }
}
