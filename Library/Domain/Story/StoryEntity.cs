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
    public class StoryEntity_ItemCriteria : ItemCriteria_Base<StoryEntity_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int?> StoryID_Property = RegisterProperty<int?>(c => c.StoryID);
        public int? StoryID
        {
            get { return ReadProperty(StoryID_Property); }
            set { LoadProperty(StoryID_Property, value); }
        }

        public static readonly PropertyInfo<int?> EntityID_Property = RegisterProperty<int?>(c => c.EntityID);
        public int? EntityID
        {
            get { return ReadProperty(EntityID_Property); }
            set { LoadProperty(EntityID_Property, value); }
        }

        public static readonly PropertyInfo<int?> RoleID_Property = RegisterProperty<int?>(c => c.RoleID);
        public int? RoleID
        {
            get { return ReadProperty(RoleID_Property); }
            set { LoadProperty(RoleID_Property, value); }
        }

        public K_STORY_ENTITY ToDto()
        {
            K_STORY_ENTITY dto = new K_STORY_ENTITY();

            dto.storyID  = StoryID;
            dto.entityID = EntityID;
            dto.roleID   = RoleID;

            base.ToDto(dto);

            return dto;
        }

        #endregion
    }

    /// <summary>
    /// List Criteria
    /// </summary>
    [Serializable]
    public class StoryEntity_ListCriteria : ListCriteria_Base<StoryEntity_ListCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int?> StoryID_Property = RegisterProperty<int?>(c => c.StoryID);
        public int? StoryID
        {
            get { return ReadProperty(StoryID_Property); }
            set { LoadProperty(StoryID_Property, value); }
        }

        public static readonly PropertyInfo<int?> EntityID_Property = RegisterProperty<int?>(c => c.EntityID);
        public int? EntityID
        {
            get { return ReadProperty(EntityID_Property); }
            set { LoadProperty(EntityID_Property, value); }
        }

        public static readonly PropertyInfo<int?> RoleID_Property = RegisterProperty<int?>(c => c.RoleID);
        public int? RoleID
        {
            get { return ReadProperty(RoleID_Property); }
            set { LoadProperty(RoleID_Property, value); }
        }

        public F_STORY_ENTITY ToDto()
        {
            F_STORY_ENTITY dto = new F_STORY_ENTITY();

            dto.entityID = EntityID;
            dto.storyID  = StoryID;
            dto.roleID   = RoleID;

            base.ToDto (dto);

            return dto;
        }

        #endregion
    }

    /// <summary>
    /// ReadOnly Item
    /// </summary>
    [Serializable]
    public class StoryEntity_InfoItem : InfoItem_Base<StoryEntity_InfoItem, StoryEntity_ItemCriteria>
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

        public static readonly PropertyInfo<int> EntityID_Property = RegisterProperty<int>(c => c.EntityID);
        public int EntityID
        {
            get { return ReadProperty(EntityID_Property); }
            private set { LoadProperty(EntityID_Property, value); }
        }

        public static readonly PropertyInfo<string> EntityNm_Property = RegisterProperty<string>(c => c.EntityNm);
        public string EntityNm
        {
            get { return ReadProperty(EntityNm_Property); }
            private set { LoadProperty(EntityNm_Property, value); }
        }

        public static readonly PropertyInfo<int?> RoleID_Property = RegisterProperty<int?>(c => c.RoleID);
        public int? RoleID
        {
            get { return ReadProperty(RoleID_Property); }
            private set { LoadProperty(RoleID_Property, value); }
        }

        public static readonly PropertyInfo<string> RoleTxt_Property = RegisterProperty<string>(c => c.RoleTxt);
        public string RoleTxt
        {
            get { return ReadProperty(RoleTxt_Property); }
            private set { LoadProperty(RoleTxt_Property, value); }
        }

        public static readonly PropertyInfo<string> DescTxt_Property = RegisterProperty<string>(c => c.DescTxt);
        public string DescTxt
        {
            get { return ReadProperty(DescTxt_Property); }
            private set { LoadProperty(DescTxt_Property, value); }
        }

        public void FromDto (D_STORY_ENTITY dto)
        {
            StoryID  = dto.storyID;
            TitleTxt = dto.titleTxt;
            EntityID = dto.entityID;
            EntityNm = dto.entityNm;
            RoleID   = dto.roleID;
            RoleTxt  = dto.roleTxt;
            DescTxt  = dto.descTxt;

            base.FromDto (dto);
        }

        #endregion

        #region DataPortal

        private void Child_Fetch (D_STORY_ENTITY dto) { FromDto(dto); }

        #endregion
    }

    /// <summary>
    /// ReadOnly List
    /// </summary>
    [Serializable]
    public class StoryEntity_InfoList : InfoList_Base<StoryEntity_InfoList, StoryEntity_ListCriteria, StoryEntity_InfoItem, StoryEntity_ItemCriteria>
    {
        #region DataPortal

        private void DataPortal_Fetch (StoryEntity_ListCriteria aCriteria)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // add select option if given
            if (aCriteria.SelectOption_Value.HasValue)
            {
                Insert(0, DataPortal.FetchChild<StoryEntity_InfoItem>(new D_STORY_ENTITY
                {
                    selectTxt = aCriteria.SelectOption_Text,
                    objectID  = aCriteria.SelectOption_Value.Value
                }));
            }

            // add elements of list from persistent store
            using (var ctx = DalFactory.GetManager (DalFactory.ARK_ENTITY_SCHEMA_NM))
            {
                var dal = ctx.GetProvider<I_STORY_ENTITY>();
                var list = dal.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<StoryEntity_InfoItem>(item));
            }

            RaiseListChangedEvents = rlce;
            IsReadOnly = true;
        }

        #endregion
    }

    [Serializable]
    public class StoryEntity_EditItem : EditItem_Base<StoryEntity_EditItem, StoryEntity_ItemCriteria>
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

        public static readonly PropertyInfo<int> EntityID_Property = RegisterProperty<int>(c => c.EntityID);
        [Required]
        public int EntityID
        {
            get { return GetProperty(EntityID_Property); }
            set { SetProperty(EntityID_Property, value); }
        }

        public static readonly PropertyInfo<string> EntityNm_Property = RegisterProperty<string>(c => c.EntityNm);
        public string EntityNm
        {
            get { return GetProperty(EntityNm_Property); }
            set { SetProperty(EntityNm_Property, value); }
        }

        public static readonly PropertyInfo<int?> RoleID_Property = RegisterProperty<int?>(c => c.RoleID);
        [Required]
        public int? RoleID
        {
            get { return GetProperty(RoleID_Property); }
            set { SetProperty(RoleID_Property, value); }
        }

        public static readonly PropertyInfo<string> RoleTxt_Property = RegisterProperty<string>(c => c.RoleTxt);
        public string RoleTxt
        {
            get { return GetProperty(RoleTxt_Property); }
            set { SetProperty(RoleTxt_Property, value); }
        }

        public static readonly PropertyInfo<string> DescTxt_Property = RegisterProperty<string>(c => c.DescTxt);
        public string DescTxt
        {
            get { return GetProperty(DescTxt_Property); }
            set { SetProperty(DescTxt_Property, value); }
        }

        public void FromDto (D_STORY_ENTITY dto)
        {
            using (BypassPropertyChecks)
            {
                StoryID  = dto.storyID;
                EntityID = dto.entityID;
                RoleID   = dto.roleID;
                DescTxt  = dto.descTxt;

                TitleTxt = dto.titleTxt;
                EntityNm = dto.entityNm;
                RoleTxt  = dto.roleTxt;

                base.FromDto (dto);
            }
        }

        public D_STORY_ENTITY ToDto()
        {
            D_STORY_ENTITY dto = new D_STORY_ENTITY();

            dto.storyID  = StoryID;
            dto.entityID = EntityID;
            dto.roleID   = RoleID;
            dto.descTxt  = DescTxt;

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

        private void DataPortal_Fetch(StoryEntity_ItemCriteria aKey)
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_ENTITY_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_STORY_ENTITY>();
                var data = dal.SelectItem(aKey.ToDto());

                FromDto(data);
            }
        }

        private void Child_Fetch(D_STORY_ENTITY dto) { FromDto(dto); }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_ENTITY_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_STORY_ENTITY>();
                var data = dal.InsertItem(ToDto());

                FromDto(data);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_ENTITY_SCHEMA_NM))
            {
                UpdateOnDts = DateTime.Now;
                UpdateByUid = AppInfo.UserID;

                var dal = dalManager.GetProvider<I_STORY_ENTITY>();
                var data = dal.UpdateItem(ToDto());

                FromDto(data);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_ENTITY_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_STORY_ENTITY>();

                dal.DeleteItem (new K_STORY_ENTITY { objectID = this.ObjectID });
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
    public class StoryEntity_EditItem_Getter : EditItem_Getter_Base<StoryEntity_EditItem, StoryEntity_ItemCriteria>
    {
        #region DataPortal

        protected override void DataPortal_Fetch(StoryEntity_ItemCriteria aCriteria)
        {
            if (aCriteria.HasKey)
                EditItem = StoryEntity_EditItem.GetItem(aCriteria);
            else
                EditItem = StoryEntity_EditItem.NewItem(aCriteria);
        }

        #endregion
    }

    /// <summary>
    /// Editable List
    /// </summary>
    [Serializable]
    public class StoryEntity_EditList : EditList_Base<StoryEntity_EditList, StoryEntity_ListCriteria, StoryEntity_EditItem, StoryEntity_ItemCriteria>
    {
        #region DataPortal

        private void DataPortal_Fetch(StoryEntity_ListCriteria aCriteria)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            using (var ctx = DalFactory.GetManager(DalFactory.ARK_ENTITY_SCHEMA_NM))
            {
                var dal = ctx.GetProvider<I_STORY_ENTITY>();
                var list = dal.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<StoryEntity_EditItem>(item));
            }

            RaiseListChangedEvents = rlce;
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var ctx = DalFactory.GetManager(DalFactory.ARK_ENTITY_SCHEMA_NM))
            {
                Child_Update();
            }
        }

        #endregion
    }
}
