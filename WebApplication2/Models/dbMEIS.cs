using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Policy;

namespace MEIS.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    /// <summary>
    /// Database Context class
    /// </summary>
    public class dbMEIS : DbContext
    {
        // Your context has been configured to use a 'dbMEIS' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'WebApplication2.Models.dbMEIS' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'dbMEIS' 
        // connection string in the application configuration file.
        public dbMEIS()
            : base("name=dbMEIS")
        {

        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
        public virtual DbSet<User> TbUser { get; set; } 
        public virtual DbSet<ParameterType> TbParameterTypes { get; set; } 
        public virtual DbSet<Parameter> TbParameters { get; set; } 
        public virtual DbSet<Form> TbForm { get; set; } 
        public virtual DbSet<Permission> TbPermissions { get; set; }

        public IQueryable<ViewModel.VPermissionDetail> VPermissionDetails(User user)
        {
            return ViewModel.GetPermissionDetails(user, this);
        } 
    }
    public interface ITable
    {
        [Key]
        decimal TableKey { get; set; }
        DateTime? CreatedDate { get; set; }
        decimal? CreatedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
        decimal? ModifiedBy { get; set; }
        string Notes { get; set; }
        decimal? StaffId { get; set; }
        bool? isDeleted { get; set; }
    }
    public class User : ITable
    {
        [Key]
        public decimal TableKey { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }

        public DateTime? CreatedDate { get; set; }
        public decimal? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public decimal? ModifiedBy { get; set; }
        public string Notes { get; set; }
        public decimal? StaffId { get; set; }
        public bool? isDeleted { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; } 
        public bool KeepMeLogIn { get; set; }
    }
    public class ParameterType : ITable
    {
        [Key]
        public decimal TableKey { get; set; }

        public string ParameterTypeName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public decimal? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public decimal? ModifiedBy { get; set; }
        public string Notes { get; set; }
        public decimal? StaffId { get; set; }
        public bool? isDeleted { get; set; }

        public virtual ICollection<Parameter> Parameters { get; set; } 
    }

    public class Parameter : ITable
    {
        [Key]
        public decimal TableKey { get; set; }

        public string ParameterName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public decimal? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public decimal? ModifiedBy { get; set; }
        public string Notes { get; set; }
        public decimal? StaffId { get; set; }
        public bool? isDeleted { get; set; }
        public ParameterType ParameterType { get; set; }
    }
    public class Form : ITable
    {
        [Key]
        public decimal TableKey { get; set; }

        public string FormName { get; set; }
        public decimal? ModuleId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public decimal? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public decimal? ModifiedBy { get; set; }
        public string Notes { get; set; }
        public decimal? StaffId { get; set; }
        public bool? isDeleted { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; } 
    }

    public class Permission : ITable
    {
        [Key]
        public decimal TableKey { get; set; }
        public Form Form { get; set; }
        public User User { get; set; }
        public bool? CanAdd { get; set; }
        public bool? CanView { get; set; }
        public bool? CanDelete { get; set; }
        public bool? CanEdit { get; set; }
        
        public DateTime? CreatedDate { get; set; }
        public decimal? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public decimal? ModifiedBy { get; set; }
        public string Notes { get; set; }
        public decimal? StaffId { get; set; }
        public bool? isDeleted { get; set; }
    }

}