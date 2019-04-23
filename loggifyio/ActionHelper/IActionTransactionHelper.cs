using Microsoft.AspNetCore.Mvc.Filters;

namespace Loggifyio.ActionHelper
{
    public interface IActionTransactionHelper
    {
        void BeginTransaction();
        void EndTransaction(ActionExecutedContext filterContext);
        void CloseSession();
    }
}