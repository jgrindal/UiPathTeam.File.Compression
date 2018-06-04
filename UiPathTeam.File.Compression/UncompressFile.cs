using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;
using System.IO.Compression;
using Ionic.Zip;

namespace UiPathTeam.File.Compression
{
    public class UncompressFile : NativeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<String> FilePath { get; set; }

        [Category("Input")]
        [RequiredArgument]
        public InArgument<String> OutputPath { get; set; }

        [Category("Input")]
        public InArgument<String> Password { get; set; }   // TODO: Make SecureString

        [Category("Input")]
        public SupportedTypes Format { get; set; }  

        protected override void Execute(NativeActivityContext context)
        {
            if(Format == SupportedTypes.AutoDetect)
            {
                Format = this.DetectType();
            }

            switch (Format)
            {
                case SupportedTypes.ZIP:
                    UncompressZIP(FilePath.Get(context), OutputPath.Get(context));
                    break;
                case SupportedTypes.RAR:
                    UncompressRAR();
                    break;
                default:
                    throw new NotImplementedException("Format not implemented yet");
            }
        }

        /**
         * Uncompresses the file in RAR format at FilePath to output location
         * 
         */
        private void UncompressRAR()
        {
            throw new NotImplementedException();
        }

        /**
         * Uncompresses the file in ZIP format at FilePath to output location
         * 
         */
        private void UncompressZIP(String FilePath, String OutputPath)
        {
            using (ZipFile zip = ZipFile.Read(FilePath))
            {
                foreach (ZipEntry e in zip)
                {
                    e.Extract(OutputPath);
                }
            }
        }

        protected SupportedTypes DetectType()
        {
            // TODO: Implement detection of type based on file extension
            return SupportedTypes.ZIP;
        }
    }
}
