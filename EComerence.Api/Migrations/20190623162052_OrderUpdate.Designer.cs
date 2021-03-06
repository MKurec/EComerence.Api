﻿// <auto-generated />
using System;
using EComerence.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EComerence.Api.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190623162052_OrderUpdate")]
    partial class OrderUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("EComerence.Core.Domain.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("EComerence.Core.Domain.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<Guid>("OrderListId");

                    b.Property<decimal>("Price");

                    b.Property<Guid>("ProductId");

                    b.Property<string>("ProductName");

                    b.HasKey("Id");

                    b.HasIndex("OrderListId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("EComerence.Core.Domain.OrderList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("PucharsedAt");

                    b.Property<bool>("Purchased");

                    b.Property<decimal>("TotalPrice");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("OrderLists");
                });

            modelBuilder.Entity("EComerence.Core.Domain.Producer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Producers");
                });

            modelBuilder.Entity("EComerence.Core.Domain.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<Guid>("CategoryId");

                    b.Property<string>("CategoryName");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<Guid>("ProducerId");

                    b.Property<string>("ProducerName");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("EComerence.Core.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("Email");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("PostalCode");

                    b.Property<string>("Role");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("EComerence.Core.Domain.Order", b =>
                {
                    b.HasOne("EComerence.Core.Domain.OrderList")
                        .WithMany("Orders")
                        .HasForeignKey("OrderListId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
