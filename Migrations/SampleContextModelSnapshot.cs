﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using demo.Models;

#nullable disable

namespace demo.Migrations
{
    [DbContext(typeof(SampleContext))]
    partial class SampleContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("demo.Models.AdminTbl", b =>
                {
                    b.Property<int>("AdminId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdminId"), 1L, 1);

                    b.Property<string>("AdminName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AdminId");

                    b.ToTable("AdminTbls");
                });

            modelBuilder.Entity("demo.Models.BookingTbl", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookingId"), 1L, 1);

                    b.Property<int>("AmountTotal")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<string>("MovieName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NoOfTickets")
                        .HasColumnType("int");

                    b.Property<string>("SeatNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("BookingId");

                    b.HasIndex("MovieId");

                    b.HasIndex("UserId");

                    b.ToTable("BookingTbl");
                });

            modelBuilder.Entity("demo.Models.MovieTbl", b =>
                {
                    b.Property<int>("MovieId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MovieId"), 1L, 1);

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MovieName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Slot")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("capacity")
                        .HasColumnType("int");

                    b.HasKey("MovieId");

                    b.ToTable("MovieTbls");
                });

            modelBuilder.Entity("demo.Models.OrderDetails", b =>
                {
                    b.Property<int>("OrderDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderDetailId"), 1L, 1);

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<DateTime>("MovieDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<string>("MovieName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NoOfTickets")
                        .HasColumnType("int");

                    b.Property<int>("OrderMasterId")
                        .HasColumnType("int");

                    b.Property<int?>("OrderMasterTblOrderMasterId")
                        .HasColumnType("int");

                    b.Property<string>("SeatNo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("bookingtblBookingId")
                        .HasColumnType("int");

                    b.Property<int?>("usertblUserId")
                        .HasColumnType("int");

                    b.HasKey("OrderDetailId");

                    b.HasIndex("OrderMasterTblOrderMasterId");

                    b.HasIndex("bookingtblBookingId");

                    b.HasIndex("usertblUserId");

                    b.ToTable("OrderDetails");
                });

            modelBuilder.Entity("demo.Models.OrderMasterTbl", b =>
                {
                    b.Property<int>("OrderMasterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderMasterId"), 1L, 1);

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("CardNo")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Paid")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderMasterId");

                    b.HasIndex("UserId");

                    b.ToTable("OrderMasterTbls");
                });

            modelBuilder.Entity("demo.Models.UserTbl", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("UserTbls");
                });

            modelBuilder.Entity("demo.Models.BookingTbl", b =>
                {
                    b.HasOne("demo.Models.MovieTbl", "Movie")
                        .WithMany("Bookings")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("demo.Models.UserTbl", "User")
                        .WithMany("BookingTbls")
                        .HasForeignKey("UserId");

                    b.Navigation("Movie");

                    b.Navigation("User");
                });

            modelBuilder.Entity("demo.Models.OrderDetails", b =>
                {
                    b.HasOne("demo.Models.OrderMasterTbl", "OrderMasterTbl")
                        .WithMany("Details")
                        .HasForeignKey("OrderMasterTblOrderMasterId");

                    b.HasOne("demo.Models.BookingTbl", "bookingtbl")
                        .WithMany("OrderDetails")
                        .HasForeignKey("bookingtblBookingId");

                    b.HasOne("demo.Models.UserTbl", "usertbl")
                        .WithMany("Details")
                        .HasForeignKey("usertblUserId");

                    b.Navigation("OrderMasterTbl");

                    b.Navigation("bookingtbl");

                    b.Navigation("usertbl");
                });

            modelBuilder.Entity("demo.Models.OrderMasterTbl", b =>
                {
                    b.HasOne("demo.Models.UserTbl", "user")
                        .WithMany("OrderMasterTbls")
                        .HasForeignKey("UserId");

                    b.Navigation("user");
                });

            modelBuilder.Entity("demo.Models.BookingTbl", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("demo.Models.MovieTbl", b =>
                {
                    b.Navigation("Bookings");
                });

            modelBuilder.Entity("demo.Models.OrderMasterTbl", b =>
                {
                    b.Navigation("Details");
                });

            modelBuilder.Entity("demo.Models.UserTbl", b =>
                {
                    b.Navigation("BookingTbls");

                    b.Navigation("Details");

                    b.Navigation("OrderMasterTbls");
                });
#pragma warning restore 612, 618
        }
    }
}
