using Microsoft.AspNetCore.Mvc.Filters;

namespace loggifyio
{
    public interface IActionTransactionHelper
    {
        void BeginTransaction();
        void EndTransaction(ActionExecutedContext filterContext);
        void CloseSession();
    }
}