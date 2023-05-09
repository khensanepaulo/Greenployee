using Greenployee.MODELS.Data;
using Greenployee.MODELS.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greenployee.CORE.Business
{
    public class PessoaBusiness : DataContext
    {
        private readonly DataContext db;

        public PessoaBusiness(DataContext context)
        {
            db = context;
        }

        public List<Pessoa> GetListPessoas()
        {
            return db.Pessoas.Select(x => x).ToList();
        }

        public Pessoa GetPessoa(int id)
        {
            return db.Pessoas.Find(id);
        }

        public Pessoa SavePessoa(Pessoa pessoa)
        {
            if(pessoa.Id == 0)
                db.Entry(pessoa).State = EntityState.Added;
            else
                db.Entry(pessoa).State = EntityState.Modified;

            db.SaveChanges();
            return pessoa;
        }

        public void DeletePessoa(int id)
        {
            var pessoa = this.GetPessoa(id);
            db.Entry(pessoa).State = EntityState.Deleted;
            db.SaveChanges();
        }

    }
}
