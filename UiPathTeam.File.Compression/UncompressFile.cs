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
                case SupportedTypes.ZIPX:
                    uncompressor = new ZipXUncompressor(FilePath.Get(context));
                    break;
                case SupportedTypes.RAR:
                    uncompressor = new RarUncompressor(FilePath.Get(context));
                    break;
                case SupportedTypes.GZ:
                    uncompressor = new GzUncompressor(FilePath.Get(context));
                    break;
                case SupportedTypes.SevenZip:
                    uncompressor = new SevenZUncompressor(FilePath.Get(context));
                    break;
                default:
                    throw new NotImplementedException("Format not implemented yet");
            }
        }

        protected IUncompressor DetectUncompressorFactory(String FilePath)
        {
            if (FilePath.Equals(""))
            {
                throw new ArgumentNullException("Please specify a valid filepath");
            }
            String FilePathLower = FilePath.ToLower();
            String suffix = FilePathLower.Substring(FilePathLower.LastIndexOf("."));

            switch (suffix)
            {
                case ".zip":
                    return new ZipUncompressor(FilePath);
                case ".zipx":
                    return new ZipXUncompressor(FilePath);
                case ".rar":
                    return new RarUncompressor(FilePath);
                case ".tz":
                case ".tgz":
                    return new GzUncompressor(FilePath);
                case ".7z":
                    return new SevenZUncompressor(FilePath);
                default:
                    throw new NotImplementedException("This filetype is not supported!");
            }
        }
    }
}
