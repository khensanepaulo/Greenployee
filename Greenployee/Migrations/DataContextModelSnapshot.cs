﻿// <auto-generated />
using System;
using Greenployee.MODELS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Greenployee.API.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Greenployee.MODELS.Model.Anotacao", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("dsMensagem")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("dtAtualizado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dtCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("dtExcluido")
                        .HasColumnType("datetime2");

<<<<<<< HEAD
                    b.Property<bool?>("flConcluido")
=======
                    b.Property<bool>("flConcluido")
>>>>>>> 46848bfbf2f82f5a024938abcab3f5b94df1ac89
                        .HasColumnType("bit");

                    b.Property<int>("idPessoa")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("idPessoa");

                    b.ToTable("Anotacoes");
                });

            modelBuilder.Entity("Greenployee.MODELS.Model.Meta", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("dsRecompensa")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("dtAtualizado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dtCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("dtExcluido")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dtFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dtInicio")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("vlMeta")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("id");

                    b.ToTable("Metas");
                });

            modelBuilder.Entity("Greenployee.MODELS.Model.OrdemServico", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("dsEndereco")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("dsFormaPagamento")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTime?>("dtAtualizado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dtCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("dtExcluido")
                        .HasColumnType("datetime2");

                    b.Property<bool>("flEntrega")
                        .HasColumnType("bit");

                    b.Property<string>("flSituacao")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int?>("idFuncionario")
                        .HasColumnType("int");

                    b.Property<string>("nmCliente")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("nrOrdem")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("nrTelefone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal?>("vlTotal")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("id");

                    b.HasIndex("idFuncionario");

                    b.ToTable("OrdensServicos");
                });

            modelBuilder.Entity("Greenployee.MODELS.Model.OrdemServicoItem", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime?>("dtAtualizado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dtCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("dtExcluido")
                        .HasColumnType("datetime2");

                    b.Property<int>("idOrdemServico")
                        .HasColumnType("int");

                    b.Property<string>("nmProduto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("nrQuantidade")
                        .HasColumnType("int");

                    b.Property<decimal>("vlUnitario")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("id");

                    b.HasIndex("idOrdemServico");

                    b.ToTable("OrdemServicoItens");
                });

            modelBuilder.Entity("Greenployee.MODELS.Model.Pessoa", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("dsEmail")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("dtAdmissao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("dtAtualizado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dtCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("dtExcluido")
                        .HasColumnType("datetime2");

                    b.Property<string>("flSituacao")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("idUsuario")
                        .HasColumnType("int");

                    b.Property<string>("nmPessoa")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("nrCPF")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("nrPIS")
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

                    b.HasKey("id");

                    b.HasIndex("idUsuario");

                    b.ToTable("Pessoas");
                });

            modelBuilder.Entity("Greenployee.MODELS.Model.PessoaMeta", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime?>("dtAtualizado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dtCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("dtExcluido")
                        .HasColumnType("datetime2");

                    b.Property<int>("idMeta")
                        .HasColumnType("int");

                    b.Property<int>("idPessoa")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("idMeta");

                    b.HasIndex("idPessoa");

                    b.ToTable("PessoaMetas");
                });

            modelBuilder.Entity("Greenployee.MODELS.Model.Usuario", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("dsLogin")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("dsSenha")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<DateTime?>("dtAtualizado")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("dtCadastro")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("dtExcluido")
                        .HasColumnType("datetime2");

                    b.Property<string>("tpAcesso")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("Greenployee.MODELS.Model.Anotacao", b =>
                {
                    b.HasOne("Greenployee.MODELS.Model.Pessoa", "Pessoa")
                        .WithMany()
                        .HasForeignKey("idPessoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("Greenployee.MODELS.Model.OrdemServico", b =>
                {
                    b.HasOne("Greenployee.MODELS.Model.Pessoa", "Funcionario")
                        .WithMany()
                        .HasForeignKey("idFuncionario");

                    b.Navigation("Funcionario");
                });

            modelBuilder.Entity("Greenployee.MODELS.Model.OrdemServicoItem", b =>
                {
                    b.HasOne("Greenployee.MODELS.Model.OrdemServico", "OrdemServico")
                        .WithMany("OrdemServicoItens")
                        .HasForeignKey("idOrdemServico")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrdemServico");
                });

            modelBuilder.Entity("Greenployee.MODELS.Model.Pessoa", b =>
                {
                    b.HasOne("Greenployee.MODELS.Model.Usuario", "Usuario")
                        .WithMany()
                        .HasForeignKey("idUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("Greenployee.MODELS.Model.PessoaMeta", b =>
                {
                    b.HasOne("Greenployee.MODELS.Model.Meta", "Meta")
                        .WithMany()
                        .HasForeignKey("idMeta")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Greenployee.MODELS.Model.Pessoa", "Pessoa")
                        .WithMany()
                        .HasForeignKey("idPessoa")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meta");

                    b.Navigation("Pessoa");
                });

            modelBuilder.Entity("Greenployee.MODELS.Model.OrdemServico", b =>
                {
                    b.Navigation("OrdemServicoItens");
                });
#pragma warning restore 612, 618
        }
    }
}
