using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Asteroids.Objects;

namespace Asteroids.EntityManagement
{
    public class GameObjects
    {
        public Ship Ship { get; set; }
        public Bullet Bullet { get; set; }
        public List<Asteroid> Asteroids { get; set; }
        public Score Score { get; set; }
    }
}
