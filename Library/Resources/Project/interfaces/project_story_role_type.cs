using System;
using System.Collections.Generic;
using Library.Common;

namespace Library.Resources.Project
{
    /// <summary>
    /// public interface for RefProjectStoryType items
    /// </summary>
    public interface I_PROJECT_STORY_ROLE_TYPE
    {
        List<D_PROJECT_STORY_ROLE_TYPE> SelectList (F_PROJECT_STORY_ROLE_TYPE aFilter);
        void                            DeleteList (F_PROJECT_STORY_ROLE_TYPE aFilter);

        D_PROJECT_STORY_ROLE_TYPE SelectItem (K_PROJECT_STORY_ROLE_TYPE aKey);
        D_PROJECT_STORY_ROLE_TYPE InsertItem (D_PROJECT_STORY_ROLE_TYPE aDto);
        D_PROJECT_STORY_ROLE_TYPE UpdateItem (D_PROJECT_STORY_ROLE_TYPE aDto);
        void                      DeleteItem (K_PROJECT_STORY_ROLE_TYPE aKey);
    }

    /// <summary>
    /// filter object for WorldMap lists
    /// </summary>
    public class F_PROJECT_STORY_ROLE_TYPE : Data_F_Base
    {
        public string typeTxt { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_PROJECT_STORY_ROLE_TYPE() { }
    }

    /// <summary>
    /// key object for WorldMap items
    /// </summary>
    public class K_PROJECT_STORY_ROLE_TYPE : Data_K_Base
    {
        public string typeTxt { get; set; }
    }

    /// <summary>
    /// data object for WorldMap items
    /// </summary>
    public class D_PROJECT_STORY_ROLE_TYPE : Data_O_Base
    {
        public string typeTxt { get; set; }
        public string descTxt { get; set; }

        /// <summary>
        /// default constructo
        /// </summary>
        public D_PROJECT_STORY_ROLE_TYPE() : base() { }
    }
}