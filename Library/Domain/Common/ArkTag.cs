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
    public class ArkTag_ItemCriteria : ItemCriteria_Base<ArkTag_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<string> TagTxt_Property = RegisterProperty<string>(c => c.TagTxt);
        public string TagTxt
        {
            get { return ReadProperty(TagTxt_Property); }
            set { LoadProperty(TagTxt_Property, value); }
        }

        public K_ARK_TAG ToDto()
        {
            K_ARK_TAG dto = new K_ARK_TAG();

            dto.tagTxt = TagTxt;

            base.ToDto(dto);

            return dto;
        }

        #endregion
    }

    /// <summary>
    /// List Criteria
    /// </summary>
    [Serializable]
    public class ArkTag_ListCriteria : ListCriteria_Base<ArkTag_ListCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<string> TagTxt_Property = RegisterProperty<string>(c => c.TagTxt);
        public string TagTxt
        {
            get { return ReadProperty(TagTxt_Property); }
            set { LoadProperty(TagTxt_Property, value); }
        }

        public F_ARK_TAG ToDto()
        {
            F_ARK_TAG dto = new F_ARK_TAG();

            dto.tagTxt   = TagTxt;

            base.ToDto (dto);

            return dto;
        }

        #endregion
    }

    /// <summary>
    /// ReadOnly Item
    /// </summary>
    [Serializable]
    public class ArkTag_InfoItem : InfoItem_Base<ArkTag_InfoItem, ArkTag_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<string> TagTxt_Property = RegisterProperty<string>(c => c.TagTxt);
        public string TagTxt
        {
            get { return ReadProperty(TagTxt_Property); }
            private set { LoadProperty(TagTxt_Property, value); }
        }

        public static readonly PropertyInfo<string> DescTxt_Property = RegisterProperty<string>(c => c.TagTxt);
        public string DescTxt
        {
            get { return ReadProperty(DescTxt_Property); }
            private set { LoadProperty(DescTxt_Property, value); }
        }

        public void FromDto (D_ARK_TAG dto)
        {
            TagTxt   = dto.tagTxt;
            DescTxt  = dto.descTxt;

            base.FromDto (dto);
        }

        #endregion

        #region DataPortal

        private void Child_Fetch(D_ARK_TAG dto) { FromDto(dto); }

        #endregion
    }

    /// <summary>
    /// ReadOnly List
    /// </summary>
    [Serializable]
    public class ArkTag_InfoList : InfoList_Base<ArkTag_InfoList, ArkTag_ListCriteria, ArkTag_InfoItem, ArkTag_ItemCriteria>
    {
        #region DataPortal

        private void DataPortal_Fetch (ArkTag_ListCriteria aCriteria)
        {
            IsReadOnly = false;
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            // add select option if given
            if (aCriteria.SelectOption_Value.HasValue)
            {
                Insert(0, DataPortal.FetchChild<ArkTag_InfoItem>(new D_ARK_TAG
                {
                    selectTxt = aCriteria.SelectOption_Text,
                    objectID  = aCriteria.SelectOption_Value.Value
                }));
            }

            // add elements of list from persistent store
            using (var ctx = DalFactory.GetManager (DalFactory.ARK_COMMON_SCHEMA_NM))
            {
                var dal = ctx.GetProvider<I_ARK_TAG>();
                var list = dal.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<ArkTag_InfoItem>(item));
            }

            RaiseListChangedEvents = rlce;
            IsReadOnly = true;
        }

        #endregion
    }

    [Serializable]
    public class ArkTag_EditItem : EditItem_Base<ArkTag_EditItem, ArkTag_ItemCriteria>
    {
        #region Properties

        public static readonly PropertyInfo<string> TagTxt_Property = RegisterProperty<string>(c => c.TagTxt);
        [Required]
        public string TagTxt
        {
            get { return GetProperty(TagTxt_Property); }
            set { SetProperty(TagTxt_Property, value); }
        }

        public static readonly PropertyInfo<string> DescTxt_Property = RegisterProperty<string>(c => c.DescTxt);
        public string DescTxt
        {
            get { return GetProperty(DescTxt_Property); }
            set { SetProperty(DescTxt_Property, value); }
        }

        public void FromDto (D_ARK_TAG dto)
        {
            using (BypassPropertyChecks)
            {
                TagTxt  = dto.tagTxt;
                DescTxt = dto.descTxt;

                base.FromDto (dto);
            }
        }

        public D_ARK_TAG ToDto()
        {
            D_ARK_TAG dto = new D_ARK_TAG();

            dto.tagTxt  = TagTxt;
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

        private void DataPortal_Fetch(ArkTag_ItemCriteria aKey)
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_COMMON_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_ARK_TAG>();
                var data = dal.SelectItem(aKey.ToDto());

                FromDto(data);
            }
        }

        private void Child_Fetch(D_ARK_TAG dto) { FromDto(dto); }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_Insert()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_COMMON_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_ARK_TAG>();
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

                var dal = dalManager.GetProvider<I_ARK_TAG>();
                var data = dal.UpdateItem(ToDto());

                FromDto(data);
            }
        }

        [Transactional(TransactionalTypes.TransactionScope)]
        protected override void DataPortal_DeleteSelf()
        {
            using (var dalManager = DalFactory.GetManager(DalFactory.ARK_COMMON_SCHEMA_NM))
            {
                var dal = dalManager.GetProvider<I_ARK_TAG>();

                dal.DeleteItem (new K_ARK_TAG { objectID = this.ObjectID });
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
    public class ArkTag_EditItem_Getter : EditItem_Getter_Base<ArkTag_EditItem, ArkTag_ItemCriteria>
    {
        #region DataPortal

        protected override void DataPortal_Fetch(ArkTag_ItemCriteria aCriteria)
        {
            if (aCriteria.HasKey)
                EditItem = ArkTag_EditItem.GetItem(aCriteria);
            else
                EditItem = ArkTag_EditItem.NewItem(aCriteria);
        }

        #endregion
    }

    /// <summary>
    /// Editable List
    /// </summary>
    [Serializable]
    public class ArkTag_EditList : EditList_Base<ArkTag_EditList, ArkTag_ListCriteria, ArkTag_EditItem, ArkTag_ItemCriteria>
    {
        #region DataPortal

        private void DataPortal_Fetch(ArkTag_ListCriteria aCriteria)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;

            using (var ctx = DalFactory.GetManager(DalFactory.ARK_COMMON_SCHEMA_NM))
            {
                var dal = ctx.GetProvider<I_ARK_TAG>();
                var list = dal.SelectList(aCriteria.ToDto());

                foreach (var item in list)
                    Add(DataPortal.FetchChild<ArkTag_EditItem>(item));
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
