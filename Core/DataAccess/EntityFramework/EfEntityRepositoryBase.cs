using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity,TContext> : IEntityRepository<TEntity>
        where TEntity: class, IEntity, new()
        where TContext: DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (TContext context = new TContext()) //using yazıp tab tab
                                                                      //using bittiği anda garbage collectore gider ve beni temizle der
                                                                      //NorthwindContext'i burada new leme nedenimiz de bu
                                                                      //NorthwindContext hemen tamizlensin ki performans artsın
            {
                var addedEntity = context.Entry(entity); //entity'i data base'deki referans ile ilişkilendirdik
                addedEntity.State = EntityState.Added; // o eklenecek bir nesne diye belirttik
                context.SaveChanges(); //ekle
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext()) //using yazıp tab tab
                                                                      //using bittiği anda garbage collectore gider ve beni temizle der
                                                                      //NorthwindContext'i burada new leme nedenimiz de bu
                                                                      //NorthwindContext hemen tamizlensin ki performans artsın
            {
                var deletedEntity = context.Entry(entity); //entity'i data base'deki referans ile ilişkilendirdik
                deletedEntity.State = EntityState.Deleted; // o silinecek bir nesne diye belirttik
                context.SaveChanges(); //Sil
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter); //context producta bağlan ve ona singleordefault ile filter uygula

            }
        }


        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null ? context.Set<TEntity>().ToList() //filter null ise context içinden product al ve hepsini liste halinde getir
                    : context.Set<TEntity>().Where(filter).ToList(); //eğer filter null değilse where ile filtre uygulayıp getir
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext()) //using yazıp tab tab
                                                                      //using bittiği anda garbage collectore gider ve beni temizle der
                                                                      //NorthwindContext'i burada new leme nedenimiz de bu
                                                                      //NorthwindContext hemen tamizlensin ki performans artsın
            {
                var updatedEntity = context.Entry(entity); //entity'i data base'deki referans ile ilişkilendirdik
                updatedEntity.State = EntityState.Modified; // o güncellenecek bir nesne diye belirttik
                context.SaveChanges(); //güncelle
            }
        }
    }
}
