namespace Employees.UITests
{
    using global::NUnit.Framework;
    using AutomatedTesting.Factory;
    using Employees.UITests.Common;

    /// <summary>
    /// Base class for NUnit tests to run using an in-memory WebApplicationFactory.
    /// Descendant classes Tests scoped to run in parallel.
    /// Hard-wired to use AutomatedTestServerFactory.
    /// </summary>
    /// <typeparam name="TStartup">Start Up class.</typeparam>
    [Parallelizable(ParallelScope.Children)]
    public abstract class BaseTestClass<TStartup>
        where TStartup : class
    {
        private AutomatedTestServerFactory<TStartup> _server;

        protected string RootUri { get; private set; }

        protected AutomatedTestServerFactory<TStartup> Server => _server;
        [OneTimeSetUp]
        public void Setup()
        {
            var factory = new AutomatedTestServerFactory<TStartup>(Configuration.AspnetcoreEnvironmentAutomatedTesting, Configuration.WebhostConfiguration);

            RootUri = factory.RootUri;

            TestContext.Out.WriteLine($"Server root: {factory.RootUri}");

            _server = factory;
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _server?.Dispose();
        }
    }
}