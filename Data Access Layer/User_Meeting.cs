//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Data_Access_Layer
{
    using System;
    using System.Collections.Generic;
    
    public partial class User_Meeting
    {
        public int Id { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<int> MeetingId { get; set; }
    
        public virtual Meeting Meeting { get; set; }
        public virtual User User { get; set; }
    }
}
