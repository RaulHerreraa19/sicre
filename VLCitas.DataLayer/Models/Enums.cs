using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VLCitas.DataLayer.Models
{
    public enum UserRoles
    {
        None= 0,
        MoreThanOneRol = 1,
        Administrador = 2013,
        Root = 2015,
        Doctor = 3019,
        Assistant = 3020
    }

    public enum TypeOfResponse
    {
        OK = 0,
        ErrorResponse = 1,
        Other = 2
    }

}