using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Common
{
    public abstract class DATA_ACCESS_BASE <T,F,K>
        where T : Data_O_Base
        where F : Data_F_Base
        where K : Data_K_Base
    {
        /// <summary>
        /// filter query by active flag - add selected item if given in criteria
        /// </summary>
        /// <param name="aQuery"></param>
        /// <param name="aFilter"></param>
        protected IEnumerable<T> CheckBaseCriteria (IEnumerable<T> aQuery, F aFilter)
        {
            // check object ID
            if (aFilter.objectID.HasValue)
            {
                aQuery = aQuery.Where (x => x.objectID == aFilter.objectID.Value);
            }

            // check active and selected
            if (aFilter.activeYn.HasValue)
            {
                if (aFilter.selectedID.HasValue)
                    aQuery = aQuery.Where (x => x.activeYn == aFilter.activeYn.Value || x.objectID == aFilter.selectedID);
                else
                    aQuery = aQuery.Where (x => x.activeYn == aFilter.activeYn.Value);
            }

            return aQuery;
        }

        /// <summary>
        /// filter query by active flag - add selected item if given in criteria
        /// </summary>
        /// <param name="aQuery"></param>
        /// <param name="aFilter"></param>
        protected IQueryable<T> CheckBaseCriteria (IQueryable<T> aQuery, F aFilter)
        {
            // check object ID
            if (aFilter.objectID.HasValue)
            {
                aQuery = aQuery.Where (x => x.objectID == aFilter.objectID.Value);
            }

            // check active and selected
            if (aFilter.activeYn.HasValue)
            {
                if (aFilter.selectedID.HasValue)
                    aQuery = aQuery.Where (x => x.activeYn == aFilter.activeYn.Value || x.objectID == aFilter.selectedID);
                else
                    aQuery = aQuery.Where (x => x.activeYn == aFilter.activeYn.Value);
            }

            return aQuery;
        }
    }
}
