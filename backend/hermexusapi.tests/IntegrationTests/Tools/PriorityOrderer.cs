using Xunit.Abstractions;
using Xunit.Sdk;

namespace hermexusapi.tests.IntegrationTests.Tools
{
    public class PriorityOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>
            (IEnumerable<TTestCase> testCases) where TTestCase
            : ITestCase
        {
            var SortedMethods = testCases.OrderBy(
                tc => tc.TestMethod.Method
                    .GetCustomAttributes(typeof(TestPriorityAttribute))
                    .FirstOrDefault()
                    ?.GetNamedArgument<int>("Priority") ?? 0);
            return SortedMethods;
        }
    }
    public class TestPriorityAttribute : Attribute
    {
        public int Priority { get; }
        public TestPriorityAttribute(int priority)
            => Priority = priority;
    }
}
