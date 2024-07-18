﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240718105848_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Map", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CountColumn")
                        .HasColumnType("integer")
                        .HasColumnName("count_column");

                    b.Property<int>("CountRow")
                        .HasColumnType("integer")
                        .HasColumnName("count_row");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int[]>("TitleType")
                        .IsRequired()
                        .HasColumnType("integer[]")
                        .HasColumnName("title_type");

                    b.HasKey("Id")
                        .HasName("pk_maps");

                    b.ToTable("maps", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CurrentMapId")
                        .HasColumnType("integer")
                        .HasColumnName("current_map_id");

                    b.Property<int>("CurrentRound")
                        .HasColumnType("integer")
                        .HasColumnName("current_round");

                    b.Property<string>("HashPass")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("hash_pass");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<int>("Status")
                        .HasColumnType("integer")
                        .HasColumnName("status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id")
                        .HasName("pk_rooms");

                    b.HasIndex("CurrentMapId")
                        .HasDatabaseName("ix_rooms_current_map_id");

                    b.ToTable("rooms", (string)null);
                });

            modelBuilder.Entity("Domain.Models.UnitType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte>("Armor")
                        .HasColumnType("smallint")
                        .HasColumnName("armor");

                    b.Property<byte>("Damage")
                        .HasColumnType("smallint")
                        .HasColumnName("damage");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<byte>("HP")
                        .HasColumnType("smallint")
                        .HasColumnName("hp");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<byte>("MovmentSpead")
                        .HasColumnType("smallint")
                        .HasColumnName("movment_spead");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("Ultimate")
                        .HasColumnType("integer")
                        .HasColumnName("ultimate");

                    b.HasKey("Id")
                        .HasName("pk_unit_types");

                    b.ToTable("unit_types", (string)null);
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Domain.Models.UserState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<bool>("ReadyForBattle")
                        .HasColumnType("boolean")
                        .HasColumnName("ready_for_battle");

                    b.Property<int>("RoomId")
                        .HasColumnType("integer")
                        .HasColumnName("room_id");

                    b.Property<int>("UnitTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("unit_type_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_user_states");

                    b.HasIndex("RoomId")
                        .HasDatabaseName("ix_user_states_room_id");

                    b.HasIndex("UnitTypeId")
                        .HasDatabaseName("ix_user_states_unit_type_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_user_states_user_id");

                    b.ToTable("user_states", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Room", b =>
                {
                    b.HasOne("Domain.Models.Map", "CurrentMap")
                        .WithMany()
                        .HasForeignKey("CurrentMapId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_rooms_maps_current_map_id");

                    b.Navigation("CurrentMap");
                });

            modelBuilder.Entity("Domain.Models.UserState", b =>
                {
                    b.HasOne("Domain.Models.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_states_rooms_room_id");

                    b.HasOne("Domain.Models.UnitType", "UnitType")
                        .WithMany()
                        .HasForeignKey("UnitTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_states_unit_types_unit_type_id");

                    b.HasOne("Domain.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_states_users_user_id");

                    b.Navigation("Room");

                    b.Navigation("UnitType");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
