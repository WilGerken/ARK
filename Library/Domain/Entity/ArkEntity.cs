using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Common;
using Library.Resources;
using Library.Resources.Entity;
using Csla;

namespace Library.Domain
{
    /// <summary>
    /// Item Criteria
    /// </summary>
    [Serializable]
    public class ArkEntity_ItemCriteria : ItemCriteria_Base<ArkEntity_ItemCriteria>
    {
        #region Properties

        public K_ARK_ENTITY ToDto()
        {
            K_ARK_ENTITY dto = new K_ARK_ENTITY();

            base.ToDto(dto);

            return dto;
        }

        #endregion
    }

    /// <summary>
    /// List Criteria
    /// </summary>
    [Serializable]
    public class ArkEntity_ListCriteria : ListCriteria_Base<ArkEntity_ListCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<string> EntityNm_Property = RegisterProperty<string>(c => c.EntityNm);
        public string EntityNm
        {
            get { return ReadProperty(EntityNm_Property); }
            set { LoadProperty(EntityNm_Property, value); }
        }

        public static readonly PropertyInfo<string> DescTxt_Property = RegisterProperty<string>(c => c.DescTxt);
        public string DescTxt
        {
            get { return ReadProperty(DescTxt_Property); }
            set { LoadProperty(DescTxt_Property, value); }
        }

        public F_ARK_ENTITY ToDto()
        {
            F_ARK_ENTITY dto = new F_ARK_ENTITY();

            dto.entityNm = EntityNm;
            dto.descTxt  = DescTxt;

            base.ToDto (dto);

            return dto;
        }

        #endregion
    }

    /// <summary>
    /// ReadOnly Item
    /// </summary>
    [Serializable]
    public class ArkEntity_InfoItem : InfoItem_Base<ArkEntity_InfoItem, ArkEntity_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<string> EntityNm_Property = RegisterProperty<string>(c => c.EntityNm);
        public string EntityNm
        {
            get { return ReadProperty(EntityNm_Property); }
            private set { LoadProperty(EntityNm_Property, value); }
        }

        public static readonly PropertyInfo<string> DescTxt_Property = RegisterProperty<string>(c => c.DescTxt);
        public string DescTxt
        {
            get { return ReadProperty(DescTxt_Property); }
            private set { LoadProperty(DescTxt_Property, value); }
        }

        public void FromDto (D_ARK_ENTITY dto)
        {
            EntityNm = dto.entityNm;
            DescTxt  = dto.descTxt;

            base.FromDto (dto);
        }

        #endregion

        #region DataPortal

        private void Child_Fetch(D_ARK_ENTITY dto) { FromDto(dto); }

        #endregion
    }

    /// <summary>
    /// ReadOnly List
    /// </summary>
    [Serializable]
    public class ArkEntity_InfoList : InfoList_Base<ArkEntity_InfoList, ArkEntity_ListCriteria, ArkEntity_InfoItem, ArkEntity_ItemCriteria>
    {
        #region DataPortal

        private void DataPortal_Fetch (ArkEntity_ListCriteria aCriteria)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // add select option if given
            if (aCriteria.SelectOption_Value.HasValue)
            {
                Insert(0, DataPortal.FetchChild<ArkEntity_InfoItem>(new D_ARK_ENTITY
                {
                    entityNm = aCriteria.SelectOption_Text,
                    objectID = aCriteria.SelectOption_Value.Value
                }));
            }

            // add elements of list from persistent store
            using (var ctx = DalFactory.GetManager (DalFactory.ARK_ENTITY_SCHEMA_NM))
            {
                var dal = ctx.GetProvider<I_ARK_ENTITY>();
                var list = dal.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<ArkEntity_InfoItem>(item));
            }

            RaiseListChangedEvents = rlce;
            IsReadOnly = true;
        }

        #endregion
    }

    [Serializable]
    public class ArkEntity_EditItem : EditItem_Base<ArkEntity_EditItem, ArkEntity_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<string> EntityNm_Property = RegisterProperty<string>(c => c.EntityNm);
        [Required]
        public string EntityNm
        {
            get { return GetProperty(EntityNm_Property); }
            set { SetProperty(EntityNm_Property, value); }
        }

        public static readonly PropertyInfo<string> DescTxt_Property = RegisterProperty<string>(c => c.DescTxt);
        public string DescTxt
        {
            get { return GetProperty(DescTxt_Property); }
            set { SetProperty(DescTxt_Property, value); }
        }

        public void FromDto (D_ARK_ENTITY dto)
        {
            using (BypassPropertyChecks)
            {
                EntityNm = dto.entityNm;
                DescTxt  = dto.descTxt;

                base.FromDto (dto);
            }
        }

        public D_ARK_ENTITY ToDto()
        {
            D_ARK_ENTITY dto = new D_ARK_ENTITY();

            dto.entityNm = EntityNm;
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

        private void DataPortal_Fetch(ArkEntity_ItemCriteria aKey)
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_ENTITY_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_ARK_ENTITY>();
                var data = dal.SelectItem(aKey.ToDto());

                FromDto(data);
            }
        }

        private void Child_Fetch(D_ARK_ENTITY dto) { FromDto(dto); }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_ENTITY_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_ARK_ENTITY>();
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

                var dal = dalManager.GetProvider<I_ARK_ENTITY>();
                var data = dal.UpdateItem(ToDto());

                FromDto(data);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_ENTITY_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_ARK_ENTITY>();

                dal.DeleteItem (new K_ARK_ENTITY { objectID = this.ObjectID });
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
    public class ArkEntity_EditItem_Getter : EditItem_Getter_Base<ArkEntity_EditItem, ArkEntity_ItemCriteria>
    {
        #region DataPortal

        protected override void DataPortal_Fetch(ArkEntity_ItemCriteria aCriteria)
        {
            if (aCriteria.HasKey)
                EditItem = ArkEntity_EditItem.GetItem(aCriteria);
            else
                EditItem = ArkEntity_EditItem.NewItem(aCriteria);
        }

        #endregion
    }

    /// <summary>
    /// Editable List
    /// </summary>
    [Serializable]
    public class ArkEntity_EditList : EditList_Base<ArkEntity_EditList, ArkEntity_ListCriteria, ArkEntity_EditItem, ArkEntity_ItemCriteria>
    {
        #region DataPortal

        private void DataPortal_Fetch(ArkEntity_ListCriteria aCriteria)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            using (var ctx = DalFactory.GetManager(DalFactory.ARK_ENTITY_SCHEMA_NM))
            {
                var dal = ctx.GetProvider<I_ARK_ENTITY>();
                var list = dal.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<ArkEntity_EditItem>(item));
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
