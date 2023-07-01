using Greenployee.CORE.Filters;
using Greenployee.CORE.Page;
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
        Task<PagedBaseResponse<Pessoa>> GetPagedAsync(PessoaFilter request);

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

        public async Task<PagedBaseResponse<Pessoa>> GetPagedAsync(PessoaFilter request)
        {
            var pessoas = db.Pessoas.Where(x => x.dtExcluido == null).AsQueryable();

            if (request.dtInicio != null)
            {
                pessoas = pessoas.Where(x => x.dtAdmissao >= request.dtInicio);
            }
            if (request.dtFim != null)
            {
                pessoas = pessoas.Where(x => x.dtAdmissao <= request.dtFim);
            }

            if (!string.IsNullOrEmpty(request.nmPessoa))
            {
                pessoas = pessoas.Where(x => x.nmPessoa.Contains(request.nmPessoa));
            }

            if (!string.IsNullOrEmpty(request.nrCPF))
            {
                pessoas = pessoas.Where(x => x.nrCPF.Contains(request.nrCPF));
            }

            if (!string.IsNullOrEmpty(request.nrRG))
            {
                pessoas = pessoas.Where(x => x.nrRG.Contains(request.nrRG));
            }

            if (!string.IsNullOrEmpty(request.dsEmail))
            {
                pessoas = pessoas.Where(x => x.dsEmail.Contains(request.dsEmail));
            }

            if (!string.IsNullOrEmpty(request.nrTelefone))
            {
                pessoas = pessoas.Where(x => x.nrTelefone.Contains(request.nrTelefone));
            }

            if (!string.IsNullOrEmpty(request.flSituacao))
            {
                pessoas = pessoas.Where(x => x.flSituacao.Contains(request.flSituacao));
            }

            if (!string.IsNullOrEmpty(request.nrPIS))
            {
                pessoas = pessoas.Where(x => x.nrPIS.Contains(request.nrPIS));
            }

            return await PageBaseResponseHelper.GetResponseAsync<PagedBaseResponse<Pessoa>, Pessoa>(pessoas, request);
        }


    }

}

