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
    public class ARK_STORY : DATA_ACCESS_BASE<D_ARK_STORY, F_ARK_STORY, K_ARK_STORY>, I_ARK_STORY
    {
        // resource list

        public static List<D_ARK_STORY> ResourceList = new List<D_ARK_STORY>();

        static ARK_STORY ()
        {
            int lID = 1;

            ResourceList.Add (new D_ARK_STORY
            {
                objectID     = lID++,
                titleTxt     = "Community Well",
                locationTxt  = "Our Community",
                narrativeTxt = "We seem to need a new well.  Our old one has too many coins in it.",
                createByUid = 1,
                updateByUid = 1
            });

            ResourceList.Add (new D_ARK_STORY
            {
                objectID     = lID++,
                titleTxt     = "Community School",
                locationTxt  = "Our Community",
                narrativeTxt = "We seem to need a new school.  Our old one has too many children in it.",
                createByUid = 1,
                updateByUid = 1
            });

            ResourceList.Add (new D_ARK_STORY
            {
                objectID     = lID++,
                titleTxt     = "Community Aid",
                locationTxt  = "Our Community",
                narrativeTxt = "We seem to need some help.  Perhaps if some aid is available?",
                createByUid = 1,
                updateByUid = 1
            });
        }

        /// <summary>
        /// select a list of items from persistent store for given filter
        /// </summary>
        /// <param name="aFilter"></param>
        /// <returns></returns>
        public List<D_ARK_STORY> SelectList (F_ARK_STORY aFilter)
        {
            var lResult = (from item in ResourceList select item);

            // apply filter attributes
            if (! string.IsNullOrEmpty (aFilter.titleTxt))
            {
                lResult = lResult.Where (x => x.titleTxt.ToLower().Contains (aFilter.titleTxt.ToLower()));
            }

            if (! string.IsNullOrEmpty (aFilter.locationTxt))
            {
                lResult = lResult.Where (x => x.locationTxt.ToLower().Contains (aFilter.locationTxt.ToLower()));
            }

            // check base criteria
            lResult = CheckBaseCriteria (lResult, aFilter);

            // return result
            return lResult.ToList<D_ARK_STORY>();
        }

        /// <summary>
        /// remove all matching items from persistent store
        /// </summary>
        /// <param name="aFilter"></param>
        public void DeleteList (F_ARK_STORY aFilter)
        {
            throw new NotImplementedException ("ARK_STORY.DeleteList not implemented");
        }

        /// <summary>
        /// select an item from persistent store for given key
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns></returns>
        public D_ARK_STORY SelectItem (K_ARK_STORY aKey)
        {
            D_ARK_STORY lResult = null;

            // apply key attributes
            if (aKey.objectID.HasValue)
                lResult = ResourceList.Where (x => x.objectID == aKey.objectID).FirstOrDefault();

            // throw exception if not found
            if (lResult == null)
                throw new DllNotFoundException (string.Format ("ARK_STORY Item not found for key {0}", aKey.objectID));

            // return result
            return lResult;
        }

        /// <summary>
        /// insert an item into persistent store
        /// </summary>
        /// <param name="aDto"></param>
        public D_ARK_STORY InsertItem (D_ARK_STORY aDto)
        {
            int lID = 0;

            if (ResourceList.Count > 0)
                lID = ResourceList.Select (x => x.objectID).Max() + 1;

            // create new item
            D_ARK_STORY lItem = new D_ARK_STORY
            { 
                objectID     = lID,
                titleTxt     = aDto.titleTxt,
                locationTxt  = aDto.locationTxt,
                narrativeTxt = aDto.narrativeTxt,

                activeYn    = aDto.activeYn,
                createByUid = aDto.createByUid,
                createOnDts = aDto.createOnDts,
                updateByUid = aDto.updateByUid,
                updateOnDts = aDto.updateOnDts,
                versionKey  = aDto.versionKey
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
        public D_ARK_STORY UpdateItem (D_ARK_STORY aDto)
        {
            // fetch indicated item
            D_ARK_STORY lItem = ResourceList.Where (x => x.objectID == aDto.objectID).FirstOrDefault();

            // compare versions
            if (aDto.versionKey != null && lItem.versionKey != null && ! aDto.versionKey.SequenceEqual (lItem.versionKey))
                throw new Exception ("version key mismatch");

            // update item
            lock (lItem)
            {
                lItem.titleTxt     = aDto.titleTxt;
                lItem.locationTxt  = aDto.locationTxt;
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
        public void DeleteItem (K_ARK_STORY aKey)
        {
            // fetch indicated item
            D_ARK_STORY lItem = ResourceList.Where (x => x.objectID == aKey.objectID).FirstOrDefault();

            // remove from list
            lock (ResourceList)
            {
                ResourceList.Remove (lItem);
            }
        }
    }
}