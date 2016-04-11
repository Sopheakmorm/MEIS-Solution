using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MEIS.Models;

namespace MEIS.Patterns.Adapter
{
    public interface IAdapter
    {
        decimal? UserId { get; }
        string UserName { get;  }
        string Password { get; }
    }
}