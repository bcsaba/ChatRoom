﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repository;

#nullable disable

namespace Repository.Migrations
{
    [DbContext(typeof(ChatRoomContext))]
    partial class BloggingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Repository.ChatRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatedById")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("ChatRooms");
                });

            modelBuilder.Entity("Repository.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChatRoomId")
                        .HasColumnType("integer");

                    b.Property<string>("CommentString")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PostEvent")
                        .HasColumnType("integer");

                    b.Property<int>("PostingUser")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ChatRoomId");

                    b.HasIndex("PostEvent");

                    b.HasIndex("PostingUser");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Repository.HourlyChatRoomEvent", b =>
                {
                    b.Property<int>("CountType")
                        .HasColumnType("integer");

                    b.Property<int>("EventTypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("HourPart")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TargetUser")
                        .HasColumnType("integer");

                    b.Property<int>("UserInAction")
                        .HasColumnType("integer");

                    b.ToTable("HourlyChatRoomEvent", null, t => t.ExcludeFromMigrations());

                    b.ToFunction("select * from gethourlychatroomdatafunc()");
                });

            modelBuilder.Entity("Repository.RoomEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ChatRoomId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EventTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EventTypeId")
                        .HasColumnType("integer");

                    b.Property<int?>("TargetUserId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ChatRoomId");

                    b.HasIndex("EventTypeId");

                    b.HasIndex("TargetUserId");

                    b.HasIndex("UserId");

                    b.ToTable("RoomEvents");
                });

            modelBuilder.Entity("Repository.RoomEventType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("RoomEventTypes");
                });

            modelBuilder.Entity("Repository.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NickNAme")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Repository.ChatRoom", b =>
                {
                    b.HasOne("Repository.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedBy");
                });

            modelBuilder.Entity("Repository.Comment", b =>
                {
                    b.HasOne("Repository.ChatRoom", "ChatRoom")
                        .WithMany("Comments")
                        .HasForeignKey("ChatRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repository.RoomEvent", "RoomEvent")
                        .WithMany()
                        .HasForeignKey("PostEvent")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repository.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("PostingUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatRoom");

                    b.Navigation("RoomEvent");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Repository.RoomEvent", b =>
                {
                    b.HasOne("Repository.ChatRoom", "ChatRoom")
                        .WithMany("RoomEvents")
                        .HasForeignKey("ChatRoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repository.RoomEventType", "EventType")
                        .WithMany()
                        .HasForeignKey("EventTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Repository.User", "TargetUser")
                        .WithMany("RoomEventsTargetUser")
                        .HasForeignKey("TargetUserId");

                    b.HasOne("Repository.User", "User")
                        .WithMany("RoomEvents")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ChatRoom");

                    b.Navigation("EventType");

                    b.Navigation("TargetUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Repository.ChatRoom", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("RoomEvents");
                });

            modelBuilder.Entity("Repository.User", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("RoomEvents");

                    b.Navigation("RoomEventsTargetUser");
                });
#pragma warning restore 612, 618
        }
    }
}
