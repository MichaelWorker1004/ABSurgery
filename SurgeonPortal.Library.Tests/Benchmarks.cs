using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using NUnit.Framework;
using SurgeonPortal.Library.Tests.Scoring;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurgeonPortal.Library.Tests
{
    public class Benchmarks
    {
        public Benchmarks() { }

        [Test]
        public void Run_Benchmarks()
        {
            var logger = new AccumulationLogger();

            var config = ManualConfig.Create(DefaultConfig.Instance)
                .AddLogger(logger)
                .WithOptions(ConfigOptions.DisableOptimizationsValidator);

            BenchmarkRunner.Run<CaseFeedbackReadOnlyTests>(config);

            // write benchmark summary
            TestContext.WriteLine(logger.GetLog());
        }

    }
}
