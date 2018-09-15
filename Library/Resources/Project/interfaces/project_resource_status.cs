using System;
using System.Collections.Generic;
using Library.Common;

namespace Library.Resources.Project
{
    /// <summary>
    /// public interface for ProjectResourceStatus items
    /// </summary>
    public interface I_PROJECT_RESOURCE_STATUS
    {
        List<D_PROJECT_RESOURCE_STATUS> SelectList (F_PROJECT_RESOURCE_STATUS aFilter);
        void                            DeleteList (F_PROJECT_RESOURCE_STATUS aFilter);

        D_PROJECT_RESOURCE_STATUS SelectItem (K_PROJECT_RESOURCE_STATUS aKey);
        D_PROJECT_RESOURCE_STATUS InsertItem (D_PROJECT_RESOURCE_STATUS aDto);
        D_PROJECT_RESOURCE_STATUS UpdateItem (D_PROJECT_RESOURCE_STATUS aDto);
        void                      DeleteItem (K_PROJECT_RESOURCE_STATUS aKey);
    }

    /// <summary>
    /// filter object for WorldMap lists
    /// </summary>
    public class F_PROJECT_RESOURCE_STATUS : Data_F_Base
    {
        public int?      projectID  { get; set; }
        public int?      resourceID { get; set; }
        public int?      entityID   { get; set; }
        public int?      roleID     { get; set; }
        public int?      statusID   { get; set; }
        public DateTime? fromDts    { get; set; }
        public DateTime? thruDts    { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_PROJECT_RESOURCE_STATUS () { }
    }

    /// <summary>
    /// key object for WorldMap items
    /// </summary>
    public class K_PROJECT_RESOURCE_STATUS : Data_K_Base
    {
        public int?      resourceID { get; set; }
        public int?      statusID   { get; set; }
        public DateTime? fromDts    { get; set; }
    }

    /// <summary>
    /// data object for WorldMap items
    /// </summary>
    public class D_PROJECT_RESOURCE_STATUS : Data_O_Base
    {
        public int       projectID     { get; set; }
        public string    projectNm     { get; set; }
        public int       resourceID    { get; set; }
        public string    resourceNm    { get; set; }
        public int       entityID      { get; set; }
        public string    entityNm      { get; set; }
        public int       roleID        { get; set; }
        public string    roleTxt       { get; set; }
        public int?      statusID      { get; set; }
        public string    statusTxt     { get; set; }
        public DateTime? statusFromDts { get; set; }
        public string    descTxt       { get; set; }

        /// <summary>
        /// default constructo
        /// </summary>
        public D_PROJECT_RESOURCE_STATUS () : base () { }
    }
}