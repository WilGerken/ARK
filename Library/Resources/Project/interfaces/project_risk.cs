using System;
using System.Collections.Generic;
using Library.Common;

namespace Library.Resources.Project
{
    /// <summary>
    /// public interface for ProjectResource items
    /// </summary>
    public interface I_PROJECT_RISK
    {
        List<D_PROJECT_RISK> SelectList (F_PROJECT_RISK aFilter);
        void                 DeleteList (F_PROJECT_RISK aFilter);

        D_PROJECT_RISK SelectItem (K_PROJECT_RISK aKey);
        D_PROJECT_RISK InsertItem (D_PROJECT_RISK aDto);
        D_PROJECT_RISK UpdateItem (D_PROJECT_RISK aDto);
        void           DeleteItem (K_PROJECT_RISK aKey);
    }

    /// <summary>
    /// filter object for WorldMap lists
    /// </summary>
    public class F_PROJECT_RISK : Data_F_Base
    {
        public int?   projectID { get; set; }
        public int?   typeID    { get; set; }
        public string titleTxt  { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_PROJECT_RISK () { }
    }

    /// <summary>
    /// key object for WorldMap items
    /// </summary>
    public class K_PROJECT_RISK : Data_K_Base
    {
        public int?   projectID { get; set; }
        public string titleTxt  { get; set; }
    }

    /// <summary>
    /// data object for WorldMap items
    /// </summary>
    public class D_PROJECT_RISK : Data_O_Base
    {
        public int    projectID { get; set; }
        public string projectNm { get; set; }
        public int?   typeID    { get; set; }
        public string typeTxt   { get; set; }
        public string titleTxt  { get; set; }
        public string descTxt   { get; set; }

        /// <summary>
        /// default constructo
        /// </summary>
        public D_PROJECT_RISK () : base () { }
    }
}