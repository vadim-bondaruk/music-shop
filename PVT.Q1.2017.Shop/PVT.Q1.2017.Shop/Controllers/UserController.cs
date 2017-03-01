namespace PVT.Q1._2017.Shop.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using global::Shop.BLL.DTO;
    using global::Shop.BLL.Services;
    using global::Shop.Common.Models;
    using ViewModels;

    /// <summary>
    /// 
    /// </summary>
    public class UserController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private UserService _userService;

        /// <summary>
        /// 
        /// </summary>
        public UserController()
        {
            this._userService = new UserService(null);           
        }

        /// <summary>
        /// GET: User
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return this.View();
        }

        /// <summary>
        /// GET: User/Details/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            return this.View();
        }

        /// <summary>
        /// GET: User/Create
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return this.View();
        }

        /// <summary>
        /// POST: User/Create
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(UserViewModel user)
        {            
                /// TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    bool result = this._userService.RegisterUser(new UserDTO()
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Login = user.Login,
                        Password = user.Password,
                        Email = user.Email,
                        BirthDate = user.BirthDate,
                        Country = user.Country,
                        PhoneNumber = user.PhoneNumber,
                        Sex = user.Sex
                    });
                    if (result)
                    {
                        return this.RedirectToAction("Success");
                    }
                }

            return this.View(user);
        }

        /// <summary>
        /// GET: User/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            return this.View();
        }

        /// <summary>
        /// POST: User/Edit/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                /// TODO: Add update logic here

                return this.RedirectToAction("Index");
            }
            catch
            {
                return this.View();
            }
        }

        /// <summary>
        /// GET: User/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            return this.View();
        }

        /// <summary>
        /// POST: User/Delete/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                /// TODO: Add delete logic here

                return this.RedirectToAction("Index");
            }
            catch
            {
                return this.View();
            }
        }
    }
}
