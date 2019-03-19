using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace EFCore_Many2ManyHelperTest.Extensions
{
    public static class DbContextExtension
    {
        public static bool AddManyToManyLink<T1, T2>(this DbContext context, T1 entity1, T2 entity2) where T1 : class where T2 : class
        {
            string entity1Name = entity1.GetType().FullName;
            string entity2Name = entity2.GetType().FullName;

            var entity1Type = context.Model.FindRuntimeEntityType(entity1.GetType());
            var entity2Type = context.Model.FindRuntimeEntityType(entity2.GetType());

            var entity1Entry = context.Entry(entity1);
            foreach (var collection in entity1Entry.Collections)
            {
                object firstItem = null;
                foreach(var item in collection.CurrentValue)
                {
                    firstItem = item;
                    break;
                }

                if (firstItem is null)
                {
                    //if many-to-many link collection is empty
                    //need to do smth with this
                    return false;
                }
                
                var linkEntityType = context.Model.FindRuntimeEntityType(firstItem.GetType());
                var fks = linkEntityType.GetForeignKeys().ToList();

                var entity1FK = fks.FirstOrDefault(key => key.PrincipalEntityType.Name == entity1Name);
                var entity2FK = fks.FirstOrDefault(key => key.PrincipalEntityType.Name == entity2Name);

                if (entity1FK is null || entity2FK is null)
                    continue;
                
                Type linkEntityClrType = linkEntityType.ClrType;
                var newLink = Activator.CreateInstance(linkEntityClrType);

                var fk1Setter = linkEntityClrType.GetProperty(entity1FK.Properties.First().Name);
                var fk2Setter = linkEntityClrType.GetProperty(entity2FK.Properties.First().Name);

                var entity1PkName = entity1Type.FindPrimaryKey().Properties.First().Name;
                var entity2PkName = entity2Type.FindPrimaryKey().Properties.First().Name;

                var entity1PkProp = entity1.GetType().GetProperty(entity1PkName);
                var entity2PkProp = entity2.GetType().GetProperty(entity2PkName);

                fk1Setter.SetValue(newLink, entity1PkProp.GetValue(entity1));
                fk2Setter.SetValue(newLink, entity2PkProp.GetValue(entity2));

                context.Add(newLink);
                context.SaveChanges();

                return true;
            }
            
            return false;
        }
    }
}
