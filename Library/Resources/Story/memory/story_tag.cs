using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Library.Common;
using Library.Resources.Story.memory;
using Library.Resources.Common.memory;
using Library.Resources.Entity.memory;

namespace Library.Resources.Story.memory
{
    /// <summary>
    /// data access class
    /// </summary>
    public class STORY_TAG : DATA_ACCESS_BASE<D_STORY_TAG, F_STORY_TAG, K_STORY_TAG>, I_STORY_TAG
    {
        // resource list
        public static List<D_STORY_TAG> ResourceList = new List<D_STORY_TAG>();

        static STORY_TAG ()
        {
            //int lID = Ref.AdminID;

            //ResourceList.Add (new D_STORY_TAG { objectID = lID++, storyID = 1, tagTxt = "Water",           createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
            //ResourceList.Add (new D_STORY_TAG { objectID = lID++, storyID = 1, tagTxt = "English",         createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
            //ResourceList.Add (new D_STORY_TAG { objectID = lID++, storyID = 1, tagTxt = "Spanish",         createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
            //ResourceList.Add (new D_STORY_TAG { objectID = lID++, storyID = 1, tagTxt = "German",          createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
            //ResourceList.Add (new D_STORY_TAG { objectID = lID++, storyID = 1, tagTxt = "Plumber",         createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
            //ResourceList.Add (new D_STORY_TAG { objectID = lID++, storyID = 1, tagTxt = "Carpenter",       createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
            //ResourceList.Add (new D_STORY_TAG { objectID = lID++, storyID = 1, tagTxt = "Project Manager", createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
            //ResourceList.Add (new D_STORY_TAG { objectID = lID++, storyID = 1, tagTxt = "Client",          createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
        }

        /// <summary>
        /// select a list of items from persistent store for given filter
        /// </summary>
        /// <param name="aFilter"></param>
        /// <returns></returns>
        public List<D_STORY_TAG> SelectList (F_STORY_TAG aFilter)
        {
            var lResult = (from item in ResourceList
                           join storyItem in ARK_STORY.ResourceList on item.storyID equals storyItem.objectID
                           join tagItem in ARK_TAG.ResourceList on item.tagID equals tagItem.objectID
                           from typeItem in TAG_TYPE.ResourceList.Where(x => x.objectID == item.typeID).DefaultIfEmpty()
                           from createItem in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.createByUid).DefaultIfEmpty()
                           from updateItem in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.updateByUid).DefaultIfEmpty()
                           select new D_STORY_TAG
                           {
                               objectID = item.objectID,
                               storyID = item.storyID,
                               titleTxt = storyItem.titleTxt,
                               tagID = item.tagID,
                               tagTxt = tagItem.tagTxt,
                               typeID = item.typeID,
                               typeTxt = typeItem != null ? typeItem.typeTxt : string.Empty,

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
            if (aFilter.storyID.HasValue)
            {
                lResult = lResult.Where (x => x.storyID == aFilter.storyID.Value);
            }

            if (aFilter.tagID.HasValue)
            {
                lResult = lResult.Where(x => x.tagID == aFilter.tagID.Value);
            }

            if (aFilter.typeID.HasValue)
            {
                lResult = lResult.Where(x => x.typeID == aFilter.typeID.Value);
            }

            // check base criteria
            lResult = CheckBaseCriteria (lResult, aFilter);

            // return result
            return lResult.ToList<D_STORY_TAG>();
        }

        /// <summary>
        /// remove all matching items from persistent store
        /// </summary>
        /// <param name="aFilter"></param>
        public void DeleteList (F_STORY_TAG aFilter)
        {
            throw new NotImplementedException ("STORY_TAG.DeleteList not implemented");
        }

        /// <summary>
        /// select an item from persistent store for given key
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns></returns>
        public D_STORY_TAG SelectItem (K_STORY_TAG aKey)
        {
            D_STORY_TAG lResult = null;

            var lQuery = (from item in ResourceList
                           join storyItem in ARK_STORY.ResourceList on item.storyID equals storyItem.objectID
                           join tagItem in ARK_TAG.ResourceList on item.tagID equals tagItem.objectID
                           from typeItem in TAG_TYPE.ResourceList.Where(x => x.objectID == item.typeID).DefaultIfEmpty()
                           from createItem in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.createByUid).DefaultIfEmpty()
                           from updateItem in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.updateByUid).DefaultIfEmpty()
                           select new D_STORY_TAG
                           {
                               objectID = item.objectID,
                               storyID = item.storyID,
                               titleTxt = storyItem.titleTxt,
                               tagID = item.tagID,
                               tagTxt = tagItem.tagTxt,
                               typeID = item.typeID,
                               typeTxt = typeItem != null ? typeItem.typeTxt : string.Empty,

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
                throw new DllNotFoundException (string.Format ("STORY_TAG Item not found for key {0}", aKey.objectID));

            // return result
            return lResult;
        }

        /// <summary>
        /// insert an item into persistent store
        /// </summary>
        /// <param name="aDto"></param>
        public D_STORY_TAG InsertItem (D_STORY_TAG aDto)
        {
            int lID = 0;

            if (ResourceList.Count > 0)
                lID = ResourceList.Select (x => x.objectID).Max() + 1;

            // create new item
            D_STORY_TAG lItem = new D_STORY_TAG
            {
                objectID  = lID,
                storyID   = aDto.storyID,
                tagID     = aDto.tagID,
                typeID    = aDto.typeID,

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
        public D_STORY_TAG UpdateItem (D_STORY_TAG aDto)
        {
            // fetch indicated item
            D_STORY_TAG lItem = ResourceList.Where (x => x.objectID == aDto.objectID).FirstOrDefault();

            // update item
            lock (lItem)
            {
                lItem.storyID   = aDto.storyID;
                lItem.tagID     = aDto.tagID;
                lItem.typeID    = aDto.typeID;

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
        public void DeleteItem (K_STORY_TAG aKey)
        {
            // fetch indicated item
            D_STORY_TAG lItem = ResourceList.Where(x => x.objectID == aKey.objectID).FirstOrDefault();

            // delete item from list
            lock (ResourceList)
            {
                ResourceList.Remove (lItem);
            }
        }
    }
}