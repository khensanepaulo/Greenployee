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
        Task<IEnumerable<dynamic>> FindByUserId(int id);
        Task<IEnumerable<dynamic>> GetLastSalesChart();
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

        public async Task<IEnumerable<dynamic>> FindByUserId(int id)
        {
            IEnumerable<dynamic> list = await (from o in db.OrdemServicoItens
                                               where o.OrdemServico.Funcionario != null && o.OrdemServico.Funcionario.Usuario != null && o.OrdemServico.Funcionario.Usuario.id == id
                                               select new
                                               {
                                                   o,
                                                   o.OrdemServico.Funcionario,
                                               })
                                               .ToListAsync();
            return list;
        }

        public async Task<IEnumerable<dynamic>> GetLastSalesChart()
        {
            DateTime currentDate = DateTime.Now;
            DateTime dateSevenDaysAgo = currentDate.AddDays(-7);

            IEnumerable<dynamic> list = await (from o in db.OrdemServicoItens
                                               where o.dtCadastro >= currentDate
                                               && o.dtCadastro <= dateSevenDaysAgo
                                               select new
                                               {
                                                   o,
                                                   o.OrdemServico.Funcionario,
                                               })
                                               .ToListAsync();
            return list;
        }

    }

}

