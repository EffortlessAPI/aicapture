using System;
using System.ComponentModel;
                        
namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class FileSetFile 
    {
        private void InitPoco()
        {
            
            this.FileSetFileId = Guid.NewGuid();
            
        }
        
        public Guid FileSetFileId { get; set; }
    
        public Guid FileSetId { get; set; }
    
        public String RelativePath { get; set; }
    
        public String FileContents { get; set; }
    
        public Byte[] ZippedFileContents { get; set; }
    
        public Byte[] BinaryFileContents { get; set; }
    
        public Byte[] ZippedTextFileContents { get; set; }
    
        public Byte[] ZippedBinaryFileContents { get; set; }
    
        public Boolean AlwaysOverwrite { get; set; }
    
        public Boolean SkipClean { get; set; }
    
        
    }
}