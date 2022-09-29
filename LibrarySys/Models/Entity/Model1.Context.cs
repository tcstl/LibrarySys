﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LibrarySys.Models.Entity
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class LibSysEntities : DbContext
    {
        public LibSysEntities()
            : base("name=LibSysEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }
        public virtual DbSet<Reader> Readers { get; set; }
        public virtual DbSet<ReaderBorrow> ReaderBorrows { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    
        public virtual ObjectResult<Top10Author_Result> Top10Author()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Top10Author_Result>("Top10Author");
        }
    
        public virtual ObjectResult<Top10ReadedBook_Result> Top10ReadedBook()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Top10ReadedBook_Result>("Top10ReadedBook");
        }
    
        public virtual ObjectResult<Top10Reader_Result> Top10Reader()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Top10Reader_Result>("Top10Reader");
        }
    }
}
