﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Teledock.dbContext;

#nullable disable

namespace Teledock.Migrations
{
    [DbContext(typeof(Db))]
    [Migration("20241129210753_init3")]
    partial class init3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("Teledock.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Inn")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Инн");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("имя");

                    b.Property<int>("_TypeClient")
                        .HasColumnType("int")
                        .HasColumnName("тип");

                    b.Property<DateOnly>("dateAdd")
                        .HasColumnType("date")
                        .HasColumnName("дата добавления");

                    b.Property<DateOnly?>("dateUpdate")
                        .HasColumnType("date")
                        .HasColumnName("дата обновления");

                    b.HasKey("Id");

                    b.ToTable("клиенты");
                });

            modelBuilder.Entity("Teledock.Models.Founder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("FIO")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("фио");

                    b.Property<string>("Inn")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("Инн");

                    b.Property<DateOnly>("dateAdd")
                        .HasColumnType("date")
                        .HasColumnName("дата добавления");

                    b.Property<DateOnly?>("dateUpdate")
                        .HasColumnType("date")
                        .HasColumnName("дата обновления");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("учредители");
                });

            modelBuilder.Entity("Teledock.Models.Founder", b =>
                {
                    b.HasOne("Teledock.Models.Client", "client")
                        .WithMany("founders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("client");
                });

            modelBuilder.Entity("Teledock.Models.Client", b =>
                {
                    b.Navigation("founders");
                });
#pragma warning restore 612, 618
        }
    }
}
