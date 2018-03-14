using FluentValidation;
using DataAccess;
using System.Data.Entity;
using ViewModel.UsuarioVMs;
using System.Web;
using System.Web.Mvc;

namespace ViewModel
{
    public abstract class ViewModelValidator<T, TViewModel> : AbstractValidator<TViewModel>
        where T : class
        where TViewModel : class
    {
        private Context _context;
        private DbSet<T> _data;

        private Context Context
        {
            get
            {
                _context = DependencyResolver.Current.GetService<Context>();
                return _context;
            }
        }

        protected DbSet<T> Data
        {
            get
            {
                _data = Context.Set<T>();
                return _data;
            }
        }

        protected FuncionarioOnline User
        {
            get
            {
                return (FuncionarioOnline)DependencyResolver.Current.GetService<HttpContextBase>().Session["UserOnline"];
            }
        }

        protected DbSet<TEntity> GetData<TEntity>() where TEntity : class
        {
            return Context.Set<TEntity>();
        }
    }
}