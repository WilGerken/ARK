using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Library.Common;
using Library.Resources.Entity.memory;

namespace Library.Resources.Project.memory
{
    /// <summary>
    /// data access class
    /// </summary>
    public class ARK_PROJECT : DATA_ACCESS_BASE<D_ARK_PROJECT, F_ARK_PROJECT, K_ARK_PROJECT>, I_ARK_PROJECT
    {
        // resource list
        public static List<D_ARK_PROJECT> ResourceList = new List<D_ARK_PROJECT>();

        static ARK_PROJECT ()
        {
            int lID = 1;

            ResourceList.Add(new D_ARK_PROJECT
            {
                objectID = lID++,
                projectNm = "Project One",
                descTxt = "Another Test Project",
                managerID = 3,
                clientID = 7,
                createByUid = 1,
                updateByUid = 1
            });
            ResourceList.Add(new D_ARK_PROJECT
            {
                objectID = lID++,
                projectNm = "Project Two",
                descTxt = "Another Test Project",
                clientID = 8,
                createByUid = 1,
                updateByUid = 1
            });
            ResourceList.Add (new D_ARK_PROJECT
            {
                objectID = lID++,
                projectNm = "Project Three",
                descTxt = "Another Test Project",
                managerID = 4,
                clientID = 7,
                createByUid = 1,
                updateByUid = 1
            });
            ResourceList.Add(new D_ARK_PROJECT
            {
                objectID = lID++,
                projectNm = "Project Four",
                descTxt = "Another Test Project",
                managerID = 5,
                createByUid = 1,
                updateByUid = 1
            });
            ResourceList.Add(new D_ARK_PROJECT
            {
                objectID = lID++,
                projectNm = "Project Five",
                descTxt = "Another Test Project",
                managerID = 6,
                clientID = 7,
                createByUid = 1,
                updateByUid = 1
            });
        }

        /// <summary>
        /// select a list of items from persistent store for given filter
        /// </summary>
        /// <param name="aFilter"></param>
        /// <returns></returns>
        public List<D_ARK_PROJECT> SelectList (F_ARK_PROJECT aFilter)
        {
            IEnumerable<D_ARK_PROJECT> lResult = (from item in ResourceList
                                                  from managerItem in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.managerID).DefaultIfEmpty()
                                                  from clientItem  in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.clientID).DefaultIfEmpty()
                                                  from createItem  in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.createByUid).DefaultIfEmpty()
                                                  from updateItem  in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.updateByUid).DefaultIfEmpty()
                                                  select new D_ARK_PROJECT
                                                  {
                                                      objectID = item.objectID,
                                                      projectNm = item.projectNm,
                                                      descTxt = item.descTxt,
                                                      managerID = item.managerID,
                                                      managerNm = managerItem != null ? managerItem.entityNm : string.Empty,
                                                      clientID = item.clientID,
                                                      clientNm = clientItem != null ? clientItem.entityNm : string.Empty,

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
            if (! string.IsNullOrEmpty (aFilter.projectNm))
            {
                lResult = lResult.Where (x => x.projectNm.Contains (aFilter.projectNm));
            }

            if (aFilter.managerID.HasValue)
            {
                lResult = lResult.Where (x => x.managerID == aFilter.managerID.Value);
            }

            if (aFilter.clientID.HasValue)
            {
                lResult = lResult.Where (x => x.clientID == aFilter.clientID.Value);
            }

            // check base criteria
            lResult = CheckBaseCriteria (lResult, aFilter);

            // return result
            return lResult.ToList<D_ARK_PROJECT>();
        }

        /// <summary>
        /// remove all matching items from persistent store
        /// </summary>
        /// <param name="aFilter"></param>
        public void DeleteList (F_ARK_PROJECT aFilter)
        {
            throw new NotImplementedException ("ARK_PROJECT.DeleteList not implemented");
        }

        /// <summary>
        /// select an item from persistent store for given key
        /// </summary>
        /// <param name="aKey"></param>
        /// <returns></returns>
        public D_ARK_PROJECT SelectItem (K_ARK_PROJECT aKey)
        {
            D_ARK_PROJECT lResult = null;

            IEnumerable<D_ARK_PROJECT> lQuery = (from item in ResourceList
                                                 from managerItem in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.managerID).DefaultIfEmpty()
                                                 from clientItem  in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.clientID).DefaultIfEmpty()
                                                 from createItem  in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.createByUid).DefaultIfEmpty()
                                                 from updateItem  in ARK_ENTITY.ResourceList.Where(x => x.objectID == item.updateByUid).DefaultIfEmpty()
                                                 select new D_ARK_PROJECT
                                                 {
                                                     objectID = item.objectID,
                                                     projectNm = item.projectNm,
                                                     descTxt = item.descTxt,
                                                     managerID = item.managerID,
                                                     managerNm = managerItem != null ? managerItem.entityNm : string.Empty,
                                                     clientID = item.clientID,
                                                     clientNm = clientItem != null ? clientItem.entityNm : string.Empty,

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
                throw new DllNotFoundException (string.Format ("ARK_PROJECT Item not found for key {0}", aKey.objectID));

            // return result
            return lResult;
        }

        /// <summary>
        /// insert an item into persistent store
        /// </summary>
        /// <param name="aDto"></param>
        public D_ARK_PROJECT InsertItem (D_ARK_PROJECT aDto)
        {
            int lID = 0;

            if (ResourceList.Count > 0)
                lID = ResourceList.Select (x => x.objectID).Max() + 1;

            // create new item
            D_ARK_PROJECT lItem = new D_ARK_PROJECT
            {
                objectID  = lID,
                projectNm = aDto.projectNm,
                descTxt   = aDto.descTxt,
                managerID = aDto.managerID,
                clientID  = aDto.clientID
            };

            // insert new item into list
            lock (ResourceList)
            {
            }

            return aDto;
        }

        /// <summary>
        /// update an item in persistent store
        /// </summary>
        /// <param name="aDto"></param>
        public D_ARK_PROJECT UpdateItem (D_ARK_PROJECT aDto)
        {
            // fetch indicated item
            D_ARK_PROJECT lItem = ResourceList.Where (x => x.objectID == aDto.objectID).FirstOrDefault();

            // update item
            lock (lItem)
            {
                lItem.projectNm = aDto.projectNm;
                lItem.descTxt   = aDto.descTxt;
                lItem.managerID = aDto.managerID;
                lItem.clientID  = aDto.clientID;
            }

            return aDto;
        }

        /// <summary>
        /// remove an item from persistent store
        /// </summary>
        /// <param name="aKey"></param>
        public void DeleteItem (K_ARK_PROJECT aKey)
        {
            lock (ResourceList)
            {

            }
        }
    }
}