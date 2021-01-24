﻿// <auto-generated />
using System;
using EasyMeal.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EasyMeal.Infrastructure.Migrations.EasyMealDb
{
    [DbContext(typeof(EasyMealDbContext))]
    [Migration("20210123142247_canCompleteOrder")]
    partial class canCompleteOrder
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EasyMeal.Domain.Models.Invoice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Birthday")
                        .HasColumnType("bit");

                    b.Property<int?>("CustomerUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("FifteenOrdersOrMore")
                        .HasColumnType("bit");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerUserId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("EasyMeal.Domain.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Appetizer")
                        .HasColumnType("bit");

                    b.Property<int?>("CustomerUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("Day")
                        .HasColumnType("int");

                    b.Property<bool>("Dessert")
                        .HasColumnType("bit");

                    b.Property<int?>("InvoiceId")
                        .HasColumnType("int");

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.Property<int>("OrderSize")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("WeekOrderId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerUserId");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("WeekOrderId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("EasyMeal.Domain.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Diabetes")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("GlutenAllergy")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Saltless")
                        .HasColumnType("bit");

                    b.Property<string>("TelephoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EasyMeal.Domain.Models.WeekOrder", b =>
                {
                    b.Property<int>("WeekOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CanCompleteOrder")
                        .HasColumnType("bit");

                    b.Property<bool>("Completed")
                        .HasColumnType("bit");

                    b.Property<int?>("CustomerUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("WeekOrderId");

                    b.HasIndex("CustomerUserId");

                    b.ToTable("WeekOrders");
                });

            modelBuilder.Entity("EasyMeal.Domain.Models.Invoice", b =>
                {
                    b.HasOne("EasyMeal.Domain.Models.User", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerUserId");
                });

            modelBuilder.Entity("EasyMeal.Domain.Models.Order", b =>
                {
                    b.HasOne("EasyMeal.Domain.Models.User", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerUserId");

                    b.HasOne("EasyMeal.Domain.Models.Invoice", null)
                        .WithMany("Orders")
                        .HasForeignKey("InvoiceId");

                    b.HasOne("EasyMeal.Domain.Models.WeekOrder", "WeekOrder")
                        .WithMany("Orders")
                        .HasForeignKey("WeekOrderId");
                });

            modelBuilder.Entity("EasyMeal.Domain.Models.WeekOrder", b =>
                {
                    b.HasOne("EasyMeal.Domain.Models.User", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerUserId");
                });
#pragma warning restore 612, 618
        }
    }
}
