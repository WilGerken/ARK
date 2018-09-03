using System;
using System.Collections.Generic;
using Library.Common;

namespace Library.Resources.Entity
{
    /// <summary>
    /// public interface for EntityTag items
    /// </summary>
    public interface I_ENTITY_TAG
    {
        List<D_ENTITY_TAG> SelectList (F_ENTITY_TAG aFilter);
        void               DeleteList (F_ENTITY_TAG aFilter);

        D_ENTITY_TAG SelectItem (K_ENTITY_TAG aKey);
        D_ENTITY_TAG InsertItem (D_ENTITY_TAG aDto);
        D_ENTITY_TAG UpdateItem (D_ENTITY_TAG aDto);
        void         DeleteItem (K_ENTITY_TAG aKey);
    }

    /// <summary>
    /// filter object for WorldMap lists
    /// </summary>
    public class F_ENTITY_TAG : Data_F_Base
    {
        public int?   entityID { get; set; }
        public int?   tagID    { get; set; }
        public int?   typeID   { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_ENTITY_TAG () { }
    }

    /// <summary>
    /// key object for WorldMap items
    /// </summary>
    public class K_ENTITY_TAG : Data_K_Base
    {
        public int? entityID { get; set; }
        public int? tagID    { get; set; }
    }

    /// <summary>
    /// data object for WorldMap items
    /// </summary>
    public class D_ENTITY_TAG : Data_O_Base
    {
        public int    entityID { get; set; }
        public string entityNm { get; set; }
        public int    tagID    { get; set; }
        public string tagTxt   { get; set; }
        public int?   typeID   { get; set; }
        public string typeTxt  { get; set; }

        /// <summary>
        /// default constructo
        /// </summary>
        public D_ENTITY_TAG () : base () { }
    }
}