using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Bloggy.Models;

namespace Bloggy.Migrations
{
    [DbContext(typeof(BloggingContext))]
    [Migration("20161227000614_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("Bloggy.Models.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<int>("PostId");

                    b.Property<DateTime>("PublishedAt");

                    b.Property<string>("Website");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Bloggy.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<string>("Excerpt");

                    b.Property<bool>("IsPublished");

                    b.Property<DateTime>("LastModified");

                    b.Property<DateTime?>("PublishedAt");

                    b.Property<string>("Slug");

                    b.Property<string>("Tags");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Bloggy.Models.Comment", b =>
                {
                    b.HasOne("Bloggy.Models.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
