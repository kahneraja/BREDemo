using BREDemo.Business.RulesEngine.Entities;
using BREDemo.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BREDemo.Business.RulesEngine.Specifications
{
    public class WorkflowSpecification : SpecificationBase, ISpecification
    {
        public Workflow Workflow { get; set; }

        public WorkflowSpecification()
        {
        }

        public Outcome IsSatisfiedBy(IPackage Package)
        {
            IsSatisfiedBySpecifications(Package);

            if (Workflow.Condition == "Or")
            {
                if (Workflow.Outcomes.Exists(x => x.Result))
                { 
                    Outcome.Result = true;
                }
            }
            else if (Workflow.Condition == "Not")
            {
                Outcome.Result = !Outcome.Result;
            }
            else if (Workflow.Outcomes.Exists(x => x.Result == false))
            {
                Outcome.Result = false;
            }

            return this.Outcome;
        }

        private void IsSatisfiedBySpecifications(IPackage Package)
        {
            foreach (var workflow in this.Workflow.Workflows)
            {
                string nSpace = this.GetType().Namespace;
                Assembly assembly = typeof(WorkflowSpecification).Assembly;

                Type type = assembly.GetType(string.Format("{0}.{1}", nSpace, workflow.Specification));

                ISpecification spec = (ISpecification)Activator.CreateInstance(type);

                Outcome outcome;

                // TODO: Conditional smell.
                if (spec.GetType() == typeof(WorkflowSpecification))
                {
                    WorkflowSpecification workflowSpecification = (WorkflowSpecification)spec;
                    workflowSpecification.Workflow = workflow;
                    outcome = workflowSpecification.IsSatisfiedBy(Package);
                }
                else
                {
                    outcome = spec.IsSatisfiedBy(Package);
                }

                Workflow.Outcomes.Add(outcome);
            }
        }

    }
}
