using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Library.Common;

namespace Library.Resources.Entity.memory
{
    /// <summary>
    /// data access class
    /// </summary>
    public class ENTITY_TAG : DATA_ACCESS_BASE<D_ENTITY_TAG, F_ENTITY_TAG, K_ENTITY_TAG>, I_ENTITY_TAG
    {
        // resource list
        public static List<D_ENTITY_TAG> ResourceList = new List<D_ENTITY_TAG>();

        static ENTITY_TAG ()
        {
            //int lID = Ref.AdminID;

            //ResourceList.Add (new D_ENTITY_TAG { objectID = lID++, entityID = 1, tagTxt = "Water",           createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
            //ResourceList.Add (new D_ENTITY_TAG { objectID = lID++, entityID = 1, tagTxt = "English",         createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
            //ResourceList.Add (new D_ENTITY_TAG { objectID = lID++, entityID = 1, tagTxt = "Spanish",         createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
            //ResourceList.Add (new D_ENTITY_TAG { objectID = lID++, entityID = 1, tagTxt = "German",          createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
            //ResourceList.Add (new D_ENTITY_TAG { objectID = lID++, entityID = 1, tagTxt = "Plumber",         createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
            //ResourceList.Add (new D_ENTITY_TAG { objectID = lID++, entityID = 1, tagTxt = "Carpenter",       createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
            //ResourceList.Add (new D_ENTITY_TAG { objectID = lID++, entityID = 1, tagTxt = "Project Manager", createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
            //ResourceList.Add (new D_ENTITY_TAG { objectID = lID++, entityID = 1, tagTxt = "Client",          createByUid = Ref.AdminID, updateByUid = Ref.AdminID });
        }

        /// <summary>
        /// select a list of items from persistent store for given filter
        /// </summary>
        /// <param name="aFilter"></param>
        /// <returns></returns>
        public List<D_ENTITY_TAG> SelectList (F_ENTITY_TAG aFilter)
        {
            IEnumerable<D_ENTITY_TAG> lResult = ResourceList;

            // apply filter attributes
            if (aFilter.entityID.HasValue)
            {
                lResult = lResult.Where (x => x.entityID == aFilter.entityID.Value);
            }

            if (aFilter.tagID.HasValue)
            {
                lResult = lResult.Where(x => x.tagID == aFilter.tagID.Value);
            }

            if (! string.IsNullOrEmpty (aFilter.typeTxt))
            {
                lResult = lResult.Where (x => x.typeTxt.Contains (aFilter.typeTxt));
            }

            // check base criteria
            lResult = CheckBaseCriteria (lResult, aFilter);

            // return result
            return lResult.ToList<D_ENTITY_TAG>();
        }

        /// <summary>
        /// remove all matching items from persistent store
        /// </summary>
        /// <param name="aFilter"></param>
        public void DeleteList (F_ENTITY_TAG aFilter)
        {
            throw new NotImplementedException ("ENTITY_TAG.DeleteList not implemented");
        }

        /// <summary>
        /// select an item from persistent store for given key
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns></returns>
        public D_ENTITY_TAG SelectItem (K_ENTITY_TAG aKey)
        {
            D_ENTITY_TAG lResult = null;

            // apply key attributes
            if (aKey.objectID.HasValue)
                lResult = ResourceList.Where (x => x.objectID == aKey.objectID).FirstOrDefault();

            // throw exception if not found
            if (lResult == null)
                throw new DllNotFoundException (string.Format ("ENTITY_TAG Item not found for key {0}", aKey.objectID));

            // return result
            return lResult;
        }

        /// <summary>
        /// insert an item into persistent store
        /// </summary>
        /// <param name="aDto"></param>
        public D_ENTITY_TAG InsertItem (D_ENTITY_TAG aDto)
        {
            int lID = 0;

            if (ResourceList.Count > 0)
                lID = ResourceList.Select (x => x.objectID).Max() + 1;

            // create new item
            D_ENTITY_TAG lItem = new D_ENTITY_TAG
            {
                objectID = lID,
                entityID = aDto.entityID,
                tagID    = aDto.tagID,
                typeTxt  = aDto.typeTxt,

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
        public D_ENTITY_TAG UpdateItem (D_ENTITY_TAG aDto)
        {
            // fetch indicated item
            D_ENTITY_TAG lItem = ResourceList.Where (x => x.objectID == aDto.objectID).FirstOrDefault();

            // update item
            lock (lItem)
            {
                lItem.entityID = aDto.entityID;
                lItem.tagID    = aDto.tagID;
                lItem.typeTxt  = aDto.typeTxt;

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
        public void DeleteItem (K_ENTITY_TAG aKey)
        {
            // fetch indicated item
            D_ENTITY_TAG lItem = ResourceList.Where (x => x.objectID == aKey.objectID).FirstOrDefault();

            // delete item from list
            lock (ResourceList)
            {
                ResourceList.Remove (lItem);
            }
        }
    }
}