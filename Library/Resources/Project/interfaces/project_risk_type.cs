﻿using System;
using System.Collections.Generic;
using Library.Common;

namespace Library.Resources.Project
{
    /// <summary>
    /// public interface for RefProjectRiskType items
    /// </summary>
    public interface I_PROJECT_RISK_TYPE
    {
        List<D_PROJECT_RISK_TYPE> SelectList (F_PROJECT_RISK_TYPE aFilter);
        void                      DeleteList (F_PROJECT_RISK_TYPE aFilter);

        D_PROJECT_RISK_TYPE SelectItem (K_PROJECT_RISK_TYPE aKey);
        D_PROJECT_RISK_TYPE InsertItem (D_PROJECT_RISK_TYPE aDto);
        D_PROJECT_RISK_TYPE UpdateItem (D_PROJECT_RISK_TYPE aDto);
        void                DeleteItem (K_PROJECT_RISK_TYPE aKey);
    }

    /// <summary>
    /// filter object for WorldMap lists
    /// </summary>
    public class F_PROJECT_RISK_TYPE : Data_F_Base
    {
        public string typeTxt { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public F_PROJECT_RISK_TYPE() { }
    }

    /// <summary>
    /// key object for WorldMap items
    /// </summary>
    public class K_PROJECT_RISK_TYPE : Data_K_Base
    {
        public string typeTxt { get; set; }
    }

    /// <summary>
    /// data object for WorldMap items
    /// </summary>
    public class D_PROJECT_RISK_TYPE : Data_O_Base
    {
        public string typeTxt { get; set; }
        public string descTxt { get; set; }

        /// <summary>
        /// default constructo
        /// </summary>
        public D_PROJECT_RISK_TYPE() : base() { }
    }
}