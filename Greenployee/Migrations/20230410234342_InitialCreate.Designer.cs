﻿// <auto-generated />
using System;
using Greenployee.MODELS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Greenployee.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230410234342_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Greenployee.Model.Anotacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("dsMensagem")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("dtAnotacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dtAtualizado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dtCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dtExcluido")
                        .HasColumnType("datetime2");

                    b.Property<int>("idPessoa")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Anotacao");
                });

            modelBuilder.Entity("Greenployee.Model.OrdemServico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("dsEndereco")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("dsFormaPagamento")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTime>("dtAtualizado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dtCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dtExcluido")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dtOrdem")
                        .HasColumnType("datetime2");

                    b.Property<bool>("flEntrega")
                        .HasColumnType("bit");

                    b.Property<string>("flSituacao")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("idFuncionario")
                        .HasColumnType("int");

                    b.Property<string>("nmCliente")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("nrOrdem")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("nrTelefone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("OrdensServicos");
                });

            modelBuilder.Entity("Greenployee.Model.Pessoa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("dsEmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("dtAdmissao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dtAtualizado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dtCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dtExcluido")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("flEntrega")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("flSituacao")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("nmPessoa")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("nrCPF")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("nrPIS")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("nrRG")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("nrTelefone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Pessoa");
                });
#pragma warning restore 612, 618
        }
    }
}
