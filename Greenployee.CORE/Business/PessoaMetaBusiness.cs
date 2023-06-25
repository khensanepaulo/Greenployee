using Greenployee.MODELS.Data;
using Greenployee.MODELS.Model;
using Microsoft.EntityFrameworkCore;

namespace Greenployee.CORE.Business
{
    public interface IPessoaMetaBusiness
    {
        Task<IEnumerable<PessoaMeta>> FindAll();
        Task<PessoaMeta> FindById(int id);
        Task<PessoaMeta> Insert(PessoaMeta pessoaMeta);
        Task<PessoaMeta> Update(PessoaMeta pessoaMeta);
        Task<bool> Delete(int id);
        Task<IEnumerable<dynamic>> FindByUserId(int id);


    }

    public class  PessoaMetaBusiness : IPessoaMetaBusiness
    {
        private readonly DataContext db;

        public PessoaMetaBusiness(DataContext context) : base()
        {
            db = context;
        }

        public async Task<IEnumerable<PessoaMeta>> FindAll()
        {
            IEnumerable<PessoaMeta> list = await db.PessoaMetas.Where(x => x.dtExcluido == null).ToListAsync();
            return list;
        }

        public async Task<PessoaMeta> FindById(int id)
        {
            PessoaMeta pessoaMeta = await db.PessoaMetas.Where(x => x.id == id).FirstOrDefaultAsync();
            return pessoaMeta;
        }

        public async Task<PessoaMeta> Insert(PessoaMeta pessoaMeta)
        {
            db.PessoaMetas.Add(pessoaMeta);
            await db.SaveChangesAsync();
            return pessoaMeta;
        }

        public async Task<PessoaMeta> Update(PessoaMeta pessoaMeta)
        {
            db.PessoaMetas.Update(pessoaMeta);
            await db.SaveChangesAsync();
            return pessoaMeta;
        }

        public async Task<bool> Delete(int id)
        {
            PessoaMeta pessoaMeta = await db.PessoaMetas.Where(x => x.id == id).FirstOrDefaultAsync();
            if (pessoaMeta == null) { return false; }

            db.PessoaMetas.Remove(pessoaMeta);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<dynamic>> FindByUserId(int id)
        {
            IEnumerable<dynamic> list = await (from p in db.PessoaMetas
                                               where p.Pessoa != null && p.Pessoa.Usuario != null && p.Pessoa.Usuario.id == id
                                               select new
                                               {
                                                   p.Pessoa,
                                                   p,
                                                   p.Meta,
                                               })
                                               .ToListAsync();
            return list;
        }
    }

}


