using Greenployee.MODELS.Data;
using Greenployee.MODELS.Model;
using Microsoft.EntityFrameworkCore;

namespace Greenployee.CORE.Business
{
    public interface IUsuarioBusiness
    {
        Task<IEnumerable<Usuario>> FindAll();
        Task<Usuario> FindById(int id);
        Task<Usuario> Insert(Usuario usuario);
        Task<Usuario> Update(Usuario usuario);
        Task<bool> Delete(int id);
        Task<Usuario> GetUserByLoginAndSenhaAsync(string dsLogin, string dsSenha);
    }

    public class UsuarioBusiness : IUsuarioBusiness
    {
        private readonly DataContext db;

        public UsuarioBusiness(DataContext context) : base()
        {
            db = context;
        }

        public async Task<IEnumerable<Usuario>> FindAll()
        {
            IEnumerable<Usuario> list = await db.Usuarios.Where(x => x.dtExcluido == null).ToListAsync();
            return list;
        }

        public async Task<Usuario> FindById(int id)
        {
            Usuario usuario = await db.Usuarios.Where(x => x.id == id).FirstOrDefaultAsync();
            return usuario;
        }

        public async Task<Usuario> Insert(Usuario usuario)
        {
            db.Usuarios.Add(usuario);
            await db.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> Update(Usuario usuario)
        {
            db.Usuarios.Update(usuario);
            await db.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> Delete(int id)
        {
            Usuario usuario = await db.Usuarios.Where(x => x.id == id).FirstOrDefaultAsync();
            if (usuario == null) { return false; }

            db.Usuarios.Remove(usuario);
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario> GetUserByLoginAndSenhaAsync(string dsLogin, string dsSenha)
        {
            return await db.Usuarios.FirstOrDefaultAsync(x => x.dsLogin == dsLogin && x.dsSenha == dsSenha);
        }
    }
}
