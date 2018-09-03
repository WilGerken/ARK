using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Common;
using Library.Resources;
using Library.Resources.Common;
using Csla;

namespace Library.Domain
{
    /// <summary>
    /// Item Criteria
    /// </summary>
    [Serializable]
    public class TagType_ItemCriteria : ItemCriteria_Base<TagType_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<string> TypeTxt_Property = RegisterProperty<string>(c => c.TypeTxt);
        public string TypeTxt
        {
            get { return ReadProperty(TypeTxt_Property); }
            set { LoadProperty(TypeTxt_Property, value); }
        }

        public K_TAG_TYPE ToDto()
        {
            K_TAG_TYPE dto = new K_TAG_TYPE();

            dto.typeTxt = TypeTxt;

            base.ToDto(dto);

            return dto;
        }

        #endregion
    }

    /// <summary>
    /// List Criteria
    /// </summary>
    [Serializable]
    public class TagType_ListCriteria : ListCriteria_Base<TagType_ListCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<string> TypeTxt_Property = RegisterProperty<string>(c => c.TypeTxt);
        public string TypeTxt
        {
            get { return ReadProperty(TypeTxt_Property); }
            set { LoadProperty(TypeTxt_Property, value); }
        }

        public F_TAG_TYPE ToDto()
        {
            F_TAG_TYPE dto = new F_TAG_TYPE();

            dto.typeTxt = TypeTxt;

            base.ToDto (dto);

            return dto;
        }

        #endregion
    }

    /// <summary>
    /// ReadOnly Item
    /// </summary>
    [Serializable]
    public class TagType_InfoItem : InfoItem_Base<TagType_InfoItem, TagType_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<string> TypeTxt_Property = RegisterProperty<string>(c => c.TypeTxt);
        public string TypeTxt
        {
            get { return ReadProperty(TypeTxt_Property); }
            private set { LoadProperty(TypeTxt_Property, value); }
        }

        public static readonly PropertyInfo<string> DescTxt_Property = RegisterProperty<string>(c => c.DescTxt);
        public string DescTxt
        {
            get { return ReadProperty(DescTxt_Property); }
            private set { LoadProperty(DescTxt_Property, value); }
        }

        public void FromDto (D_TAG_TYPE dto)
        {
            TypeTxt = dto.typeTxt;
            DescTxt = dto.descTxt;

            base.FromDto (dto);
        }

        #endregion

        #region DataPortal

        private void Child_Fetch(D_TAG_TYPE dto) { FromDto(dto); }

        #endregion
    }

    /// <summary>
    /// ReadOnly List
    /// </summary>
    [Serializable]
    public class TagType_InfoList : InfoList_Base<TagType_InfoList, TagType_ListCriteria, TagType_InfoItem, TagType_ItemCriteria>
    {
        #region DataPortal

        private void DataPortal_Fetch (TagType_ListCriteria aCriteria)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // add select option if given
            if (aCriteria.SelectOption_Value.HasValue)
            {
                Insert(0, DataPortal.FetchChild<TagType_InfoItem>(new D_TAG_TYPE
                {
                    selectTxt = aCriteria.SelectOption_Text,
                    objectID  = aCriteria.SelectOption_Value.Value
                }));
            }

            // add elements of list from persistent store
            using (var ctx = DalFactory.GetManager (DalFactory.ARK_COMMON_SCHEMA_NM))
            {
                var dal = ctx.GetProvider<I_TAG_TYPE>();
                var list = dal.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<TagType_InfoItem>(item));
            }

            RaiseListChangedEvents = rlce;
            IsReadOnly = true;
        }

        #endregion
    }

    [Serializable]
    public class TagType_EditItem : EditItem_Base<TagType_EditItem, TagType_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<string> TypeTxt_Property = RegisterProperty<string>(c => c.TypeTxt);
        [Required]
        public string TypeTxt
        {
            get { return GetProperty(TypeTxt_Property); }
            set { SetProperty(TypeTxt_Property, value); }
        }

        public static readonly PropertyInfo<string> DescTxt_Property = RegisterProperty<string>(c => c.DescTxt);
        public string DescTxt
        {
            get { return GetProperty(DescTxt_Property); }
            set { SetProperty(DescTxt_Property, value); }
        }

        public void FromDto (D_TAG_TYPE dto)
        {
            using (BypassPropertyChecks)
            {
                TypeTxt = dto.typeTxt;
                DescTxt = dto.descTxt;

                base.FromDto (dto);
            }
        }

        public D_TAG_TYPE ToDto()
        {
            D_TAG_TYPE dto = new D_TAG_TYPE();

            dto.typeTxt = TypeTxt;
            dto.descTxt = DescTxt;

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

        private void DataPortal_Fetch(TagType_ItemCriteria aKey)
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_COMMON_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_TAG_TYPE>();
                var data = dal.SelectItem(aKey.ToDto());

                FromDto(data);
            }
        }

        private void Child_Fetch(D_TAG_TYPE dto) { FromDto(dto); }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_COMMON_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_TAG_TYPE>();
                var data = dal.InsertItem(ToDto());

                FromDto(data);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_COMMON_SCHEMA_NM))
            {
                UpdateOnDts = DateTime.Now;
                UpdateByUid = AppInfo.UserID;

                var dal = dalManager.GetProvider<I_TAG_TYPE>();
                var data = dal.UpdateItem(ToDto());

                FromDto(data);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_COMMON_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_TAG_TYPE>();

                dal.DeleteItem (new K_TAG_TYPE { objectID = this.ObjectID });
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
    public class TagType_EditItem_Getter : EditItem_Getter_Base<TagType_EditItem, TagType_ItemCriteria>
    {
        #region DataPortal

        protected override void DataPortal_Fetch(TagType_ItemCriteria aCriteria)
        {
            if (aCriteria.HasKey)
                EditItem = TagType_EditItem.GetItem(aCriteria);
            else
                EditItem = TagType_EditItem.NewItem(aCriteria);
        }

        #endregion
    }

    /// <summary>
    /// Editable List
    /// </summary>
    [Serializable]
    public class TagType_EditList : EditList_Base<TagType_EditList, TagType_ListCriteria, TagType_EditItem, TagType_ItemCriteria>
    {
        #region DataPortal

        private void DataPortal_Fetch(TagType_ListCriteria aCriteria)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            using (var ctx = DalFactory.GetManager(DalFactory.ARK_COMMON_SCHEMA_NM))
            {
                var dal = ctx.GetProvider<I_TAG_TYPE>();
                var list = dal.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<TagType_EditItem>(item));
            }

            RaiseListChangedEvents = rlce;
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Update()
        {
            using (var ctx = DalFactory.GetManager(DalFactory.ARK_COMMON_SCHEMA_NM))
            {
                Child_Update();
            }
        }

        #endregion
    }
}
