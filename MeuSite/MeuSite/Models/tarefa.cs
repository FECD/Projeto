//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MeuSite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tarefa
    {
        public int idtarefa { get; set; }
        public int idSala { get; set; }
        public string titulo { get; set; }
        public System.DateTime dataEntrega { get; set; }
        public bool entregue { get; set; }
    }
}
