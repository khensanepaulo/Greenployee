using Greenployee.MODELS.Data;
using Greenployee.MODELS.Model;
using Microsoft.EntityFrameworkCore;

namespace Greenployee.CORE.Business
{
    public interface IOrdemServicoItemBusiness
    {
        Task<IEnumerable<OrdemServicoItem>> FindAll();
        Task<OrdemServicoItem> FindById(int id);
        Task<OrdemServicoItem> Insert(OrdemServicoItem ordemServicoItem);
        Task<OrdemServicoItem> Update(OrdemServicoItem ordemServicoItem);
        Task<bool> Delete(int id);
    }

    public class OrdemServicoItemBusiness : IOrdemServicoItemBusiness
    {
        private readonly DataContext db;

        public OrdemServicoItemBusiness(DataContext context) : base()
        {
            db = context;
        }

        public async Task<IEnumerable<OrdemServicoItem>> FindAll()
        {
            IEnumerable<OrdemServicoItem> list = await db.OrdemServicoItens.Where(x => x.dtExcluido == null).ToListAsync();
            return list;
        }

        public async Task<OrdemServicoItem> FindById(int id)
        {
            OrdemServicoItem ordemServicoItem = await db.OrdemServicoItens.Where(x => x.id == id).FirstOrDefaultAsync();
            return ordemServicoItem;
        }

        public async Task<OrdemServicoItem> Insert(OrdemServicoItem ordemServicoItem)
        {
            db.OrdemServicoItens.Add(ordemServicoItem);
            await db.SaveChangesAsync();
            return ordemServicoItem;
        }

        public async Task<OrdemServicoItem> Update(OrdemServicoItem ordemServicoItem)
        {
            db.OrdemServicoItens.Update(ordemServicoItem);
            await db.SaveChangesAsync();
            return ordemServicoItem;
        }

        public async Task<bool> Delete(int id)
        {
            OrdemServicoItem ordemServicoItem = await db.OrdemServicoItens.Where(x => x.id == id).FirstOrDefaultAsync();
            if (ordemServicoItem == null) { return false; }

            db.OrdemServicoItens.Remove(ordemServicoItem);
            await db.SaveChangesAsync();
            return true;
        }

    }

}

