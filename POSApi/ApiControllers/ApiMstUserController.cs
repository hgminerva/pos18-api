using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    public class ApiMstUserController : ApiController
    {
        //*************
        // DATA CONTEXT
        //*************
        private Data.posDBDataContext db = new Data.posDBDataContext();

        //*************
        //  METHOD ID
        //*************
        public Int32 UserId()
        {
            var userId = from d in db.MstUsers select d.Id;
            return userId.FirstOrDefault();
        }

        //*************
        //LIST USER 
        //*************
        [HttpGet, Route("list")]
        public List<Entities.MstUser> listUser()
        {
            var user = from d in db.MstUsers
                           select new Entities.MstUser
                           {
                               Id = d.Id,
                               UserName = d.UserName,
                               Password = d.Password,
                               FullName = d.FullName,
                               UserCardNumber = d.UserCardNumber,
                               EntryUserId = d.EntryUserId,
                               EntryDateTime = d.EntryDateTime,
                               UpdateUserId = d.UpdateUserId,
                               UpdateDateTime = d.UpdateDateTime,
                               IsLocked = d.IsLocked
                           };
            return user.ToList();
        }

        //************
        //ADD USER 
        //************
        [HttpPost, Route("post")]
        public Int32 postUser()
        {
            try
            {

                Data.MstUser newUser = new Data.MstUser();
                newUser.UserName = "n/a";
                newUser.Password = "n/a";
                newUser.FullName = "n/a";
                newUser.UserCardNumber = "n/a";
                newUser.EntryUserId = UserId();
                newUser.EntryDateTime = DateTime.Today;
                newUser.UpdateUserId = UserId();
                newUser.UpdateDateTime = DateTime.Today;
                newUser.IsLocked = false;
                db.MstUsers.InsertOnSubmit(newUser);
                db.SubmitChanges();

                return newUser.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //*************
        //EDIT USER 
        //*************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putUser(String id, Entities.MstUser user)
        {
            try
            {
                var users = from d in db.MstUsers where d.Id == Convert.ToInt32(id) select d;
                if (users.Any())
                {
                    var updateUsers = users.FirstOrDefault();
                    updateUsers.UserName = user.UserName;
                    updateUsers.Password = user.Password;
                    updateUsers.FullName = user.FullName;
                    updateUsers.UserCardNumber = user.UserCardNumber;
                    updateUsers.EntryUserId = UserId();
                    updateUsers.EntryDateTime = DateTime.Today;
                    updateUsers.UpdateUserId = UserId();
                    updateUsers.UpdateDateTime = DateTime.Today;
                    updateUsers.IsLocked = true;
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        //***************
        //DELETE USER
        //***************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteUser(String id)
        {
            try
            {
                var delete = from d in db.MstUsers where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstUsers.DeleteOnSubmit(delete.First());
                    db.SubmitChanges();

                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }
    }
}
