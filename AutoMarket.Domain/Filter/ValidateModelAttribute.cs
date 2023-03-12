using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AutoMarket.Domain.Filter
{
    /// <summary>
    /// Фильтр действий для валидации ModelStateя наследую от абстрактного класса ActionFilterAttribute. 
    /// Он реализует интерфейсы IActionFilter и IResultFilter, а также их асинхронные аналоги, 
    /// поэтому вы можете переопределить нужные вам 
    /// методы по мере необходимости.Это позволяет избежать необходимости добавлять неиспользуемый метод OnActionExecuted(), но
    /// использование базового класса совершенно необязательно. Это вопрос предпочтения
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Переопределяет метод Executing для запуска фильтра до выполнения действия
        /// Если модель невалидна, задаем свойство Result; 
        /// это приводит к прерыванию выполнения действия
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }

    }
}
