using System;
using System.Collections.Generic;
using System.Text;

namespace RedisDotNetConsoleFirstExample.Entities
{
    class Town
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public City City { get; set; }
    }
}
