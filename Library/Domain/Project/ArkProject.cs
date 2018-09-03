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
    public class ArkProject_ItemCriteria : ItemCriteria_Base<ArkProject_ItemCriteria>
    {
        #region Properties

        public K_ARK_PROJECT ToDto()
        {
            K_ARK_PROJECT dto = new K_ARK_PROJECT();

            base.ToDto(dto);

            return dto;
        }

        #endregion
    }

    /// <summary>
    /// List Criteria
    /// </summary>
    [Serializable]
    public class ArkProject_ListCriteria : ListCriteria_Base<ArkProject_ListCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<string> ProjectNm_Property = RegisterProperty<string>(c => c.ProjectNm);
        public string ProjectNm
        {
            get { return ReadProperty(ProjectNm_Property); }
            set { LoadProperty(ProjectNm_Property, value); }
        }

        public static readonly PropertyInfo<string> DescTxt_Property = RegisterProperty<string>(c => c.DescTxt);
        public string DescTxt
        {
            get { return ReadProperty(DescTxt_Property); }
            set { LoadProperty(DescTxt_Property, value); }
        }

        public static readonly PropertyInfo<int?> ManagerID_Property = RegisterProperty<int?>(c => c.ManagerID);
        public int? ManagerID
        {
            get { return ReadProperty(ManagerID_Property); }
            private set { LoadProperty(ManagerID_Property, value); }
        }

        public static readonly PropertyInfo<int?> ClientID_Property = RegisterProperty<int?>(c => c.ClientID);
        public int? ClientID
        {
            get { return ReadProperty(ClientID_Property); }
            private set { LoadProperty(ClientID_Property, value); }
        }

        public static readonly PropertyInfo<string> TagTxt_Property = RegisterProperty<string>(c => c.TagTxt);
        public string TagTxt
        {
            get { return ReadProperty(TagTxt_Property); }
            set { LoadProperty(TagTxt_Property, value); }
        }

        public F_ARK_PROJECT ToDto()
        {
            F_ARK_PROJECT dto = new F_ARK_PROJECT();

            dto.projectNm = ProjectNm;
            dto.descTxt   = DescTxt;
            dto.managerID = ManagerID;
            dto.clientID  = ClientID;
            dto.tagTxt    = TagTxt;

            base.ToDto (dto);

            return dto;
        }

        #endregion
    }

    /// <summary>
    /// ReadOnly Item
    /// </summary>
    [Serializable]
    public class ArkProject_InfoItem : InfoItem_Base<ArkProject_InfoItem, ArkProject_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<string> ProjectNm_Property = RegisterProperty<string>(c => c.ProjectNm);
        public string ProjectNm
        {
            get { return ReadProperty(ProjectNm_Property); }
            private set { LoadProperty(ProjectNm_Property, value); }
        }

        public static readonly PropertyInfo<string> DescTxt_Property = RegisterProperty<string>(c => c.DescTxt);
        public string DescTxt
        {
            get { return ReadProperty(DescTxt_Property); }
            private set { LoadProperty(DescTxt_Property, value); }
        }

        public static readonly PropertyInfo<int?> ManagerID_Property = RegisterProperty<int?>(c => c.ManagerID);
        public int? ManagerID
        {
            get { return ReadProperty(ManagerID_Property); }
            private set { LoadProperty(ManagerID_Property, value); }
        }

        public static readonly PropertyInfo<string> ManagerNm_Property = RegisterProperty<string>(c => c.ManagerNm);
        public string ManagerNm
        {
            get { return ReadProperty(ManagerNm_Property); }
            private set { LoadProperty(ManagerNm_Property, value); }
        }

        public static readonly PropertyInfo<int?> ClientID_Property = RegisterProperty<int?>(c => c.ClientID);
        public int? ClientID
        {
            get { return ReadProperty(ClientID_Property); }
            private set { LoadProperty(ClientID_Property, value); }
        }

        public static readonly PropertyInfo<string> ClientNm_Property = RegisterProperty<string>(c => c.ClientNm);
        public string ClientNm
        {
            get { return ReadProperty(ClientNm_Property); }
            private set { LoadProperty(ClientNm_Property, value); }
        }

        public void FromDto (D_ARK_PROJECT dto)
        {
            ProjectNm = dto.projectNm;
            DescTxt   = dto.descTxt;
            ManagerID = dto.managerID;
            ClientID  = dto.clientID;

            ManagerNm = dto.managerNm;
            ClientNm  = dto.clientNm;

            base.FromDto (dto);
        }

        #endregion

        #region DataPortal

        private void Child_Fetch(D_ARK_PROJECT dto) { FromDto(dto); }

        #endregion
    }

    /// <summary>
    /// ReadOnly List
    /// </summary>
    [Serializable]
    public class ArkProject_InfoList : InfoList_Base<ArkProject_InfoList, ArkProject_ListCriteria, ArkProject_InfoItem, ArkProject_ItemCriteria>
    {
        #region DataPortal

        private void DataPortal_Fetch (ArkProject_ListCriteria aCriteria)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // add select option if given
            if (aCriteria.SelectOption_Value.HasValue)
            {
                Insert(0, DataPortal.FetchChild<ArkProject_InfoItem>(new D_ARK_PROJECT
                {
                    selectTxt = aCriteria.SelectOption_Text,
                    objectID  = aCriteria.SelectOption_Value.Value
                }));
            }

            // add elements of list from persistent store
            using (var ctx = DalFactory.GetManager (DalFactory.ARK_PROJECT_SCHEMA_NM))
            {
                var dal = ctx.GetProvider<I_ARK_PROJECT>();
                var list = dal.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<ArkProject_InfoItem>(item));
            }

            RaiseListChangedEvents = rlce;
            IsReadOnly = true;
        }

        #endregion
    }

    [Serializable]
    public class ArkProject_EditItem : EditItem_Base<ArkProject_EditItem, ArkProject_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<string> ProjectNm_Property = RegisterProperty<string>(c => c.ProjectNm);
        [Required]
        public string ProjectNm
        {
            get { return GetProperty(ProjectNm_Property); }
            set { SetProperty(ProjectNm_Property, value); }
        }

        public static readonly PropertyInfo<string> DescTxt_Property = RegisterProperty<string>(c => c.DescTxt);
        [Required]
        public string DescTxt
        {
            get { return GetProperty(DescTxt_Property); }
            set { SetProperty(DescTxt_Property, value); }
        }

        public static readonly PropertyInfo<int?> ManagerID_Property = RegisterProperty<int?>(c => c.ManagerID);
        public int? ManagerID
        {
            get { return GetProperty(ManagerID_Property); }
            set { SetProperty(ManagerID_Property, value); }
        }

        public static readonly PropertyInfo<string> ManagerNm_Property = RegisterProperty<string>(c => c.ManagerNm);
        public string ManagerNm
        {
            get { return GetProperty(ManagerNm_Property); }
            set { SetProperty(ManagerNm_Property, value); }
        }

        public static readonly PropertyInfo<int?> ClientID_Property = RegisterProperty<int?>(c => c.ClientID);
        public int? ClientID
        {
            get { return GetProperty(ClientID_Property); }
            set { SetProperty(ClientID_Property, value); }
        }

        public static readonly PropertyInfo<string> ClientNm_Property = RegisterProperty<string>(c => c.ClientNm);
        public string ClientNm
        {
            get { return GetProperty(ClientNm_Property); }
            set { SetProperty(ClientNm_Property, value); }
        }

        public void FromDto (D_ARK_PROJECT dto)
        {
            using (BypassPropertyChecks)
            {
                ProjectNm = dto.projectNm;
                DescTxt   = dto.descTxt;
                ManagerID = dto.managerID;
                ClientID  = dto.clientID;

                ManagerNm = dto.managerNm;
                ClientNm  = dto.clientNm;

                base.FromDto (dto);
            }
        }

        public D_ARK_PROJECT ToDto()
        {
            D_ARK_PROJECT dto = new D_ARK_PROJECT();

            dto.projectNm = ProjectNm;
            dto.descTxt   = DescTxt;
            dto.managerID = ManagerID;
            dto.clientID  = ClientID;

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

        private void DataPortal_Fetch(ArkProject_ItemCriteria aKey)
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_PROJECT_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_ARK_PROJECT>();
                var data = dal.SelectItem(aKey.ToDto());

                FromDto(data);
            }
        }

        private void Child_Fetch(D_ARK_PROJECT dto) { FromDto(dto); }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_PROJECT_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_ARK_PROJECT>();
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

                var dal = dalManager.GetProvider<I_ARK_PROJECT>();
                var data = dal.UpdateItem(ToDto());

                FromDto(data);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_PROJECT_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_ARK_PROJECT>();

                dal.DeleteItem (new K_ARK_PROJECT { objectID = this.ObjectID });
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
    public class ArkProject_EditItem_Getter : EditItem_Getter_Base<ArkProject_EditItem, ArkProject_ItemCriteria>
    {
        #region DataPortal

        protected override void DataPortal_Fetch(ArkProject_ItemCriteria aCriteria)
        {
            if (aCriteria.HasKey)
                EditItem = ArkProject_EditItem.GetItem(aCriteria);
            else
                EditItem = ArkProject_EditItem.NewItem(aCriteria);
        }

        #endregion
    }

    /// <summary>
    /// Editable List
    /// </summary>
    [Serializable]
    public class ArkProject_EditList : EditList_Base<ArkProject_EditList, ArkProject_ListCriteria, ArkProject_EditItem, ArkProject_ItemCriteria>
    {
        #region DataPortal

        private void DataPortal_Fetch(ArkProject_ListCriteria aCriteria)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            using (var ctx = DalFactory.GetManager(DalFactory.ARK_PROJECT_SCHEMA_NM))
            {
                var dal = ctx.GetProvider<I_ARK_PROJECT>();
                var list = dal.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<ArkProject_EditItem>(item));
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
