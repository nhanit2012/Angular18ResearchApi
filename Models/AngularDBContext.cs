﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TutorialApi.Models;

public partial class AngularDBContext : DbContext
{
    public AngularDBContext()
    {
    }

    public AngularDBContext(DbContextOptions<AngularDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Tutorial> Tutorials { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=IT-022\\SQLEXPRESS03;Database=AngularDB;User Id=sa; Password=tnhan@123;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tutorial>(entity =>
        {
            entity.ToTable("Tutorial");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .HasColumnName("description");
            entity.Property(e => e.Published).HasColumnName("published");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
