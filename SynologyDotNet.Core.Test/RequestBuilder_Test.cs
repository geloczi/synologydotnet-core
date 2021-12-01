using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynologyDotNet.Core.Helpers;

namespace SynologyDotNet.Core.Test
{
    [TestClass]
    public class RequestBuilder_Test
    {
        [TestMethod]
        public void ToPostRequest()
        {
            

            var r = new RequestBuilder("AudioStation/album.cgi", "SYNO.AudioStation.Album", 1, "list").ToPostRequest();
            Assert.AreEqual("AudioStation/album.cgi", r.RequestUri.OriginalString);
            Assert.AreEqual(HttpMethod.Post, r.Method);
            if (r.Content is FormUrlEncodedContent form)
            {
                var content = form.ReadAsStringAsync().Result;
                Assert.AreEqual(@"api=SYNO.AudioStation.Album&version=1&method=list", content);
            }
            else
                Assert.Fail("Should be FormUrlEncodedContent");
        }
    }
}
