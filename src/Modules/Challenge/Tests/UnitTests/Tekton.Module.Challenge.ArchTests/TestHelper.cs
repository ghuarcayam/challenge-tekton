
using NetArchTest.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tekton.Module.Challenge.Application.Contract;
using Tekton.Module.Challenge.Domain.Products;
using Tekton.Module.Challenge.Infraestructure;

namespace Tekton.Module.Challenge.ArchTests
{
    internal class TestHelper
    {
         public static Assembly ApplicationAssembly => typeof(IChallengeModule).Assembly;

        public static Assembly DomainAssembly => typeof(Product).Assembly;

        public static Assembly InfrastructureAssembly => typeof(ChallengeModule).Assembly;

        public static void AssertAreImmutable(IEnumerable<Type> types)
        {
            IList<Type> failingTypes = new List<Type>();
            foreach (var type in types)
            {
                if (type.GetFields().Any(x => !x.IsInitOnly) || type.GetProperties().Any(x => x.CanWrite))
                {
                    failingTypes.Add(type);
                    break;
                }
            }

            AssertFailingTypes(failingTypes);
        }
        public static void AssertFailingTypes(IEnumerable<Type> types)
        {
            Assert.That(types, Is.Null.Or.Empty);
        }

        public static void AssertArchTestResult(TestResult result)
        {
            AssertFailingTypes(result.FailingTypes);
        }
    }
}
