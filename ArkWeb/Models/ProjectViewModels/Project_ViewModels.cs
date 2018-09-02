using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using ArkWeb.Common;
using Library.Domain;
using Microsoft.AspNetCore.Http;

namespace ArkWeb.Models
{
    /// <summary>
    /// model for viewing a list of ArkProject items
    ///
    /// @created gerken.wil@gmail.com 30-Aug-2018
    /// </summary>
    public class ArkProject_EditList_ViewModel : EditList_ViewModel_Base<ArkProject_EditList, ArkProject_ListCriteria, ArkProject_EditItem, ArkProject_ItemCriteria>
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public ArkProject_EditList_ViewModel()
        {
        }

        /// <summary>
        /// common constructor
        /// </summary>
        public ArkProject_EditList_ViewModel (IQueryCollection aQuery) : base (aQuery)
        {
        }

        /// <summary>
        /// load the independent objects
        /// </summary>
        protected override void PreLoad()
        {
            base.PreLoad();
        }

        /// <summary>
        /// load the dependent objects
        /// </summary>
        protected override void PostLoad()
        {
            base.PostLoad();
        }
#if (NOTYET)
        public override string BuildNodeUrl(string aControllerNm, string aActionNm, string aLinkTxt)
        {
            StringBuilder lQueryTxt = new StringBuilder();
            string lDelimTxt = string.Empty;

            return base.BuildNodeUrl(aControllerNm, aActionNm, aLinkTxt, lQueryTxt.ToString());
        }
#endif
        public List<ArkProject_ViewModel_ListItem> SimpleListModel
        {
            get
            {
                if (ModelObject != null)
                    return ModelObject.Select(x => new ArkProject_ViewModel_ListItem
                    {
                        ObjectID     = x.ObjectID,
                        ProjectNm    = x.ProjectNm,
                        ManagerID    = x.ManagerID,
                        ManagerNm    = x.ManagerNm,
                        ClientID     = x.ClientID,
                        ClientNm     = x.ClientNm,
                        DescTxt      = x.DescTxt,
                        ActiveYn     = x.ActiveYn
                    }).ToList();
                else
                    return new List<ArkProject_ViewModel_ListItem>();
            }
        }
    }

    public class ArkProject_ViewModel_ListItem
    {
        public int    ObjectID     { get; set; }
        public string ProjectNm    { get; set; }
        public string DescTxt      { get; set; }
        public int?   ManagerID    { get; set; }
        public string ManagerNm    { get; set; }
        public int?   ClientID     { get; set; }
        public string ClientNm     { get; set; }
        public bool   ActiveYn     { get; set; }
    }

    /// <summary>
    /// model for viewing an ArkProject item
    /// 
    /// @created gerken.wil@gmail.com 30-Aug-2018
    /// </summary>
    public class ArkProject_EditItem_ViewModel : EditItem_ViewModel_Base<ArkProject_EditItem, ArkProject_ItemCriteria>
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public ArkProject_EditItem_ViewModel() : base (new ArkProject_ItemCriteria()) { }

        /// <summary>
        /// common constructor
        /// </summary>
        /// <param name="aCriteria">item key criteria</param>
        public ArkProject_EditItem_ViewModel (ArkProject_ItemCriteria aCriteria) : base (aCriteria) { }

        /// <summary>
        /// load the independent objects
        /// </summary>
        protected override void PreLoad()
        {
        }

        /// <summary>
        /// load the dependent objects
        /// </summary>
        protected override void PostLoad()
        {
        }

        /// <summary>
        /// save model changes to persistent store
        /// </summary>
        /// <returns></returns>
        public override bool Save()
        {
            bool lResult = false;

            if (base.Save())
            {
                ItemCriteria.ObjectID = ModelObject.ObjectID;

                lResult = true;
            }

            return lResult;
        }
    }
}