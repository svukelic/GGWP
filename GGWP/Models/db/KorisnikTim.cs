//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GGWP.Models.db
{
    using System;
    using System.Collections.Generic;
    
    public partial class KorisnikTim
    {
        public int id { get; set; }
        public int korisnik_id { get; set; }
        public int tim_id { get; set; }
        public int tip { get; set; }
    
        public virtual Korisnik Korisnik { get; set; }
        public virtual Tim Tim { get; set; }
    }
}