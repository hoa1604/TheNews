using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TheNewsWebsite.Models.TheNewsWebsite;

namespace TheNewsWebsite.Migrations
{
    [DbContext(typeof(TheNewsContext))]
    [Migration("20170804023110_MyMigration")]
    partial class MyMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TheNewsWebsite.Models.TheNewsWebsite.Admin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Authority");

                    b.Property<string>("Name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Status");

                    b.Property<string>("Username")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Admins");
                });

            modelBuilder.Entity("TheNewsWebsite.Models.TheNewsWebsite.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("TheNewsWebsite.Models.TheNewsWebsite.Categary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.ToTable("Categaries");
                });

            modelBuilder.Entity("TheNewsWebsite.Models.TheNewsWebsite.CategaryChild", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategariesId");

                    b.Property<string>("Name");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.HasIndex("CategariesId");

                    b.ToTable("CategaryChilds");
                });

            modelBuilder.Entity("TheNewsWebsite.Models.TheNewsWebsite.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cmt");

                    b.Property<DateTime>("CmtDate");

                    b.Property<int>("PostId");

                    b.Property<string>("Status");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("TheNewsWebsite.Models.TheNewsWebsite.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AdminId");

                    b.Property<int>("AuthorId");

                    b.Property<int>("CategaryChildId");

                    b.Property<int?>("CategaryId");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("DateCreate");

                    b.Property<DateTime>("DatePublish");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Image");

                    b.Property<string>("Keyword");

                    b.Property<int>("NumView");

                    b.Property<int>("PublishBy");

                    b.Property<int>("Status");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.HasIndex("AuthorId");

                    b.HasIndex("CategaryChildId");

                    b.HasIndex("CategaryId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("TheNewsWebsite.Models.TheNewsWebsite.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Avatar");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Gender");

                    b.Property<string>("Name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Status");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TheNewsWebsite.Models.TheNewsWebsite.CategaryChild", b =>
                {
                    b.HasOne("TheNewsWebsite.Models.TheNewsWebsite.Categary", "Categaries")
                        .WithMany("CategaryChilds")
                        .HasForeignKey("CategariesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TheNewsWebsite.Models.TheNewsWebsite.Comment", b =>
                {
                    b.HasOne("TheNewsWebsite.Models.TheNewsWebsite.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TheNewsWebsite.Models.TheNewsWebsite.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TheNewsWebsite.Models.TheNewsWebsite.Post", b =>
                {
                    b.HasOne("TheNewsWebsite.Models.TheNewsWebsite.Admin", "Admin")
                        .WithMany("Post")
                        .HasForeignKey("AdminId");

                    b.HasOne("TheNewsWebsite.Models.TheNewsWebsite.Author", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TheNewsWebsite.Models.TheNewsWebsite.CategaryChild", "CategaryChild")
                        .WithMany("Posts")
                        .HasForeignKey("CategaryChildId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TheNewsWebsite.Models.TheNewsWebsite.Categary")
                        .WithMany("Posts")
                        .HasForeignKey("CategaryId");
                });
        }
    }
}
