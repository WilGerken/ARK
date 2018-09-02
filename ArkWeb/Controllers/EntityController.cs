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
    public class EntityController : Csla.Web.Mvc.MyController
    {
        public IActionResult Index()
        {
            return View();
        }

        #region ArkEntity

        [HttpGet]
        public ActionResult ArkEntity_List()
        {
            // create the viewModel
            var vModel = new ArkEntity_EditList_ViewModel (Request.Query);

            // refresh the viewModel (meta only)
            vModel.Refresh(); // (Request.Query.Count > 0);

            // update breadcrumb
            // TODO: Session["NodeUrl"] = vModel.BuildNodeUrl("Agencies", "ArkEntity_List", "Agency List");

            // return view to client
            return View("ArkEntity_List", vModel);
        }

        [HttpPost]
        public ActionResult ArkEntity_List(ArkEntity_EditList_ViewModel model)
        {
            // refresh the viewModel
            model.Refresh();

            // update breadcrumb
            // TODO: Session["NodeUrl"] = model.BuildNodeUrl("Agencies", "ArkEntity_List", "Agency List");

            // return view to client
            return View("ArkEntity_List", model);
        }

        [HttpGet]
        public ActionResult ArkEntity_Info(int id)
        {
            // establish the view model with given criteria
            var vCriteria = new ArkEntity_ItemCriteria { ObjectID = id };
            var vModel    = new ArkEntity_EditItem_ViewModel(vCriteria);

            // refresh the viewModel
            vModel.Refresh();

            // return view to client
            return View("ArkEntity_Info", vModel);
        }

        [HttpGet]
        public ActionResult ArkEntity_New()
        {
            // establish the view model with given criteria
            var vCriteria = new ArkEntity_ItemCriteria();
            var vModel    = new ArkEntity_EditItem_ViewModel(vCriteria);

            // refresh the viewModel
            vModel.Refresh();

            // return view to client
            return View("ArkEntity_Edit", vModel);
        }

        [HttpGet]
        public ActionResult ArkEntity_Edit(int id)
        {
            // establish the view model with given criteria
            var vCriteria = new ArkEntity_ItemCriteria { ObjectID = id };
            var vModel    = new ArkEntity_EditItem_ViewModel(vCriteria);

            // refresh the viewModel
            vModel.Refresh();

            // return view to client
            return View("ArkEntity_Edit", vModel);
        }

        [HttpPost]
        public ActionResult ArkEntity_Save (ArkEntity_EditItem_ViewModel model)
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

                return View("ArkEntity_Info", model);
            }

            // refresh the viewModel (meta only)
            model.Refresh(false);

            // return view to client
            return View("ArkEntity_Edit", model);
        }

        #endregion

        #region Entity Reference
        public IActionResult Reference_List()
        {
            ViewData["Message"] = "Your reference list page.";

            return View();
        }

        #endregion

        #region Entity Contact

        public IActionResult Contact_List()
        {
            ViewData["Message"] = "Your contact list page.";

            return View();
        }

        #endregion
    }
}