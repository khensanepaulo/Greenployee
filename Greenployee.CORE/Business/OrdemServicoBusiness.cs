using Greenployee.MODELS.Data;
using Greenployee.MODELS.DTO;
using Greenployee.MODELS.Model;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        string GetSequenceNrOrdem();

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
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                MaxDepth = 32 // Defina o valor adequado para a profundidade máxima permitida, se necessário
            };

            var query = from os in db.OrdensServicos.Include(x => x.Funcionario).Include(x => x.OrdemServicoItem)
                        where os.Funcionario != null && os.Funcionario.Usuario != null && os.Funcionario.Usuario.id == id
                        select os;

            var results = await query.ToListAsync();

            // Serializar cada objeto da lista para JSON
            var serializedResults = results.Select(obj =>
            {
                string jsonString = JsonSerializer.Serialize(obj, options);
                return JsonSerializer.Deserialize<dynamic>(jsonString, options);
            });

            return serializedResults;
        }

        public string GetSequenceNrOrdem()
        {
            var lastNrOrdem = int.Parse(db.OrdensServicos.OrderByDescending(x => x.id).FirstOrDefault().nrOrdem) + 1;
            var nrOrdem = lastNrOrdem.ToString("00000");
            return nrOrdem;
        }
    }
}
