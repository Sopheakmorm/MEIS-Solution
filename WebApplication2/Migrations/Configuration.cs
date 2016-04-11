using MEIS.Models;
using MEIS.Patterns;

namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MEIS.Models.dbMEIS>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(dbMEIS context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.TbUser.AddOrUpdate(u => u.TableKey,
                new User
                {
                    CreatedDate = DateTime.Now,
                    Notes = "Testing User",
                    TableKey = 201604110001,
                    UserName = "Admin",
                    Password = "123".GetUserPassword()
                });

            context.TbParameterTypes.AddOrUpdate(t => t.TableKey,
                new ParameterType
                {
                    Notes = "Testing",
                    TableKey = 201604110001,
                    CreatedDate = DateTime.Now,
                    ParameterTypeName = "Module"
                });

            context.TbParameters.AddOrUpdate(p => p.TableKey,
                new Parameter
                {
                    TableKey = 201604110001,
                    CreatedDate = DateTime.Now,
                    Notes = "Testing",
                    ParameterName = "Developer",
                    ParameterType = context.TbParameterTypes.FirstOrDefault(x => x.TableKey == 201604110001)
                });
            context.TbForm.AddOrUpdate(x => x.TableKey,
                new Form
                {
                    TableKey = 201604110001,
                    Notes = "Testing",
                    CreatedDate = DateTime.Now,
                    FormName = "User Authentication",
                    ModuleId = 201604110001
                });
            context.TbForm.AddOrUpdate(x => x.TableKey,
                new Form
                {
                    TableKey = 201604110002,
                    Notes = "Testing",
                    CreatedDate = DateTime.Now,
                    FormName = "Parameter",
                    ModuleId = 201604110001
                });

            context.TbPermissions.AddOrUpdate(x => x.TableKey,
                new Permission
                {
                    Notes = "Testing",
                    CreatedDate = DateTime.Now,
                    TableKey = 201604110001,
                    Form = context.TbForm.FirstOrDefault(x => x.TableKey == 201604110001),
                    User = context.TbUser.FirstOrDefault(x => x.TableKey == 201604110001),
                    CanAdd = true,
                    CanDelete = true,
                    CanEdit = true,
                    CanView = true
                });
        }
    }
}