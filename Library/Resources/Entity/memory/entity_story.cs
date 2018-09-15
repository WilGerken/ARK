using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Library.Common;
using Library.Resources.Common.memory;
using Library.Resources.Story.memory;

namespace Library.Resources.Entity.memory
{
    /// <summary>
    /// data access class
    /// </summary>
    public class ENTITY_STORY : DATA_ACCESS_BASE<D_ENTITY_STORY, F_ENTITY_STORY, K_ENTITY_STORY>, I_ENTITY_STORY
    {
        // resource list
        public static List<D_ENTITY_STORY> ResourceList = new List<D_ENTITY_STORY>();

        static ENTITY_STORY ()
        {
            int lID = Ref.AdminID;

            //ResourceList.Add (new D_ENTITY_STORY { objectID = lID++, entityID = 1, tagID = 1, typeID = 1, createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
            //ResourceList.Add (new D_ENTITY_STORY { objectID = lID++, entityID = 1, tagID = 1, typeID = 2, createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
            //ResourceList.Add (new D_ENTITY_STORY { objectID = lID++, entityID = 1, tagID = 2, typeID = 1, createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
            //ResourceList.Add (new D_ENTITY_STORY { objectID = lID++, entityID = 1, tagID = 3, typeID = 1, createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
        }

        /// <summary>
        /// select a list of items from persistent store for given filter
        /// </summary>
        /// <param name="aFilter"></param>
        /// <returns></returns>
        public List<D_ENTITY_STORY> SelectList (F_ENTITY_STORY aFilter)
        {
            var lResult = (from item in ResourceList
                           join entityItem in ARK_ENTITY.ResourceList on item.entityID equals entityItem.objectID
                           join storyItem in ARK_STORY.ResourceList on item.storyID equals storyItem.objectID
                           from roleItem in ENTITY_STORY_ROLE_TYPE.ResourceList.Where(x => x.objectID == item.roleID).DefaultIfEmpty()
                           from createItem in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.createByUid).DefaultIfEmpty()
                           from updateItem in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.updateByUid).DefaultIfEmpty()
                           select new D_ENTITY_STORY
                           {
                               objectID = item.objectID,
                               entityID = item.entityID,
                               entityNm = item.entityNm,
                               storyID = item.storyID,
                               titleTxt = storyItem.titleTxt,
                               roleID = item.roleID,
                               roleTxt = roleItem.typeTxt,
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

            // apply filter attributes
            if (aFilter.entityID.HasValue)
            {
                lResult = lResult.Where (x => x.entityID == aFilter.entityID.Value);
            }

            if (aFilter.storyID.HasValue)
            {
                lResult = lResult.Where(x => x.storyID == aFilter.storyID.Value);
            }

            if (aFilter.roleID.HasValue)
            {
                lResult = lResult.Where(x => x.roleID == aFilter.roleID.Value);
            }

            // check base criteria
            lResult = CheckBaseCriteria (lResult, aFilter);

            // return result
            return lResult.ToList<D_ENTITY_STORY>();
        }

        /// <summary>
        /// remove all matching items from persistent store
        /// </summary>
        /// <param name="aFilter"></param>
        public void DeleteList (F_ENTITY_STORY aFilter)
        {
            throw new NotImplementedException ("ENTITY_STORY.DeleteList not implemented");
        }

        /// <summary>
        /// select an item from persistent store for given key
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns></returns>
        public D_ENTITY_STORY SelectItem (K_ENTITY_STORY aKey)
        {
            D_ENTITY_STORY lResult = null;

            var lQuery = (from item in ResourceList
                          join entityItem in ARK_ENTITY.ResourceList on item.entityID equals entityItem.objectID
                          join storyItem  in ARK_STORY.ResourceList on item.storyID equals storyItem.objectID
                          from roleItem   in ENTITY_STORY_ROLE_TYPE.ResourceList.Where(x => x.objectID == item.roleID).DefaultIfEmpty()
                          from createItem in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.createByUid).DefaultIfEmpty()
                          from updateItem in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.updateByUid).DefaultIfEmpty()
                          select new D_ENTITY_STORY
                          {
                              objectID = item.objectID,
                              entityID = item.entityID,
                              entityNm = item.entityNm,
                              storyID = item.storyID,
                              titleTxt = storyItem.titleTxt,
                              roleID = item.roleID,
                              roleTxt = roleItem.typeTxt,
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
                throw new DllNotFoundException (string.Format ("ENTITY_STORY Item not found for key {0}", aKey.objectID));

            // return result
            return lResult;
        }

        /// <summary>
        /// insert an item into persistent store
        /// </summary>
        /// <param name="aDto"></param>
        public D_ENTITY_STORY InsertItem (D_ENTITY_STORY aDto)
        {
            int lID = 0;

            if (ResourceList.Count > 0)
                lID = ResourceList.Select (x => x.objectID).Max() + 1;

            // create new item
            D_ENTITY_STORY lItem = new D_ENTITY_STORY
            {
                objectID = lID,
                entityID = aDto.entityID,
                storyID  = aDto.storyID,
                roleID   = aDto.roleID,
                descTxt  = aDto.descTxt,

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
        public D_ENTITY_STORY UpdateItem (D_ENTITY_STORY aDto)
        {
            // fetch indicated item
            D_ENTITY_STORY lItem = ResourceList.Where (x => x.objectID == aDto.objectID).FirstOrDefault();

            // update item
            lock (lItem)
            {
                lItem.entityID = aDto.entityID;
                lItem.storyID  = aDto.storyID;
                lItem.roleID   = aDto.roleID;
                lItem.descTxt  = aDto.descTxt;

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
        public void DeleteItem (K_ENTITY_STORY aKey)
        {
            // fetch indicated item
            D_ENTITY_STORY lItem = ResourceList.Where (x => x.objectID == aKey.objectID).FirstOrDefault();

            // delete item from list
            lock (ResourceList)
            {
                ResourceList.Remove (lItem);
            }
        }
    }
}