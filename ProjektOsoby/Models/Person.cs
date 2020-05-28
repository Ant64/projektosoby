using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjektOsoby.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Sex { get; set; }
        public DateTime Added { get; set; }
        public bool IsActual { get; set; }

    }
}