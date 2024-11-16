﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TicketReservationSystem.Entities;

#nullable disable

namespace TicketReservationSystem.Migrations
{
    [DbContext(typeof(TicketSystemDbContext))]
    [Migration("20241116184102_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TicketReservationSystem.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("TicketReservationSystem.Entities.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("TicketPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TotalSeats")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("TicketReservationSystem.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReservationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReservationId")
                        .IsUnique();

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("TicketReservationSystem.Entities.Reservation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.HasIndex("EventId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("TicketReservationSystem.Entities.Seat", b =>
                {
                    b.Property<int>("SeatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SeatId"));

                    b.Property<int>("EventId")
                        .HasColumnType("int");

                    b.Property<bool>("IsReserved")
                        .HasColumnType("bit");

                    b.Property<int?>("ReservationId")
                        .HasColumnType("int");

                    b.Property<string>("Row")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SeatNumber")
                        .HasColumnType("int");

                    b.HasKey("SeatId");

                    b.HasIndex("EventId");

                    b.HasIndex("ReservationId");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("TicketReservationSystem.Entities.Payment", b =>
                {
                    b.HasOne("TicketReservationSystem.Entities.Reservation", null)
                        .WithOne("Payment")
                        .HasForeignKey("TicketReservationSystem.Entities.Payment", "ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TicketReservationSystem.Entities.Reservation", b =>
                {
                    b.HasOne("TicketReservationSystem.Entities.Customer", "Customer")
                        .WithOne("Reservation")
                        .HasForeignKey("TicketReservationSystem.Entities.Reservation", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketReservationSystem.Entities.Event", "Event")
                        .WithMany("Reservations")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Event");
                });

            modelBuilder.Entity("TicketReservationSystem.Entities.Seat", b =>
                {
                    b.HasOne("TicketReservationSystem.Entities.Event", "Event")
                        .WithMany("Seats")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TicketReservationSystem.Entities.Reservation", "Reservation")
                        .WithMany("Seats")
                        .HasForeignKey("ReservationId");

                    b.Navigation("Event");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("TicketReservationSystem.Entities.Customer", b =>
                {
                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("TicketReservationSystem.Entities.Event", b =>
                {
                    b.Navigation("Reservations");

                    b.Navigation("Seats");
                });

            modelBuilder.Entity("TicketReservationSystem.Entities.Reservation", b =>
                {
                    b.Navigation("Payment");

                    b.Navigation("Seats");
                });
#pragma warning restore 612, 618
        }
    }
}