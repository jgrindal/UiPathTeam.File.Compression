using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;

namespace UiPathTeam.File.Compression
{
    class ZipUncompressor : IUncompressor
    {
        public ZipUncompressor(String FilePath) : base(FilePath) { }

        public override void UncompressFile()
        {
            using (ZipFile zip = ZipFile.Read(FilePath))
            {
                foreach (ZipEntry e in zip)
                {
                    e.Extract(OutputPath);
                }
            }
        }
    }
}
