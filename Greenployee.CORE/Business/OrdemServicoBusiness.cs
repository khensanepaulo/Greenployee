﻿using Greenployee.MODELS.Data;
using Greenployee.MODELS.DTO;
using Greenployee.MODELS.Model;
using Microsoft.EntityFrameworkCore;


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
            IEnumerable<OrdemServico> list = await db.OrdensServicos.Where(x => x.dtExcluido == null).ToListAsync();
            return list;
        }

        public async Task<OrdemServico> FindById(int id)
        {
            OrdemServico ordemServico = await db.OrdensServicos.Where(x => x.id == id).FirstOrDefaultAsync();
            return ordemServico;
        }

        public async Task<OrdemServico> Insert(OrdemServico ordemServico)
        {

            db.Entry(ordemServico).State = EntityState.Added;
            await db.SaveChangesAsync();

            foreach (var osItem in ordemServico.OrdemServicoItem)
            {
                osItem.idOrdemServico = ordemServico.id;
                osItem.OrdemServico = null;
                db.OrdemServicoItens.Add(osItem);
            }

            await db.SaveChangesAsync();
            return ordemServico;
        }

        public async Task<OrdemServico> Update(OrdemServico ordemServico)
        {
            db.OrdensServicos.Update(ordemServico);
            await db.SaveChangesAsync();
            return ordemServico;
        }

        public async Task<bool> Delete(int id)
        {
            OrdemServico ordemServico = await db.OrdensServicos.Where(x => x.id == id).FirstOrDefaultAsync();
            if (ordemServico == null) { return false; }

            db.OrdensServicos.Remove(ordemServico);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<dynamic>> FindByUserId(int id)
        {
            IEnumerable<dynamic> list = await (from os in db.OrdensServicos

                                               join osi in db.OrdemServicoItens
                                               on os.id equals osi.id

                                               where os.Funcionario != null && os.Funcionario.Usuario != null && os.Funcionario.Usuario.id == id

                                               select new
                                               {
                                                   os,
                                                   os.Funcionario,
                                                   osi

                                               })
                                               .ToListAsync();

            return list;
        }
    }
}
