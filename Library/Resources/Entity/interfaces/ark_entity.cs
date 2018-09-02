using System;
using System.Collections.Generic;
using Library.Common;

namespace Library.Resources.Entity
{
    /// <summary>
    /// public interface for ArkEntity items
    /// </summary>
    public interface I_ARK_ENTITY
    {
        List<D_ARK_ENTITY> SelectList (F_ARK_ENTITY aFilter);
        void               DeleteList (F_ARK_ENTITY aFilter);

        D_ARK_ENTITY SelectItem (K_ARK_ENTITY aKey);
        D_ARK_ENTITY InsertItem (D_ARK_ENTITY aDto);
        D_ARK_ENTITY UpdateItem (D_ARK_ENTITY aDto);
        void         DeleteItem (K_ARK_ENTITY aKey);
    }

    /// <summary>
    /// filter object for WorldMap lists
    /// </summary>
    public class F_ARK_ENTITY : Data_F_Base
    {
        public string entityNm { get; set; }
        public string descTxt  { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_ARK_ENTITY () { }
    }

    /// <summary>
    /// key object for WorldMap items
    /// </summary>
    public class K_ARK_ENTITY : Data_K_Base
    {
        public string entityNm { get; set; }
    }

    /// <summary>
    /// data object for WorldMap items
    /// </summary>
    public class D_ARK_ENTITY : Data_O_Base
    {
        public string entityNm { get; set; }
        public string descTxt  { get; set; }

        /// <summary>
        /// default constructo
        /// </summary>
        public D_ARK_ENTITY () : base () { }
    }
}