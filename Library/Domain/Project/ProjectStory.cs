using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Common;
using Library.Resources;
using Library.Resources.Project;
using Csla;

namespace Library.Domain
{
    /// <summary>
    /// Item Criteria
    /// </summary>
    [Serializable]
    public class ProjectStory_ItemCriteria : ItemCriteria_Base<ProjectStory_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int?> ProjectID_Property = RegisterProperty<int?>(c => c.ProjectID);
        public int? ProjectID
        {
            get { return ReadProperty(ProjectID_Property); }
            set { LoadProperty(ProjectID_Property, value); }
        }

        public static readonly PropertyInfo<int?> StoryID_Property = RegisterProperty<int?>(c => c.StoryID);
        public int? StoryID
        {
            get { return ReadProperty(StoryID_Property); }
            set { LoadProperty(StoryID_Property, value); }
        }

        public static readonly PropertyInfo<int?> RoleID_Property = RegisterProperty<int?>(c => c.RoleID);
        public int? RoleID
        {
            get { return ReadProperty(RoleID_Property); }
            set { LoadProperty(RoleID_Property, value); }
        }

        public K_PROJECT_STORY ToDto()
        {
            K_PROJECT_STORY dto = new K_PROJECT_STORY();

            dto.projectID = ProjectID;
            dto.storyID   = StoryID;
            dto.roleID    = RoleID;

            base.ToDto(dto);

            return dto;
        }

        #endregion
    }

    /// <summary>
    /// List Criteria
    /// </summary>
    [Serializable]
    public class ProjectStory_ListCriteria : ListCriteria_Base<ProjectStory_ListCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int?> ProjectID_Property = RegisterProperty<int?>(c => c.ProjectID);
        public int? ProjectID
        {
            get { return ReadProperty(ProjectID_Property); }
            set { LoadProperty(ProjectID_Property, value); }
        }

        public static readonly PropertyInfo<int?> StoryID_Property = RegisterProperty<int?>(c => c.StoryID);
        public int? StoryID
        {
            get { return ReadProperty(StoryID_Property); }
            set { LoadProperty(StoryID_Property, value); }
        }

        public static readonly PropertyInfo<int?> RoleID_Property = RegisterProperty<int?>(c => c.RoleID);
        public int? RoleID
        {
            get { return ReadProperty(RoleID_Property); }
            set { LoadProperty(RoleID_Property, value); }
        }

        public F_PROJECT_STORY ToDto()
        {
            F_PROJECT_STORY dto = new F_PROJECT_STORY();

            dto.projectID = ProjectID;
            dto.storyID  = StoryID;
            dto.roleID    = RoleID;

            base.ToDto (dto);

            return dto;
        }

        #endregion
    }

    /// <summary>
    /// ReadOnly Item
    /// </summary>
    [Serializable]
    public class ProjectStory_InfoItem : InfoItem_Base<ProjectStory_InfoItem, ProjectStory_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int> ProjectID_Property = RegisterProperty<int>(c => c.ProjectID);
        public int ProjectID
        {
            get { return ReadProperty(ProjectID_Property); }
            private set { LoadProperty(ProjectID_Property, value); }
        }

        public static readonly PropertyInfo<string> ProjectNm_Property = RegisterProperty<string>(c => c.ProjectNm);
        public string ProjectNm
        {
            get { return ReadProperty(ProjectNm_Property); }
            private set { LoadProperty(ProjectNm_Property, value); }
        }

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

        public void FromDto (D_PROJECT_STORY dto)
        {
            ProjectID = dto.projectID;
            ProjectNm = dto.projectNm;
            StoryID   = dto.storyID;
            TitleTxt  = dto.titleTxt;
            RoleID    = dto.roleID;
            RoleTxt   = dto.roleTxt;
            DescTxt   = dto.descTxt;

            base.FromDto (dto);
        }

        #endregion

        #region DataPortal

        private void Child_Fetch (D_PROJECT_STORY dto) { FromDto(dto); }

        #endregion
    }

    /// <summary>
    /// ReadOnly List
    /// </summary>
    [Serializable]
    public class ProjectStory_InfoList : InfoList_Base<ProjectStory_InfoList, ProjectStory_ListCriteria, ProjectStory_InfoItem, ProjectStory_ItemCriteria>
    {
        #region DataPortal

        private void DataPortal_Fetch (ProjectStory_ListCriteria aCriteria)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // add select option if given
            if (aCriteria.SelectOption_Value.HasValue)
            {
                Insert(0, DataPortal.FetchChild<ProjectStory_InfoItem>(new D_PROJECT_STORY
                {
                    selectTxt = aCriteria.SelectOption_Text,
                    objectID  = aCriteria.SelectOption_Value.Value
                }));
            }

            // add elements of list from persistent store
            using (var ctx = DalFactory.GetManager (DalFactory.ARK_PROJECT_SCHEMA_NM))
            {
                var dal = ctx.GetProvider<I_PROJECT_STORY>();
                var list = dal.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<ProjectStory_InfoItem>(item));
            }

            RaiseListChangedEvents = rlce;
            IsReadOnly = true;
        }

        #endregion
    }

    [Serializable]
    public class ProjectStory_EditItem : EditItem_Base<ProjectStory_EditItem, ProjectStory_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int> ProjectID_Property = RegisterProperty<int>(c => c.ProjectID);
        [Required]
        public int ProjectID
        {
            get { return GetProperty(ProjectID_Property); }
            set { SetProperty(ProjectID_Property, value); }
        }

        public static readonly PropertyInfo<string> ProjectNm_Property = RegisterProperty<string>(c => c.ProjectNm);
        public string ProjectNm
        {
            get { return GetProperty(ProjectNm_Property); }
            set { SetProperty(ProjectNm_Property, value); }
        }

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

        public void FromDto (D_PROJECT_STORY dto)
        {
            using (BypassPropertyChecks)
            {
                ProjectID = dto.projectID;
                StoryID  = dto.storyID;
                RoleID    = dto.roleID;
                DescTxt   = dto.descTxt;

                ProjectNm = dto.projectNm;
                TitleTxt  = dto.titleTxt;
                RoleTxt   = dto.roleTxt;

                base.FromDto (dto);
            }
        }

        public D_PROJECT_STORY ToDto()
        {
            D_PROJECT_STORY dto = new D_PROJECT_STORY();

            dto.projectID = ProjectID;
            dto.storyID   = StoryID;
            dto.roleID    = RoleID;
            dto.descTxt   = DescTxt;

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

        private void DataPortal_Fetch(ProjectStory_ItemCriteria aKey)
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_PROJECT_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_PROJECT_STORY>();
                var data = dal.SelectItem(aKey.ToDto());

                FromDto(data);
            }
        }

        private void Child_Fetch(D_PROJECT_STORY dto) { FromDto(dto); }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_PROJECT_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_PROJECT_STORY>();
                var data = dal.InsertItem(ToDto());

                FromDto(data);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_PROJECT_SCHEMA_NM))
            {
                UpdateOnDts = DateTime.Now;
                UpdateByUid = AppInfo.UserID;

                var dal = dalManager.GetProvider<I_PROJECT_STORY>();
                var data = dal.UpdateItem(ToDto());

                FromDto(data);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_PROJECT_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_PROJECT_STORY>();

                dal.DeleteItem (new K_PROJECT_STORY { objectID = this.ObjectID });
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
    public class ProjectStory_EditItem_Getter : EditItem_Getter_Base<ProjectStory_EditItem, ProjectStory_ItemCriteria>
    {
        #region DataPortal

        protected override void DataPortal_Fetch(ProjectStory_ItemCriteria aCriteria)
        {
            if (aCriteria.HasKey)
                EditItem = ProjectStory_EditItem.GetItem(aCriteria);
            else
                EditItem = ProjectStory_EditItem.NewItem(aCriteria);
        }

        #endregion
    }

    /// <summary>
    /// Editable List
    /// </summary>
    [Serializable]
    public class ProjectStory_EditList : EditList_Base<ProjectStory_EditList, ProjectStory_ListCriteria, ProjectStory_EditItem, ProjectStory_ItemCriteria>
    {
        #region DataPortal

        private void DataPortal_Fetch(ProjectStory_ListCriteria aCriteria)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            using (var ctx = DalFactory.GetManager(DalFactory.ARK_PROJECT_SCHEMA_NM))
            {
                var dal = ctx.GetProvider<I_PROJECT_STORY>();
                var list = dal.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<ProjectStory_EditItem>(item));
            }

            RaiseListChangedEvents = rlce;
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var ctx = DalFactory.GetManager(DalFactory.ARK_PROJECT_SCHEMA_NM))
            {
                Child_Update();
            }
        }

        #endregion
    }
}
