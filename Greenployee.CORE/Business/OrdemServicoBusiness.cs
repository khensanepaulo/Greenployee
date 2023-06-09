﻿using Greenployee.MODELS.Data;
using Greenployee.MODELS.DTO;
using Greenployee.MODELS.Model;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Greenployee.CORE.Page;
using Greenployee.CORE.Filters;

namespace Greenployee.CORE.Business
{
    public interface IOrdemServicoBusiness
    {
        Task<IEnumerable<OrdemServico>> FindAll();
        Task<OrdemServico> FindById(int id);
        Task<OrdemServico> Insert(OrdemServico ordemServico);
        Task<OrdemServico> Update(OrdemServico ordemServico);
        Task<bool> Delete(int id);
        Task<IEnumerable<dynamic>> FindByUserId(int id);
        string GetSequenceNrOrdem();
        Task<IEnumerable<dynamic>> FindBycommissionsByMonthById(int id);
        Task<IEnumerable<dynamic>> FindByCommissionsByMonthAll();

        Task<PagedBaseResponse<OrdemServico>> GetPagedAsync(OrdemServicoFilter request);

    }

    public class OrdemServicoBusiness : IOrdemServicoBusiness
    {
        private readonly DataContext db;

        public OrdemServicoBusiness(DataContext context) : base()
        {
            db = context;
        }

        public async Task<IEnumerable<OrdemServico>> FindAll()
        {            
            var list = await db.OrdensServicos.Include(x => x.Funcionario).Include(x => x.OrdemServicoItem)
                                                                    .Where(x => x.dtExcluido == null)
                                                                    .OrderByDescending(x => x.nrOrdem)
                                                                    .ToListAsync();          
            return list;
        }

        public async Task<OrdemServico> FindById(int id)
        {
            OrdemServico ordemServico = await db.OrdensServicos.Where(x => x.id == id).FirstOrDefaultAsync();
            return ordemServico;
        }

        public async Task<OrdemServico> Insert(OrdemServico ordemServico)
        {
            ordemServico.nrOrdem = GetSequenceNrOrdem();
            ordemServico.idFuncionario = ordemServico.Funcionario.id;
            ordemServico.Funcionario = null;

            db.Entry(ordemServico).State = EntityState.Added;
            await db.SaveChangesAsync();

            foreach (var osItem in ordemServico.OrdemServicoItem)
            {
                osItem.idOrdemServico = ordemServico.id;
                osItem.OrdemServico = null;
                db.OrdemServicoItens.Add(osItem);
            }

            var metas = db.PessoaMetas.Where(x => x.idPessoa == ordemServico.idFuncionario && x.flConcluido == false).ToList();
            if(metas.Count() > 0)
            {
                foreach(var meta in metas)
                {
                    meta.vlAlcancado += ordemServico.vlTotal ?? 0;
                    db.PessoaMetas.Update(meta);
                }
            }

            await db.SaveChangesAsync();
            return ordemServico;
        }
        
        public async Task<OrdemServico> Update(OrdemServico ordemServico)
        {
            ordemServico.dtAtualizado = DateTime.Now;
            db.OrdensServicos.Update(ordemServico);
            await db.SaveChangesAsync();
            return ordemServico;
        }

        public async Task<bool> Delete(int id)
        {
            OrdemServico ordemServico = await db.OrdensServicos.Where(x => x.id == id).FirstOrDefaultAsync();
            if (ordemServico == null) { return false; }

            ordemServico.dtExcluido = DateTime.Now;
            db.OrdensServicos.Update(ordemServico);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<dynamic>> FindByUserId(int id)
        {
            var query = from os in db.OrdensServicos.Include(x => x.Funcionario)
                                                    .Include(x => x.OrdemServicoItem)
                                                    .Where(x => x.dtExcluido == null)
                                                    .OrderByDescending(x => x.nrOrdem)
                        where os.Funcionario != null && os.Funcionario.Usuario != null && os.Funcionario.Usuario.id == id
                        select os;

            var results = await query.OrderByDescending(x => x.nrOrdem).ToListAsync();
            return results;
        }

        public string GetSequenceNrOrdem()
        {
            var lastNrOrdem = int.Parse(db.OrdensServicos.OrderByDescending(x => x.id).FirstOrDefault().nrOrdem) + 1;
            var nrOrdem = lastNrOrdem.ToString("00000");
            return nrOrdem;
        }

        public async Task<IEnumerable<dynamic>> FindByCommissionsByMonthAll()
        {
            var list = await (from o in db.OrdensServicos
                              where o.dtExcluido == null
                              group o by new { o.dtCadastro.Year, o.dtCadastro.Month } into g
                              select new
                              {
                                  nmMes = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM yyyy").ToUpper(),
                                  vlTotal = g.Sum(os => os.vlTotal),
                                  dtOrdem = g.Select(x => x.dtCadastro),
                                  vlOrdem = g.Select(x => x.vlTotal),
                                  ordensServico = g.ToList()
                              }).ToListAsync();

            return list;
        }

        public async Task<IEnumerable<dynamic>> FindBycommissionsByMonthById(int id)
        {
            var list = await (from o in db.OrdensServicos
                              where o.Funcionario != null && o.Funcionario.Usuario != null && o.Funcionario.Usuario.id == id &&
                              o.dtExcluido == null
                              group o by new { o.dtCadastro.Year, o.dtCadastro.Month } into g
                              select new
                              {
                                  nmMes = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM yyyy").ToUpper(),
                                  vlTotal = g.Sum(os => os.vlTotal),
                                  dtOrdem = g.Select(x => x.dtCadastro),
                                  vlOrdem = g.Select(x => x.vlTotal),
                                  OrdensServico = g.ToList()
                              }).ToListAsync();

            return list;
        }

        public async Task<PagedBaseResponse<OrdemServico>> GetPagedAsync(OrdemServicoFilter request)
        {
            var ordens = db.OrdensServicos.Include(x => x.OrdemServicoItem).Include(x => x.Funcionario).ThenInclude(x => x.Usuario).AsQueryable();

            if (request.idUsuario != null)
            {
                ordens = ordens.Where(x => x.Funcionario != null && x.Funcionario.Usuario != null && x.Funcionario.Usuario.id == request.idUsuario);
            }
            if (request.dtInicio != null)
            {
                ordens = ordens.Where(x => x.dtCadastro >= Convert.ToDateTime(request.dtInicio));
            }
            if (request.dtFim != null)
            {
                ordens = ordens.Where(x => x.dtCadastro <= Convert.ToDateTime(request.dtFim));
            }
            if (!string.IsNullOrEmpty(request.nrOrdem))
            {
                ordens = ordens.Where(x => x.nrOrdem.Contains(request.nrOrdem));
            }
            if (!string.IsNullOrEmpty(request.nmCliente))
            {
                ordens = ordens.Where(x => x.nmCliente.Contains(request.nmCliente));
            }
            if (!string.IsNullOrEmpty(request.nmFuncionario))
            {
                ordens = ordens.Where(x => x.Funcionario.nmPessoa.Contains(request.nmFuncionario));
            }

            return await PageBaseResponseHelper.GetResponseAsync<PagedBaseResponse<OrdemServico>, OrdemServico>(ordens, request);
        }
    }
}
