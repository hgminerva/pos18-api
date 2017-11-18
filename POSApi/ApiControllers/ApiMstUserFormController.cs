using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace POSApi.ApiControllers
{
    public class ApiMstUserFormController : ApiController
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

        public Int32 FormId()
        {
            var formId = from d in db.SysForms select d.Id;
            return formId.FirstOrDefault();
        }

        //*************
        //LIST USER FORM
        //*************
        [HttpGet, Route("list")]
        public List<Entities.MstUserForm> listUserForm()
        {
            var userForm = from d in db.MstUserForms
                       select new Entities.MstUserForm
                       {
                           Id = d.Id,
                           FormId = d.FormId,
                           UserId = d.UserId,
                           CanDelete = d.CanDelete,
                           CanAdd = d.CanAdd,
                           CanLock = d.CanLock,
                           CanUnlock = d.CanUnlock,
                           CanPrint = d.CanPrint,
                           CanPreview = d.CanPreview,
                           CanEdit = d.CanEdit,
                           CanTender = d.CanTender,
                           CanDiscount = d.CanDiscount,
                           CanView = d.CanView,
                           CanSplit = d.CanSplit,
                           CanCancel = d.CanCancel,
                           CanReturn = d.CanReturn
                       };
            return userForm.ToList();
        }

        //************
        //ADD USER FORM
        //************
        [HttpPost, Route("post")]
        public Int32 postUserForm()
        {
            try
            {
                Data.MstUserForm newUserForm = new Data.MstUserForm();
                newUserForm.FormId = FormId();
                newUserForm.UserId = UserId();
                newUserForm.CanDelete = false;
                newUserForm.CanAdd = false;
                newUserForm.CanLock = false;
                newUserForm.CanUnlock = false;
                newUserForm.CanPrint = false;
                newUserForm.CanPreview = false;
                newUserForm.CanEdit = false;
                newUserForm.CanTender = false;
                newUserForm.CanDiscount = false;
                newUserForm.CanView = false;
                newUserForm.CanSplit = false;
                newUserForm.CanCancel = false;
                newUserForm.CanReturn = false;
                db.MstUserForms.InsertOnSubmit(newUserForm);
                db.SubmitChanges();

                return newUserForm.Id;

            }
            catch (Exception e)
            {
                return 0;
            }
        }

        //*************
        //EDIT USER FORM
        //*************
        [HttpPut, Route("put/{id}")]
        public HttpResponseMessage putUserForm(String id, Entities.MstUserForm userForm)
        {
            try
            {
                var userForms = from d in db.MstUserForms where d.Id == Convert.ToInt32(id) select d;
                if (userForms.Any())
                {
                    var updateUserForms = userForms.FirstOrDefault();
                    updateUserForms.FormId = userForm.FormId;
                    updateUserForms.UserId = userForm.UserId;
                    updateUserForms.CanDelete = userForm.CanDelete;
                    updateUserForms.CanAdd = userForm.CanAdd;
                    updateUserForms.CanLock = userForm.CanLock;
                    updateUserForms.CanUnlock = userForm.CanUnlock;
                    updateUserForms.CanPrint = userForm.CanPrint;
                    updateUserForms.CanPreview = userForm.CanPreview;
                    updateUserForms.CanEdit = userForm.CanEdit;
                    updateUserForms.CanTender = userForm.CanTender;
                    updateUserForms.CanDiscount = userForm.CanDiscount;
                    updateUserForms.CanView = userForm.CanView;
                    updateUserForms.CanSplit = userForm.CanSplit;
                    updateUserForms.CanCancel = userForm.CanCancel;
                    updateUserForms.CanReturn = userForm.CanReturn;
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
        //DELETE USER FORM
        //***************
        [HttpDelete, Route("delete/{id}")]
        public HttpResponseMessage deleteUserForm(String id)
        {
            try
            {
                var delete = from d in db.MstUserForms where d.Id == Convert.ToInt32(id) select d;
                if (delete.Any())
                {
                    db.MstUserForms.DeleteOnSubmit(delete.First());
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
