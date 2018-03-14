using DataAccess;
using Domain.Auth;
using Helper;
using Helper.CacheHelper;
using Model;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace Domain
{
    public class ModelDomain<T> where T : class, IKey
    {
        protected Context _context;
        protected readonly ICacheManager _cacheManager;
        protected readonly IFuncionarioProvider _funcionarioProvider;
        protected readonly IAdminProvider _adminProvider;

        protected DbSet<T> Data
        {
            get
            {
                return _context.Set<T>();
            }
        }

        public ModelDomain()
        {
            _context = DependencyResolver.Current.GetService<Context>();
            _cacheManager = DependencyResolver.Current.GetService<ICacheManager>();
            _funcionarioProvider = DependencyResolver.Current.GetService<IFuncionarioProvider>();
            _adminProvider = DependencyResolver.Current.GetService<IAdminProvider>();
        }

        public virtual IQueryable<T> Get()
        {
            return Data.AsQueryable();
        }

        public virtual T Get(int id)
        {
            return Data.SingleOrDefault(a => a.Id == id);
        }

        public virtual T Save(T model)
        {
            if (model.Id == 0)
            {
                Data.Add(model);
            }
            else
            {
                var currentModel = Get(model.Id);
                currentModel.Inject(currentModel);
            }

            _context.SaveChanges();
            return model;
        }

        public virtual T Insert(T model)
        {
            Data.Add(model);
            _context.SaveChanges();
            return model;
        }


        public virtual IEnumerable<T> Save(IEnumerable<T> models)
        {
            models.Foreach((model) =>
            {
                return Save(model);
            });

            _context.SaveChanges();
            return models;
        }

        public virtual T Save<TViewModel>(TViewModel vm) where TViewModel : class, IKey
        {
            T model;
            if (vm.Id == 0)
            {
                model = vm.ConvertTo<T>();
                Data.Add(model);
            }
            else
            {
                model = Get(vm.Id);
                model.Inject(vm);
            }

            _context.SaveChanges();
            return model;
        }

        public virtual void Remove(int id)
        {
            var model = Get(id);
            Data.Remove(model);
            _context.SaveChanges();
        }

        public void Reload(T model)
        {
            _context.Entry(model).Reload();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}