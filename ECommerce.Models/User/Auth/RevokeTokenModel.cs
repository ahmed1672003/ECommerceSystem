using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Models.User.Auth;
public class RevokeTokenModel
{
    public bool IsRevoked { get; set; }
}
