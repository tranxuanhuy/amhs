using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amhs
{
    class ReadWriteFile
    {
        private string sFileName;
        private string data;

        public ReadWriteFile()
        {
        }

        public ReadWriteFile(string sFileName, string data)
        {
            this.sFileName = sFileName;
            this.data = data;
        }

        internal void Append()
        {
            //plaint text log for debug
            using (StreamWriter w = File.AppendText(Path.Combine(Directory.GetCurrentDirectory(), "log", sFileName)))
            {
                w.WriteLine(data);
            }

            //crypto log for program logging
            var key = Encoding.UTF8.GetBytes("Simple key");
            var iv = Encoding.UTF8.GetBytes("Simple IV");

            Array.Resize(ref key, 128 / 8);
            Array.Resize(ref iv, 128 / 8);

            //if (File.Exists("test.bin"))
            //{
            //    File.Delete("test.bin");
            //}


            Crypto.AppendStringToFile(Path.Combine(Directory.GetCurrentDirectory(), "cryptolog", sFileName), data+"xuongdong", key, iv);


            ////string plainText = ReadStringFromFile("test.bin", key, iv);
        }

        internal IEnumerable<string> ReadLines(string fileName)
        {
            var key = Encoding.UTF8.GetBytes("Simple key");
            var iv = Encoding.UTF8.GetBytes("Simple IV");

            Array.Resize(ref key, 128 / 8);
            Array.Resize(ref iv, 128 / 8);

          
            string plainText = Crypto.ReadStringFromFile(Path.Combine(Directory.GetCurrentDirectory(), "cryptolog", fileName), key, iv);
            return plainText.Split(new[] { "xuongdong" }, StringSplitOptions.None);
        }
    }
}
