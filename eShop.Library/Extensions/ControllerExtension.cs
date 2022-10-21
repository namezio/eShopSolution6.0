using Microsoft.AspNetCore.Mvc;

namespace eShop.Library.Extensions;

public class ControllerExtension
{
    // public static JsonResult GetJsonResult(this HomeController controller, string message = null, bool error = true, object data = null, int code = 0)
    // {
    //     return new JsonResult(new
    //     {
    //         Message = controller.Languages[message ?? "Error in processing"].ToString(),
    //         Code = code,
    //         Error = error,
    //         Data = data
    //     });
    // }
}