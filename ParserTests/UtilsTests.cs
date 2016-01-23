using NUnit.Framework;
using Parser;

namespace ParserTests
{
    [TestFixture]
    public class UtilsTests
    {
        [Test]
        public void BytesToHexString_EmptyArray_EmptyString()
        {
            var actual = Utils.BytesToHexString(new byte[0]);

            Assert.AreEqual("", actual);
        }

        [Test]
        public void BytesToHexString_2Bytes_4HexChars()
        {
            var str = Utils.BytesToHexString(new byte[] {0x61, 0x62});

            Assert.AreEqual("6162", str);
        }

        [Test]
        public void HexStringToBytes_EmptyString_EmptyArray()
        {
            var actual = Utils.HexStringToBytes("");

            Assert.AreEqual(0, actual.Length);
        }

        [Test]
        public void HexStringToBytes_4HexChars_2Bytes()
        {
            var actual = Utils.HexStringToBytes("1234");

            Assert.AreEqual(2, actual.Length);
            Assert.AreEqual(0x12, actual[0]);
            Assert.AreEqual(0x34, actual[1]);
        }
    }
}
