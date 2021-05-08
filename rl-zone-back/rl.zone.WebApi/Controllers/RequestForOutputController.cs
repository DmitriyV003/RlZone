using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace rl.zone.WebApi.Controllers
{
    [ApiController]
    [Route("/api/request/")]
    public class RequestForOutputController
    {
        public IActionResult Create()
        {
            throw new Exception();
        }

        public IActionResult Deny()
        {
            throw new Exception();
        }

        public IActionResult Confirm()
        {
            throw new Exception();
        }

        public IActionResult GetAll()
        {
            throw new Exception();
        }

        public IActionResult GetById()
        {
            throw new Exception();
        }
    }
}