﻿// <auto-generated />
using System;
using BLHX.Server.Common.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BLHX.Server.Common.Migrations.Player
{
    [DbContext(typeof(PlayerContext))]
    [Migration("20240222064540_ResourceFieldsUpgradeTime")]
    partial class ResourceFieldsUpgradeTime
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true);

            modelBuilder.Entity("BLHX.Server.Common.Database.Player", b =>
                {
                    b.Property<uint>("Uid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Adv")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValue("");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayInfo")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<uint>("Exp")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Fleets")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("jsonb")
                        .HasDefaultValue("[{\"Id\":1,\"Name\":null,\"ShipLists\":[1,2],\"Commanders\":[]},{\"Id\":2,\"Name\":null,\"ShipLists\":[],\"Commanders\":[]},{\"Id\":11,\"Name\":null,\"ShipLists\":[],\"Commanders\":[]},{\"Id\":12,\"Name\":null,\"ShipLists\":[],\"Commanders\":[]}]");

                    b.Property<uint>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Uid");

                    b.HasIndex("Token")
                        .IsUnique();

                    b.ToTable("Players");
                });

            modelBuilder.Entity("BLHX.Server.Common.Database.PlayerResource", b =>
                {
                    b.Property<uint>("Id")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("PlayerUid")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("Num")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id", "PlayerUid");

                    b.HasIndex("PlayerUid");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("BLHX.Server.Common.Database.PlayerShip", b =>
                {
                    b.Property<uint>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<uint>("ActivityNpc")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("BluePrintFlag")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("CommanderId")
                        .HasColumnType("INTEGER");

                    b.Property<uint?>("CommonFlag")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CoreLists")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<uint>("Energy")
                        .HasColumnType("INTEGER");

                    b.Property<string>("EquipInfoLists")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<uint>("Exp")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("Intimacy")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsLocked")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastChangeName")
                        .HasColumnType("TEXT");

                    b.Property<uint>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<string>("MetaRepairLists")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<uint>("PlayerUid")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("Proficiency")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("Propose")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SkillIdLists")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<uint>("SkinId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<string>("StrengthLists")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.Property<uint>("TemplateId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TransformLists")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.HasKey("Id");

                    b.HasIndex("PlayerUid");

                    b.ToTable("Ships");
                });

            modelBuilder.Entity("BLHX.Server.Common.Database.ResourceField", b =>
                {
                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.Property<uint>("PlayerUid")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("LastHarvestTime")
                        .HasColumnType("TEXT");

                    b.Property<uint>("Level")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("UpgradeTime")
                        .HasColumnType("TEXT");

                    b.HasKey("Type", "PlayerUid");

                    b.HasIndex("PlayerUid");

                    b.ToTable("ResourceFields");
                });

            modelBuilder.Entity("BLHX.Server.Common.Database.PlayerResource", b =>
                {
                    b.HasOne("BLHX.Server.Common.Database.Player", "Player")
                        .WithMany("Resources")
                        .HasForeignKey("PlayerUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("BLHX.Server.Common.Database.PlayerShip", b =>
                {
                    b.HasOne("BLHX.Server.Common.Database.Player", "Player")
                        .WithMany("Ships")
                        .HasForeignKey("PlayerUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("BLHX.Server.Common.Database.ResourceField", b =>
                {
                    b.HasOne("BLHX.Server.Common.Database.Player", "Player")
                        .WithMany("ResourceFields")
                        .HasForeignKey("PlayerUid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("BLHX.Server.Common.Database.Player", b =>
                {
                    b.Navigation("ResourceFields");

                    b.Navigation("Resources");

                    b.Navigation("Ships");
                });
#pragma warning restore 612, 618
        }
    }
}
