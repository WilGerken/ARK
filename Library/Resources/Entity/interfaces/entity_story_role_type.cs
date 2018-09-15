using System;
using System.Collections.Generic;
using Library.Common;

namespace Library.Resources.Entity
{
    /// <summary>
    /// public interface for RefEntityStoryType items
    /// </summary>
    public interface I_ENTITY_STORY_ROLE_TYPE
    {
        List<D_ENTITY_STORY_ROLE_TYPE> SelectList (F_ENTITY_STORY_ROLE_TYPE aFilter);
        void                           DeleteList (F_ENTITY_STORY_ROLE_TYPE aFilter);

        D_ENTITY_STORY_ROLE_TYPE SelectItem (K_ENTITY_STORY_ROLE_TYPE aKey);
        D_ENTITY_STORY_ROLE_TYPE InsertItem (D_ENTITY_STORY_ROLE_TYPE aDto);
        D_ENTITY_STORY_ROLE_TYPE UpdateItem (D_ENTITY_STORY_ROLE_TYPE aDto);
        void                     DeleteItem (K_ENTITY_STORY_ROLE_TYPE aKey);
    }

    /// <summary>
    /// filter object for WorldMap lists
    /// </summary>
    public class F_ENTITY_STORY_ROLE_TYPE : Data_F_Base
    {
        public string typeTxt { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_ENTITY_STORY_ROLE_TYPE () { }
    }

    /// <summary>
    /// key object for WorldMap items
    /// </summary>
    public class K_ENTITY_STORY_ROLE_TYPE : Data_K_Base
    {
        public string typeTxt { get; set; }
    }

    /// <summary>
    /// data object for WorldMap items
    /// </summary>
    public class D_ENTITY_STORY_ROLE_TYPE : Data_O_Base
    {
        public string typeTxt { get; set; }
        public string descTxt { get; set; }

        /// <summary>
        /// default constructo
        /// </summary>
        public D_ENTITY_STORY_ROLE_TYPE () : base () { }
    }
}