using System;
using System.Collections.Generic;
using Library.Common;

namespace Library.Resources.Project
{
    /// <summary>
    /// public interface for ProjectTag items
    /// </summary>
    public interface I_PROJECT_TAG
    {
        List<D_PROJECT_TAG> SelectList (F_PROJECT_TAG aFilter);
        void                DeleteList (F_PROJECT_TAG aFilter);

        D_PROJECT_TAG SelectItem (K_PROJECT_TAG aKey);
        D_PROJECT_TAG InsertItem (D_PROJECT_TAG aDto);
        D_PROJECT_TAG UpdateItem (D_PROJECT_TAG aDto);
        void          DeleteItem (K_PROJECT_TAG aKey);
    }

    /// <summary>
    /// filter object for WorldMap lists
    /// </summary>
    public class F_PROJECT_TAG : Data_F_Base
    {
        public int?   projectID { get; set; }
        public int?   tagID     { get; set; }
        public int?   typeID    { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_PROJECT_TAG() { }
    }

    /// <summary>
    /// key object for WorldMap items
    /// </summary>
    public class K_PROJECT_TAG : Data_K_Base
    {
        public int? projectID { get; set; }
        public int? tagID     { get; set; }
    }

    /// <summary>
    /// data object for WorldMap items
    /// </summary>
    public class D_PROJECT_TAG : Data_O_Base
    {
        public int    projectID { get; set; }
        public string projectNm { get; set; }
        public int    tagID     { get; set; }
        public string tagTxt    { get; set; }
        public int?   typeID    { get; set; }
        public string typeTxt   { get; set; }

        /// <summary>
        /// default constructo
        /// </summary>
        public D_PROJECT_TAG() : base() { }
    }
}