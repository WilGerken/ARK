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
    public class EntityTag_ItemCriteria : ItemCriteria_Base<EntityTag_ItemCriteria>
    {
        #region Properties

        public K_ENTITY_TAG ToDto()
        {
            K_ENTITY_TAG dto = new K_ENTITY_TAG();

            base.ToDto(dto);

            return dto;
        }

        #endregion
    }

    /// <summary>
    /// List Criteria
    /// </summary>
    [Serializable]
    public class EntityTag_ListCriteria : ListCriteria_Base<EntityTag_ListCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<int?> EntityID_Property = RegisterProperty<int?>(c => c.EntityID);
        public int? EntityID
        {
            get { return ReadProperty(EntityID_Property); }
            set { LoadProperty(EntityID_Property, value); }
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

        public F_ENTITY_TAG ToDto()
        {
            F_ENTITY_TAG dto = new F_ENTITY_TAG();

            dto.entityID = EntityID;
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
    public class EntityTag_InfoItem : InfoItem_Base<EntityTag_InfoItem, EntityTag_ItemCriteria>
    {
        #region Properties

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

        public void FromDto (D_ENTITY_TAG dto)
        {
            EntityID = dto.entityID;
            EntityNm = dto.entityNm;
            TagID    = dto.tagID;
            TagTxt   = dto.tagTxt;
            TypeID   = dto.typeID;
            TypeTxt  = dto.typeTxt;

            base.FromDto (dto);
        }

        #endregion

        #region DataPortal

        private void Child_Fetch(D_ENTITY_TAG dto) { FromDto(dto); }

        #endregion
    }

    /// <summary>
    /// ReadOnly List
    /// </summary>
    [Serializable]
    public class EntityTag_InfoList : InfoList_Base<EntityTag_InfoList, EntityTag_ListCriteria, EntityTag_InfoItem, EntityTag_ItemCriteria>
    {
        #region DataPortal

        private void DataPortal_Fetch (EntityTag_ListCriteria aCriteria)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // add select option if given
            if (aCriteria.SelectOption_Value.HasValue)
            {
                Insert(0, DataPortal.FetchChild<EntityTag_InfoItem>(new D_ENTITY_TAG
                {
                    selectTxt = aCriteria.SelectOption_Text,
                    objectID  = aCriteria.SelectOption_Value.Value
                }));
            }

            // add elements of list from persistent store
            using (var ctx = DalFactory.GetManager (DalFactory.ARK_ENTITY_SCHEMA_NM))
            {
                var dal = ctx.GetProvider<I_ENTITY_TAG>();
                var list = dal.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<EntityTag_InfoItem>(item));
            }

            RaiseListChangedEvents = rlce;
            IsReadOnly = true;
        }

        #endregion
    }

    [Serializable]
    public class EntityTag_EditItem : EditItem_Base<EntityTag_EditItem, EntityTag_ItemCriteria>
    {
        #region Properties

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

        public void FromDto (D_ENTITY_TAG dto)
        {
            using (BypassPropertyChecks)
            {
                EntityID = dto.entityID;
                TagID  = dto.tagID;
                TypeID = dto.typeID;

                EntityNm = dto.entityNm;
                TagTxt = dto.tagTxt;
                TypeTxt = dto.typeTxt;

                base.FromDto (dto);
            }
        }

        public D_ENTITY_TAG ToDto()
        {
            D_ENTITY_TAG dto = new D_ENTITY_TAG();

            dto.entityID = EntityID;
            dto.tagID    = TagID;
            dto.typeID   = TypeID;

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

        private void DataPortal_Fetch(EntityTag_ItemCriteria aKey)
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_ENTITY_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_ENTITY_TAG>();
                var data = dal.SelectItem(aKey.ToDto());

                FromDto(data);
            }
        }

        private void Child_Fetch(D_ENTITY_TAG dto) { FromDto(dto); }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_ENTITY_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_ENTITY_TAG>();
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

                var dal = dalManager.GetProvider<I_ENTITY_TAG>();
                var data = dal.UpdateItem(ToDto());

                FromDto(data);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_ENTITY_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_ENTITY_TAG>();

                dal.DeleteItem (new K_ENTITY_TAG { objectID = this.ObjectID });
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
    public class EntityTag_EditItem_Getter : EditItem_Getter_Base<EntityTag_EditItem, EntityTag_ItemCriteria>
    {
        #region DataPortal

        protected override void DataPortal_Fetch(EntityTag_ItemCriteria aCriteria)
        {
            if (aCriteria.HasKey)
                EditItem = EntityTag_EditItem.GetItem(aCriteria);
            else
                EditItem = EntityTag_EditItem.NewItem(aCriteria);
        }

        #endregion
    }

    /// <summary>
    /// Editable List
    /// </summary>
    [Serializable]
    public class EntityTag_EditList : EditList_Base<EntityTag_EditList, EntityTag_ListCriteria, EntityTag_EditItem, EntityTag_ItemCriteria>
    {
        #region DataPortal

        private void DataPortal_Fetch(EntityTag_ListCriteria aCriteria)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            using (var ctx = DalFactory.GetManager(DalFactory.ARK_ENTITY_SCHEMA_NM))
            {
                var dal = ctx.GetProvider<I_ENTITY_TAG>();
                var list = dal.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<EntityTag_EditItem>(item));
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
