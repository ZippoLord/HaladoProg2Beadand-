﻿// <auto-generated />
using System;
using HaladoProg2Beadandó.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HaladoProg2Beadandó.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HaladoProg2Beadandó.Entities.CryptoPriceHistory", b =>
                {
                    b.Property<int>("CryptoPriceHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CryptoPriceHistoryId"));

                    b.Property<int>("CryptoCurrencyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LoggedAt")
                        .HasColumnType("datetime2");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("CryptoPriceHistoryId");

                    b.HasIndex("CryptoCurrencyId");

                    b.ToTable("CryptoPriceHistories", (string)null);
                });

            modelBuilder.Entity("HaladoProg2Beadandó.Models.CryptoAsset", b =>
                {
                    b.Property<int>("CryptoAssetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CryptoAssetId"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("CryptoCurrencyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VirtualWalletId")
                        .HasColumnType("int");

                    b.HasKey("CryptoAssetId");

                    b.HasIndex("VirtualWalletId");

                    b.ToTable("CryptoAssets", (string)null);
                });

            modelBuilder.Entity("HaladoProg2Beadandó.Models.CryptoCurrency", b =>
                {
                    b.Property<int>("CryptoCurrencyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CryptoCurrencyId"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("CryptoCurrencyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CryptoCurrencyId");

                    b.ToTable("CryptoCurrencies", (string)null);
                });

            modelBuilder.Entity("HaladoProg2Beadandó.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("CryptoCurrencyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TransactionId");

                    b.ToTable("Transactions", (string)null);
                });

            modelBuilder.Entity("HaladoProg2Beadandó.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("HaladoProg2Beadandó.Models.VirtualWallet", b =>
                {
                    b.Property<int>("VirtualWalletId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VirtualWalletId"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("VirtualWalletId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("VirtualWallets", (string)null);
                });

            modelBuilder.Entity("HaladoProg2Beadandó.Entities.CryptoPriceHistory", b =>
                {
                    b.HasOne("HaladoProg2Beadandó.Models.CryptoCurrency", "CryptoCurrency")
                        .WithMany("PriceHistory")
                        .HasForeignKey("CryptoCurrencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CryptoCurrency");
                });

            modelBuilder.Entity("HaladoProg2Beadandó.Models.CryptoAsset", b =>
                {
                    b.HasOne("HaladoProg2Beadandó.Models.VirtualWallet", "VirtualWallet")
                        .WithMany("CryptoAssets")
                        .HasForeignKey("VirtualWalletId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VirtualWallet");
                });

            modelBuilder.Entity("HaladoProg2Beadandó.Models.VirtualWallet", b =>
                {
                    b.HasOne("HaladoProg2Beadandó.Models.User", "User")
                        .WithOne("VirtualWallet")
                        .HasForeignKey("HaladoProg2Beadandó.Models.VirtualWallet", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("HaladoProg2Beadandó.Models.CryptoCurrency", b =>
                {
                    b.Navigation("PriceHistory");
                });

            modelBuilder.Entity("HaladoProg2Beadandó.Models.User", b =>
                {
                    b.Navigation("VirtualWallet")
                        .IsRequired();
                });

            modelBuilder.Entity("HaladoProg2Beadandó.Models.VirtualWallet", b =>
                {
                    b.Navigation("CryptoAssets");
                });
#pragma warning restore 612, 618
        }
    }
}
