using System;
using Microsoft.AspNetCore.Mvc;

namespace rl.zone.WebApi.Controllers
{
    [ApiController]
    [Route("/api/case-type")]
    public class CaseTypeController
    {
        /// <summary>
        /// Создает новую категорию для кейсов
        /// </summary>
        /// <returns>Новую категорию</returns>
        /// <exception cref="Exception"></exception>
        public IActionResult Create()
        {
            throw new Exception();
        }

        /// <summary>
        /// Возращает все категории кейсов
        /// </summary>
        /// <returns>Все категории кейсов</returns>
        /// <exception cref="Exception"></exception>
        public IActionResult GetAll()
        {
            throw new Exception();
        }

        /// <summary>
        /// Обновляет категорию кейсрв по id
        /// </summary>
        /// <returns>Обновленную категорию</returns>
        /// <exception cref="Exception"></exception>
        public IActionResult Update()
        {
            throw new Exception();
        }
    }
}