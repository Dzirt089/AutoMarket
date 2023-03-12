using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace AutoMarket.Domain.Filter
{
    /// <summary>
    /// В этом примере, если данная функция IsEnabled
    ///не активирована, вы прервете выполнение оставшегося конвейера,
    ///вернув BadRequestResult, который вернет клиенту ошибку 400
    /// </summary>
    public class FeatureEnabledAttribute : Attribute, IResourceFilter
    {
        /// <summary>
        /// Переключатель функции, который можно использовать, чтобы отключить доступность API.
        /// На данный момент значения вшиты жестко. Далее, их импортировать из БД или конфига вне проекта.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Если функция IsEnabled не активирована, то прерываем выполнение конвеера, задав свойство context.Result
        /// *Executing, который  вызывается перед привязкой модели
        /// </summary>
        /// <param name="context"></param>
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            if (!IsEnabled)
            {
                context.Result = new BadRequestResult();
            }
        }


        /// <summary>
        /// Должен быть реализован для удовлетворения IResourceFilter, нов данном случае он не требуется
        /// и *Executed, который вызывается после выполнения результата.
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnResourceExecuted(ResourceExecutedContext context) { }
    }



}
