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
    public class ProjectRisk_ItemCriteria : ItemCriteria_Base<ProjectRisk_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int?> ProjectID_Property = RegisterProperty<int?>(c => c.ProjectID);
        public int? ProjectID
        {
            get { return ReadProperty(ProjectID_Property); }
            set { LoadProperty(ProjectID_Property, value); }
        }

        public static readonly PropertyInfo<string> TitleTxt_Property = RegisterProperty<string>(c => c.TitleTxt);
        public string TitleTxt
        {
            get { return ReadProperty(TitleTxt_Property); }
            set { LoadProperty(TitleTxt_Property, value); }
        }

        public K_PROJECT_RISK ToDto()
        {
            K_PROJECT_RISK dto = new K_PROJECT_RISK();

            dto.projectID = ProjectID;
            dto.titleTxt  = TitleTxt;

            base.ToDto(dto);

            return dto;
        }

        #endregion
    }

    /// <summary>
    /// List Criteria
    /// </summary>
    [Serializable]
    public class ProjectRisk_ListCriteria : ListCriteria_Base<ProjectRisk_ListCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int?> ProjectID_Property = RegisterProperty<int?>(c => c.ProjectID);
        public int? ProjectID
        {
            get { return ReadProperty(ProjectID_Property); }
            set { LoadProperty(ProjectID_Property, value); }
        }

        public static readonly PropertyInfo<int?> TypeID_Property = RegisterProperty<int?>(c => c.TypeID);
        public int? TypeID
        {
            get { return ReadProperty(TypeID_Property); }
            set { LoadProperty(TypeID_Property, value); }
        }

        public static readonly PropertyInfo<string> TitleTxt_Property = RegisterProperty<string>(c => c.TitleTxt);
        public string TitleTxt
        {
            get { return ReadProperty(TitleTxt_Property); }
            set { LoadProperty(TitleTxt_Property, value); }
        }

        public F_PROJECT_RISK ToDto()
        {
            F_PROJECT_RISK dto = new F_PROJECT_RISK();

            dto.projectID = ProjectID;
            dto.typeID    = TypeID;
            dto.titleTxt  = TitleTxt;

            base.ToDto (dto);

            return dto;
        }

        #endregion
    }

    /// <summary>
    /// ReadOnly Item
    /// </summary>
    [Serializable]
    public class ProjectRisk_InfoItem : InfoItem_Base<ProjectRisk_InfoItem, ProjectRisk_ItemCriteria>
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

        public static readonly PropertyInfo<string> TitleTxt_Property = RegisterProperty<string>(c => c.TitleTxt);
        public string TitleTxt
        {
            get { return ReadProperty(TitleTxt_Property); }
            private set { LoadProperty(TitleTxt_Property, value); }
        }

        public static readonly PropertyInfo<string> DescTxt_Property = RegisterProperty<string>(c => c.DescTxt);
        public string DescTxt
        {
            get { return ReadProperty(DescTxt_Property); }
            private set { LoadProperty(DescTxt_Property, value); }
        }

        public void FromDto (D_PROJECT_RISK dto)
        {
            ProjectID = dto.projectID;
            ProjectNm = dto.projectNm;
            TypeID    = dto.typeID;
            TypeTxt   = dto.typeTxt;
            TitleTxt  = dto.titleTxt;
            DescTxt   = dto.descTxt;

            base.FromDto (dto);
        }

        #endregion

        #region DataPortal

        private void Child_Fetch (D_PROJECT_RISK dto) { FromDto(dto); }

        #endregion
    }

    /// <summary>
    /// ReadOnly List
    /// </summary>
    [Serializable]
    public class ProjectRisk_InfoList : InfoList_Base<ProjectRisk_InfoList, ProjectRisk_ListCriteria, ProjectRisk_InfoItem, ProjectRisk_ItemCriteria>
    {
        #region DataPortal

        private void DataPortal_Fetch (ProjectRisk_ListCriteria aCriteria)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // add select option if given
            if (aCriteria.SelectOption_Value.HasValue)
            {
                Insert(0, DataPortal.FetchChild<ProjectRisk_InfoItem>(new D_PROJECT_RISK
                {
                    selectTxt = aCriteria.SelectOption_Text,
                    objectID  = aCriteria.SelectOption_Value.Value
                }));
            }

            // add elements of list from persistent store
            using (var ctx = DalFactory.GetManager (DalFactory.ARK_PROJECT_SCHEMA_NM))
            {
                var dal = ctx.GetProvider<I_PROJECT_RISK>();
                var list = dal.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<ProjectRisk_InfoItem>(item));
            }

            RaiseListChangedEvents = rlce;
            IsReadOnly = true;
        }

        #endregion
    }

    [Serializable]
    public class ProjectRisk_EditItem : EditItem_Base<ProjectRisk_EditItem, ProjectRisk_ItemCriteria>
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

        public static readonly PropertyInfo<int?> TypeID_Property = RegisterProperty<int?>(c => c.TypeID);
        [Required]
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

        public static readonly PropertyInfo<string> TitleTxt_Property = RegisterProperty<string>(c => c.TitleTxt);
        public string TitleTxt
        {
            get { return GetProperty(TitleTxt_Property); }
            set { SetProperty(TitleTxt_Property, value); }
        }

        public static readonly PropertyInfo<string> DescTxt_Property = RegisterProperty<string>(c => c.DescTxt);
        public string DescTxt
        {
            get { return GetProperty(DescTxt_Property); }
            set { SetProperty(DescTxt_Property, value); }
        }

        public void FromDto (D_PROJECT_RISK dto)
        {
            using (BypassPropertyChecks)
            {
                ProjectID = dto.projectID;
                TypeID    = dto.typeID;
                DescTxt   = dto.descTxt;

                ProjectNm = dto.projectNm;
                TypeTxt   = dto.typeTxt;

                base.FromDto (dto);
            }
        }

        public D_PROJECT_RISK ToDto()
        {
            D_PROJECT_RISK dto = new D_PROJECT_RISK();

            dto.projectID = ProjectID;
            dto.typeID    = TypeID;
            dto.titleTxt  = TitleTxt;
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

        private void DataPortal_Fetch(ProjectRisk_ItemCriteria aKey)
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_PROJECT_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_PROJECT_RISK>();
                var data = dal.SelectItem(aKey.ToDto());

                FromDto(data);
            }
        }

        private void Child_Fetch(D_PROJECT_RISK dto) { FromDto(dto); }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_PROJECT_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_PROJECT_RISK>();
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

                var dal = dalManager.GetProvider<I_PROJECT_RISK>();
                var data = dal.UpdateItem(ToDto());

                FromDto(data);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_PROJECT_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_PROJECT_RISK>();

                dal.DeleteItem (new K_PROJECT_RISK { objectID = this.ObjectID });
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
    public class ProjectRisk_EditItem_Getter : EditItem_Getter_Base<ProjectRisk_EditItem, ProjectRisk_ItemCriteria>
    {
        #region DataPortal

        protected override void DataPortal_Fetch(ProjectRisk_ItemCriteria aCriteria)
        {
            if (aCriteria.HasKey)
                EditItem = ProjectRisk_EditItem.GetItem(aCriteria);
            else
                EditItem = ProjectRisk_EditItem.NewItem(aCriteria);
        }

        #endregion
    }

    /// <summary>
    /// Editable List
    /// </summary>
    [Serializable]
    public class ProjectRisk_EditList : EditList_Base<ProjectRisk_EditList, ProjectRisk_ListCriteria, ProjectRisk_EditItem, ProjectRisk_ItemCriteria>
    {
        #region DataPortal

        private void DataPortal_Fetch(ProjectRisk_ListCriteria aCriteria)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            using (var ctx = DalFactory.GetManager(DalFactory.ARK_PROJECT_SCHEMA_NM))
            {
                var dal = ctx.GetProvider<I_PROJECT_RISK>();
                var list = dal.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<ProjectRisk_EditItem>(item));
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
