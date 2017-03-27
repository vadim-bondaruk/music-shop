using Shop.DAL.Infrastruture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Routing;

namespace PVT.Q1._2017.Shop.Areas.User.Controllers
{
    //[RoutePrefix("api/validation")]
    public class ValidationController : ApiController
    {
        private readonly IUserRepository _userRepository;

        //public ValidationController()
        //{

        //}
        public ValidationController(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public ValidationController():base()
        {

        }

        //[Route("emailunique")]
        [HttpGet]
        public string GetEmail(string id)
        {
            bool validationResult = true;

            if (!String.IsNullOrEmpty(id))
            {
                var user = _userRepository?.GetAll(u => u.Login == id).FirstOrDefault();

                validationResult = (user == null);
            }

            return (validationResult.ToString());
        }

        //[Route("uniquelogin")]
        //[HttpGet]
        //public IHttpActionResult ValidateUniquelogin(string login)
        //{
        //    bool validationResult = true;

        //    if (!String.IsNullOrEmpty(login))
        //    {
        //        var user = _userRepository?.GetAll(u => u.Login == login).FirstOrDefault();

        //        validationResult = (user == null);
        //    }

        //    return Ok(validationResult);
        //}

    }
}

