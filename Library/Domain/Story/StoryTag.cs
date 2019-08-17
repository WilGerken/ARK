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
    public class StoryTag_ItemCriteria : ItemCriteria_Base<StoryTag_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int?> StoryID_Property = RegisterProperty<int?>(c => c.StoryID);
        public int? StoryID
        {
            get { return ReadProperty(StoryID_Property); }
            set { LoadProperty(StoryID_Property, value); }
        }

        public static readonly PropertyInfo<int?> TagID_Property = RegisterProperty<int?>(c => c.TagID);
        public int? TagID
        {
            get { return ReadProperty(TagID_Property); }
            set { LoadProperty(TagID_Property, value); }
        }

        public K_STORY_TAG ToDto()
        {
            K_STORY_TAG dto = new K_STORY_TAG();

            dto.storyID = StoryID;
            dto.tagID = TagID;

            base.ToDto(dto);

            return dto;
        }

        #endregion
    }

    /// <summary>
    /// List Criteria
    /// </summary>
    [Serializable]
    public class StoryTag_ListCriteria : ListCriteria_Base<StoryTag_ListCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int?> StoryID_Property = RegisterProperty<int?>(c => c.StoryID);
        public int? StoryID
        {
            get { return ReadProperty(StoryID_Property); }
            set { LoadProperty(StoryID_Property, value); }
        }

        public static readonly PropertyInfo<int?> TagID_Property = RegisterProperty<int?>(c => c.TagID);
        public int? TagID
        {
            get { return ReadProperty(TagID_Property); }
            set { LoadProperty(TagID_Property, value); }
        }

        public static readonly PropertyInfo<int?> TypeID_Property = RegisterProperty<int?>(c => c.TypeID);
        public int? TypeID
        {
            get { return ReadProperty(TypeID_Property); }
            set { LoadProperty(TypeID_Property, value); }
        }

        public F_STORY_TAG ToDto()
        {
            F_STORY_TAG dto = new F_STORY_TAG();

            dto.storyID = StoryID;
            dto.tagID    = TagID;
            dto.typeID   = TypeID;

            base.ToDto (dto);

            return dto;
        }

        #endregion
    }

    /// <summary>
    /// ReadOnly Item
    /// </summary>
    [Serializable]
    public class StoryTag_InfoItem : InfoItem_Base<StoryTag_InfoItem, StoryTag_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int> StoryID_Property = RegisterProperty<int>(c => c.StoryID);
        public int StoryID
        {
            get { return ReadProperty(StoryID_Property); }
            private set { LoadProperty(StoryID_Property, value); }
        }

        public static readonly PropertyInfo<string> TitleTxt_Property = RegisterProperty<string>(c => c.TitleTxt);
        public string TitleTxt
        {
            get { return ReadProperty(TitleTxt_Property); }
            private set { LoadProperty(TitleTxt_Property, value); }
        }

        public static readonly PropertyInfo<int> TagID_Property = RegisterProperty<int>(c => c.TagID);
        public int TagID
        {
            get { return ReadProperty(TagID_Property); }
            private set { LoadProperty(TagID_Property, value); }
        }

        public static readonly PropertyInfo<string> TagTxt_Property = RegisterProperty<string>(c => c.TagTxt);
        public string TagTxt
        {
            get { return ReadProperty(TagTxt_Property); }
            private set { LoadProperty(TagTxt_Property, value); }
        }

        public static readonly PropertyInfo<int?> TypeID_Property = RegisterProperty<int?>(c => c.TypeID);
        public int? TypeID
        {
            get { return ReadProperty(TypeID_Property); }
            private set { LoadProperty(TypeID_Property, value); }
        }

        public static readonly PropertyInfo<string> TypeTxt_Property = RegisterProperty<string>(c => c.TypeTxt);
        public string TypeTxt
        {
            get { return ReadProperty(TypeTxt_Property); }
            private set { LoadProperty(TypeTxt_Property, value); }
        }

        public void FromDto (D_STORY_TAG dto)
        {
            StoryID = dto.storyID;
            TitleTxt = dto.titleTxt;
            TagID    = dto.tagID;
            TagTxt   = dto.tagTxt;
            TypeID   = dto.typeID;
            TypeTxt  = dto.typeTxt;

            base.FromDto (dto);
        }

        #endregion

        #region DataPortal

        private void Child_Fetch(D_STORY_TAG dto) { FromDto(dto); }

        #endregion
    }

    /// <summary>
    /// ReadOnly List
    /// </summary>
    [Serializable]
    public class StoryTag_InfoList : InfoList_Base<StoryTag_InfoList, StoryTag_ListCriteria, StoryTag_InfoItem, StoryTag_ItemCriteria>
    {
        #region DataPortal

        private void DataPortal_Fetch (StoryTag_ListCriteria aCriteria)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // add select option if given
            if (aCriteria.SelectOption_Value.HasValue)
            {
                Insert(0, DataPortal.FetchChild<StoryTag_InfoItem>(new D_STORY_TAG
                {
                    selectTxt = aCriteria.SelectOption_Text,
                    objectID  = aCriteria.SelectOption_Value.Value
                }));
            }

            // add elements of list from persistent store
            using (var ctx = DalFactory.GetManager (DalFactory.ARK_STORY_SCHEMA_NM))
            {
                var dal = ctx.GetProvider<I_STORY_TAG>();
                var list = dal.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<StoryTag_InfoItem>(item));
            }

            RaiseListChangedEvents = rlce;
            IsReadOnly = true;
        }

        #endregion
    }

    [Serializable]
    public class StoryTag_EditItem : EditItem_Base<StoryTag_EditItem, StoryTag_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int> StoryID_Property = RegisterProperty<int>(c => c.StoryID);
        [Required]
        public int StoryID
        {
            get { return GetProperty(StoryID_Property); }
            set { SetProperty(StoryID_Property, value); }
        }

        public static readonly PropertyInfo<string> TitleTxt_Property = RegisterProperty<string>(c => c.TitleTxt);
        public string TitleTxt
        {
            get { return GetProperty(TitleTxt_Property); }
            set { SetProperty(TitleTxt_Property, value); }
        }

        public static readonly PropertyInfo<int> TagID_Property = RegisterProperty<int>(c => c.TagID);
        [Required]
        public int TagID
        {
            get { return GetProperty(TagID_Property); }
            set { SetProperty(TagID_Property, value); }
        }

        public static readonly PropertyInfo<string> TagTxt_Property = RegisterProperty<string>(c => c.TagTxt);
        public string TagTxt
        {
            get { return GetProperty(TagTxt_Property); }
            set { SetProperty(TagTxt_Property, value); }
        }

        public static readonly PropertyInfo<int?> TypeID_Property = RegisterProperty<int?>(c => c.TypeID);
        public int? TypeID
        {
            get { return GetProperty(TypeID_Property); }
            set { SetProperty(TypeID_Property, value); }
        }

        public static readonly PropertyInfo<string> TypeTxt_Property = RegisterProperty<string>(c => c.TypeTxt);
        public string TypeTxt
        {
            get { return GetProperty(TypeTxt_Property); }
            set { SetProperty(TypeTxt_Property, value); }
        }

        public void FromDto (D_STORY_TAG dto)
        {
            using (BypassPropertyChecks)
            {
                StoryID = dto.storyID;
                TagID   = dto.tagID;
                TypeID  = dto.typeID;

                TitleTxt = dto.titleTxt;
                TagTxt   = dto.tagTxt;
                TypeTxt  = dto.typeTxt;

                base.FromDto (dto);
            }
        }

        public D_STORY_TAG ToDto()
        {
            D_STORY_TAG dto = new D_STORY_TAG();

            dto.storyID = StoryID;
            dto.tagID   = TagID;
            dto.typeID  = TypeID;

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

        private void DataPortal_Fetch(StoryTag_ItemCriteria aKey)
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_STORY_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_STORY_TAG>();
                var data = dal.SelectItem(aKey.ToDto());

                FromDto(data);
            }
        }

        private void Child_Fetch(D_STORY_TAG dto) { FromDto(dto); }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_STORY_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_STORY_TAG>();
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

                var dal = dalManager.GetProvider<I_STORY_TAG>();
                var data = dal.UpdateItem(ToDto());

                FromDto(data);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_STORY_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_STORY_TAG>();

                dal.DeleteItem (new K_STORY_TAG { objectID = this.ObjectID });
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
    public class StoryTag_EditItem_Getter : EditItem_Getter_Base<StoryTag_EditItem, StoryTag_ItemCriteria>
    {
        #region DataPortal

        protected override void DataPortal_Fetch(StoryTag_ItemCriteria aCriteria)
        {
            if (aCriteria.HasKey)
                EditItem = StoryTag_EditItem.GetItem(aCriteria);
            else
                EditItem = StoryTag_EditItem.NewItem(aCriteria);
        }

        #endregion
    }

    /// <summary>
    /// Editable List
    /// </summary>
    [Serializable]
    public class StoryTag_EditList : EditList_Base<StoryTag_EditList, StoryTag_ListCriteria, StoryTag_EditItem, StoryTag_ItemCriteria>
    {
        #region DataPortal

        private void DataPortal_Fetch(StoryTag_ListCriteria aCriteria)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            using (var ctx = DalFactory.GetManager(DalFactory.ARK_STORY_SCHEMA_NM))
            {
                var dal = ctx.GetProvider<I_STORY_TAG>();
                var list = dal.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<StoryTag_EditItem>(item));
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
