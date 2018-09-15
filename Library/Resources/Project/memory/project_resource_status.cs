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
    public class PROJECT_RESOURCE_STATUS : DATA_ACCESS_BASE<D_PROJECT_RESOURCE_STATUS, F_PROJECT_RESOURCE_STATUS, K_PROJECT_RESOURCE_STATUS>, I_PROJECT_RESOURCE_STATUS
    {
        // resource list
        public static List<D_PROJECT_RESOURCE_STATUS> ResourceList = new List<D_PROJECT_RESOURCE_STATUS>();

        static PROJECT_RESOURCE_STATUS()
        {
            int lID = Ref.AdminID;

            //ResourceList.Add (new D_PROJECT_RESOURCE_STATUS{ objectID = lID++, entityID = 1, tagID = 1, typeID = 1, createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
            //ResourceList.Add (new D_PROJECT_RESOURCE_STATUS{ objectID = lID++, entityID = 1, tagID = 1, typeID = 2, createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
            //ResourceList.Add (new D_PROJECT_RESOURCE_STATUS{ objectID = lID++, entityID = 1, tagID = 2, typeID = 1, createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
            //ResourceList.Add (new D_PROJECT_RESOURCE_STATUS{ objectID = lID++, entityID = 1, tagID = 3, typeID = 1, createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
        }

        /// <summary>
        /// select a list of items from persistent store for given filter
        /// </summary>
        /// <param name="aFilter"></param>
        /// <returns></returns>
        public List<D_PROJECT_RESOURCE_STATUS> SelectList (F_PROJECT_RESOURCE_STATUS aFilter)
        {
            var lResult = (from item in ResourceList
                           join resourceItem in PROJECT_RESOURCE.ResourceList on item.resourceID equals resourceItem.objectID
                           join entityItem   in ARK_ENTITY.ResourceList on resourceItem.entityID equals entityItem.objectID
                           join projectItem  in ARK_PROJECT.ResourceList on resourceItem.projectID equals projectItem.objectID
                           join roleItem     in PROJECT_RESOURCE_ROLE_TYPE.ResourceList on resourceItem.roleID equals roleItem.objectID
                           join statusItem   in PROJECT_RESOURCE_STATUS_TYPE.ResourceList on item.statusID equals statusItem.objectID
                           from createItem   in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.createByUid).DefaultIfEmpty()
                           from updateItem   in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.updateByUid).DefaultIfEmpty()
                           select new D_PROJECT_RESOURCE_STATUS
                           {
                               objectID      = item.objectID,
                               entityID      = item.entityID,
                               entityNm      = item.entityNm,
                               projectID     = item.projectID,
                               projectNm     = projectItem.projectNm,
                               roleID        = item.roleID,
                               roleTxt       = roleItem.typeTxt,
                               statusID      = item.statusID,
                               statusTxt     =  statusItem.typeTxt,
                               statusFromDts = item.statusFromDts,
                               descTxt       = item.descTxt,

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
            if (aFilter.entityID.HasValue)
            {
                lResult = lResult.Where (x => x.entityID == aFilter.entityID.Value);
            }

            if (aFilter.projectID.HasValue)
            {
                lResult = lResult.Where(x => x.projectID == aFilter.projectID.Value);
            }

            if (aFilter.roleID.HasValue)
            {
                lResult = lResult.Where(x => x.roleID == aFilter.roleID.Value);
            }

            if (aFilter.statusID.HasValue)
            {
                lResult = lResult.Where(x => x.statusID == aFilter.statusID.Value);
            }

            if (aFilter.fromDts.HasValue)
            {
                lResult = lResult.Where(x => x.statusFromDts == aFilter.fromDts.Value);
            }

            if (aFilter.thruDts.HasValue)
            {
                lResult = lResult.Where(x => x.statusFromDts <= aFilter.thruDts.Value);
            }

            // check base criteria
            lResult = CheckBaseCriteria (lResult, aFilter);

            // return result
            return lResult.ToList<D_PROJECT_RESOURCE_STATUS>();
        }

        /// <summary>
        /// remove all matching items from persistent store
        /// </summary>
        /// <param name="aFilter"></param>
        public void DeleteList (F_PROJECT_RESOURCE_STATUS aFilter)
        {
            throw new NotImplementedException ("PROJECT_RESOURCE_STATUS.DeleteList not implemented");
        }

        /// <summary>
        /// select an item from persistent store for given key
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns></returns>
        public D_PROJECT_RESOURCE_STATUS SelectItem (K_PROJECT_RESOURCE_STATUS aKey)
        {
            D_PROJECT_RESOURCE_STATUS lResult = null;

            var lQuery = (from item in ResourceList
                          join resourceItem in PROJECT_RESOURCE.ResourceList on item.resourceID equals resourceItem.objectID
                          join entityItem in ARK_ENTITY.ResourceList on resourceItem.entityID equals entityItem.objectID
                          join projectItem in ARK_PROJECT.ResourceList on resourceItem.projectID equals projectItem.objectID
                          join roleItem in PROJECT_RESOURCE_ROLE_TYPE.ResourceList on resourceItem.roleID equals roleItem.objectID
                          join statusItem in PROJECT_RESOURCE_STATUS_TYPE.ResourceList on item.statusID equals statusItem.objectID
                          from createItem in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.createByUid).DefaultIfEmpty()
                          from updateItem in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.updateByUid).DefaultIfEmpty()
                          select new D_PROJECT_RESOURCE_STATUS
                          {
                              objectID = item.objectID,
                              entityID = item.entityID,
                              entityNm = item.entityNm,
                              projectID = item.projectID,
                              projectNm = projectItem.projectNm,
                              roleID = item.roleID,
                              roleTxt = roleItem.typeTxt,
                              statusID = item.statusID,
                              statusTxt = statusItem.typeTxt,
                              statusFromDts = item.statusFromDts,
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
                throw new DllNotFoundException (string.Format ("PROJECT_RESOURCE_STATUS Item not found for key {0}", aKey.objectID));

            // return result
            return lResult;
        }

        /// <summary>
        /// insert an item into persistent store
        /// </summary>
        /// <param name="aDto"></param>
        public D_PROJECT_RESOURCE_STATUS InsertItem (D_PROJECT_RESOURCE_STATUS aDto)
        {
            int lID = 0;

            if (ResourceList.Count > 0)
                lID = ResourceList.Select (x => x.objectID).Max() + 1;

            // create new item
            D_PROJECT_RESOURCE_STATUS lItem = new D_PROJECT_RESOURCE_STATUS
            {
                objectID      = lID,
                resourceID    = aDto.resourceID,
                statusID      = aDto.statusID,
                statusFromDts = aDto.statusFromDts,
                descTxt       = aDto.descTxt,

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
        public D_PROJECT_RESOURCE_STATUS UpdateItem (D_PROJECT_RESOURCE_STATUS aDto)
        {
            // fetch indicated item
            D_PROJECT_RESOURCE_STATUS lItem = ResourceList.Where (x => x.objectID == aDto.objectID).FirstOrDefault();

            // update item
            lock (lItem)
            {
                lItem.entityID      = aDto.entityID;
                lItem.resourceID    = aDto.resourceID;
                lItem.statusID      = aDto.statusID;
                lItem.statusFromDts = aDto.statusFromDts;
                lItem.descTxt       = aDto.descTxt;

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
        public void DeleteItem (K_PROJECT_RESOURCE_STATUS aKey)
        {
            // fetch indicated item
            D_PROJECT_RESOURCE_STATUS lItem = ResourceList.Where (x => x.objectID == aKey.objectID).FirstOrDefault();

            // delete item from list
            lock (ResourceList)
            {
                ResourceList.Remove (lItem);
            }
        }
    }
}