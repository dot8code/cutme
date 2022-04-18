﻿// <auto-generated />
using CutMe.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CutMe.Migrations
{
    [DbContext(typeof(RedirectionDbContext))]
    [Migration("20220412213013_AddRedirectInformation")]
    partial class AddRedirectInformation
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("CutMe.Models.RedirectInformation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FullUrl")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Shortcut")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("RedirectInformations");
                });
#pragma warning restore 612, 618
        }
    }
}
