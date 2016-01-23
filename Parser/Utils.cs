using System;
using System.Linq;
using System.Text;

namespace Parser
{
    public class Utils
    {
        public static byte[] HexStringToBytes(string hex)
        {
            return Enumerable.Range(0, hex.Length/2)
                .Select(x => Convert.ToByte(hex.Substring(x*2, 2), 16)).ToArray();
        }

        public static string BytesToHexString(byte[] src)
        {
            var sb = new StringBuilder(src.Length * 2);
            foreach (var b in src)
            {
                sb.AppendFormat("{0:X2}", b);
            }
            return sb.ToString();
        }
    }
}
