﻿// <auto-generated />
using System;
using AccessControlReader.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AccessControlReader.Migrations
{
    [DbContext(typeof(AccessControlDbContext))]
    partial class AccessControlDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AccessControlReader.Entities.Card", b =>
                {
                    b.Property<int>("Card_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Card_ID"));

                    b.Property<long>("Card_ID_number")
                        .HasColumnType("bigint");

                    b.Property<string>("Card_UID")
                        .HasColumnType("varchar(25)");

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("Tier")
                        .HasColumnType("smallint");

                    b.Property<int>("User_ID")
                        .HasColumnType("int");

                    b.Property<int?>("User_ID1")
                        .HasColumnType("int");

                    b.HasKey("Card_ID");

                    b.HasIndex("User_ID1");

                    b.ToTable("CardItems");
                });

            modelBuilder.Entity("AccessControlReader.Entities.Reader", b =>
                {
                    b.Property<int>("Reader_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Reader_ID"));

                    b.Property<string>("Comment")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("ErrorNo")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("MACaddr")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<short>("Tier")
                        .HasColumnType("smallint");

                    b.Property<string>("ToShow")
                        .HasMaxLength(34)
                        .HasColumnType("nvarchar(34)");

                    b.HasKey("Reader_ID");

                    b.ToTable("ReadersItems");
                });

            modelBuilder.Entity("AccessControlReader.Entities.Reading", b =>
                {
                    b.Property<int>("Reading_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Reading_ID"));

                    b.Property<bool>("Access")
                        .HasColumnType("bit");

                    b.Property<int?>("Card_ID")
                        .HasColumnType("int");

                    b.Property<decimal?>("Card_ID_number")
                        .HasMaxLength(12)
                        .HasColumnType("decimal(20,0)");

                    b.Property<string>("Data")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Reader_ID")
                        .HasColumnType("int");

                    b.Property<int?>("Reader_ID1")
                        .HasColumnType("int");

                    b.Property<DateTime>("Time")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<int?>("User_ID")
                        .HasColumnType("int");

                    b.Property<int?>("User_ID1")
                        .HasColumnType("int");

                    b.HasKey("Reading_ID");

                    b.HasIndex("Card_ID");

                    b.HasIndex("Reader_ID1");

                    b.HasIndex("User_ID1");

                    b.ToTable("ReadingsItems");
                });

            modelBuilder.Entity("AccessControlReader.Entities.User", b =>
                {
                    b.Property<int>("User_ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("User_ID"));

                    b.Property<string>("FirstName")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("LastName")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("User_ID");

                    b.ToTable("UsersItems");
                });

            modelBuilder.Entity("AccessControlReader.Entities.Card", b =>
                {
                    b.HasOne("AccessControlReader.Entities.User", "User")
                        .WithMany("Cards")
                        .HasForeignKey("User_ID1");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AccessControlReader.Entities.Reading", b =>
                {
                    b.HasOne("AccessControlReader.Entities.Card", "Card")
                        .WithMany("Readings")
                        .HasForeignKey("Card_ID");

                    b.HasOne("AccessControlReader.Entities.Reader", "Reader")
                        .WithMany("Readings")
                        .HasForeignKey("Reader_ID1");

                    b.HasOne("AccessControlReader.Entities.User", "User")
                        .WithMany("Readings")
                        .HasForeignKey("User_ID1");

                    b.Navigation("Card");

                    b.Navigation("Reader");

                    b.Navigation("User");
                });

            modelBuilder.Entity("AccessControlReader.Entities.Card", b =>
                {
                    b.Navigation("Readings");
                });

            modelBuilder.Entity("AccessControlReader.Entities.Reader", b =>
                {
                    b.Navigation("Readings");
                });

            modelBuilder.Entity("AccessControlReader.Entities.User", b =>
                {
                    b.Navigation("Cards");

                    b.Navigation("Readings");
                });
#pragma warning restore 612, 618
        }
    }
}