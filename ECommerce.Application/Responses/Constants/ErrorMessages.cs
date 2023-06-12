using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Responses.Constants;
public class ErrorMessages
{
    public static List<string> SuccessErrorsList = null;
    public static List<string> NotFoundErrorsList = new() { "Not found error" };
    public static List<string> BadRequestErrorsList = new() { "Bad request error" };
    public static List<string> UnAuthorizedErrorsList = new() { "Un authorized error" };
    public static List<string> InternalServerErrorsList = new() { "Internal server error" };
}