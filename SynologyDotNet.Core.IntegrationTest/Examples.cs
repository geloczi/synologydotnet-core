using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynologyDotNet.Core.Helpers.Testing;

namespace SynologyDotNet.Core.IntegrationTest
{
    [TestClass]
    public class Examples : TestBase
    {
        public static Uri MyServer { get; } = new Uri(CoreConfig.Server);

        [TestMethod]
        public async Task Example1()
        {
            var client = new SynoClient(MyServer, true);
            var apis = await client.QueryApiInfos();
            foreach (var api in apis)
                Debug.WriteLine(api.ToString());
        }

    }
}
