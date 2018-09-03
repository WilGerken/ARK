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
    public class ARK_TAG : DATA_ACCESS_BASE<D_ARK_TAG, F_ARK_TAG, K_ARK_TAG>, I_ARK_TAG
    {
        // resource list
        public static List<D_ARK_TAG> ResourceList = new List<D_ARK_TAG>();

        static ARK_TAG ()
        {
            int lID = Ref.AdminID;

            ResourceList.Add (new D_ARK_TAG { objectID = lID++, tagTxt = "Water",           createByUid = 1, updateByUid = 1 });
            ResourceList.Add (new D_ARK_TAG { objectID = lID++, tagTxt = "English",         createByUid = 1, updateByUid = 1 });
            ResourceList.Add (new D_ARK_TAG { objectID = lID++, tagTxt = "Spanish",         createByUid = 1, updateByUid = 1 });
            ResourceList.Add (new D_ARK_TAG { objectID = lID++, tagTxt = "German",          createByUid = 1, updateByUid = 1 });
            ResourceList.Add (new D_ARK_TAG { objectID = lID++, tagTxt = "Plumber",         createByUid = 1, updateByUid = 1 });
            ResourceList.Add (new D_ARK_TAG { objectID = lID++, tagTxt = "Carpenter",       createByUid = 1, updateByUid = 1 });
            ResourceList.Add (new D_ARK_TAG { objectID = lID++, tagTxt = "Project Manager", createByUid = 1, updateByUid = 1 });
            ResourceList.Add (new D_ARK_TAG { objectID = lID++, tagTxt = "Client",          createByUid = 1, updateByUid = 1 });
        }

        /// <summary>
        /// select a list of items from persistent store for given filter
        /// </summary>
        /// <param name="aFilter"></param>
        /// <returns></returns>
        public List<D_ARK_TAG> SelectList (F_ARK_TAG aFilter)
        {
            IEnumerable<D_ARK_TAG> lResult = ResourceList;

            // apply filter attributes
            if (! string.IsNullOrEmpty (aFilter.tagTxt))
            {
                lResult = lResult.Where (x => x.tagTxt.Contains (aFilter.tagTxt));
            }

            // check base criteria
            lResult = CheckBaseCriteria (lResult, aFilter);

            // return result
            return lResult.ToList<D_ARK_TAG>();
        }

        /// <summary>
        /// remove all matching items from persistent store
        /// </summary>
        /// <param name="aFilter"></param>
        public void DeleteList (F_ARK_TAG aFilter)
        {
            throw new NotImplementedException ("ARK_TAG.DeleteList not implemented");
        }

        /// <summary>
        /// select an item from persistent store for given key
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns></returns>
        public D_ARK_TAG SelectItem (K_ARK_TAG aKey)
        {
            D_ARK_TAG lResult = null;

            // apply key attributes
            if (aKey.objectID.HasValue)
                lResult = ResourceList.Where (x => x.objectID == aKey.objectID).FirstOrDefault();

            // throw exception if not found
            if (lResult == null)
                throw new DllNotFoundException (string.Format ("ARK_TAG Item not found for key {0}", aKey.objectID));

            // return result
            return lResult;
        }

        /// <summary>
        /// insert an item into persistent store
        /// </summary>
        /// <param name="aDto"></param>
        public D_ARK_TAG InsertItem (D_ARK_TAG aDto)
        {
            int lID = 0;

            if (ResourceList.Count > 0)
                lID = ResourceList.Select (x => x.objectID).Max() + 1;

            // create new item
            D_ARK_TAG lItem = new D_ARK_TAG
            {
                objectID = lID,
                tagTxt   = aDto.tagTxt,
                descTxt  = aDto.descTxt,

                activeYn = aDto.activeYn,
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
        public D_ARK_TAG UpdateItem (D_ARK_TAG aDto)
        {
            // fetch indicated item
            D_ARK_TAG lItem = ResourceList.Where (x => x.objectID == aDto.objectID).FirstOrDefault();

            // update item
            lock (lItem)
            {
                lItem.tagTxt  = aDto.tagTxt;
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
        public void DeleteItem (K_ARK_TAG aKey)
        {
            // fetch indicated item
            D_ARK_TAG lItem = ResourceList.Where(x => x.objectID == aKey.objectID).FirstOrDefault();

            // delete item from list
            lock (ResourceList)
            {
                ResourceList.Remove (lItem);
            }
        }
    }
}