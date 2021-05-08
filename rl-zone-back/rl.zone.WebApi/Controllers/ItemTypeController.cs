using System;
using Microsoft.AspNetCore.Mvc;

namespace rl.zone.WebApi.Controllers
{
    [ApiController]
    [Route("/api/item-type")]
    public class ItemTypeController
    {
        /// <summary>
        /// Создает новую категорию преметов
        /// </summary>
        /// <returns>Созданную категорию предметов</returns>
        /// <exception cref="Exception"></exception>
        public IActionResult Create()
        {
            throw new Exception();
        }

        /// <summary>
        /// Возвращает все категории
        /// </summary>
        /// <returns>Возвращает все категории</returns>
        /// <exception cref="Exception"></exception>
        public IActionResult GetAll()
        {
            throw new Exception();
        }

        /// <summary>
        /// Обновляет название категории предметов по id
        /// </summary>
        /// <returns>Возвращает обновленную категорию</returns>
        /// <exception cref="Exception"></exception>
        public IActionResult Update()
        {
            throw new Exception();
        }
    }
}