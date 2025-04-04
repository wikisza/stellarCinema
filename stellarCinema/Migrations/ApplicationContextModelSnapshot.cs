﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using stellarCinema.Entities;

#nullable disable

namespace stellarCinema.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("stellarCinema.Entities.Configuration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("KeyValue")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.HasKey("Id");

                    b.ToTable("Configurations");
                });

            modelBuilder.Entity("stellarCinema.Entities.Hall", b =>
                {
                    b.Property<int>("IdHall")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdHall"));

                    b.Property<string>("HallName")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<int>("TotalSeats")
                        .HasColumnType("int");

                    b.HasKey("IdHall");

                    b.ToTable("Halls");
                });

            modelBuilder.Entity("stellarCinema.Entities.Movie", b =>
                {
                    b.Property<int>("IdMovie")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdMovie"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time(6)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("MovieLink")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("PosterUrl")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<DateOnly>("ReleaseDate")
                        .HasColumnType("date");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("IdMovie");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("stellarCinema.Entities.Payment", b =>
                {
                    b.Property<int>("IdPayment")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdPayment"));

                    b.Property<decimal>("Amount")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)");

                    b.Property<int>("IdReservation")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .HasPrecision(2)
                        .HasColumnType("datetime(2)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("IdPayment");

                    b.HasIndex("IdReservation")
                        .IsUnique();

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("stellarCinema.Entities.Reservation", b =>
                {
                    b.Property<int>("IdReservation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdReservation"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("IdShowtime")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReservationDate")
                        .HasPrecision(2)
                        .HasColumnType("datetime(2)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar(30)");

                    b.HasKey("IdReservation");

                    b.HasIndex("IdShowtime");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("stellarCinema.Entities.ReservationSeat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdReservation")
                        .HasColumnType("int");

                    b.Property<int>("IdSeat")
                        .HasColumnType("int");

                    b.Property<int>("IdShowtime")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdReservation");

                    b.HasIndex("IdSeat");

                    b.HasIndex("IdShowtime");

                    b.ToTable("ReservationSeats");
                });

            modelBuilder.Entity("stellarCinema.Entities.Seat", b =>
                {
                    b.Property<int>("IdSeat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdSeat"));

                    b.Property<int>("IdHall")
                        .HasColumnType("int");

                    b.Property<string>("SeatNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("IdSeat");

                    b.HasIndex("IdHall");

                    b.ToTable("Seats");
                });

            modelBuilder.Entity("stellarCinema.Entities.Showtime", b =>
                {
                    b.Property<int>("IdShowtime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("IdShowtime"));

                    b.Property<int>("IdHall")
                        .HasColumnType("int");

                    b.Property<int>("IdMovie")
                        .HasColumnType("int");

                    b.Property<DateTime>("ShowtimeDateEnd")
                        .HasPrecision(0)
                        .HasColumnType("datetime(0)");

                    b.Property<DateTime>("ShowtimeDateStart")
                        .HasPrecision(0)
                        .HasColumnType("datetime(0)");

                    b.HasKey("IdShowtime");

                    b.HasIndex("IdHall");

                    b.HasIndex("IdMovie");

                    b.ToTable("Showtimes");
                });

            modelBuilder.Entity("stellarCinema.Entities.UserAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("UserAccounts");
                });

            modelBuilder.Entity("stellarCinema.Entities.Payment", b =>
                {
                    b.HasOne("stellarCinema.Entities.Reservation", "Reservation")
                        .WithOne("Payment")
                        .HasForeignKey("stellarCinema.Entities.Payment", "IdReservation")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("stellarCinema.Entities.Reservation", b =>
                {
                    b.HasOne("stellarCinema.Entities.Showtime", "Showtime")
                        .WithMany("Reservations")
                        .HasForeignKey("IdShowtime")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Showtime");
                });

            modelBuilder.Entity("stellarCinema.Entities.ReservationSeat", b =>
                {
                    b.HasOne("stellarCinema.Entities.Reservation", "Reservation")
                        .WithMany("ReservationSeats")
                        .HasForeignKey("IdReservation")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("stellarCinema.Entities.Seat", "Seat")
                        .WithMany("ReservationSeats")
                        .HasForeignKey("IdSeat")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("stellarCinema.Entities.Showtime", "Showtime")
                        .WithMany("ReservationSeats")
                        .HasForeignKey("IdShowtime")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reservation");

                    b.Navigation("Seat");

                    b.Navigation("Showtime");
                });

            modelBuilder.Entity("stellarCinema.Entities.Seat", b =>
                {
                    b.HasOne("stellarCinema.Entities.Hall", "Hall")
                        .WithMany("Seats")
                        .HasForeignKey("IdHall")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hall");
                });

            modelBuilder.Entity("stellarCinema.Entities.Showtime", b =>
                {
                    b.HasOne("stellarCinema.Entities.Hall", "Hall")
                        .WithMany("Showtimes")
                        .HasForeignKey("IdHall")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("stellarCinema.Entities.Movie", "Movie")
                        .WithMany("Showtimes")
                        .HasForeignKey("IdMovie")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hall");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("stellarCinema.Entities.Hall", b =>
                {
                    b.Navigation("Seats");

                    b.Navigation("Showtimes");
                });

            modelBuilder.Entity("stellarCinema.Entities.Movie", b =>
                {
                    b.Navigation("Showtimes");
                });

            modelBuilder.Entity("stellarCinema.Entities.Reservation", b =>
                {
                    b.Navigation("Payment");

                    b.Navigation("ReservationSeats");
                });

            modelBuilder.Entity("stellarCinema.Entities.Seat", b =>
                {
                    b.Navigation("ReservationSeats");
                });

            modelBuilder.Entity("stellarCinema.Entities.Showtime", b =>
                {
                    b.Navigation("ReservationSeats");

                    b.Navigation("Reservations");
                });
#pragma warning restore 612, 618
        }
    }
}
