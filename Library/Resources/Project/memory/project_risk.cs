using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Library.Common;
using Library.Resources.Common.memory;
using Library.Resources.Entity.memory;

namespace Library.Resources.Project.memory
{
    /// <summary>
    /// data access class
    /// </summary>
    public class PROJECT_RISK : DATA_ACCESS_BASE<D_PROJECT_RISK, F_PROJECT_RISK, K_PROJECT_RISK>, I_PROJECT_RISK
    {
        // resource list
        public static List<D_PROJECT_RISK> ResourceList = new List<D_PROJECT_RISK>();

        static PROJECT_RISK()
        {
            int lID = Ref.AdminID;

            //ResourceList.Add (new D_PROJECT_RISK{ objectID = lID++, entityID = 1, tagID = 1, typeID = 1, createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
            //ResourceList.Add (new D_PROJECT_RISK{ objectID = lID++, entityID = 1, tagID = 1, typeID = 2, createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
            //ResourceList.Add (new D_PROJECT_RISK{ objectID = lID++, entityID = 1, tagID = 2, typeID = 1, createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
            //ResourceList.Add (new D_PROJECT_RISK{ objectID = lID++, entityID = 1, tagID = 3, typeID = 1, createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
        }

        /// <summary>
        /// select a list of items from persistent store for given filter
        /// </summary>
        /// <param name="aFilter"></param>
        /// <returns></returns>
        public List<D_PROJECT_RISK> SelectList (F_PROJECT_RISK aFilter)
        {
            var lResult = (from item in ResourceList
                           join projectItem in ARK_PROJECT.ResourceList on item.projectID equals projectItem.objectID
                           join typeItem    in PROJECT_RISK_TYPE.ResourceList on item.typeID equals typeItem.objectID
                           from createItem  in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.createByUid).DefaultIfEmpty()
                           from updateItem  in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.updateByUid).DefaultIfEmpty()
                           select new D_PROJECT_RISK
                           {
                               objectID  = item.objectID,
                               projectID = item.projectID,
                               projectNm = projectItem.projectNm,
                               typeID    = item.typeID,
                               typeTxt   = typeItem.typeTxt,
                               titleTxt = item.titleTxt,
                               descTxt   = item.descTxt,

                               activeYn = item.activeYn,
                               createByUid = item.createByUid,
                               createByNm = createItem != null ? createItem.entityNm : string.Empty,
                               createOnDts = item.createOnDts,
                               updateByUid = item.updateByUid,
                               updateByNm = updateItem != null ? updateItem.entityNm : string.Empty,
                               updateOnDts = item.updateOnDts,
                               versionKey = item.versionKey
                           });

            // apply filter attributes
            if (aFilter.projectID.HasValue)
            {
                lResult = lResult.Where(x => x.projectID == aFilter.projectID.Value);
            }

            if (aFilter.typeID.HasValue)
            {
                lResult = lResult.Where(x => x.typeID == aFilter.typeID.Value);
            }

            // check base criteria
            lResult = CheckBaseCriteria (lResult, aFilter);

            // return result
            return lResult.ToList<D_PROJECT_RISK>();
        }

        /// <summary>
        /// remove all matching items from persistent store
        /// </summary>
        /// <param name="aFilter"></param>
        public void DeleteList (F_PROJECT_RISK aFilter)
        {
            throw new NotImplementedException ("PROJECT_RISK.DeleteList not implemented");
        }

        /// <summary>
        /// select an item from persistent store for given key
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns></returns>
        public D_PROJECT_RISK SelectItem (K_PROJECT_RISK aKey)
        {
            D_PROJECT_RISK lResult = null;

            var lQuery = (from item in ResourceList
                          join projectItem in ARK_PROJECT.ResourceList on item.projectID equals projectItem.objectID
                          join typeItem in PROJECT_RISK_TYPE.ResourceList on item.typeID equals typeItem.objectID
                          from createItem in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.createByUid).DefaultIfEmpty()
                          from updateItem in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.updateByUid).DefaultIfEmpty()
                          select new D_PROJECT_RISK
                          {
                              objectID = item.objectID,
                              projectID = item.projectID,
                              projectNm = projectItem.projectNm,
                              typeID = item.typeID,
                              typeTxt = typeItem.typeTxt,
                              titleTxt = item.titleTxt,
                              descTxt = item.descTxt,

                              activeYn = item.activeYn,
                              createByUid = item.createByUid,
                              createByNm = createItem != null ? createItem.entityNm : string.Empty,
                              createOnDts = item.createOnDts,
                              updateByUid = item.updateByUid,
                              updateByNm = updateItem != null ? updateItem.entityNm : string.Empty,
                              updateOnDts = item.updateOnDts,
                              versionKey = item.versionKey
                          });

            // apply key attributes
            if (aKey.objectID.HasValue)
                lResult = lQuery.Where (x => x.objectID == aKey.objectID).FirstOrDefault();

            // throw exception if not found
            if (lResult == null)
                throw new DllNotFoundException (string.Format ("PROJECT_RISK Item not found for key {0}", aKey.objectID));

            // return result
            return lResult;
        }

        /// <summary>
        /// insert an item into persistent store
        /// </summary>
        /// <param name="aDto"></param>
        public D_PROJECT_RISK InsertItem (D_PROJECT_RISK aDto)
        {
            int lID = 0;

            if (ResourceList.Count > 0)
                lID = ResourceList.Select (x => x.objectID).Max() + 1;

            // create new item
            D_PROJECT_RISK lItem = new D_PROJECT_RISK
            {
                objectID  = lID,
                projectID = aDto.projectID,
                typeID    = aDto.typeID,
                titleTxt = aDto.titleTxt,
                descTxt   = aDto.descTxt,

                activeYn    = aDto.activeYn,
                createByUid = aDto.createByUid,
                createOnDts = aDto.createOnDts,
                updateByUid = aDto.updateByUid,
                updateOnDts = aDto.updateOnDts,
            };

            // insert new item into list
            lock (ResourceList)
            {
                ResourceList.Add (lItem);
            }

            return aDto;
        }

        /// <summary>
        /// update an item in persistent store
        /// </summary>
        /// <param name="aDto"></param>
        public D_PROJECT_RISK UpdateItem (D_PROJECT_RISK aDto)
        {
            // fetch indicated item
            D_PROJECT_RISK lItem = ResourceList.Where (x => x.objectID == aDto.objectID).FirstOrDefault();

            // update item
            lock (lItem)
            {
                lItem.projectID = aDto.projectID;
                lItem.typeID    = aDto.typeID;
                lItem.titleTxt  = aDto.titleTxt;
                lItem.descTxt   = aDto.descTxt;

                lItem.activeYn    = aDto.activeYn;
                lItem.createByUid = aDto.createByUid;
                lItem.createOnDts = aDto.createOnDts;
                lItem.updateByUid = aDto.updateByUid;
                lItem.updateOnDts = aDto.updateOnDts;
            }

            return aDto;
        }

        /// <summary>
        /// remove an item from persistent store
        /// </summary>
        /// <param name="aKey"></param>
        public void DeleteItem (K_PROJECT_RISK aKey)
        {
            // fetch indicated item
            D_PROJECT_RISK lItem = ResourceList.Where (x => x.objectID == aKey.objectID).FirstOrDefault();

            // delete item from list
            lock (ResourceList)
            {
                ResourceList.Remove (lItem);
            }
        }
    }
}