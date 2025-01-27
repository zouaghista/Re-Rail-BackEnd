﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReRailBackEnd.Contexts;

#nullable disable

namespace ReRailBackEnd.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241113090252_alter1")]
    partial class alter1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ReRailBackEnd.Entities.TrackPoint", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Prediction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("trackSnapShotId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("trackSnapShotId");

                    b.ToTable("TrackPoints");
                });

            modelBuilder.Entity("ReRailBackEnd.Entities.TrackSnapShot", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Treated")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("ReRailBackEnd.Entities.TrackPoint", b =>
                {
                    b.HasOne("ReRailBackEnd.Entities.TrackSnapShot", "trackSnapShot")
                        .WithMany()
                        .HasForeignKey("trackSnapShotId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("trackSnapShot");
                });
#pragma warning restore 612, 618
        }
    }
}
