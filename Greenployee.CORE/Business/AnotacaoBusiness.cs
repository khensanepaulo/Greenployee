﻿using Greenployee.CORE.Filters;
using Greenployee.CORE.Page;
using Greenployee.MODELS.Data;
using Greenployee.MODELS.DTO;
using Greenployee.MODELS.DTO.Anotacao;
using Greenployee.MODELS.DTO.PermissaoUsuario;
using Greenployee.MODELS.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenployee.CORE.Business
{
    public interface IAnotacaoBusiness
    {
        Task<IEnumerable<Anotacao>> FindAll();
        Task<Anotacao> FindById(int id);
        Task<Anotacao> Insert(AnotacaoDTO anotacao);
        Task<Anotacao> Update(Anotacao anotacao);
        Task<bool> Delete(int id);
        Task<IEnumerable<Anotacao>> FindByUserId(int id);
        Task<PagedBaseResponse<Anotacao>> GetPagedAsync(AnotacaoFilter request);
    }

    public class AnotacaoBusiness : IAnotacaoBusiness
    {
        private readonly DataContext db;

        public AnotacaoBusiness(DataContext context) : base()
        {
            db = context;
        }

        public async Task<IEnumerable<Anotacao>> FindAll()
        {
            IEnumerable<Anotacao> list = await db.Anotacoes.Where(x => x.dtExcluido == null).OrderByDescending(x => x.dtCadastro).ToListAsync();
            return list;
        }

        public async Task<Anotacao> FindById(int id)
        {
            Anotacao anotacao = await db.Anotacoes.Where(x => x.id == id).FirstOrDefaultAsync();
            return anotacao;
        }

        public async Task<Anotacao> Insert(AnotacaoDTO anotacao)
        {
            var entity = new Anotacao(anotacao.idPessoa, anotacao.dsMensagem);
            db.Anotacoes.Add(entity);
            await db.SaveChangesAsync();
            return entity;
        }

        public async Task<Anotacao> Update(Anotacao anotacao)
        {
            anotacao.dtAtualizado = DateTime.Now;
            db.Anotacoes.Update(anotacao);
            await db.SaveChangesAsync();
            return anotacao;
        }

        public async Task<bool> Delete(int id)
        {
            Anotacao anotacao = await db.Anotacoes.Where(x => x.id == id).FirstOrDefaultAsync();
            if (anotacao == null) { return false; }

            anotacao.dtExcluido = DateTime.Now;
            db.Anotacoes.Update(anotacao);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Anotacao>> FindByUserId(int id)
        {
            IEnumerable<Anotacao> list = await db.Anotacoes.Include(x => x.Pessoa)
                                                           .ThenInclude(x => x.Usuario)
                                                           .Where(x => x.dtExcluido == null && x.Pessoa != null && x.Pessoa.Usuario != null && x.Pessoa.Usuario.id == id)
                                                           .ToListAsync();
            return list;
        }

        public async Task<PagedBaseResponse<Anotacao>> GetPagedAsync(AnotacaoFilter request)
        {
            var anotacoes = db.Anotacoes.Where(x => x.dtExcluido == null).AsQueryable();

            if (request.dtInicio != null)
            {
                anotacoes = anotacoes.Where(x => x.dtCadastro >= request.dtInicio);
            }
            if (request.dtFim != null)
            {
                anotacoes = anotacoes.Where(x => x.dtCadastro <= request.dtFim);
            }
            if (!string.IsNullOrEmpty(request.nmPessoa))
            {
                anotacoes = anotacoes.Where(x => x.Pessoa.nmPessoa.Contains(request.nmPessoa));
            }

            if (!string.IsNullOrEmpty(request.dsMensagem))
            {
                anotacoes = anotacoes.Where(x => x.dsMensagem.Contains(request.dsMensagem));
            }

            if (request.idUsuario != null)
            {
                anotacoes = anotacoes.Where(x => x.Pessoa != null && x.Pessoa.Usuario != null && x.Pessoa.Usuario.id == request.idUsuario);
            }


            return await PageBaseResponseHelper.GetResponseAsync<PagedBaseResponse<Anotacao>, Anotacao>(anotacoes.OrderByDescending(x => x.dtCadastro), request);
        }
    }
}
