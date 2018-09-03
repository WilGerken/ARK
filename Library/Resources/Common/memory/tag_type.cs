using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Library.Common;

namespace Library.Resources.Common.memory
{
    /// <summary>
    /// data access class
    /// </summary>
    public class TAG_TYPE : DATA_ACCESS_BASE<D_TAG_TYPE, F_TAG_TYPE, K_TAG_TYPE>, I_TAG_TYPE
    {
        // resource list
        public static List<D_TAG_TYPE> ResourceList = new List<D_TAG_TYPE>();

        static TAG_TYPE ()
        {
            int lID = 1;

            ResourceList.Add (new D_TAG_TYPE { objectID = lID++, typeTxt = "Craft",    createByUid = 1, updateByUid = 1 });
            ResourceList.Add (new D_TAG_TYPE { objectID = lID++, typeTxt = "Language", createByUid = 1, updateByUid = 1 });
            ResourceList.Add (new D_TAG_TYPE { objectID = lID++, typeTxt = "Goal",     createByUid = 1, updateByUid = 1 });
            ResourceList.Add (new D_TAG_TYPE { objectID = lID++, typeTxt = "Location", createByUid = 1, updateByUid = 1 });
            ResourceList.Add (new D_TAG_TYPE { objectID = lID++, typeTxt = "Resource", createByUid = 1, updateByUid = 1 });
            ResourceList.Add (new D_TAG_TYPE { objectID = lID++, typeTxt = "Material", createByUid = 1, updateByUid = 1 });
        }

        /// <summary>
        /// select a list of items from persistent store for given filter
        /// </summary>
        /// <param name="aFilter"></param>
        /// <returns></returns>
        public List<D_TAG_TYPE> SelectList (F_TAG_TYPE aFilter)
        {
            IEnumerable<D_TAG_TYPE> lResult = ResourceList;

            // apply filter attributes
            if (! string.IsNullOrEmpty (aFilter.typeTxt))
            {
                lResult = lResult.Where (x => x.typeTxt.Contains (aFilter.typeTxt));
            }

            // check base criteria
            lResult = CheckBaseCriteria (lResult, aFilter);

            // return result
            return lResult.ToList<D_TAG_TYPE>();
        }

        /// <summary>
        /// remove all matching items from persistent store
        /// </summary>
        /// <param name="aFilter"></param>
        public void DeleteList (F_TAG_TYPE aFilter)
        {
            throw new NotImplementedException ("TAG_TYPE.DeleteList not implemented");
        }

        /// <summary>
        /// select an item from persistent store for given key
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns></returns>
        public D_TAG_TYPE SelectItem (K_TAG_TYPE aKey)
        {
            D_TAG_TYPE lResult = null;

            // apply key attributes
            if (aKey.objectID.HasValue)
                lResult = ResourceList.Where (x => x.objectID == aKey.objectID).FirstOrDefault();

            // throw exception if not found
            if (lResult == null)
                throw new DllNotFoundException (string.Format ("TAG_TYPE Item not found for key {0}", aKey.objectID));

            // return result
            return lResult;
        }

        /// <summary>
        /// insert an item into persistent store
        /// </summary>
        /// <param name="aDto"></param>
        public D_TAG_TYPE InsertItem (D_TAG_TYPE aDto)
        {
            int lID = 0;

            if (ResourceList.Count > 0)
                lID = ResourceList.Select (x => x.objectID).Max() + 1;

            // create new item
            D_TAG_TYPE lItem = new D_TAG_TYPE
            {
                objectID    = lID,
                typeTxt     = aDto.typeTxt,
                descTxt     = aDto.descTxt,

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
        public D_TAG_TYPE UpdateItem (D_TAG_TYPE aDto)
        {
            // fetch indicated item
            D_TAG_TYPE lItem = ResourceList.Where (x => x.objectID == aDto.objectID).FirstOrDefault();

            // update item
            lock (lItem)
            {
                lItem.typeTxt = aDto.typeTxt;
                lItem.descTxt = aDto.descTxt;

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
        public void DeleteItem (K_TAG_TYPE aKey)
        {
            // fetch indicated item
            D_TAG_TYPE lItem = ResourceList.Where(x => x.objectID == aKey.objectID).FirstOrDefault();

            // delete item from list
            lock (ResourceList)
            {
                ResourceList.Remove (lItem);
            }
        }
    }
}