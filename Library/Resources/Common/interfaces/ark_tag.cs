using System;
using System.Collections.Generic;
using Library.Common;

namespace Library.Resources.Common
{
    /// <summary>
    /// public interface for ArkTag items
    /// </summary>
    public interface I_ARK_TAG
    {
        List<D_ARK_TAG> SelectList (F_ARK_TAG aFilter);
        void            DeleteList (F_ARK_TAG aFilter);

        D_ARK_TAG SelectItem (K_ARK_TAG aKey);
        D_ARK_TAG InsertItem (D_ARK_TAG aDto);
        D_ARK_TAG UpdateItem (D_ARK_TAG aDto);
        void      DeleteItem (K_ARK_TAG aKey);
    }

    /// <summary>
    /// filter object for WorldMap lists
    /// </summary>
    public class F_ARK_TAG : Data_F_Base
    {
        public string tagTxt { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_ARK_TAG () { }
    }

    /// <summary>
    /// key object for WorldMap items
    /// </summary>
    public class K_ARK_TAG : Data_K_Base
    {
        public string tagTxt { get; set; }
    }

    /// <summary>
    /// data object for WorldMap items
    /// </summary>
    public class D_ARK_TAG : Data_O_Base
    {
        public string tagTxt  { get; set; }
        public string descTxt { get; set; }

        /// <summary>
        /// default constructo
        /// </summary>
        public D_ARK_TAG () : base () { }
    }
}