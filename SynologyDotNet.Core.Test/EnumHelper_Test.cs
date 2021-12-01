using Microsoft.VisualStudio.TestTools.UnitTesting;
using SynologyDotNet.Core.Helpers;
using SynologyDotNet.Core.Responses;

namespace SynologyDotNet.Core.Test
{
    [TestClass]
    public class EnumHelper_Test
    {
        [TestMethod]
        public void GetEnumDescription_Int()
        {
            string result = EnumHelper.GetEnumDescription<CommonErrorCode>(100);
            Assert.AreEqual("Unknown error.", result);
        }

        [TestMethod]
        public void GetEnumDescription_Enum()
        {
            string result = EnumHelper.GetEnumDescription(CommonErrorCode.UnknownError);
            Assert.AreEqual("Unknown error.", result);
        }
    }
}