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
        Task<Pessoa> FindByUserId(int id);
        Task<IEnumerable<Pessoa>> FindAllWithUsuarios();

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
            if(pessoa.Usuario != null && pessoa.Usuario.id == 0)
            {
                db.Usuarios.Add(pessoa.Usuario);
            }

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

        public async Task<Pessoa> FindByUserId(int id)
        {
            Pessoa pessoa = await db.Pessoas.Include(x => x.Usuario).Where(x => x.Usuario != null && x.Usuario.id == id).FirstOrDefaultAsync();
            return pessoa;
        }

        public async Task<IEnumerable<Pessoa>> FindAllWithUsuarios()
        {
            IEnumerable<Pessoa> list = await db.Pessoas.Include(p => p.Usuario).ToListAsync();
            return list;
        }

    }

}

