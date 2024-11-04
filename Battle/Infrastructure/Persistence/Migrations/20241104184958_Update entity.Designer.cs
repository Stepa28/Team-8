﻿// <auto-generated />
using System;
using System.Collections.Generic;
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
    [Migration("20241104184958_Update entity")]
    partial class Updateentity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.Battle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<int[]>("MapTilesType")
                        .IsRequired()
                        .HasColumnType("integer[]")
                        .HasColumnName("map_tiles_type");

                    b.Property<int>("Round")
                        .HasColumnType("integer")
                        .HasColumnName("round");

                    b.Property<int>("State")
                        .HasColumnType("integer")
                        .HasColumnName("state");

                    b.Property<List<Guid>>("Users")
                        .IsRequired()
                        .HasColumnType("uuid[]")
                        .HasColumnName("users");

                    b.Property<Guid>("WalkingPlayer")
                        .HasColumnType("uuid")
                        .HasColumnName("walking_player");

                    b.HasKey("Id")
                        .HasName("pk_battles");

                    b.HasIndex("MapTilesType")
                        .HasDatabaseName("ix_battles_map_tiles_type");

                    b.ToTable("battles", (string)null);
                });

            modelBuilder.Entity("Domain.Models.CurrentUnitState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<byte>("AttackPower")
                        .HasColumnType("smallint")
                        .HasColumnName("attack_power");

                    b.Property<int>("BattleId")
                        .HasColumnType("integer")
                        .HasColumnName("battle_id");

                    b.Property<int>("DefensePower")
                        .HasColumnType("integer")
                        .HasColumnName("defense_power");

                    b.Property<byte>("Health")
                        .HasColumnType("smallint")
                        .HasColumnName("health");

                    b.Property<int>("Index")
                        .HasColumnType("integer")
                        .HasColumnName("index");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<int>("Speed")
                        .HasColumnType("integer")
                        .HasColumnName("speed");

                    b.Property<int>("Ultimate")
                        .HasColumnType("integer")
                        .HasColumnName("ultimate");

                    b.Property<int>("UnitTypeId")
                        .HasColumnType("integer")
                        .HasColumnName("unit_type_id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<int>("XCord")
                        .HasColumnType("integer")
                        .HasColumnName("x_cord");

                    b.Property<int>("YCord")
                        .HasColumnType("integer")
                        .HasColumnName("y_cord");

                    b.HasKey("Id")
                        .HasName("pk_current_unit_states");

                    b.HasIndex("BattleId")
                        .HasDatabaseName("ix_current_unit_states_battle_id");

                    b.ToTable("current_unit_states", (string)null);
                });

            modelBuilder.Entity("Team_8.Contracts.DTOs.TilesDto", b =>
                {
                    b.Property<int[]>("TilesType")
                        .HasColumnType("integer[]")
                        .HasColumnName("tiles_type");

                    b.Property<int>("CountColumn")
                        .HasColumnType("integer")
                        .HasColumnName("count_column");

                    b.Property<int>("CountRow")
                        .HasColumnType("integer")
                        .HasColumnName("count_row");

                    b.HasKey("TilesType")
                        .HasName("pk_tiles_dto");

                    b.ToTable("tiles_dto", (string)null);
                });

            modelBuilder.Entity("Domain.Models.Battle", b =>
                {
                    b.HasOne("Team_8.Contracts.DTOs.TilesDto", "Map")
                        .WithMany()
                        .HasForeignKey("MapTilesType")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_battles_tiles_dto_map_tiles_type");

                    b.Navigation("Map");
                });

            modelBuilder.Entity("Domain.Models.CurrentUnitState", b =>
                {
                    b.HasOne("Domain.Models.Battle", "Battle")
                        .WithMany("CurrentUnitStates")
                        .HasForeignKey("BattleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_current_unit_states_battles_battle_id");

                    b.Navigation("Battle");
                });

            modelBuilder.Entity("Domain.Models.Battle", b =>
                {
                    b.Navigation("CurrentUnitStates");
                });
#pragma warning restore 612, 618
        }
    }
}
