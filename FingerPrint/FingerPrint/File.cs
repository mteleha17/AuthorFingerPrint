//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FingerPrint
{
    using System;
    using System.Collections.Generic;
    
    public partial class File
    {
        public int FileID { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public int QuoteInd { get; set; }
        public int File_GroupFileID { get; set; }
    
        public virtual NumbersWithQuote NumbersWithQuote { get; set; }
        public virtual NumbersWithoutQuote NumbersWithoutQuote { get; set; }
        public virtual File_Group File_Group { get; set; }
    }
}
