﻿// <auto-generated />
using System;
using EasyMealOrder.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EasyMealOrder.Migrations
{
    [DbContext(typeof(EasyMealDbContext))]
    [Migration("20201107154146_WeekOrderRelation2")]
    partial class WeekOrderRelation2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EasyMealOrder.Models.Order", b =>
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

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.Property<int>("OrderSize")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("WeekOrderId")
                        .HasColumnType("int");

                    b.Property<int?>("WeekOrderId1")
                        .HasColumnType("int");

                    b.Property<int?>("WeekOrderId2")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerUserId");

                    b.HasIndex("WeekOrderId");

                    b.HasIndex("WeekOrderId1");

                    b.HasIndex("WeekOrderId2");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("EasyMealOrder.Models.User", b =>
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

            modelBuilder.Entity("EasyMealOrder.Models.WeekOrder", b =>
                {
                    b.Property<int>("WeekOrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("EasyMealOrder.Models.Order", b =>
                {
                    b.HasOne("EasyMealOrder.Models.User", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerUserId");

                    b.HasOne("EasyMealOrder.Models.WeekOrder", null)
                        .WithMany("Orders")
                        .HasForeignKey("WeekOrderId");

                    b.HasOne("EasyMealOrder.Models.WeekOrder", null)
                        .WithMany()
                        .HasForeignKey("WeekOrderId1");

                    b.HasOne("EasyMealOrder.Models.WeekOrder", null)
                        .WithMany()
                        .HasForeignKey("WeekOrderId2");
                });

            modelBuilder.Entity("EasyMealOrder.Models.WeekOrder", b =>
                {
                    b.HasOne("EasyMealOrder.Models.User", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerUserId");
                });
#pragma warning restore 612, 618
        }
    }
}