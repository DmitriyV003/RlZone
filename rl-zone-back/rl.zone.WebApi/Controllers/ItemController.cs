using System;
using Microsoft.AspNetCore.Mvc;

namespace rl.zone.WebApi.Controllers
{
    [ApiController]
    [Route("/api/item")]
    public class ItemController
    {
        /// <summary>
        /// Возвращает предмет по id
        /// </summary>
        /// <returns>Предмет</returns>
        /// <exception cref="Exception"></exception>
        public IActionResult GetById()
        {
            throw new Exception();
        }

        /// <summary>
        /// Возвращает все предметы
        /// </summary>
        /// <returns>Все предметы</returns>
        /// <exception cref="Exception"></exception>
        public IActionResult GetAll()
        {
            throw new Exception();
        }

        /// <summary>
        /// Создает новый предмет
        /// </summary>
        /// <returns>Возвращает созданный предмет</returns>
        /// <exception cref="Exception"></exception>
        public IActionResult Create()
        {
            throw new Exception();
        }

        /// <summary>
        /// Обновляет предмет по id
        /// </summary>
        /// <returns>Обновленный предмет</returns>
        /// <exception cref="Exception"></exception>
        public IActionResult Update()
        {
            throw new Exception();
        }
    }
}