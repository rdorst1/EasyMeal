﻿// <auto-generated />
using System;
using MealManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MealManagement.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200330173413_Cook")]
    partial class Cook
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MealManagement.Models.Chef", b =>
                {
                    b.Property<int>("ChefId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TelephoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ChefId");

                    b.ToTable("Chef");
                });

            modelBuilder.Entity("MealManagement.Models.Meal", b =>
                {
                    b.Property<int>("MealId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Appetizer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dessert")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Diabetes")
                        .HasColumnType("bit");

                    b.Property<bool>("GlutenAllergy")
                        .HasColumnType("bit");

                    b.Property<byte[]>("ImageData")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("MainDish")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<bool>("Saltless")
                        .HasColumnType("bit");

                    b.HasKey("MealId");

                    b.ToTable("Meals");
                });
#pragma warning restore 612, 618
        }
    }
}
