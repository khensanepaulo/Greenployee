using Greenployee.MODELS.Data;
using Greenployee.MODELS.Model;
using Microsoft.EntityFrameworkCore;

namespace Greenployee.CORE.Business
{
    public interface IPermissaoUsuarioBusiness
    {
        Task<IEnumerable<PermissaoUsuario>> FindAll();
        Task<PermissaoUsuario> FindById(int id);
        Task<PermissaoUsuario> Insert(PermissaoUsuario permissaoUsuario);
        Task<PermissaoUsuario> Update(PermissaoUsuario permissaoUsuario);
        Task<bool> Delete(int id);
        Task<PermissaoUsuario> FindByUserId(int id);
        Task<IEnumerable<PermissaoUsuario>> FindAllWithUsuarios();

    }

    public class PermissaoUsuarioBusiness : IPermissaoUsuarioBusiness
    {
        private readonly DataContext db;

        public PermissaoUsuarioBusiness(DataContext context) : base()
        {
            db = context;
        }

        public async Task<IEnumerable<PermissaoUsuario>> FindAll()
        {
            IEnumerable<PermissaoUsuario> list = await db.PermissaoUsuarios.Where(x => x.dtExcluido == null).ToListAsync();
            return list;
        }

        public async Task<PermissaoUsuario> FindById(int id)
        {
            PermissaoUsuario permissaoUsuario = await db.PermissaoUsuarios.Where(x => x.id == id).FirstOrDefaultAsync();
            return permissaoUsuario;
        }

        public async Task<PermissaoUsuario> Insert(PermissaoUsuario permissaoUsuario)
        {
            db.PermissaoUsuarios.Add(permissaoUsuario);
            await db.SaveChangesAsync();
            return permissaoUsuario;
        }

        public async Task<PermissaoUsuario> Update(PermissaoUsuario permissaoUsuario)
        {
            db.PermissaoUsuarios.Update(permissaoUsuario);
            await db.SaveChangesAsync();
            return permissaoUsuario;
        }

        public async Task<bool> Delete(int id)
        {
            PermissaoUsuario permissaoUsuario = await db.PermissaoUsuarios.Where(x => x.id == id).FirstOrDefaultAsync();
            if (permissaoUsuario == null) { return false; }

            db.PermissaoUsuarios.Remove(permissaoUsuario);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<PermissaoUsuario> FindByUserId(int id)
        {
            PermissaoUsuario permissaoUsuario = await db.PermissaoUsuarios.Include(x => x.Usuario).Where(x => x.Usuario != null && x.Usuario.id == id).FirstOrDefaultAsync();
            return permissaoUsuario;
        }

        public async Task<IEnumerable<PermissaoUsuario>> FindAllWithUsuarios()
        {
            IEnumerable<PermissaoUsuario> list = await db.PermissaoUsuarios.Include(p => p.Usuario).ToListAsync();
            return list;
        }

    }

}

