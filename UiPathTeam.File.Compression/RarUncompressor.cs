using SharpCompress.Readers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiPathTeam.File.Compression
{
    class RarUncompressor : IUncompressor
    {
        public RarUncompressor(String FilePath) : base(FilePath) { }

        public override void UncompressFile()
        {
            using (Stream stream = System.IO.File.OpenRead(this.OutputPath))
            {
                var reader = ReaderFactory.Open(stream);
                while (reader.MoveToNextEntry())
                {
                    if (!reader.Entry.IsDirectory)
                    {
                        Console.WriteLine(reader.Entry.Key);
                        reader.WriteEntryToDirectory(OutputPath, new ExtractionOptions() { ExtractFullPath = true, Overwrite = true });
                    }
                }
            }
        }
    }
}
