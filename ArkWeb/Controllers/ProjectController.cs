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
    public class ProjectController : Csla.Web.Mvc.MyController
    {
        public IActionResult Index()
        {
            return View();

        }

        #region ArkProject

        [HttpGet]
        public ActionResult ArkProject_List()
        {
            // create the viewModel
            var vModel = new ArkProject_EditList_ViewModel(Request.Query);

            // refresh the viewModel (meta only)
            vModel.Refresh(); // (Request.Query.Count > 0);

            // update breadcrumb
            // TODO: Session["NodeUrl"] = vModel.BuildNodeUrl("Agencies", "ArkProject_List", "Agency List");

            // return view to client
            return View("ArkProject_List", vModel);
        }

        [HttpPost]
        public ActionResult ArkProject_List(ArkProject_EditList_ViewModel model)
        {
            // refresh the viewModel
            model.Refresh();

            // update breadcrumb
            // TODO: Session["NodeUrl"] = model.BuildNodeUrl("Agencies", "ArkProject_List", "Agency List");

            // return view to client
            return View("ArkProject_List", model);
        }

        [HttpGet]
        public ActionResult ArkProject_Info(int id)
        {
            // establish the view model with given criteria
            var vCriteria = new ArkProject_ItemCriteria { ObjectID = id };
            var vModel = new ArkProject_EditItem_ViewModel(vCriteria);

            // refresh the viewModel
            vModel.Refresh();

            // return view to client
            return View("ArkProject_Info", vModel);
        }

        [HttpGet]
        public ActionResult ArkProject_New()
        {
            // establish the view model with given criteria
            var vCriteria = new ArkProject_ItemCriteria();
            var vModel = new ArkProject_EditItem_ViewModel(vCriteria);

            // refresh the viewModel
            vModel.Refresh();

            // return view to client
            return View("ArkProject_Edit", vModel);
        }

        [HttpGet]
        public ActionResult ArkProject_Edit(int id)
        {
            // establish the view model with given criteria
            var vCriteria = new ArkProject_ItemCriteria { ObjectID = id };
            var vModel = new ArkProject_EditItem_ViewModel(vCriteria);

            // refresh the viewModel
            vModel.Refresh();

            // return view to client
            return View("ArkProject_Edit", vModel);
        }

        [HttpPost]
        public ActionResult ArkProject_Save(ArkProject_EditItem_ViewModel model)
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

                return View("ArkProject_Info", model);
            }

            // refresh the viewModel (meta only)
            model.Refresh(false);

            // return view to client
            return View("ArkProject_Edit", model);
        }

        #endregion

        public IActionResult Location_List()
        {
            ViewData["Message"] = "Your location list page.";

            return View();
        }
    }
}