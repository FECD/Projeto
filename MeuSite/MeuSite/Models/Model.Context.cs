﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class edbancoEntities : DbContext
    {
        public edbancoEntities()
            : base("name=edbancoEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<arquivobiblioteca> arquivobiblioteca { get; set; }
        public virtual DbSet<arquivotarefa> arquivotarefa { get; set; }
        public virtual DbSet<chat> chat { get; set; }
        public virtual DbSet<sala> sala { get; set; }
        public virtual DbSet<tarefa> tarefa { get; set; }
        public virtual DbSet<temasala> temasala { get; set; }
        public virtual DbSet<usuario> usuario { get; set; }
        public virtual DbSet<usuariosala> usuariosala { get; set; }
    }
}
