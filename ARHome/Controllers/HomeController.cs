using ARHome.Models;
using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using Microsoft.AspNetCore.Http; // 引入用於會話的命名空間
using Microsoft.AspNetCore.Identity;
using Google.Apis.Util;
using FirebaseAdmin.Auth;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System;

namespace ARHome.Controllers
{

    public class HomeController : Controller
    {
        FirebaseAuthProvider auth;
        public HomeController()
        {
            auth = new FirebaseAuthProvider(
                            new FirebaseConfig("AIzaSyCuJICZfjPRYfSLZ4LRUqGris0T3klukKU"));

            if (FirebaseApp.DefaultInstance == null)
            {
                // 如果不存在，則初始化FirebaseAdmin
                var firebaseCredentialsPath = "ar-home-design-firebase-adminsdk-pcy6j-cadf5e2d4d.json"; // 替換為您的Service Account金鑰的路徑
                FirebaseApp.Create(new AppOptions
                {
                    Credential = GoogleCredential.FromFile(firebaseCredentialsPath)
                });
            }
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
            catch (Firebase.Auth.FirebaseAuthException ex)
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
            catch (Firebase.Auth.FirebaseAuthException ex)
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
                ViewBag.token=token;
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

        [HttpPost]
        public async Task<IActionResult> ChangePassword(string newPassword)
        {
            try
            {
                var token = HttpContext.Session.GetString("_UserToken");
                if (token != null)
                {
                    // 使用 Firebase SDK 方法獲得目前登入使用者的唯一識別碼
                    var user = await auth.GetUserAsync(token);

                    var decodedToken = FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance
                        .VerifyIdTokenAsync(token).Result;

                    // 從解碼的令牌中獲取用戶ID
                    var userId = decodedToken.Uid;

                    await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance.UpdateUserAsync(new UserRecordArgs
                    {
                        Uid = userId,
                        Password = newPassword
                    });

                    HttpContext.Session.Remove("_UserToken");

                    // 將需要傳遞的數據包裝成 JSON 返回
                    return Json(new { success = true, message = "密碼變更成功" });
                }
                else
                {
                    // 如果沒有登入的 token，返回一個 JSON 錯誤響應
                    return Json(new { success = false, message = "密碼變更失敗" });
                }
            }
            catch (Firebase.Auth.FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(ex.ResponseData);
                ModelState.AddModelError(String.Empty, firebaseEx.error.message);
                // 返回變更密碼的表單視圖，並顯示錯誤訊息
                return Json(new { success = false, message = "密碼變更失敗" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangeDisplayName(string newDN)
        {
            try
            {
                var token = HttpContext.Session.GetString("_UserToken");
                if (token != null)
                {
                    // 使用 Firebase SDK 方法獲得目前登入使用者的唯一識別碼
                    var user = await auth.GetUserAsync(token);

                    var decodedToken = FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance
                        .VerifyIdTokenAsync(token).Result;

                    // 從解碼的令牌中獲取用戶ID
                    var userId = decodedToken.Uid;

                    await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance.UpdateUserAsync(new UserRecordArgs
                    {
                        Uid = userId,
                        Email = user.Email,
                        DisplayName = newDN
                    });

                    // 將需要傳遞的數據包裝成 JSON 返回
                    return Json(new { success = true, message = "DisplayName變更成功" });
                }
                else
                {
                    // 如果沒有登入的 token，返回一個 JSON 錯誤響應
                    return Json(new { success = false, message = "DisplayName變更失敗" });
                }
            }
            catch (Firebase.Auth.FirebaseAuthException ex)
            {
                var firebaseEx = JsonConvert.DeserializeObject<FirebaseError>(ex.ResponseData);
                ModelState.AddModelError(String.Empty, firebaseEx.error.message);
                // 返回變更密碼的表單視圖，並顯示錯誤訊息
                return Json(new { success = false, message = "DisplayName變更失敗" });
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