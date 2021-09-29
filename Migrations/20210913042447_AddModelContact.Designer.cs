﻿// <auto-generated />
using System;
using FloraYFaunaAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FloraYFaunaAPI.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210913042447_AddModelContact")]
    partial class AddModelContact
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FloraYFaunaAPI.Models.BlogPost", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2500)
                        .HasColumnType("nvarchar(2500)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Likes")
                        .HasColumnType("int");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("BlogPosts", "dbo");
                });

            modelBuilder.Entity("FloraYFaunaAPI.Models.Carousel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Caption")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Carousel", "dbo");
                });

            modelBuilder.Entity("FloraYFaunaAPI.Models.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Categories", "dbo");
                });

            modelBuilder.Entity("FloraYFaunaAPI.Models.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("MarkAsRead")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contacts", "dbo");
                });

            modelBuilder.Entity("FloraYFaunaAPI.Models.Document", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasMaxLength(600)
                        .HasColumnType("nvarchar(600)");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Documents", "dbo");
                });

            modelBuilder.Entity("FloraYFaunaAPI.Models.Gallery", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Description")
                        .HasMaxLength(600)
                        .HasColumnType("nvarchar(600)");

                    b.Property<string>("Extension")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Gallery", "dbo");
                });

            modelBuilder.Entity("FloraYFaunaAPI.Models.IpPost", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BlogPostId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("IpAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BlogPostId");

                    b.ToTable("IpPosts", "dbo");
                });

            modelBuilder.Entity("FloraYFaunaAPI.Models.Newsletter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FullName")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Newsletters", "dbo");
                });

            modelBuilder.Entity("FloraYFaunaAPI.Models.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedByIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReasonRevoked")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReplacedByToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("datetime2");

                    b.Property<string>("RevokedByIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshToken", "dbo");
                });

            modelBuilder.Entity("FloraYFaunaAPI.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Rol")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users", "dbo");
                });

            modelBuilder.Entity("FloraYFaunaAPI.Models.BlogPost", b =>
                {
                    b.HasOne("FloraYFaunaAPI.Models.Category", "Category")
                        .WithMany("BlogPost")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("FloraYFaunaAPI.Models.Metadata", "Metadata", b1 =>
                        {
                            b1.Property<Guid>("BlogPostId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("CreatedBy")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<bool>("IsDeleted")
                                .HasColumnType("bit");

                            b1.Property<DateTime>("UpdatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("UpdatedBy")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("BlogPostId");

                            b1.ToTable("BlogPosts");

                            b1.WithOwner()
                                .HasForeignKey("BlogPostId");
                        });

                    b.Navigation("Category");

                    b.Navigation("Metadata");
                });

            modelBuilder.Entity("FloraYFaunaAPI.Models.Carousel", b =>
                {
                    b.OwnsOne("FloraYFaunaAPI.Models.Metadata", "Metadata", b1 =>
                        {
                            b1.Property<Guid>("CarouselId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("CreatedBy")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<bool>("IsDeleted")
                                .HasColumnType("bit");

                            b1.Property<DateTime>("UpdatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("UpdatedBy")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("CarouselId");

                            b1.ToTable("Carousel");

                            b1.WithOwner()
                                .HasForeignKey("CarouselId");
                        });

                    b.Navigation("Metadata");
                });

            modelBuilder.Entity("FloraYFaunaAPI.Models.Category", b =>
                {
                    b.OwnsOne("FloraYFaunaAPI.Models.Metadata", "Metadata", b1 =>
                        {
                            b1.Property<Guid>("CategoryId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("CreatedBy")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<bool>("IsDeleted")
                                .HasColumnType("bit");

                            b1.Property<DateTime>("UpdatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("UpdatedBy")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("CategoryId");

                            b1.ToTable("Categories");

                            b1.WithOwner()
                                .HasForeignKey("CategoryId");
                        });

                    b.Navigation("Metadata");
                });

            modelBuilder.Entity("FloraYFaunaAPI.Models.Contact", b =>
                {
                    b.OwnsOne("FloraYFaunaAPI.Models.Metadata", "Metadata", b1 =>
                        {
                            b1.Property<Guid>("ContactId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("CreatedBy")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<bool>("IsDeleted")
                                .HasColumnType("bit");

                            b1.Property<DateTime>("UpdatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("UpdatedBy")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("ContactId");

                            b1.ToTable("Contacts");

                            b1.WithOwner()
                                .HasForeignKey("ContactId");
                        });

                    b.Navigation("Metadata");
                });

            modelBuilder.Entity("FloraYFaunaAPI.Models.Document", b =>
                {
                    b.OwnsOne("FloraYFaunaAPI.Models.Metadata", "Metadata", b1 =>
                        {
                            b1.Property<Guid>("DocumentId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("CreatedBy")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<bool>("IsDeleted")
                                .HasColumnType("bit");

                            b1.Property<DateTime>("UpdatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("UpdatedBy")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("DocumentId");

                            b1.ToTable("Documents");

                            b1.WithOwner()
                                .HasForeignKey("DocumentId");
                        });

                    b.Navigation("Metadata");
                });

            modelBuilder.Entity("FloraYFaunaAPI.Models.Gallery", b =>
                {
                    b.OwnsOne("FloraYFaunaAPI.Models.Metadata", "Metadata", b1 =>
                        {
                            b1.Property<Guid>("GalleryId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("CreatedBy")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<bool>("IsDeleted")
                                .HasColumnType("bit");

                            b1.Property<DateTime>("UpdatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("UpdatedBy")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("GalleryId");

                            b1.ToTable("Gallery");

                            b1.WithOwner()
                                .HasForeignKey("GalleryId");
                        });

                    b.Navigation("Metadata");
                });

            modelBuilder.Entity("FloraYFaunaAPI.Models.IpPost", b =>
                {
                    b.HasOne("FloraYFaunaAPI.Models.BlogPost", "BlogPost")
                        .WithMany("IpPost")
                        .HasForeignKey("BlogPostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BlogPost");
                });

            modelBuilder.Entity("FloraYFaunaAPI.Models.Newsletter", b =>
                {
                    b.OwnsOne("FloraYFaunaAPI.Models.Metadata", "Metadata", b1 =>
                        {
                            b1.Property<Guid>("NewsletterId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("CreatedBy")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<bool>("IsDeleted")
                                .HasColumnType("bit");

                            b1.Property<DateTime>("UpdatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("UpdatedBy")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("NewsletterId");

                            b1.ToTable("Newsletters");

                            b1.WithOwner()
                                .HasForeignKey("NewsletterId");
                        });

                    b.Navigation("Metadata");
                });

            modelBuilder.Entity("FloraYFaunaAPI.Models.RefreshToken", b =>
                {
                    b.HasOne("FloraYFaunaAPI.Models.User", "User")
                        .WithMany("RefreshToken")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FloraYFaunaAPI.Models.User", b =>
                {
                    b.OwnsOne("FloraYFaunaAPI.Models.Metadata", "Metadata", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("CreatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("CreatedBy")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<bool>("IsDeleted")
                                .HasColumnType("bit");

                            b1.Property<DateTime>("UpdatedAt")
                                .HasColumnType("datetime2");

                            b1.Property<Guid>("UpdatedBy")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Metadata");
                });

            modelBuilder.Entity("FloraYFaunaAPI.Models.BlogPost", b =>
                {
                    b.Navigation("IpPost");
                });

            modelBuilder.Entity("FloraYFaunaAPI.Models.Category", b =>
                {
                    b.Navigation("BlogPost");
                });

            modelBuilder.Entity("FloraYFaunaAPI.Models.User", b =>
                {
                    b.Navigation("RefreshToken");
                });
#pragma warning restore 612, 618
        }
    }
}
