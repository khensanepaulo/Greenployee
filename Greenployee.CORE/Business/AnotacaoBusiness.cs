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
            IEnumerable<Anotacao> list = await db.Anotacoes.Where(x => x.dtExcluido == null).ToListAsync();
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
            db.Anotacoes.Update(anotacao);
            await db.SaveChangesAsync();
            return anotacao;
        }

        public async Task<bool> Delete(int id)
        {
            Anotacao anotacao = await db.Anotacoes.Where(x => x.id == id).FirstOrDefaultAsync();
            if (anotacao == null) { return false; }

            db.Anotacoes.Remove(anotacao);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Anotacao>> FindByUserId(int id)
        {
            IEnumerable<Anotacao> list = await db.Anotacoes.Include(x => x.Pessoa).ThenInclude(x => x.Usuario).Where(x => x.Pessoa != null && x.Pessoa.Usuario != null && x.Pessoa.Usuario.id == id).ToListAsync();
            return list;
        }
    }
}
