using Microsoft.AspNetCore.Mvc;
using System;

namespace laboratorna5shchedrov.Controllers
{
    public class CookieController : Controller
    {
        [HttpGet]
        public IActionResult SetCookie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SetCookie(string value, DateTime expiryDate)
        {
            if (string.IsNullOrEmpty(value))
            {
                ModelState.AddModelError("", "value cant be empty");
                return View();
            }

            var options = new Microsoft.AspNetCore.Http.CookieOptions
            {
                Expires = expiryDate
            };

            Response.Cookies.Append("MyCookie", value, options);

            Response.Cookies.Append("MyCookieExpiry", expiryDate.ToString(), options);

            return RedirectToAction("CheckCookie");
        }

        [HttpGet]
        public IActionResult CheckCookie()
        {
            var cookieValue = Request.Cookies["MyCookie"];
            var cookieExpiry = Request.Cookies["MyCookieExpiry"]; 

            if (string.IsNullOrEmpty(cookieValue))
            {
                ViewBag.Message = "Cookie wasn't found";
            }
            else
            {
                if (string.IsNullOrEmpty(cookieExpiry))
                {
                    ViewBag.Message = $"Value of cookie: {cookieValue}. Expiry date is not set.";
                }
                else
                {
                    ViewBag.Message = $"Value of cookie: {cookieValue}. Expiry date: {cookieExpiry}";
                }
            }

            return View();
        }

        [HttpGet("cause-exception")]
        public IActionResult CauseException()
        {
            throw new Exception("test exception!!");
        }

    }

}
