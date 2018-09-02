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
    public class ARK_ENTITY : DATA_ACCESS_BASE<D_ARK_ENTITY, F_ARK_ENTITY, K_ARK_ENTITY>, I_ARK_ENTITY
    {
        // resource list
        public static List<D_ARK_ENTITY> _ResourceList = new List<D_ARK_ENTITY>();

        static ARK_ENTITY ()
        {
            int lID = Ref.AdminID;

            _ResourceList.Add(new D_ARK_ENTITY
            {
                objectID = lID++,
                entityNm = "Admin",
                descTxt = "Application Administrative Account"
            });
            _ResourceList.Add(new D_ARK_ENTITY
            {
                objectID = lID++,
                entityNm = "Guest",
                descTxt = "Application Guest Account"
            });
            _ResourceList.Add (new D_ARK_ENTITY
            {
                objectID = lID++,
                entityNm = "Fred Head",
                descTxt = "Fred of the clan Head"
            });
            _ResourceList.Add(new D_ARK_ENTITY
            {
                objectID = lID++,
                entityNm = "Jed Head",
                descTxt = "Jed of the clan Head"
            });
            _ResourceList.Add(new D_ARK_ENTITY
            {
                objectID = lID++,
                entityNm = "Ned Head",
                descTxt = "Ned of the clan Head"
            });
            _ResourceList.Add(new D_ARK_ENTITY
            {
                objectID = lID++,
                entityNm = "Ted Head",
                descTxt = "Ted of the clan Head"
            });
            _ResourceList.Add(new D_ARK_ENTITY
            {
                objectID = lID++,
                entityNm = "Edith Head",
                descTxt = "Matriarch of the clan Head"
            });
            _ResourceList.Add(new D_ARK_ENTITY
            {
                objectID = lID++,
                entityNm = "Naomi Head",
                descTxt = "Naomi of the clan Head"
            });
            _ResourceList.Add(new D_ARK_ENTITY
            {
                objectID = lID++,
                entityNm = "Nicole Head",
                descTxt = "Nicole of the clan Head"
            });
        }

        /// <summary>
        /// select a list of items from persistent store for given filter
        /// </summary>
        /// <param name="aFilter"></param>
        /// <returns></returns>
        public List<D_ARK_ENTITY> SelectList (F_ARK_ENTITY aFilter)
        {
            IEnumerable<D_ARK_ENTITY> lResult = _ResourceList;

            // apply filter attributes
            if (! string.IsNullOrEmpty (aFilter.entityNm))
            {
                lResult = lResult.Where (x => x.entityNm.Contains (aFilter.entityNm));
            }

            // check base criteria
            lResult = CheckBaseCriteria (lResult, aFilter);

            // return result
            return lResult.ToList<D_ARK_ENTITY>();
        }

        /// <summary>
        /// remove all matching items from persistent store
        /// </summary>
        /// <param name="aFilter"></param>
        public void DeleteList (F_ARK_ENTITY aFilter)
        {
            throw new NotImplementedException ("ARK_ENTITY.DeleteList not implemented");
        }

        /// <summary>
        /// select an item from persistent store for given key
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns></returns>
        public D_ARK_ENTITY SelectItem (K_ARK_ENTITY aKey)
        {
            D_ARK_ENTITY lResult = null;

            // apply key attributes
            if (aKey.objectID.HasValue)
                lResult = _ResourceList.Where (x => x.objectID == aKey.objectID).FirstOrDefault();

            // throw exception if not found
            if (lResult == null)
                throw new DllNotFoundException (string.Format ("ARK_ENTITY Item not found for key {0}", aKey.objectID));

            // return result
            return lResult;
        }

        /// <summary>
        /// insert an item into persistent store
        /// </summary>
        /// <param name="aDto"></param>
        public D_ARK_ENTITY InsertItem (D_ARK_ENTITY aDto)
        {
            int lID = 0;

            if (_ResourceList.Count > 0)
                lID = _ResourceList.Select (x => x.objectID).Max() + 1;

            // create new item
            D_ARK_ENTITY lItem = new D_ARK_ENTITY
            {
                objectID = lID,
                entityNm = aDto.entityNm,
                descTxt  = aDto.descTxt
            };

            // insert new item into list
            lock (_ResourceList)
            {
            }

            return aDto;
        }

        /// <summary>
        /// update an item in persistent store
        /// </summary>
        /// <param name="aDto"></param>
        public D_ARK_ENTITY UpdateItem (D_ARK_ENTITY aDto)
        {
            // fetch indicated item
            D_ARK_ENTITY lItem = _ResourceList.Where (x => x.objectID == aDto.objectID).FirstOrDefault();

            // update item
            lock (lItem)
            {
                lItem.entityNm = aDto.entityNm;
                lItem.descTxt  = aDto.descTxt;
            }

            return aDto;
        }

        /// <summary>
        /// remove an item from persistent store
        /// </summary>
        /// <param name="aKey"></param>
        public void DeleteItem (K_ARK_ENTITY aKey)
        {
            lock (_ResourceList)
            {

            }
        }
    }
}