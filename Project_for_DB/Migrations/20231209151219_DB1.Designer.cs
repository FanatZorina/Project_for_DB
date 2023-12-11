﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Project_for_DB;

#nullable disable

namespace Project_for_DB.Migrations
{
    [DbContext(typeof(DoorContext))]
    [Migration("20231209151219_DB1")]
    partial class DB1
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Project_for_DB.Adress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("Building")
                        .HasColumnType("integer")
                        .HasColumnName("building");

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("street");

                    b.HasKey("Id")
                        .HasName("adress_pkey");

                    b.ToTable("adress", (string)null);
                });

            modelBuilder.Entity("Project_for_DB.Audit", b =>
                {
                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.Property<bool>("Closed")
                        .HasColumnType("boolean")
                        .HasColumnName("closed");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("date");

                    b.Property<int>("IdDoor")
                        .HasColumnType("integer")
                        .HasColumnName("id_door");

                    b.Property<int>("IdStreet")
                        .HasColumnType("integer")
                        .HasColumnName("id_street");

                    b.Property<string>("Login")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("login");

                    b.HasKey("Number")
                        .HasName("audit_pkey");

                    b.HasIndex("Login");

                    b.HasIndex("IdDoor", "IdStreet");

                    b.ToTable("audit", (string)null);
                });

            modelBuilder.Entity("Project_for_DB.Departament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<int>("Number")
                        .HasColumnType("integer")
                        .HasColumnName("number");

                    b.HasKey("Id")
                        .HasName("departament_pkey");

                    b.ToTable("departament", (string)null);
                });

            modelBuilder.Entity("Project_for_DB.Lock", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer")
                        .HasColumnName("id");

                    b.Property<int>("IdStreet")
                        .HasColumnType("integer")
                        .HasColumnName("id_street");

                    b.Property<bool>("Closed")
                        .HasColumnType("boolean")
                        .HasColumnName("closed");

                    b.Property<int>("Level")
                        .HasColumnType("integer")
                        .HasColumnName("level");

                    b.HasKey("Id", "IdStreet")
                        .HasName("lock_pkey");

                    b.HasIndex("IdStreet");

                    b.ToTable("lock", (string)null);
                });

            modelBuilder.Entity("Project_for_DB.User", b =>
                {
                    b.Property<string>("Login")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("login");

                    b.Property<int?>("DepartamentId")
                        .HasColumnType("integer")
                        .HasColumnName("departament_id");

                    b.Property<int>("Level")
                        .HasColumnType("integer")
                        .HasColumnName("level");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("password");

                    b.Property<string>("Sname")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("sname");

                    b.HasKey("Login")
                        .HasName("users_pkey");

                    b.HasIndex("DepartamentId");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Project_for_DB.Audit", b =>
                {
                    b.HasOne("Project_for_DB.User", "LoginNavigation")
                        .WithMany("Audits")
                        .HasForeignKey("Login")
                        .HasConstraintName("audit_login_fkey");

                    b.HasOne("Project_for_DB.Lock", "Id")
                        .WithMany("Audits")
                        .HasForeignKey("IdDoor", "IdStreet")
                        .IsRequired()
                        .HasConstraintName("audit_id_door_id_street_fkey");

                    b.Navigation("Id");

                    b.Navigation("LoginNavigation");
                });

            modelBuilder.Entity("Project_for_DB.Lock", b =>
                {
                    b.HasOne("Project_for_DB.Adress", "IdStreetNavigation")
                        .WithMany("Locks")
                        .HasForeignKey("IdStreet")
                        .IsRequired()
                        .HasConstraintName("lock_id_street_fkey");

                    b.Navigation("IdStreetNavigation");
                });

            modelBuilder.Entity("Project_for_DB.User", b =>
                {
                    b.HasOne("Project_for_DB.Departament", "Departament")
                        .WithMany("Users")
                        .HasForeignKey("DepartamentId")
                        .HasConstraintName("users_departament_id_fkey");

                    b.Navigation("Departament");
                });

            modelBuilder.Entity("Project_for_DB.Adress", b =>
                {
                    b.Navigation("Locks");
                });

            modelBuilder.Entity("Project_for_DB.Departament", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Project_for_DB.Lock", b =>
                {
                    b.Navigation("Audits");
                });

            modelBuilder.Entity("Project_for_DB.User", b =>
                {
                    b.Navigation("Audits");
                });
#pragma warning restore 612, 618
        }
    }
}