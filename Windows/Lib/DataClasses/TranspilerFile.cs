using System;
using System.ComponentModel;
                        
namespace SSoTme.OST.Lib.DataClasses
{                            
    public partial class TranspilerFile 
    {
        public TranspilerFile()
        {
            this.InitPoco();
        }

        public object TranspileFileIdTranspileInputFiles { get; set; }
        // Your code goes here...
        // The "default" code is in the designer file
    }
}