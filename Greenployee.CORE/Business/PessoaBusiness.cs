using Greenployee.MODELS.Data;
using Greenployee.MODELS.Model;
using Microsoft.EntityFrameworkCore;

namespace Greenployee.CORE.Business
{
    public interface IPessoaBusiness
    {
        Task<IEnumerable<Pessoa>> FindAll();
        Task<Pessoa> FindById(int id);
        Task<Pessoa> Insert(Pessoa pessoa);
        Task<Pessoa> Update(Pessoa pessoa);
        Task<bool> Delete(int id);
    }

    public class PessoaBusiness : IPessoaBusiness
    {
        private readonly DataContext db;

        public PessoaBusiness(DataContext context) : base()
        {
            db = context;
        }

        public async Task<IEnumerable<Pessoa>> FindAll()
        {
            IEnumerable<Pessoa> list = await db.Pessoas.Where(x => x.dtExcluido == null).ToListAsync();
            return list;
        }

        public async Task<Pessoa> FindById(int id)
        {
            Pessoa pessoa = await db.Pessoas.Where(x => x.id == id).FirstOrDefaultAsync();
            return pessoa;
        }

        public async Task<Pessoa> Insert(Pessoa pessoa)
        {
            db.Pessoas.Add(pessoa);
            await db.SaveChangesAsync();
            return pessoa;
        }

        public async Task<Pessoa> Update(Pessoa pessoa)
        {
            db.Pessoas.Update(pessoa);
            await db.SaveChangesAsync();
            return pessoa;
        }

        public async Task<bool> Delete(int id)
        {
            Pessoa pessoa = await db.Pessoas.Where(x => x.id == id).FirstOrDefaultAsync();
            if (pessoa == null) { return false; }

            db.Pessoas.Remove(pessoa);
            await db.SaveChangesAsync();
            return true;
        }

    }

}

