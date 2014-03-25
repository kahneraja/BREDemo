using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BREDemo.Business.RulesEngine.Specifications;
using BREDemo.Data.Entities;

namespace BREDemo.Business.RulesEngine.Entities
{
    public class Workflow
    {
        public string Specification;
        public string Condition;
        public List<Workflow> Workflows;
        public List<Outcome> Outcomes;

        public Workflow()
        {
            Workflows = new List<Workflow>();
            Outcomes = new List<Outcome>();
            Specification = "WorkflowSpecification";
        }
    }
}
