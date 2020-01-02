using System;
using System.Collections.Generic;

namespace TestApi
{
    public class Character
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public List<Item> Gear { get; set; }

    }
}
