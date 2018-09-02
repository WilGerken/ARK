using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Common;
using Library.Resources;
using Library.Resources.Story;
using Csla;

namespace Library.Domain
{
    /// <summary>
    /// Item Criteria
    /// </summary>
    [Serializable]
    public class ArkBlog_ItemCriteria : ItemCriteria_Base<ArkBlog_ItemCriteria>
    {
        #region Properties

        public static PropertyInfo<int?> EntityID_Property = RegisterProperty<int?>(c => c.EntityID);
        public int? EntityID
        {
            get { return ReadProperty(EntityID_Property); }
            set { LoadProperty(EntityID_Property, value); }
        }

        public static PropertyInfo<DateTime?> EntryDts_Property = RegisterProperty<DateTime?>(c => c.EntryDts);
        public DateTime? EntryDts
        {
            get { return ReadProperty(EntryDts_Property); }
            set { LoadProperty(EntryDts_Property, value); }
        }

        public K_ARK_BLOG ToDto()
        {
            K_ARK_BLOG dto = new K_ARK_BLOG();

            dto.entityID = EntityID;
            dto.entryDts = EntryDts;

            base.ToDto (dto);

            return dto;
        }

        #endregion
    }

    /// <summary>
    /// List Criteria
    /// </summary>
    [Serializable]
    public class ArkBlog_ListCriteria : ListCriteria_Base<ArkBlog_ListCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int?> EntityID_Property = RegisterProperty<int?>(c => c.EntityID);
        public int? EntityID
        {
            get { return ReadProperty(EntityID_Property); }
            set { LoadProperty(EntityID_Property, value); }
        }

        public static readonly PropertyInfo<DateTime?> FromEntryDt_Property = RegisterProperty<DateTime?>(c => c.FromEntryDt);
        public DateTime? FromEntryDt
        {
            get { return ReadProperty(FromEntryDt_Property); }
            set { LoadProperty(FromEntryDt_Property, value); }
        }

        public static readonly PropertyInfo<DateTime?> ThruEntryDt_Property = RegisterProperty<DateTime?>(c => c.ThruEntryDt);
        public DateTime? ThruEntryDt
        {
            get { return ReadProperty(ThruEntryDt_Property); }
            set { LoadProperty(ThruEntryDt_Property, value); }
        }

        public static readonly PropertyInfo<string> TitleTxt_Property = RegisterProperty<string>(c => c.TitleTxt);
        public string TitleTxt
        {
            get { return ReadProperty(TitleTxt_Property); }
            set { LoadProperty(TitleTxt_Property, value); }
        }

        public static readonly PropertyInfo<string> NarrativeTxt_Property = RegisterProperty<string>(c => c.NarrativeTxt);
        public string NarrativeTxt
        {
            get { return ReadProperty(NarrativeTxt_Property); }
            set { LoadProperty(NarrativeTxt_Property, value); }
        }

        public F_ARK_BLOG ToDto()
        {
            F_ARK_BLOG dto = new F_ARK_BLOG();

            dto.entityID     = EntityID;
            dto.fromEntryDt  = FromEntryDt;
            dto.thruEntryDt  = ThruEntryDt;
            dto.titleTxt     = TitleTxt;
            dto.narrativeTxt = NarrativeTxt;

            base.ToDto(dto);

            return dto;
        }

        #endregion
    }

    /// <summary>
    /// ReadOnly Item
    /// </summary>
    [Serializable]
    public class ArkBlog_InfoItem : InfoItem_Base<ArkBlog_InfoItem, ArkBlog_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int> EntityID_Property = RegisterProperty<int>(c => c.EntityID);
        public int EntityID
        {
            get { return ReadProperty(EntityID_Property); }
            set { LoadProperty(EntityID_Property, value); }
        }

        public static readonly PropertyInfo<DateTime> EntryDts_Property = RegisterProperty<DateTime>(c => c.EntryDts);
        public DateTime EntryDts
        {
            get { return ReadProperty(EntryDts_Property); }
            set { LoadProperty(EntryDts_Property, value); }
        }

        public static readonly PropertyInfo<string> TitleTxt_Property = RegisterProperty<string>(c => c.TitleTxt);
        public string TitleTxt
        {
            get { return ReadProperty(TitleTxt_Property); }
            private set { LoadProperty(TitleTxt_Property, value); }
        }

        public static readonly PropertyInfo<string> NarrativeTxt_Property = RegisterProperty<string>(c => c.NarrativeTxt);
        public string NarrativeTxt
        {
            get { return ReadProperty(NarrativeTxt_Property); }
            private set { LoadProperty(NarrativeTxt_Property, value); }
        }

        public void FromDto (D_ARK_BLOG dto)
        {
            EntityID     = dto.entityID;
            EntryDts     = dto.entryDts;
            TitleTxt     = dto.titleTxt;
            NarrativeTxt = dto.narrativeTxt;

            base.FromDto (dto);
        }

        #endregion

        #region DataPortal

        private void Child_Fetch(D_ARK_BLOG dto) { FromDto(dto); }

        #endregion
    }

    /// <summary>
    /// ReadOnly List
    /// </summary>
    [Serializable]
    public class ArkBlog_InfoList : InfoList_Base<ArkBlog_InfoList, ArkBlog_ListCriteria, ArkBlog_InfoItem, ArkBlog_ItemCriteria>
    {
        #region DataPortal

        private void DataPortal_Fetch (ArkBlog_ListCriteria aCriteria)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // add select option if given
            if (aCriteria.SelectOption_Value.HasValue)
            {
                Insert(0, DataPortal.FetchChild<ArkBlog_InfoItem>(new D_ARK_BLOG
                {
                    titleTxt = aCriteria.SelectOption_Text,
                    objectID = aCriteria.SelectOption_Value.Value
                }));
            }

            // add elements of list from persistent store
            using (var ctx = DalFactory.GetManager (DalFactory.ARK_STORY_SCHEMA_NM))
            {
                var dal = ctx.GetProvider<I_ARK_BLOG>();
                var list = dal.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<ArkBlog_InfoItem>(item));
            }

            RaiseListChangedEvents = rlce;
            IsReadOnly = true;
        }

        #endregion
    }

    [Serializable]
    public class ArkBlog_EditItem : EditItem_Base<ArkBlog_EditItem, ArkBlog_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int> EntityID_Property = RegisterProperty<int>(c => c.EntityID);
        [Required]
        public int EntityID
        {
            get { return GetProperty(EntityID_Property); }
            set { SetProperty(EntityID_Property, value); }
        }

        public static readonly PropertyInfo<DateTime> EntryDts_Property = RegisterProperty<DateTime>(c => c.EntryDts);
        [Required]
        public DateTime EntryDts
        {
            get { return GetProperty(EntryDts_Property); }
            set { SetProperty(EntryDts_Property, value); }
        }

        public static readonly PropertyInfo<string> TitleTxt_Property = RegisterProperty<string>(c => c.TitleTxt);
        [Required]
        public string TitleTxt
        {
            get { return GetProperty(TitleTxt_Property); }
            set { SetProperty(TitleTxt_Property, value); }
        }

        public static readonly PropertyInfo<string> NarrativeTxt_Property = RegisterProperty<string>(c => c.NarrativeTxt);
        [Required]
        public string NarrativeTxt
        {
            get { return GetProperty(NarrativeTxt_Property); }
            set { SetProperty(NarrativeTxt_Property, value); }
        }

        public void FromDto (D_ARK_BLOG dto)
        {
            using (BypassPropertyChecks)
            {
                EntityID     = dto.entityID;
                EntryDts     = dto.entryDts;
                TitleTxt     = dto.titleTxt;
                NarrativeTxt = dto.narrativeTxt;

                base.FromDto (dto);
            }
        }

        public D_ARK_BLOG ToDto()
        {
            D_ARK_BLOG dto = new D_ARK_BLOG ();

            dto.entityID     = EntityID;
            dto.entryDts     = EntryDts;
            dto.titleTxt     = TitleTxt;
            dto.narrativeTxt = NarrativeTxt;

            base.ToDto (dto);

            return dto;
        }

        #endregion

        #region DataPortal

        [RunLocal]
        protected override void DataPortal_Create()
        {
            base.DataPortal_Create();
        }

        private void DataPortal_Fetch(ArkBlog_ItemCriteria aKey)
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_STORY_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_ARK_BLOG>();
                var data = dal.SelectItem(aKey.ToDto());

                FromDto (data);
            }
        }

        private void Child_Fetch(D_ARK_BLOG dto) { FromDto(dto); }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_STORY_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_ARK_BLOG>();
                var data = dal.InsertItem(ToDto());

                FromDto (data);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_STORY_SCHEMA_NM))
            {
                UpdateOnDts = DateTime.Now;
                UpdateByUid = AppInfo.UserID;

                var dal = dalManager.GetProvider<I_ARK_BLOG>();
                var data = dal.UpdateItem(ToDto());

                FromDto(data);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_STORY_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_ARK_BLOG>();

                dal.DeleteItem (new K_ARK_BLOG { objectID = this.ObjectID });
            }
        }

        //protected override void DataPortal_DeleteSelf()
        //{
        //    using (BypassPropertyChecks)
        //        DataPortal_Delete(this.Id);
        //}

        //private void DataPortal_Delete(int id)
        //{
        //    using (var ctx = ProjectTracker.Dal.DalFactory.GetManager())
        //    {
        //        Resources.Clear();
        //        FieldManager.UpdateChildren(this);
        //        var dal = ctx.GetProvider<ProjectTracker.Dal.IProjectDal>();
        //        dal.Delete(id);
        //    }
        //}
        #endregion
    }

    /// <summary>
    /// Unit of Work Getter
    /// </summary>
    [Serializable]
    public class ArkBlog_EditItem_Getter : EditItem_Getter_Base<ArkBlog_EditItem, ArkBlog_ItemCriteria>
    {
        #region DataPortal

        protected override void DataPortal_Fetch(ArkBlog_ItemCriteria aCriteria)
        {
            if (aCriteria.HasKey)
                EditItem = ArkBlog_EditItem.GetItem(aCriteria);
            else
                EditItem = ArkBlog_EditItem.NewItem(aCriteria);
        }

        #endregion
    }

    /// <summary>
    /// Editable List
    /// </summary>
    [Serializable]
    public class ArkBlog_EditList : EditList_Base<ArkBlog_EditList, ArkBlog_ListCriteria, ArkBlog_EditItem, ArkBlog_ItemCriteria>
    {
        #region DataPortal

        private void DataPortal_Fetch(ArkBlog_ListCriteria aCriteria)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            using (var ctx = DalFactory.GetManager(DalFactory.ARK_STORY_SCHEMA_NM))
            {
                var dal = ctx.GetProvider<I_ARK_BLOG>();
                var list = dal.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<ArkBlog_EditItem>(item));
            }

            RaiseListChangedEvents = rlce;
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var ctx = DalFactory.GetManager(DalFactory.ARK_STORY_SCHEMA_NM))
            {
                Child_Update();
            }
        }

        #endregion
    }
}
