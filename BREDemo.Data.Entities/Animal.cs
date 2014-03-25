using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BREDemo.Data.Entities
{
    public class Animal : IEntity
    {
        public int Id { get; set; }
        public AnimalType Type { get; set; }
        public int Age { get; set; }
        public HealthStatus Health { get; set; }
        public float Weight { get; set; }
    }
}
