using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEIS.Models;
using MEIS.Patterns.Adapter;

namespace MEIS.Patterns
{
    public class UserAdapter : IAdapter
    {
        private User UserAdaptee;

        public UserAdapter(User userAdaptee)
        {
            UserAdaptee = userAdaptee;
        }

        public decimal? UserId => UserAdaptee.TableKey;
        public string UserName => UserAdaptee.UserName;
        public string Password { get; set; }
    }
}