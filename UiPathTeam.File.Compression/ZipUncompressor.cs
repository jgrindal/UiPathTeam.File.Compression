using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;

namespace UiPathTeam.File.Compression
{
    class ZipUncompressor : IUncompressor, IPasswordPossible
    {
        String password = "";

        /// <summary>
        /// Compressor Constructor
        /// </summary>
        /// <param name="FilePath"></param>
        public ZipUncompressor(String FilePath) : base(FilePath) { }

        /// <summary>
        /// Sets password for the uncompressor
        /// </summary>
        /// <param name="userPassword"></param>
        public void SetPassword(string userPassword)
        {
            this.password = userPassword;
        }

        /// <summary>
        /// Uncompresses the file
        /// </summary>
        public override void UncompressFile()
        {
            using (ZipFile zip = ZipFile.Read(FilePath))
            {
                if (password != "")
                {
                    zip.Password = password;
                }
                foreach (ZipEntry e in zip)
                {
                    e.Extract(OutputPath);
                }
            }
        }
    }
}
