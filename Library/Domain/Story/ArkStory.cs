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
    public class ArkStory_ItemCriteria : ItemCriteria_Base<ArkStory_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int?> AuthorID_Property = RegisterProperty<int?>(c => c.AuthorID);
        public int? AuthorID
        {
            get { return ReadProperty(AuthorID_Property); }
            set { LoadProperty(AuthorID_Property, value); }
        }

        public static readonly PropertyInfo<string> TitleTxt_Property = RegisterProperty<string>(c => c.TitleTxt);
        public string TitleTxt
        {
            get { return ReadProperty(TitleTxt_Property); }
            set { LoadProperty(TitleTxt_Property, value); }
        }

        public K_ARK_STORY ToDto()
        {
            K_ARK_STORY dto = new K_ARK_STORY();

            dto.authorID = AuthorID;
            dto.titleTxt = TitleTxt;

            base.ToDto(dto);

            return dto;
        }

        #endregion
    }

    /// <summary>
    /// List Criteria
    /// </summary>
    [Serializable]
    public class ArkStory_ListCriteria : ListCriteria_Base<ArkStory_ListCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int?> AuthorID_Property = RegisterProperty<int?>(c => c.AuthorID);
        public int? AuthorID
        {
            get { return ReadProperty(AuthorID_Property); }
            set { LoadProperty(AuthorID_Property, value); }
        }

        public static readonly PropertyInfo<string> TitleTxt_Property = RegisterProperty<string>(c => c.TitleTxt);
        public string TitleTxt
        {
            get { return ReadProperty(TitleTxt_Property); }
            set { LoadProperty(TitleTxt_Property, value); }
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

        public static readonly PropertyInfo<string> NarrativeTxt_Property = RegisterProperty<string>(c => c.NarrativeTxt);
        public string NarrativeTxt
        {
            get { return ReadProperty(NarrativeTxt_Property); }
            set { LoadProperty(NarrativeTxt_Property, value); }
        }

        public static readonly PropertyInfo<string> TagTxt_Property = RegisterProperty<string>(c => c.TagTxt);
        public string TagTxt
        {
            get { return ReadProperty(TagTxt_Property); }
            set { LoadProperty(TagTxt_Property, value); }
        }

        public F_ARK_STORY ToDto()
        {
            F_ARK_STORY dto = new F_ARK_STORY();

            dto.authorID     = AuthorID;
            dto.titleTxt     = TitleTxt;
            dto.fromEntryDt  = FromEntryDt;
            dto.thruEntryDt  = ThruEntryDt;
            dto.narrativeTxt = NarrativeTxt;
            dto.tagTxt       = TagTxt;

            base.ToDto(dto);

            return dto;
        }

        #endregion
    }

    /// <summary>
    /// ReadOnly Item
    /// </summary>
    [Serializable]
    public class ArkStory_InfoItem : InfoItem_Base<ArkStory_InfoItem, ArkStory_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int> AuthorID_Property = RegisterProperty<int>(c => c.AuthorID);
        public int AuthorID
        {
            get { return ReadProperty(AuthorID_Property); }
            private set { LoadProperty(AuthorID_Property, value); }
        }

        public static readonly PropertyInfo<string> AuthorNm_Property = RegisterProperty<string>(c => c.AuthorNm);
        public string AuthorNm
        {
            get { return ReadProperty(AuthorNm_Property); }
            private set { LoadProperty(AuthorNm_Property, value); }
        }

        public static readonly PropertyInfo<string> TitleTxt_Property = RegisterProperty<string>(c => c.TitleTxt);
        public string TitleTxt
        {
            get { return ReadProperty(TitleTxt_Property); }
            private set { LoadProperty(TitleTxt_Property, value); }
        }

        public static readonly PropertyInfo<DateTime> EntryDts_Property = RegisterProperty<DateTime>(c => c.EntryDts);
        public DateTime EntryDts
        {
            get { return ReadProperty(EntryDts_Property); }
            private set { LoadProperty(EntryDts_Property, value); }
        }

        public static readonly PropertyInfo<string> NarrativeTxt_Property = RegisterProperty<string>(c => c.NarrativeTxt);
        public string NarrativeTxt
        {
            get { return ReadProperty(NarrativeTxt_Property); }
            private set { LoadProperty(NarrativeTxt_Property, value); }
        }

        public void FromDto (D_ARK_STORY dto)
        {
            AuthorID     = dto.authorID;
            AuthorNm     = dto.authorNm;
            TitleTxt     = dto.titleTxt;
            EntryDts     = dto.entryDts;
            NarrativeTxt = dto.narrativeTxt;

            base.FromDto (dto);
        }

        #endregion

        #region DataPortal

        private void Child_Fetch(D_ARK_STORY dto) { FromDto(dto); }

        #endregion
    }

    /// <summary>
    /// ReadOnly List
    /// </summary>
    [Serializable]
    public class ArkStory_InfoList : InfoList_Base<ArkStory_InfoList, ArkStory_ListCriteria, ArkStory_InfoItem, ArkStory_ItemCriteria>
    {
        #region DataPortal

        private void DataPortal_Fetch (ArkStory_ListCriteria aCriteria)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // add select option if given
            if (aCriteria.SelectOption_Value.HasValue)
            {
                Insert(0, DataPortal.FetchChild<ArkStory_InfoItem>(new D_ARK_STORY
                {
                    titleTxt = aCriteria.SelectOption_Text,
                    objectID = aCriteria.SelectOption_Value.Value
                }));
            }

            // add elements of list from persistent store
            using (var ctx = DalFactory.GetManager (DalFactory.ARK_STORY_SCHEMA_NM))
            {
                var dal = ctx.GetProvider<I_ARK_STORY>();
                var list = dal.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<ArkStory_InfoItem>(item));
            }

            RaiseListChangedEvents = rlce;
            IsReadOnly = true;
        }

        #endregion
    }

    [Serializable]
    public class ArkStory_EditItem : EditItem_Base<ArkStory_EditItem, ArkStory_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int> AuthorID_Property = RegisterProperty<int>(c => c.AuthorID);
        [Required]
        public int AuthorID
        {
            get { return GetProperty(AuthorID_Property); }
            set { SetProperty(AuthorID_Property, value); }
        }

        public static readonly PropertyInfo<string> AuthorNm_Property = RegisterProperty<string>(c => c.AuthorNm);
        public string AuthorNm
        {
            get { return GetProperty(AuthorNm_Property); }
            set { SetProperty(AuthorNm_Property, value); }
        }

        public static readonly PropertyInfo<string> TitleTxt_Property = RegisterProperty<string>(c => c.TitleTxt);
        [Required]
        public string TitleTxt
        {
            get { return GetProperty(TitleTxt_Property); }
            set { SetProperty(TitleTxt_Property, value); }
        }

        public static readonly PropertyInfo<DateTime> EntryDts_Property = RegisterProperty<DateTime>(c => c.EntryDts);
        [Required]
        public DateTime EntryDts
        {
            get { return GetProperty(EntryDts_Property); }
            set { SetProperty(EntryDts_Property, value); }
        }

        public static readonly PropertyInfo<string> NarrativeTxt_Property = RegisterProperty<string>(c => c.NarrativeTxt);
        [Required]
        public string NarrativeTxt
        {
            get { return GetProperty(NarrativeTxt_Property); }
            set { SetProperty(NarrativeTxt_Property, value); }
        }

        public void FromDto (D_ARK_STORY dto)
        {
            using (BypassPropertyChecks)
            {
                AuthorID = dto.authorID;
                TitleTxt     = dto.titleTxt;
                EntryDts     = dto.entryDts;
                NarrativeTxt = dto.narrativeTxt;

                AuthorNm = dto.authorNm;

                base.FromDto (dto);
            }
        }

        public D_ARK_STORY ToDto()
        {
            D_ARK_STORY dto = new D_ARK_STORY();

            dto.authorID     = AuthorID;
            dto.titleTxt     = TitleTxt;
            dto.entryDts     = EntryDts;
            dto.narrativeTxt = NarrativeTxt;

            base.ToDto (dto);

            return dto;
        }

        public static readonly PropertyInfo<StoryTag_EditList> StoryTagList_Property =
            RegisterProperty<StoryTag_EditList>(p => p.StoryTagList, RelationshipTypes.Child | RelationshipTypes.LazyLoad);
        public StoryTag_EditList StoryTagList
        {
            get
            {
                return LazyGetProperty(StoryTagList_Property,
                    () => DataPortal.Fetch<StoryTag_EditList>(new StoryTag_ListCriteria { StoryID = ReadProperty(ObjectID_Property) }));
            }
            private set { LoadProperty(StoryTagList_Property, value); }
        }

        public static readonly PropertyInfo<StoryEntity_EditList> StoryEntityList_Property =
            RegisterProperty<StoryEntity_EditList>(p => p.StoryEntityList, RelationshipTypes.Child | RelationshipTypes.LazyLoad);
        public StoryEntity_EditList StoryEntityList
        {
            get
            {
                return LazyGetProperty(StoryEntityList_Property,
                    () => DataPortal.Fetch<StoryEntity_EditList>(new StoryEntity_ListCriteria { StoryID = ReadProperty(ObjectID_Property) }));
            }
            private set { LoadProperty(StoryEntityList_Property, value); }
        }

        public static readonly PropertyInfo<ProjectStory_EditList> ProjectStoryList_Property =
            RegisterProperty<ProjectStory_EditList>(p => p.ProjectStoryList, RelationshipTypes.Child | RelationshipTypes.LazyLoad);
        public ProjectStory_EditList ProjectStoryList
        {
            get
            {
                return LazyGetProperty(ProjectStoryList_Property,
                    () => DataPortal.Fetch<ProjectStory_EditList>(new ProjectStory_ListCriteria { StoryID = ReadProperty(ObjectID_Property) }));
            }
            private set { LoadProperty(ProjectStoryList_Property, value); }
        }

        #endregion

        #region DataPortal

        [RunLocal]
        protected override void DataPortal_Create()
        {
            base.DataPortal_Create();
        }

        private void DataPortal_Fetch(ArkStory_ItemCriteria aKey)
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_STORY_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_ARK_STORY>();
                var data = dal.SelectItem(aKey.ToDto());

                FromDto(data);
            }
        }

        private void Child_Fetch(D_ARK_STORY dto) { FromDto(dto); }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_STORY_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_ARK_STORY>();
                var data = dal.InsertItem(ToDto());

                FromDto(data);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_STORY_SCHEMA_NM))
            {
                UpdateOnDts = DateTime.Now;
                UpdateByUid = AppInfo.UserID;

                var dal = dalManager.GetProvider<I_ARK_STORY>();
                var data = dal.UpdateItem(ToDto());

                FromDto(data);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_STORY_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_ARK_STORY>();

                dal.DeleteItem (new K_ARK_STORY { objectID = this.ObjectID });
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
    public class ArkStory_EditItem_Getter : EditItem_Getter_Base<ArkStory_EditItem, ArkStory_ItemCriteria>
    {
        #region DataPortal

        protected override void DataPortal_Fetch(ArkStory_ItemCriteria aCriteria)
        {
            if (aCriteria.HasKey)
                EditItem = ArkStory_EditItem.GetItem(aCriteria);
            else
                EditItem = ArkStory_EditItem.NewItem(aCriteria);
        }

        #endregion
    }

    /// <summary>
    /// Editable List
    /// </summary>
    [Serializable]
    public class ArkStory_EditList : EditList_Base<ArkStory_EditList, ArkStory_ListCriteria, ArkStory_EditItem, ArkStory_ItemCriteria>
    {
        #region DataPortal

        private void DataPortal_Fetch(ArkStory_ListCriteria aCriteria)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            using (var ctx = DalFactory.GetManager(DalFactory.ARK_STORY_SCHEMA_NM))
            {
                var dal = ctx.GetProvider<I_ARK_STORY>();
                var list = dal.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<ArkStory_EditItem>(item));
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
