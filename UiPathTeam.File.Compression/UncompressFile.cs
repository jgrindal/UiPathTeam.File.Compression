using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Activities;
using System.ComponentModel;

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

        private IUncompressor uncompressor;

        protected override void Execute(NativeActivityContext context)
        {
            switch (Format)
            {
                case SupportedTypes.AutoDetect:
                    uncompressor = this.DetectUncompressorFactory(FilePath.Get(context));
                    break;
                case SupportedTypes.ZIP:
                    uncompressor = new ZipUncompressor(FilePath.Get(context));
                    break;
                case SupportedTypes.RAR:
                    break;
                default:
                    throw new NotImplementedException("Format not implemented yet");
            }
        }

        protected IUncompressor DetectUncompressorFactory(String FilePath)
        {

            // TODO: replace this with a switch statement
            if (FilePath.Equals(""))
            {
                throw new ArgumentNullException("Please specify a valid filepath");
            }
            FilePath = FilePath.ToLower();

            if (FilePath.EndsWith(".zip"))
            {
                return new ZipUncompressor(FilePath);
            }
            else if (FilePath.EndsWith(".zipx"))
            {
                return new ZipXUncompressor(FilePath);
            }
            else if (FilePath.EndsWith(".rar"))
            {
                return new RarUncompressor(FilePath);
            }
            else if (FilePath.EndsWith(".gz") || FilePath.EndsWith(".tgz"))
            {
                return new GzUncompressor(FilePath);
            }
            else if (FilePath.EndsWith(".7z"))
            {
                return new SevenZUncompressor(FilePath);
            }
            else
            {
                throw new NotImplementedException("This filetype is not supported!");
            }
        }
    }
}
