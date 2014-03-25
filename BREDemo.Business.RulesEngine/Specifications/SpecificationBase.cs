using BREDemo.Business.RulesEngine.Entities;
using BREDemo.Data.Entities;
using System.Collections.Generic;
namespace BREDemo.Business.RulesEngine.Specifications
{
    public class SpecificationBase
    {
        public Outcome Outcome { get; set; }

        public SpecificationBase()
        {
            Outcome = new Outcome();
            Outcome.SpecificationName = this.GetType().FullName;
        }

    }
}
