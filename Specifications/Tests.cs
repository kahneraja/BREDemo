using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BREDemo.Data.Entities;
using BREDemo.Business.RulesEngine.Specifications;
using BREDemo.Business.RulesEngine.Entities;
using BREDemo.Business.RulesEngine.Specifications.Processor;
using System.Reflection;
using System.Diagnostics;

namespace Specifications
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void single_spec()
        {
            Animal animal = new Animal { Age = 1, Health = HealthStatus.Healthy, Type = AnimalType.Cow };

            var package = new IPackage() { Entity = animal };
            
            var spec = new CowSpecification();

            Outcome outcome = spec.IsSatisfiedBy(package);

            Assert.IsTrue(outcome.Result);
        }

        [TestMethod]
        public void workflow_single_spec()
        {
            Animal animal = new Animal { Age = 1, Health = HealthStatus.Healthy, Type = AnimalType.Cow };
            var package = new IPackage() { Entity = animal };

            Workflow parentWorkflow = new Workflow();

            Workflow childworkflow = new Workflow();
            childworkflow.Specification = "Processor.CowSpecification";
            childworkflow.Condition = "And";

            parentWorkflow.Workflows.Add(childworkflow);

            var spec = new WorkflowSpecification();
            spec.Workflow.Workflows.Add(parentWorkflow);

            Outcome outcome = spec.IsSatisfiedBy(package);

            TraceWorkflow(parentWorkflow);

            Assert.IsTrue(outcome.Result);
        }

        [TestMethod]
        public void workflow_nested_spec()
        {
            Animal animal = new Animal { Age = 1, Health = HealthStatus.Healthy, Type = AnimalType.Cow };
            var package = new IPackage() { Entity = animal };

            Workflow grandchildWorkflow = new Workflow();
            grandchildWorkflow.Workflows.Add(new Workflow() { Specification = "Processor.CowSpecification" });
            
            Workflow childworkflow = new Workflow();
            childworkflow.Workflows.Add(new Workflow() { Specification = "Processor.HealthySpecification" });
            childworkflow.Workflows.Add(grandchildWorkflow);

            Workflow parentWorkflow = new Workflow();
            parentWorkflow.Workflows.Add(new Workflow() { Specification = "Processor.HeavySpecification" });
            parentWorkflow.Workflows.Add(childworkflow);

            Workflow grandParentWorkflow = new Workflow();
            grandParentWorkflow.Workflows.Add(new Workflow() { Specification = "Processor.YoungSpecification" });
            grandParentWorkflow.Workflows.Add(parentWorkflow);

            var spec = new WorkflowSpecification();
            spec.Workflow = grandParentWorkflow;

            Outcome outcome = spec.IsSatisfiedBy(package);

            TraceWorkflow(parentWorkflow);

            Assert.IsFalse(outcome.Result);
        }

        [TestMethod]
        public void get_type_from_name()
        {
            CowSpecification spec = new CowSpecification();
            string name = "CowSpecification";
            string nSpace = spec.GetType().Namespace;

            Assembly assembly = typeof(CowSpecification).Assembly; // in the same assembly!
            Type type = assembly.GetType(string.Format("{0}.{1}", nSpace, name)); // full name - i.e. with namespace (perhaps concatenate)
            object obj = Activator.CreateInstance(type);

            Assert.IsNotNull(obj);

        }

        public void TraceWorkflow(Workflow Workflow)
        {
            foreach (var workflow in Workflow.Workflows)
            {
                TraceWorkflow(workflow);
            }

            foreach (var outcome in Workflow.Outcomes)
            {
                TraceOutcome(outcome);
            }
        }

        private void TraceOutcome(Outcome outcome)
        {
            Debug.WriteLine(string.Format("[{0}] {1}", outcome.Result, outcome.SpecificationName));
        }



    }
}
