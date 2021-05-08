using System;
using Microsoft.AspNetCore.Mvc;

namespace rl.zone.WebApi.Controllers
{
    [ApiController]
    [Route("/api/craft/")]
    public class CraftController
    {
        /// <summary>
        /// Крафтит предмет с выбранной вероятностью успеха за определенную сумму
        /// </summary>
        /// <returns>Предмет, если крафт прошел успешно.</returns>
        /// <exception cref="Exception"></exception>
        public IActionResult craftItem()
        {
            throw new Exception();
        }

        /// <summary>
        /// Возвращает предметы, доступные для крафта
        /// </summary>
        /// <returns>Предметы, доступные для крафта</returns>
        /// <exception cref="Exception"></exception>
        public IActionResult getCraftItems()
        {
            throw new Exception();
        }
    }
}