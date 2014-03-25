using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BREDemo.Business.RulesEngine.Entities
{
    public class Outcome
    {
        public string SpecificationName { get; set; }
        public bool Result { get; set; }

        public Outcome()
        {
            Result = true;
        }
    }
}
