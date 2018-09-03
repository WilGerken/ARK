using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Domain;
using ArkWeb.Common;
using ArkWeb.Models;

namespace ArkWeb.Controllers
{
    public class StoryController : Csla.Web.Mvc.MyController
    {
        /// <summary>
        /// default action
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        #region ArkStory

        [HttpGet]
        public ActionResult ArkStory_List()
        {
            // create the viewModel
            var vModel = new ArkStory_EditList_ViewModel (Request.Query);

            // refresh the viewModel (meta only)
            vModel.Refresh(); // (Request.Query.Count > 0);

            // update breadcrumb
            // TODO: Session["NodeUrl"] = vModel.BuildNodeUrl("Agencies", "ArkStory_List", "Agency List");

            // return view to client
            return View ("ArkStory_List", vModel);
        }

        [HttpPost]
        public ActionResult ArkStory_List (ArkStory_EditList_ViewModel model)
        {
            // refresh the viewModel
            model.Refresh();

            // update breadcrumb
            // TODO: Session["NodeUrl"] = model.BuildNodeUrl("Agencies", "ArkStory_List", "Agency List");

            // return view to client
            return View ("ArkStory_List", model);
        }

        [HttpGet]
        public ActionResult ArkStory_Info (int id)
        {
            // establish the view model with given criteria
            var vCriteria = new ArkStory_ItemCriteria { ObjectID = id };
            var vModel    = new ArkStory_EditItem_ViewModel (vCriteria);

            // refresh the viewModel
            vModel.Refresh();

            // return view to client
            return View ("ArkStory_Info", vModel);
        }

        [HttpGet]
        public ActionResult ArkStory_New ()
        {
            // establish the view model with given criteria
            var vCriteria = new ArkStory_ItemCriteria ();
            var vModel    = new ArkStory_EditItem_ViewModel (vCriteria);

            // refresh the viewModel
            vModel.Refresh();

            // return view to client
            return View ("ArkStory_Edit", vModel);
        }

        [HttpGet]
        public ActionResult ArkStory_Edit (int id)
        {
            // establish the view model with given criteria
            var vCriteria = new ArkStory_ItemCriteria { ObjectID = id };
            var vModel    = new ArkStory_EditItem_ViewModel (vCriteria);

            // refresh the viewModel
            vModel.Refresh();

            // return view to client
            return View ("ArkStory_Edit", vModel);
        }

        [HttpPost]
        public ActionResult ArkStory_Save (ArkStory_EditItem_ViewModel model)
        {
            if (model.ModelObject.BrokenRulesCollection.Count > 0)
            {
                // update validation summary
                foreach (var item in model.ModelObject.BrokenRulesCollection)
                {
                    ModelState.AddModelError(item.Property, item.Description);
                }
            }
            else if (model.Save())
            {
                // refresh the viewModel
                model.Refresh();

                return View ("ArkStory_Info", model);
            }

            // refresh the viewModel (meta only)
            model.Refresh (false);

            // return view to client
            return View ("ArkStory_Edit", model);
        }

        #endregion
    }
}