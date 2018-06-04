using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiPathTeam.File.Compression
{
    interface IUncompressor
    {
        void UncompressFile(String FilePath);
    }
}
