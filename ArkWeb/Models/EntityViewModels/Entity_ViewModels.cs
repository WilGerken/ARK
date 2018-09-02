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
    /// model for viewing a list of ArkEntity items
    ///
    /// @created gerken.wil@gmail.com 30-Aug-2018
    /// </summary>
    public class ArkEntity_EditList_ViewModel : EditList_ViewModel_Base<ArkEntity_EditList, ArkEntity_ListCriteria, ArkEntity_EditItem, ArkEntity_ItemCriteria>
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public ArkEntity_EditList_ViewModel()
        {
        }

        /// <summary>
        /// common constructor
        /// </summary>
        public ArkEntity_EditList_ViewModel (IQueryCollection aQuery) : base (aQuery)
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
        public List<ArkEntity_ViewModel_ListItem> SimpleListModel
        {
            get
            {
                if (ModelObject != null)
                    return ModelObject.Select(x => new ArkEntity_ViewModel_ListItem
                    {
                        ObjectID     = x.ObjectID,
                        EntityNm     = x.EntityNm,
                        DescTxt      = x.DescTxt,
                        ActiveYn     = x.ActiveYn
                    }).ToList();
                else
                    return new List<ArkEntity_ViewModel_ListItem>();
            }
        }
    }

    public class ArkEntity_ViewModel_ListItem
    {
        public int    ObjectID     { get; set; }
        public string EntityNm     { get; set; }
        public string DescTxt      { get; set; }
        public bool   ActiveYn     { get; set; }
    }

    /// <summary>
    /// model for viewing an ArkEntity item
    /// 
    /// @created gerken.wil@gmail.com 30-Aug-2018
    /// </summary>
    public class ArkEntity_EditItem_ViewModel : EditItem_ViewModel_Base<ArkEntity_EditItem, ArkEntity_ItemCriteria>
    {
        /// <summary>
        /// default constructor
        /// </summary>
        public ArkEntity_EditItem_ViewModel() : base (new ArkEntity_ItemCriteria()) { }

        /// <summary>
        /// common constructor
        /// </summary>
        /// <param name="aCriteria">item key criteria</param>
        public ArkEntity_EditItem_ViewModel (ArkEntity_ItemCriteria aCriteria) : base (aCriteria) { }

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