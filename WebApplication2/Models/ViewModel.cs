using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.WebSockets;
using MEIS.Models;

namespace MEIS.Models
{
    public interface IView
    {
        [Key]
        decimal ViewKey { get; set; }

        DateTime? CreatedDate { get; set; }
        decimal? CreatedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
        decimal? ModifiedBy { get; set; }
        string Notes { get; set; }
        decimal? StaffId { get; set; }
    }

    public class ViewModel
    {
        public static IQueryable<VPermissionDetail> GetPermissionDetails(User user, dbMEIS context)
        {
            var forms = context.TbForm.ToList();
            var listPermission = new List<VPermissionDetail>();
            foreach (var form in forms)
            {
                var per =
                    context.TbPermissions.FirstOrDefault(
                        x => x.Form.TableKey == form.TableKey && x.User.TableKey == user.TableKey) ?? new Permission();
                var permission = new VPermissionDetail
                {
                    CreatedDate = form.CreatedDate,
                    Notes = form.Notes,
                    CreatedBy = form.CreatedBy,
                    FormId = form.TableKey,
                    FormName = form.FormName,
                    ModifiedBy = form.ModifiedBy,
                    ModifiedDate = form.ModifiedDate,
                    UserId = user.TableKey,
                    UserName = user.UserName,
                    CanEdit = per.CanEdit,
                    CanDelete = per.CanDelete,
                    CanView = per.CanView,
                    CanAdd = per.CanAdd

                };
                listPermission.Add(permission);
            }
            return listPermission.AsQueryable();
        }

        public class VPermissionDetail : IView
        {
            public decimal ViewKey { get; set; }

            public decimal? UserId { get; set; }
            public string UserName { get; set; }
            public decimal? FormId { get; set; }
            public string FormName { get; set; }
            public bool? CanAdd { get; set; }
            public bool? CanDelete { get; set; }
            public bool? CanView { get; set; }
            public bool? CanEdit { get; set; }
            public DateTime? CreatedDate { get; set; }
            public decimal? CreatedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }
            public decimal? ModifiedBy { get; set; }
            public string Notes { get; set; }
            public decimal? StaffId { get; set; }
        }
    }
}