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
            IEnumerable<Meta> list = await db.Metas.Where(x => x.dtExcluido == null).ToListAsync();
            return list;
        }

        public async Task<Meta> FindById(int id)
        {
            Meta meta = await db.Metas.Where(x => x.id == id).FirstOrDefaultAsync();
            return meta;
        }

        public async Task<Meta> Insert(Meta meta)
        {
            db.Metas.Add(meta);
            await db.SaveChangesAsync();
            return meta;
        }

        public async Task<Meta> Update(Meta meta)
        {
            db.Metas.Update(meta);
            await db.SaveChangesAsync();
            return meta;
        }

        public async Task<bool> Delete(int id)
        {
            Meta meta = await db.Metas.Where(x => x.id == id).FirstOrDefaultAsync();
            if(meta == null) { return false; }

            db.Metas.Remove(meta);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Meta>> FindByUserId(int id)
        {
            IEnumerable<Meta> list = await db.PessoaMetas.Include(x => x.Meta).Include(x => x.Pessoa).ThenInclude(x => x.Usuario)
                                                         .Where(x => x.Pessoa != null && x.Pessoa.Usuario != null && x.Meta != null && x.Pessoa.Usuario.id == id)
                                                         .Select(x => x.Meta)
                                                         .ToListAsync();
            return list;
        }
    }
}
