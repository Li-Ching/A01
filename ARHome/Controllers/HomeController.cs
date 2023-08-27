using ARHome.Models;
using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.AspNetCore.Http; // 引入用於會話的命名空間

namespace ARHome.Controllers
{
    public class HomeController : Controller
    {
        FirebaseAuthProvider auth;
        public HomeController()
        {
            auth = new FirebaseAuthProvider(
                            new FirebaseConfig("AIzaSyCuJICZfjPRYfSLZ4LRUqGris0T3klukKU"));
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(LoginModel loginModel)
        {
            try
            {
                //create the user
                await auth.CreateUserWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password, loginModel.displayName);
                //log in the new user
                var fbAuthLink = await auth
                                .SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
                string token = fbAuthLink.FirebaseToken;
                //saving the token in a session variable
                if (token != null)
                {
                    HttpContext.Session.SetString("_UserToken", token);

                    return RedirectToAction("Index");
                }
            }
            catch (FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(ex.ResponseData);
                ModelState.AddModelError(String.Empty, firebaseEx.error.message);
                return View(loginModel);
            }

            return View();

        }
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(LoginModel loginModel)
        {
            try
            {
                // log in an existing user
                var fbAuthLink = await auth
                                .SignInWithEmailAndPasswordAsync(loginModel.Email, loginModel.Password);
                string token = fbAuthLink.FirebaseToken;
                // save the token to a session variable
                if (token != null)
                {
                    HttpContext.Session.SetString("_UserToken", token);

                    return RedirectToAction("Index");
                }
            }
            catch (FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(ex.ResponseData);
                ModelState.AddModelError(String.Empty, firebaseEx.error.message);
                return View(loginModel);
            }

            return View();
        }


        public IActionResult LogOut()
        {
            HttpContext.Session.Remove("_UserToken");
            return RedirectToAction("SignIn");
        }

        public async Task<IActionResult> Index()
        {
            
                var token = HttpContext.Session.GetString("_UserToken");

                if (token != null)
                {
                try
                {
                    // 在此处从 Firebase 或其他身份验证系统中获取用户的 displayName 和 email
                    // 你可能需要使用 Firebase SDK 或其他适当的方式来获取这些信息
                    // 以下是一种示例方法，假设你可以从 Firebase 中获取用户信息
                    var user = await auth.GetUserAsync(token); // 使用 Firebase SDK 获取用户信息

                    ViewBag.DisplayName = user.DisplayName;
                    ViewBag.Email = user.Email;
                }
                catch (Exception ex)
                {
                    // 处理获取用户信息时可能发生的异常
                    // 你可以选择如何处理这些异常，例如，如果未能获取用户信息，你可以设置为 null 或其他默认值
                    ViewBag.DisplayName = "N/A";
                    ViewBag.Email = "N/A";
                }
                return View();
                }
                else
                {
                    return RedirectToAction("SignIn");
                }

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}