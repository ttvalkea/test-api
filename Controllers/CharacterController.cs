using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

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
            var heroes = new List<Database.Hero>();
            using (var db = new Database.DatabaseContext())
            {
                heroes = db.Hero.ToList();
            }
            var highestLevelHero = heroes.OrderByDescending(x => x.Level).First();
            return new Character()
            {
                Name = highestLevelHero.Name,
                Level = highestLevelHero.Level,
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
