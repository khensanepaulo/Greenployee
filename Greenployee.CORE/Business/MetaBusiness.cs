using Greenployee.CORE.Filters;
using Greenployee.CORE.Page;
using Greenployee.MODELS.Data;
using Greenployee.MODELS.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenployee.CORE.Business
{
    public interface IMetaBusiness
    {
        Task<IEnumerable<Meta>> FindAll();
        Task<Meta> FindById(int id);
        Task<Meta> Insert(Meta meta);
        Task<Meta> Update(Meta meta);
        Task<bool> Delete(int id);
        Task<IEnumerable<Meta>> FindByUserId(int id);
        Task<PagedBaseResponse<Meta>> GetPagedAsync(MetaFilter request); 
    }

    public class MetaBusiness : IMetaBusiness
    {
        private readonly DataContext db;

        public MetaBusiness(DataContext context) : base()
        {
            db = context;
        }

        public async Task<IEnumerable<Meta>> FindAll()
        {
            IEnumerable<Meta> list = await db.Metas.Include(x => x.PessoasMeta).Where(x => x.dtExcluido == null).OrderByDescending(x => x.dtCadastro).ToListAsync();
            return list;
        }

        public async Task<Meta> FindById(int id)
        {
            Meta meta = await db.Metas.Where(x => x.id == id).FirstOrDefaultAsync();
            return meta;
        }

        public async Task<Meta> Insert(Meta meta)
        {
            db.Entry(meta).State = EntityState.Added;
            await db.SaveChangesAsync();

            foreach (var item in meta.PessoasMeta)
            {
                item.idMeta = meta.id;
                item.Meta = null;
                db.Entry(item).State = EntityState.Added;
            }

            await db.SaveChangesAsync();
            return meta;
        }

        public async Task<Meta> Update(Meta meta)
        {
            meta.dtAtualizado = DateTime.Now;
            foreach (var item in meta.PessoasMeta)
            {
                item.dtAtualizado = DateTime.Now;
                item.dtConcluido = DateTime.Now;
                item.flConcluido = true;
            }
            
            db.Metas.Update(meta);
            await db.SaveChangesAsync();
            return meta;
        }

        public async Task<bool> Delete(int id)
        {
            Meta meta = await db.Metas.Where(x => x.id == id).FirstOrDefaultAsync();
            if(meta == null) { return false; }

            meta.dtExcluido = DateTime.Now;
            db.Metas.Update(meta);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Meta>> FindByUserId(int id)
        {
            IEnumerable<Meta> list = await db.PessoaMetas.Include(x => x.Meta).Include(x => x.Pessoa).ThenInclude(x => x.Usuario)
                                                         .Where(x => x.dtExcluido == null && x.Pessoa != null && x.Pessoa.Usuario != null && x.Meta != null && x.Pessoa.Usuario.id == id)
                                                         .Select(x => x.Meta)
                                                         .OrderByDescending(x => x.dtCadastro)
                                                         .ToListAsync();
            return list;
        }

        public async Task<PagedBaseResponse<Meta>> GetPagedAsync(MetaFilter request)
        {
            var metas = db.Metas.Include(x => x.PessoasMeta).Where(x => x.dtExcluido == null).AsQueryable();

            if (request.dtInicio != null)
            {
                metas = metas.Where(x => x.dtCadastro >= request.dtInicio);
            }
            if (request.dtFim != null)
            {
                metas = metas.Where(x => x.dtCadastro <= request.dtFim);
            }
            if (!string.IsNullOrEmpty(request.dsRecompensa))
            {
                metas = metas.Where(x => x.dsRecompensa.Contains(request.dsRecompensa));
            }

            if (!string.IsNullOrEmpty(request.flConcluida))
            {
               if (request.flConcluida == "sim")
                {
                    metas = metas.Where(x => x.PessoasMeta.Any(pm => pm.flConcluido == true));
                }
                else if (request.flConcluida == "nao")
                {
                    metas = metas.Where(x => !x.PessoasMeta.Any(pm => pm.flConcluido == true));
                }
            }

            return await PageBaseResponseHelper.GetResponseAsync<PagedBaseResponse<Meta>, Meta>(metas.OrderByDescending(x => x.dtCadastro), request);
        }
    }
}
