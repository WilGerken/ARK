using System;
using System.Collections.Generic;
using Library.Common;

namespace Library.Resources.Story
{
    /// <summary>
    /// public interface for RefStoryEntityType items
    /// </summary>
    public interface I_STORY_ENTITY_ROLE_TYPE
    {
        List<D_STORY_ENTITY_ROLE_TYPE> SelectList (F_STORY_ENTITY_ROLE_TYPE aFilter);
        void                           DeleteList (F_STORY_ENTITY_ROLE_TYPE aFilter);

        D_STORY_ENTITY_ROLE_TYPE SelectItem (K_STORY_ENTITY_ROLE_TYPE aKey);
        D_STORY_ENTITY_ROLE_TYPE InsertItem (D_STORY_ENTITY_ROLE_TYPE aDto);
        D_STORY_ENTITY_ROLE_TYPE UpdateItem (D_STORY_ENTITY_ROLE_TYPE aDto);
        void                     DeleteItem (K_STORY_ENTITY_ROLE_TYPE aKey);
    }

    /// <summary>
    /// filter object for WorldMap lists
    /// </summary>
    public class F_STORY_ENTITY_ROLE_TYPE : Data_F_Base
    {
        public string typeTxt { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_STORY_ENTITY_ROLE_TYPE () { }
    }

    /// <summary>
    /// key object for WorldMap items
    /// </summary>
    public class K_STORY_ENTITY_ROLE_TYPE : Data_K_Base
    {
        public string typeTxt { get; set; }
    }

    /// <summary>
    /// data object for WorldMap items
    /// </summary>
    public class D_STORY_ENTITY_ROLE_TYPE : Data_O_Base
    {
        public string typeTxt { get; set; }
        public string descTxt { get; set; }

        /// <summary>
        /// default constructo
        /// </summary>
        public D_STORY_ENTITY_ROLE_TYPE () : base () { }
    }
}