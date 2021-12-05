using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynologyDotNet.Core.Helpers.Testing;
using SynologyDotNet.Core.Model;

namespace SynologyDotNet.Core.IntegrationTest
{
    [TestClass]
    public class SynoClient_Tests : TestBase
    {
        static SynoClient Client;
        static SynoSession Session;
        //const string SessionName = ""; // "DSM" is accepted too

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            // Connect to the server
            Client = new SynoClient(new Uri(CoreConfig.Server), true);
            //Client.ConnectAsync().Wait();
            // Login
            Session = Client.LoginAsync(CoreConfig.Username, CoreConfig.Password/*, SessionName*/).Result;
            Assert.IsNotNull(Session);
        }

        [TestMethod]
        public void CheckSession()
        {
            Assert.IsNotNull(Session);
            //Assert.AreEqual(Session.Name, SessionName);
            Assert.IsFalse(string.IsNullOrEmpty(Session.Id));
            Assert.IsFalse(string.IsNullOrEmpty(Session.Token));
            Assert.IsTrue(Session.Cookie.Length > 0);
        }

        [TestMethod]
        public async Task LoginWithPreviousSessionAsync()
        {
            var client2 = new SynoClient(new Uri(CoreConfig.Server), true);
            await client2.LoginWithPreviousSessionAsync(Session);
        }

        [TestMethod]
        public async Task LoginWithPreviousSessionAsync_Invalid()
        {
            var client2 = new SynoClient(new Uri(CoreConfig.Server), true);
            try
            {
                await client2.LoginWithPreviousSessionAsync(new SynoSession()
                {
                    Id = Session.Id,
                    Name = Session.Name,
                    Token = Session.Token + "123",
                    Cookie = Session.Cookie.Select(x => "invalid cookie").ToArray()
                });
                Assert.Fail("Exception must be thrown on invalid session.");
            }
            catch
            {
            }
        }

        [TestMethod]
        public async Task GetExternalIpAndHostname()
        {
            var result = await Client.GetExternalIpAndHostname();
            Assert.IsTrue(result.Success);
            // result.Data.DdnsHostname can be empty if it's not configured on the server, so makes no sense to test this.
            Assert.IsFalse(string.IsNullOrWhiteSpace(result.Data.ExternalIp)); // But external IP must be there.
        }

        [TestMethod]
        public async Task GetApiList()
        {
            var allApis = await Client.QueryApiInfos();
            //System.IO.File.WriteAllLines("api_list.txt", allApis.Select(x => x.ToString()).ToArray());
            Assert.IsNotNull(allApis);
            Assert.IsTrue(allApis.Length > 0);
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            // Logout
            var logoutResult = Client.LogoutAsync().Result;
            Assert.IsTrue(logoutResult);
            Client.Dispose();
        }
    }
}
