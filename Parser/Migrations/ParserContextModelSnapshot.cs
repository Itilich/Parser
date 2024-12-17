﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Parser.Data;

#nullable disable

namespace Parser.Migrations
{
    [DbContext(typeof(ParserContext))]
    partial class ParserContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.11");

            modelBuilder.Entity("Parser.Data.AddedData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("LinkCersanit")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LinkVodoparad")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("addedDatas");
                });

            modelBuilder.Entity("Parser.Data.PriceLog", b =>
                {
                    b.Property<string>("DateTime")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Price")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.ToTable("priceLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
