﻿// <auto-generated />
using System;
using FeriasTJBase.Infra.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FeriasTJBase.Migrations
{
    [DbContext(typeof(PgDbContext))]
    [Migration("20241206152330_CreateTableFeriasUsufruto")]
    partial class CreateTableFeriasUsufruto
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FeriasTJBase.Domain.Entities.Ferias", b =>
                {
                    b.Property<int>("IdFerias")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdFerias"));

                    b.Property<int>("Matricula")
                        .HasColumnType("integer");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("PeriodoAquisitivoFinal")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("PeriodoAquisitivoInicial")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("IdFerias");

                    b.ToTable("Ferias");
                });

            modelBuilder.Entity("FeriasTJBase.Domain.Entities.Usufruto", b =>
                {
                    b.Property<int>("IdUsufruto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdUsufruto"));

                    b.Property<int>("IdFerias")
                        .HasColumnType("integer");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("UsufrutoFinal")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("UsufrutoInicial")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("IdUsufruto");

                    b.HasIndex("IdFerias");

                    b.ToTable("Usufruto");
                });

            modelBuilder.Entity("FeriasTJBase.Domain.Entities.Usufruto", b =>
                {
                    b.HasOne("FeriasTJBase.Domain.Entities.Ferias", "Ferias")
                        .WithMany("Usufrutos")
                        .HasForeignKey("IdFerias")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ferias");
                });

            modelBuilder.Entity("FeriasTJBase.Domain.Entities.Ferias", b =>
                {
                    b.Navigation("Usufrutos");
                });
#pragma warning restore 612, 618
        }
    }
}