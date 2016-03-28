namespace MeuSite.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model6 : DbContext
    {
        public Model6(string Conexao)
            : base(Conexao)
        {
        }
        public Model6()
        {
            Model6 obj = new Model6(Conexao);
        }

        const string Conexao = "metadata=res://*/Models.Model.csdl|res://*/Models.Model.ssdl|res://*/Models.Model.msl;provider=MySql.Data.MySqlClient;provider connection string='server=localhost;user id=root;persistsecurityinfo=True;database=edbanco'";
        static Model6 Instancia()
        {
            Model6 obj = new Model6(Conexao);
            return obj;
        }
        //public virtual DbSet<arquivobiblioteca> arquivobiblioteca { get; set; }
        //public virtual DbSet<arquivotarefa> arquivotarefa { get; set; }
        //public virtual DbSet<chat> chat { get; set; }
        //public virtual DbSet<sala> sala { get; set; }
        //public virtual DbSet<salabiblioteca> salabiblioteca { get; set; }
        //public virtual DbSet<tarefa> tarefa { get; set; }
        //public virtual DbSet<temasala> temasala { get; set; }
        //public virtual DbSet<usuario> usuario { get; set; }
        //public virtual DbSet<usuariosala> usuariosala { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<arquivobiblioteca>()
            //    .Property(e => e.nome)
            //    .IsUnicode(false);

            //modelBuilder.Entity<arquivobiblioteca>()
            //    .Property(e => e.conteudo)
            //    .IsUnicode(false);

            //modelBuilder.Entity<arquivobiblioteca>()
            //    .HasMany(e => e.salabiblioteca)
            //    .WithRequired(e => e.arquivobiblioteca)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<chat>()
            //    .Property(e => e.mensagem)
            //    .IsUnicode(false);

            //modelBuilder.Entity<sala>()
            //    .HasMany(e => e.tarefa)
            //    .WithRequired(e => e.sala)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<sala>()
            //    .HasMany(e => e.chat)
            //    .WithRequired(e => e.sala)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<sala>()
            //    .HasMany(e => e.salabiblioteca)
            //    .WithRequired(e => e.sala)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<sala>()
            //    .HasMany(e => e.usuariosala)
            //    .WithRequired(e => e.sala)
            //    .WillCascadeOnDelete(false);
            
            //modelBuilder.Entity<sala>()
            //    .Property(e => e.Nome)
            //    .IsUnicode(false);

            //modelBuilder.Entity<sala>()
            //    .Property(e => e.Descricao)
            //    .IsUnicode(false);

            //modelBuilder.Entity<tarefa>()
            //    .Property(e => e.titulo)
            //    .IsUnicode(false);

            //modelBuilder.Entity<temasala>()
            //    .Property(e => e.nome)
            //    .IsUnicode(false);

            //modelBuilder.Entity<temasala>()
            //    .HasMany(e => e.arquivobiblioteca)
            //    .WithRequired(e => e.temasala)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<temasala>()
            //    .HasMany(e => e.sala)
            //    .WithRequired(e => e.temasala)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<usuario>()
            //    .Property(e => e.nome)
            //    .IsUnicode(false);

            //modelBuilder.Entity<usuario>()
            //    .Property(e => e.email)
            //    .IsUnicode(false);

            //modelBuilder.Entity<usuario>()
            //    .Property(e => e.senha)
            //    .IsUnicode(false);
            
            //modelBuilder.Entity<usuario>()
            //    .HasMany(e => e.chat)
            //    .WithRequired(e => e.usuario)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<usuario>()
            //    .HasMany(e => e.usuariosala)
            //    .WithRequired(e => e.usuario)
            //    .WillCascadeOnDelete(false);
        }
    }
}
