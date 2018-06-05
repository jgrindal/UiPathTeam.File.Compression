using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiPathTeam.File.Compression
{
    public abstract class IUncompressor
    {
        protected String FilePath;
        protected String OutputPath;

        public IUncompressor(String FilePath)
        {
            this.FilePath = FilePath;
            this.OutputPath = OutputPath;
        }

        public void SetOutputPath(String OutputPath)
        {
            this.OutputPath = OutputPath;
        }

        public abstract void UncompressFile();
    }
}
