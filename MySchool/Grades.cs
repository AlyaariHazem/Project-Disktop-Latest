//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MySchool
{
    using System;
    using System.Collections.Generic;
    
    public partial class Grades
    {
        public int id { get; set; }
        public int value { get; set; }
        public int studentID { get; set; }
    
        public virtual Students Students { get; set; }
    }
}
