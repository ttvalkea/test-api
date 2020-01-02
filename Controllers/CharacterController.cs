using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        [HttpGet]
        public Character Get()
        {
            var rng = new Random();
            
            return new Character()
            {
                Name = "Conan",
                Level = rng.Next(1, 10),
                Gear = new List<Item>()
                {
                    new Item()
                    {
                        Name = "Axe", Damage = rng.Next(35, 50), Defence = 0
                    },
                    new Item()
                    {
                        Name = "Shield", Damage = 0, Defence = rng.Next(10, 30)
                    }
                }
            };
        }
    }
}
