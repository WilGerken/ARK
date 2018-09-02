using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Library.Common;

namespace Library.Resources.Story.memory
{
    /// <summary>
    /// data access class
    /// </summary>
    public class ARK_BLOG : DATA_ACCESS_BASE<D_ARK_BLOG, F_ARK_BLOG, K_ARK_BLOG>, I_ARK_BLOG
    {
        // resource list

        public static List<D_ARK_BLOG> _ResourceList = new List<D_ARK_BLOG>();

        static ARK_BLOG ()
        {
            int lID = 1;

            _ResourceList.Add (new D_ARK_BLOG
            {
                objectID     = lID++,
                entityID     = 1,
                entryDts     = DateTime.Now.AddDays(-7),
                titleTxt     = "My Trip - Day One",
                narrativeTxt = "First day, Great Start."
            });
            _ResourceList.Add(new D_ARK_BLOG
            {
                objectID     = lID++,
                entityID     = 1,
                entryDts     = DateTime.Now.AddDays(-6),
                titleTxt     = "My Trip - Day Two",
                narrativeTxt = "Second day, sunshine."
            });
            _ResourceList.Add(new D_ARK_BLOG
            {
                objectID     = lID++,
                entityID     = 1,
                entryDts     = DateTime.Now.AddDays(-5),
                titleTxt     = "My Trip - Day Three",
                narrativeTxt = "Third day, rainy."
            });
            _ResourceList.Add(new D_ARK_BLOG
            {
                objectID     = lID++,
                entityID     = 1,
                entryDts     = DateTime.Now.AddDays(-5),
                titleTxt     = "My Trip - Day Four",
                narrativeTxt = "Last day, heading home."
            });
            _ResourceList.Add(new D_ARK_BLOG
            {
                objectID = lID++,
                entityID = 2,
                entryDts = DateTime.Now.AddDays(-1),
                titleTxt = "Thoughts on Yesterday",
                narrativeTxt = "Was great."
            });
            _ResourceList.Add(new D_ARK_BLOG
            {
                objectID = lID++,
                entityID = 2,
                entryDts = DateTime.Now.AddDays(1),
                titleTxt = "Thoughts on Tomorrow",
                narrativeTxt = "Should be good."
            });
        }

        /// <summary>
        /// select a list of items from persistent store for given filter
        /// </summary>
        /// <param name="aFilter"></param>
        /// <returns></returns>
        public List<D_ARK_BLOG> SelectList (F_ARK_BLOG aFilter)
        {
            var lResult = (from item in _ResourceList select item);

            // apply filter attributes
            if (aFilter.entityID.HasValue)
            {
                lResult = lResult.Where (x => x.entityID == aFilter.entityID.Value);
            }

            if (aFilter.fromEntryDt.HasValue)
            {
                lResult = lResult.Where (x => x.entryDts >= aFilter.fromEntryDt.Value);
            }

            if (aFilter.thruEntryDt.HasValue)
            {
                lResult = lResult.Where (x => x.entryDts <= aFilter.thruEntryDt.Value);
            }

            if (! string.IsNullOrEmpty (aFilter.titleTxt))
            {
                lResult = lResult.Where (x => x.titleTxt.ToLower().Contains (aFilter.titleTxt.ToLower()));
            }

            if (! string.IsNullOrEmpty (aFilter.narrativeTxt))
            {
                lResult = lResult.Where (x => x.narrativeTxt.ToLower().Contains (aFilter.narrativeTxt.ToLower()));
            }

            // check base criteria
            lResult = CheckBaseCriteria(lResult, aFilter);

            // return result
            return lResult.ToList<D_ARK_BLOG>();
        }

        /// <summary>
        /// remove all matching items from persistent store
        /// </summary>
        /// <param name="aFilter"></param>
        public void DeleteList (F_ARK_BLOG aFilter)
        {
            throw new NotImplementedException ("ARK_BLOG.DeleteList not implemented");
        }

        /// <summary>
        /// select an item from persistent store for given key
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns></returns>
        public D_ARK_BLOG SelectItem (K_ARK_BLOG aKey)
        {
            D_ARK_BLOG lResult = null;

            // apply key attributes
            if (aKey.objectID.HasValue)
                lResult = _ResourceList.Where (x => x.objectID == aKey.objectID).FirstOrDefault();

            // throw exception if not found
            if (lResult == null)
                throw new DllNotFoundException (string.Format ("ARK_BLOG Item not found for key {0}", aKey.objectID));

            // return result
            return lResult;
        }

        /// <summary>
        /// insert an item into persistent store
        /// </summary>
        /// <param name="aDto"></param>
        public D_ARK_BLOG InsertItem (D_ARK_BLOG aDto)
        {
            int lID = 0;

            if (_ResourceList.Count > 0)
                lID = _ResourceList.Select (x => x.objectID).Max() + 1;

            // create new item
            D_ARK_BLOG lItem = new D_ARK_BLOG
            { 
                objectID     = lID,
                entityID     = aDto.entityID,
                entryDts     = aDto.entryDts,
                titleTxt     = aDto.titleTxt,
                narrativeTxt = aDto.narrativeTxt,

                activeYn    = aDto.activeYn,
                createByUid = aDto.createByUid,
                createOnDts = aDto.createOnDts,
                updateByUid = aDto.updateByUid,
                updateOnDts = aDto.updateOnDts,
                versionKey  = aDto.versionKey
            };

            // insert new item into list
            lock (_ResourceList)
            {
                _ResourceList.Add (lItem);
            }

            return aDto;
        }

        /// <summary>
        /// update an item in persistent store
        /// </summary>
        /// <param name="aDto"></param>
        public D_ARK_BLOG UpdateItem (D_ARK_BLOG aDto)
        {
            // fetch indicated item
            D_ARK_BLOG lItem = _ResourceList.Where (x => x.objectID == aDto.objectID).FirstOrDefault();

            // compare versions
            if (aDto.versionKey != null && lItem.versionKey != null && ! aDto.versionKey.SequenceEqual (lItem.versionKey))
                throw new Exception ("version key mismatch");

            // update item
            lock (lItem)
            {
                lItem.titleTxt     = aDto.titleTxt;
                lItem.entityID     = aDto.entityID;
                lItem.entryDts     = aDto.entryDts;
                lItem.titleTxt     = aDto.titleTxt;
                lItem.narrativeTxt = aDto.narrativeTxt;

                lItem.activeYn    = aDto.activeYn;
                lItem.createByUid = aDto.createByUid;
                lItem.createOnDts = aDto.createOnDts;
                lItem.updateByUid = aDto.updateByUid;
                lItem.updateOnDts = aDto.updateOnDts;
                lItem.versionKey  = aDto.versionKey;
            }

            return aDto;
        }

        /// <summary>
        /// remove an item from persistent store
        /// </summary>
        /// <param name="aKey"></param>
        public void DeleteItem (K_ARK_BLOG aKey)
        {
            // fetch indicated item
            D_ARK_BLOG lItem = _ResourceList.Where (x => x.objectID == aKey.objectID).FirstOrDefault();

            // remove from list
            lock (_ResourceList)
            {
                _ResourceList.Remove (lItem);
            }
        }
    }
}