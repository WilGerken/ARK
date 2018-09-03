using System;
using System.Collections.Generic;
using Library.Common;

namespace Library.Resources.Project
{
    /// <summary>
    /// public interface for ArkEntity items
    /// </summary>
    public interface I_ARK_PROJECT
    {
        List<D_ARK_PROJECT> SelectList (F_ARK_PROJECT aFilter);
        void                DeleteList (F_ARK_PROJECT aFilter);

        D_ARK_PROJECT SelectItem (K_ARK_PROJECT aKey);
        D_ARK_PROJECT InsertItem (D_ARK_PROJECT aDto);
        D_ARK_PROJECT UpdateItem (D_ARK_PROJECT aDto);
        void          DeleteItem (K_ARK_PROJECT aKey);
    }

    /// <summary>
    /// filter object for WorldMap lists
    /// </summary>
    public class F_ARK_PROJECT : Data_F_Base
    {
        public string projectNm { get; set; }
        public int?   managerID { get; set; }
        public int?   clientID  { get; set; }
        public string descTxt   { get; set; }
        public string tagTxt    { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_ARK_PROJECT () { }
    }

    /// <summary>
    /// key object for WorldMap items
    /// </summary>
    public class K_ARK_PROJECT : Data_K_Base
    {
        public string projectNm { get; set; }
    }

    /// <summary>
    /// data object for WorldMap items
    /// </summary>
    public class D_ARK_PROJECT : Data_O_Base
    {
        public string projectNm { get; set; }
        public string descTxt   { get; set; }
        public int?   managerID { get; set; }
        public string managerNm { get; set; }
        public int?   clientID  { get; set; }
        public string clientNm  { get; set; }

        /// <summary>
        /// default constructo
        /// </summary>
        public D_ARK_PROJECT () : base () { }
    }
}