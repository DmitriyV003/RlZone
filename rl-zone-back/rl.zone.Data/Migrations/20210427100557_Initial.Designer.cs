﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using rl.zone.Data;

namespace rl.zone.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210427100557_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("rl.zone.Domain.Intermediary.CaseItem", b =>
                {
                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("CaseId")
                        .HasColumnType("int");

                    b.Property<int>("ItemWeight")
                        .HasColumnType("int");

                    b.HasKey("ItemId", "CaseId");

                    b.HasIndex("CaseId");

                    b.ToTable("CaseItems");
                });

            modelBuilder.Entity("rl.zone.Domain.Intermediary.CraftAttemptItem", b =>
                {
                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("CraftAttemptId")
                        .HasColumnType("int");

                    b.HasKey("ItemId", "CraftAttemptId");

                    b.HasIndex("CraftAttemptId");

                    b.ToTable("CraftAttempItems");
                });

            modelBuilder.Entity("rl.zone.Domain.Intermediary.UserLink", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("LinkId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "LinkId");

                    b.HasIndex("LinkId");

                    b.ToTable("UserLinks");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.Case", b =>
                {
                    b.Property<int>("CaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("CurrentOpens")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MaxOpens")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("CaseId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Cases");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.CaseCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("CategoryId");

                    b.ToTable("CaseCategories");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.CasePrice", b =>
                {
                    b.Property<int>("PriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<int>("CaseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("PlatformId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("PriceId");

                    b.HasIndex("CaseId");

                    b.HasIndex("PlatformId");

                    b.ToTable("CasePrices");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.CircleColor", b =>
                {
                    b.Property<int>("ColorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ColorId");

                    b.ToTable("CircleColors");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.CraftAttempt", b =>
                {
                    b.Property<int>("AttemptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<float>("Percent")
                        .HasColumnType("real");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<string>("Result")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("AttemptId");

                    b.HasIndex("UserId");

                    b.ToTable("CraftAttempts");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAvailableInCase")
                        .HasColumnType("bit");

                    b.Property<bool>("IsAvailableInCraft")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("ItemId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.ItemCategory", b =>
                {
                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("CategoryId");

                    b.ToTable("ItemCategories");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.ItemPrice", b =>
                {
                    b.Property<int>("PriceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("PlatformId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("PriceId");

                    b.HasIndex("ItemId");

                    b.HasIndex("PlatformId");

                    b.ToTable("ItemPrices");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.ItemSale", b =>
                {
                    b.Property<int>("SaleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("SaleId");

                    b.HasIndex("ItemId");

                    b.HasIndex("UserId");

                    b.ToTable("ItemSales");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.LineColor", b =>
                {
                    b.Property<int>("ColorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ColorId");

                    b.ToTable("LineColors");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.Link", b =>
                {
                    b.Property<int>("LinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlatformId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("LinkId");

                    b.HasIndex("PlatformId");

                    b.ToTable("Links");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.OpenCase", b =>
                {
                    b.Property<int>("OpenCaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<int>("CaseId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OpenCaseId");

                    b.HasIndex("CaseId");

                    b.HasIndex("ItemId");

                    b.HasIndex("UserId");

                    b.ToTable("OpenCases");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.Platform", b =>
                {
                    b.Property<int>("PlatformId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("PlatformId");

                    b.ToTable("Platforms");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.RequestForOutput", b =>
                {
                    b.Property<int>("RequestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("RequestId");

                    b.ToTable("RequestsForOutput");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Amount")
                        .HasColumnType("real");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TransactionId");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Balance")
                        .HasColumnType("real");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecretPhrase")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.ItemRunner", b =>
                {
                    b.HasBaseType("rl.zone.Domain.Models.User");

                    b.HasDiscriminator().HasValue("ItemRunner");
                });

            modelBuilder.Entity("rl.zone.Domain.Intermediary.CaseItem", b =>
                {
                    b.HasOne("rl.zone.Domain.Models.Case", "Case")
                        .WithMany("CaseItems")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("rl.zone.Domain.Models.Item", "Item")
                        .WithMany("CaseItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Case");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("rl.zone.Domain.Intermediary.CraftAttemptItem", b =>
                {
                    b.HasOne("rl.zone.Domain.Models.CraftAttempt", "CraftAttempt")
                        .WithMany("CraftAttempItems")
                        .HasForeignKey("CraftAttemptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("rl.zone.Domain.Models.Item", "Item")
                        .WithMany("CraftAttempItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CraftAttempt");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("rl.zone.Domain.Intermediary.UserLink", b =>
                {
                    b.HasOne("rl.zone.Domain.Models.Link", "Link")
                        .WithMany("UserLinks")
                        .HasForeignKey("LinkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("rl.zone.Domain.Models.User", "User")
                        .WithMany("UserLinks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Link");

                    b.Navigation("User");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.Case", b =>
                {
                    b.HasOne("rl.zone.Domain.Models.CaseCategory", "Category")
                        .WithMany("Cases")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.CasePrice", b =>
                {
                    b.HasOne("rl.zone.Domain.Models.Case", "Case")
                        .WithMany("CasePrices")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("rl.zone.Domain.Models.Platform", "Platform")
                        .WithMany("CasePrices")
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Case");

                    b.Navigation("Platform");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.CraftAttempt", b =>
                {
                    b.HasOne("rl.zone.Domain.Models.User", "User")
                        .WithMany("CraftAttempts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.Item", b =>
                {
                    b.HasOne("rl.zone.Domain.Models.ItemCategory", "Category")
                        .WithMany("Items")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.ItemPrice", b =>
                {
                    b.HasOne("rl.zone.Domain.Models.Item", "Item")
                        .WithMany("ItemPrices")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("rl.zone.Domain.Models.Platform", "Platform")
                        .WithMany("ItemPrices")
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Platform");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.ItemSale", b =>
                {
                    b.HasOne("rl.zone.Domain.Models.Item", "Item")
                        .WithMany("ItemSales")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("rl.zone.Domain.Models.User", "User")
                        .WithMany("ItemSales")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("User");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.Link", b =>
                {
                    b.HasOne("rl.zone.Domain.Models.Platform", "Platform")
                        .WithMany("Links")
                        .HasForeignKey("PlatformId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Platform");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.OpenCase", b =>
                {
                    b.HasOne("rl.zone.Domain.Models.Case", "Case")
                        .WithMany("OpenCases")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("rl.zone.Domain.Models.Item", "Item")
                        .WithMany("OpenCases")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("rl.zone.Domain.Models.User", "User")
                        .WithMany("OpenCases")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Case");

                    b.Navigation("Item");

                    b.Navigation("User");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.Transaction", b =>
                {
                    b.HasOne("rl.zone.Domain.Models.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.Case", b =>
                {
                    b.Navigation("CaseItems");

                    b.Navigation("CasePrices");

                    b.Navigation("OpenCases");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.CaseCategory", b =>
                {
                    b.Navigation("Cases");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.CraftAttempt", b =>
                {
                    b.Navigation("CraftAttempItems");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.Item", b =>
                {
                    b.Navigation("CaseItems");

                    b.Navigation("CraftAttempItems");

                    b.Navigation("ItemPrices");

                    b.Navigation("ItemSales");

                    b.Navigation("OpenCases");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.ItemCategory", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.Link", b =>
                {
                    b.Navigation("UserLinks");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.Platform", b =>
                {
                    b.Navigation("CasePrices");

                    b.Navigation("ItemPrices");

                    b.Navigation("Links");
                });

            modelBuilder.Entity("rl.zone.Domain.Models.User", b =>
                {
                    b.Navigation("CraftAttempts");

                    b.Navigation("ItemSales");

                    b.Navigation("OpenCases");

                    b.Navigation("Transactions");

                    b.Navigation("UserLinks");
                });
#pragma warning restore 612, 618
        }
    }
}
