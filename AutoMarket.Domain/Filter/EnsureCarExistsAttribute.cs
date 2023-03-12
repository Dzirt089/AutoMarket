namespace AutoMarket.Domain.Filter
{
    #region Пока не удачно. Использование внедрения зависимостей с атрибутами фильтра.

    //public class EnsureCarExistsAttribute : TypeFilterAttribute
    //{
    //    public EnsureCarExistsAttribute() : base(typeof(EnsureCarExistsFilter)) { }

    //    public class EnsureCarExistsFilter : IAsyncActionFilter
    //    {
    //        private readonly ICarService _service;
    //        public EnsureCarExistsFilter(ICarService service)
    //        {
    //            _service = service;
    //        }

    //        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    //        {
    //            var carId = (int)context.ActionArguments["id"];
    //            if (!await _service.GetCar(carId))
    //            {
    //                context.Result = new NotFoundResult();
    //            }
    //            else
    //            {
    //                await next();
    //            }
    //        }
    //    }
    //}

    #endregion
}
