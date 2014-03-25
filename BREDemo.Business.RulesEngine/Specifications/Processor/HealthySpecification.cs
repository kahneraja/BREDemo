using BREDemo.Business.RulesEngine.Entities;
using BREDemo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BREDemo.Business.RulesEngine.Specifications.Processor
{
    public class HealthySpecification : SpecificationBase, ISpecification
    {
        public Outcome IsSatisfiedBy(IPackage Package)
        {
            Animal animal = (Animal)Package.Entity;
            bool result = (animal.Health == HealthStatus.Healthy);
            this.Outcome.Result = result;
            return this.Outcome;
        }
    }
}
