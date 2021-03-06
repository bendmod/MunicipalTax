﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MunicipalTax.Data;

namespace MunicipalTax.Data.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    [Migration("20210213233551_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.3");

            modelBuilder.Entity("MunicipalTax.Data.Entities.Municipality", b =>
                {
                    b.Property<int>("MunicipalityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("MunicipalityName")
                        .HasColumnType("TEXT");

                    b.HasKey("MunicipalityId");

                    b.ToTable("Municipalities");
                });

            modelBuilder.Entity("MunicipalTax.Data.Entities.Tax", b =>
                {
                    b.Property<int>("RecordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("MunicipalityId")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Rate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("TaxType")
                        .HasColumnType("TEXT");

                    b.HasKey("RecordId");

                    b.HasIndex("MunicipalityId");

                    b.ToTable("Taxes");
                });

            modelBuilder.Entity("MunicipalTax.Data.Entities.Tax", b =>
                {
                    b.HasOne("MunicipalTax.Data.Entities.Municipality", null)
                        .WithMany("TaxList")
                        .HasForeignKey("MunicipalityId");
                });

            modelBuilder.Entity("MunicipalTax.Data.Entities.Municipality", b =>
                {
                    b.Navigation("TaxList");
                });
#pragma warning restore 612, 618
        }
    }
}
