﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UiPathTeam.File.Compression
{
    class GzUncompressor : IUncompressor
    {
        public GzUncompressor(String FilePath) : base(FilePath) { }

        public override void UncompressFile()
        {
            throw new NotImplementedException();
        }
    }
}

