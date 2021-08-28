using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    public interface IEntityRepository <T> where T:class,IEntity,new() // Generic yapı sayesimde biz hem Iproductdal daki
        //hem de IcategoryDal a yazacağımız kodları aynı kodlar olduğu için burada birleştirdik
        //buradaki T biz ne gönderirsek o olur
        //T herşey olmasın diye sınırlandırma getirmek gerekiyor
        //where T:class yani T sadece bir referans tip olarailir
        //T bir IEntity veya IEntity implemente eden bir nesne olabilir
        //T bir new'lenebilir bir nesne olmalı. Bu sayede IEntity alamadı.
    {
        List<T> GetAll(Expression<Func<T,bool>> filter=null); // Bütün ürünleri getirmek için ayrı ayrı kod yazmak yerine
        //mesela ıd=2 olan ürünler için ayrı kod veya fiyatı>5000 olan için ayrı kod gibi
        //bu expression u yazdık bu bize ne istersek onu vercek
        //filter=null boş da gelebilir demek
        T Get(Expression<Func<T, bool>> filter); // tek bir ürün listelemek için
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        // bu koda ihtiyacımız kalmadı çünkü expression yazdık. List<T> GetAllByCategory(int categoryId); //Ürünleri category'e göre filtrele
    }
}
