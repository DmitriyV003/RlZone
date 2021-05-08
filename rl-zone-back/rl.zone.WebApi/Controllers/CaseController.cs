using System;
using Microsoft.AspNetCore.Mvc;

namespace rl.zone.WebApi.Controllers
{
    [ApiController]
    [Route("/api/case")]
    public class CaseController
    {
        /// <summary>
        /// Возвращает кейс по id.
        /// </summary>
        /// <returns>Кейс</returns>
        /// <exception cref="Exception"></exception>
        public IActionResult GetById()
        {
            throw new Exception();
        }

        /// <summary>
        /// Возвращает все кейсы.
        /// </summary>
        /// <returns>Все кейсы</returns>
        /// <exception cref="Exception"></exception>
        public IActionResult GetAll()
        {
            throw new Exception();
        }

        /// <summary>
        /// Открывает кейс по id
        /// </summary>
        /// <returns>Возвращает выпавший предмет из кейса</returns>
        /// <exception cref="Exception"></exception>
        public IActionResult OpenCase()
        {
            throw new Exception();
        }

        /// <summary>
        /// Создает новый кейс.
        /// </summary>
        /// <returns>Новый кейс</returns>
        /// <exception cref="Exception"></exception>
        public IActionResult Create()
        {
            throw new Exception();
        }

        /// <summary>
        /// Удаляет(архивирует) существующий кейс по id.
        /// Удаленный кейс не удаляется из базы данных.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IActionResult Delete()
        {
            throw new Exception();
        }

        /// <summary>
        /// Обновляет кейс по id
        /// </summary>
        /// <returns>Обновленный кейс</returns>
        /// <exception cref="Exception"></exception>
        public IActionResult Update()
        {
            throw new Exception();
        }
    }
}