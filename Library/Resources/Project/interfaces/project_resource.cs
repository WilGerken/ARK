using System;
using System.Collections.Generic;
using Library.Common;

namespace Library.Resources.Project
{
    /// <summary>
    /// public interface for ProjectResource items
    /// </summary>
    public interface I_PROJECT_RESOURCE
    {
        List<D_PROJECT_RESOURCE> SelectList (F_PROJECT_RESOURCE aFilter);
        void                   DeleteList (F_PROJECT_RESOURCE aFilter);

        D_PROJECT_RESOURCE SelectItem (K_PROJECT_RESOURCE aKey);
        D_PROJECT_RESOURCE InsertItem (D_PROJECT_RESOURCE aDto);
        D_PROJECT_RESOURCE UpdateItem (D_PROJECT_RESOURCE aDto);
        void               DeleteItem (K_PROJECT_RESOURCE aKey);
    }

    /// <summary>
    /// filter object for WorldMap lists
    /// </summary>
    public class F_PROJECT_RESOURCE : Data_F_Base
    {
        public int?   projectID { get; set; }
        public int?   entityID  { get; set; }
        public int?   roleID    { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_PROJECT_RESOURCE () { }
    }

    /// <summary>
    /// key object for WorldMap items
    /// </summary>
    public class K_PROJECT_RESOURCE : Data_K_Base
    {
        public int? projectID { get; set; }
        public int? entityID  { get; set; }
        public int? roleID    { get; set; }
    }

    /// <summary>
    /// data object for WorldMap items
    /// </summary>
    public class D_PROJECT_RESOURCE : Data_O_Base
    {
        public int    projectID { get; set; }
        public string projectNm { get; set; }
        public int    entityID  { get; set; }
        public string entityNm  { get; set; }
        public int?   roleID    { get; set; }
        public string roleTxt   { get; set; }
        public string descTxt   { get; set; }

        /// <summary>
        /// default constructo
        /// </summary>
        public D_PROJECT_RESOURCE () : base () { }
    }
}