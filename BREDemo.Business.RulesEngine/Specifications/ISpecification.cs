using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BREDemo.Business.RulesEngine.Entities;
using BREDemo.Data.Entities;

namespace BREDemo.Business.RulesEngine.Specifications
{
    public interface ISpecification
    {
        Outcome IsSatisfiedBy(IPackage Package);
    }
}

