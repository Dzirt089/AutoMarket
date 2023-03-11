using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMarket.Domain.Filter
{
    public class FeatureEnabledAttribute : Attribute, IResourceFilter
    {
        /// <summary>
        /// Переключатель функции, который можно использовать, чтобы отключить доступность API.
        /// На данный момент значения вшиты жестко. Далее, их импортировать из БД или конфига вне проекта.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Если функция IsEnabled не активирована, то прерываем выполнение конвеера, задав свойство context.Result
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
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            throw new NotImplementedException();
        }
    }



}
