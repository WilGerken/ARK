using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Library.Common;

namespace ArkWeb.Common
{
    #region SelectList

    public enum SelectedTypes { Not = -1, Any }

#if (! MVC)

    public class SelectListItem
    {
        public bool   Selected { get; set; } = false;
        public bool   Disabled { get; set; } = false;
        public string Text     { get; set; }
        public string Value    { get; set; }

        public SelectListItem () { }
        public SelectListItem (string aText, string aValue) { Text  = aText; Value = aValue; }
    }

    public class SelectList : List<SelectListItem>
    {
        public SelectList(IEnumerable<SelectListItem> aItems)
        {
            foreach (var item in aItems) { Add (item); }
        }
    }

#endif

    public static class SelectListExtensions
    {
        public static bool HasValue<T>(this SelectListItem aItem, T aValue)
        {
            bool lResult = false;

            try { lResult = Convert.ChangeType (aItem.Value, typeof(T)).Equals (aValue); } catch { }

            return lResult;
        }
    }

    #endregion

    #region INotification

    /// <summary>
    /// viewModel notification base
    /// </summary>
    public class Notification_Base : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

#endregion

    /// <summary>
    /// viewModel base
    /// </summary>
    [Serializable]
    public abstract class ViewModel_Base : Notification_Base
    {
        /// <summary>
        /// message text
        /// </summary>
        public string MessageTxt
        {
            get { return _messageTxt; }
            set { _messageTxt = value; NotifyPropertyChanged(); }
        }
        private string _messageTxt;
        
        /// <summary>
        /// status text
        /// </summary>
        public string StatusTxt
        {
            get { return _statusTxt; }
            set { _statusTxt = value; NotifyPropertyChanged(); }
        }
        private string _statusTxt;

        /// <summary>
        /// default constructor
        /// </summary>
        public ViewModel_Base ()
        {
        }

        #region SelectList

        /// <summary>
        /// build selectlistitem from string representation of enum value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="aValue"></param>
        /// <returns></returns>
        public static SelectListItem SelectItemFromValue<T>(string aValue)
        {
            T lValue;

            if (typeof(T).GetTypeInfo().IsEnum)
                lValue = (T) Enum.Parse (typeof(T), aValue);
            else
                lValue = (T) Convert.ChangeType (aValue, typeof(T));

            return SelectItemFromEnum (lValue);
        }

        /// <summary>
        /// build selectlistitem from enum value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="aItem"></param>
        /// <returns></returns>
        public static SelectListItem SelectItemFromEnum<T>(T aItem)
        {
            Regex CamelCase = new Regex("(?<!^)([A-Z])", RegexOptions.Compiled);

            string lName = CamelCase.Replace (Enum.GetName (typeof(T), aItem), " $1");

            return new SelectListItem { Text = lName, Value = aItem.ToString() };
        }

        /// <summary>
        /// create select list from enum type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static SelectList SelectListFromEnum<T>()
        {
            List<SelectListItem> lResult = new List<SelectListItem>();

            if (typeof(T).GetTypeInfo().IsEnum)
            {
                foreach (T e in Enum.GetValues (typeof (T)))
                {
                    lResult.Add (SelectItemFromEnum(e));
                }
            }

            return new SelectList (lResult);
        }

        //public bool Equals<T> (T aValue)
        //{
        //    return ((T) Convert.ChangeType (Value, typeof(T)).ToString() == aValue);
        //}
        //public static bool operator == (SelectListItem aItem, Enum aValue)
        //{
        //    return aItem.Equals (aValue);
        //}
        //public static bool operator != (SelectListItem aItem, Enum aValue)
        //{
        //    return ! aItem.Equals (aValue);
        //}

        #endregion

    }

    /// <summary>
    /// viewModel object base
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public abstract class ViewModel_Base<T> : ViewModel_Base
        where T : class
    {
        #region Select Options

        public enum ActiveTypes { Any = 0, ActiveOnly, InactiveOnly }

        protected bool IsSelected (SelectedTypes  aValue) { return aValue > SelectedTypes.Any; }
        protected bool IsSelected (SelectedTypes? aValue) { return (aValue.HasValue && aValue.Value > SelectedTypes.Any); }
        
        #endregion

        /// <summary>
        /// primary model object
        /// </summary>
        public T ModelObject { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public ViewModel_Base ()
        {
        }

        /// <summary>
        /// pre-load operations
        /// </summary>
        protected virtual void PreLoad() { }

        /// <summary>
        /// post-load operations
        /// </summary>
        protected virtual void PostLoad() { }

        /// <summary>
        /// load the model object
        /// </summary>
        protected abstract void LoadModel();

        /// <summary>
        /// refresh the viewmodel from persistent store
        /// </summary>
        public void Refresh (bool includeModel = true)
        {
            PreLoad();

            if (includeModel)
                LoadModel();

            PostLoad();
        }
    }

    /// <summary>
    /// viewModel editItem base
    /// </summary>
    /// <typeparam name="I"></typeparam>
    /// <typeparam name="K"></typeparam>
    [Serializable]
    public abstract class EditItem_ViewModel_Base<I, K> : ViewModel_Base<I>
        where I : EditItem_Base<I, K>
        where K : ItemCriteria_Base<K>, new()
    {
        /// <summary>
        /// List Criteria
        /// </summary>
        public K ItemCriteria { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public EditItem_ViewModel_Base ()
        {
            ItemCriteria = new K ();
        }

        /// <summary>
        /// common constructor
        /// </summary>
        /// <param name="aCriteria">item key criteria</param>
        public EditItem_ViewModel_Base (K aCriteria)
        {
            ItemCriteria = aCriteria;
        }

        /// <summary>
        /// load the model object
        /// </summary>
        protected override void LoadModel()
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;

            // for testing - var methodList = (typeof(L)).GetMethods(bindingFlags);

            // establish the method (using Reflection)
            string     lMethodNm   = ItemCriteria.HasKey ? "GetItem" : "NewItem";
            MethodInfo lMethodInfo = (typeof (I)).GetMethod (lMethodNm, bindingFlags, null, new Type[] { typeof (K) }, null);

            // invoke the method
            ModelObject = (I) lMethodInfo.Invoke (null, new object[] { ItemCriteria });
        }

        /// <summary>
        /// save item to persistent store
        /// </summary>
        public virtual bool Save ()
        {
            // save to persistent store
            try
            {
                // update IsNew
                ModelObject.SetIsNew ();

                ModelObject = ModelObject.Save ();

                return true;
            }
            catch (Exception ex)
            {
                // Logger.Log(LogLevel.Error, "BaseViewModel::Save", "Error saving item", ex);

                return false;
            }
        }

        /// <summary>
        /// is referrer a valid url to cancel to
        /// </summary>
        /// <param name="aUrl"></param>
        /// <returns></returns>
        public bool IsValidReferrer(Uri aUri)
        {
            if (aUri == null)
                return false;

            if (aUri.AbsolutePath.Contains("_Edit") || aUri.AbsolutePath.Contains("_Save"))
                return false;

            return true;
        }
    }

    /// <summary>
    /// viewModel infoList base
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="F"></typeparam>
    /// <typeparam name="I"></typeparam>
    /// <typeparam name="K"></typeparam>
    [Serializable]
    public abstract class InfoList_ViewModel_Base<L, F, I, K> : ViewModel_Base<L>
        where L : InfoList_Base<L, F, I, K>, new()
        where F : ListCriteria_Base<F>, new()
        where I : InfoItem_Base<I, K>
        where K : ItemCriteria_Base<K>, new()
    {
        /// <summary>
        /// List Criteria
        /// </summary>
        public F ListCriteria { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public InfoList_ViewModel_Base()
        {
            ListCriteria = new F();
            ModelObject  = new L();
        }

        /// <summary>
        /// load the model object
        /// </summary>
        protected override void LoadModel()
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;

            // for testing - var methodList = (typeof(L)).GetMethods(bindingFlags);

            // establish the method (using Reflection)
            MethodInfo lMethodInfo = (typeof (L)).GetMethod ("GetList", bindingFlags, null, new Type[] { typeof (F) }, null);

            // invoke the method
            ModelObject = (L) lMethodInfo.Invoke (null, new object[] { ListCriteria });
        }
    }

    /// <summary>
    /// viewModel editList base
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="F"></typeparam>
    /// <typeparam name="I"></typeparam>
    /// <typeparam name="K"></typeparam>
    [Serializable]
    public abstract class EditList_ViewModel_Base<L, F, I, K> : ViewModel_Base<L>
        where L : EditList_Base<L, F, I, K>, new()
        where F : ListCriteria_Base<F>, new()
        where I : EditItem_Base<I, K>
        where K : ItemCriteria_Base<K>, new()
    {
        /// <summary>
        /// selectlist of active options
        /// </summary>
        public SelectList ActiveOption_SelectList { get; set; }

        /// <summary>
        /// selected active option
        /// </summary>
        public SelectListItem ActiveOption_SelectItem { get; set; }

        /// <summary>
        /// List Criteria
        /// </summary>
        public F ListCriteria { get; set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public EditList_ViewModel_Base ()
        {
            ListCriteria = new F();
            ModelObject  = new L();

            ActiveOption_SelectItem = SelectItemFromEnum (ActiveTypes.Any);
        }

        /// <summary>
        /// common constructor
        /// </summary>
        public EditList_ViewModel_Base (IQueryCollection lQuery)
        {
            ListCriteria = new F();
            ModelObject  = new L();

            if (string.IsNullOrEmpty(lQuery["ActiveOption"]))
                ActiveOption_SelectItem = SelectItemFromEnum (SelectedTypes.Any);
            else
            {
                string lValue = lQuery["ActiveOption"];

                ActiveOption_SelectItem = SelectItemFromValue<ActiveTypes> (lValue);
            }
        }

        /// <summary>
        /// load the model object
        /// </summary>
        protected override void LoadModel()
        {
            var bindingFlags = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;

            // for testing - var methodList = (typeof(L)).GetMethods(bindingFlags);

            // establish the method (using Reflection)
            MethodInfo lMethodInfo = (typeof(L)).GetMethod("GetList", bindingFlags, null, new Type[] { typeof(F) }, null);

            // invoke the method
            ModelObject = (L)lMethodInfo.Invoke(null, new object[] { ListCriteria });
        }

        /// <summary>
        /// pre-load operations
        /// </summary>
        protected override void PreLoad()
        {
            if (ActiveOption_SelectItem.HasValue (ActiveTypes.ActiveOnly))   ListCriteria.ActiveYn = true;
            if (ActiveOption_SelectItem.HasValue (ActiveTypes.InactiveOnly)) ListCriteria.ActiveYn = false;
        }

        /// <summary>
        /// post-load operations
        /// </summary>
        protected override void PostLoad()
        {
            ActiveOption_SelectList = SelectListFromEnum<ActiveTypes>();
        }

        #region NodeUrl
#if (NOTYET)
        /// <summary>
        /// controller version of build href for breadcrumb
        /// </summary>
        /// <param name="aControllerNm">controller name</param>
        /// <param name="aActionNm">action name</param>
        /// <param name="aLinkTxt">link display text</param>
        /// <returns>href for breadcrumb</returns>
        public abstract string BuildNodeUrl (string aControllerNm, string aActionNm, string aLinkTxt);

        /// <summary>
        /// viewmodel version of build href for breadcrumb
        /// </summary>
        /// <param name="aControllerNm">controller name</param>
        /// <param name="aActionNm">action name</param>
        /// <param name="aLinkTxt">link display text</param>
        /// <param name="aQueryTxt">query string</param>
        /// <returns>href for breadcrumb</returns>
        protected string BuildNodeUrl (string aControllerNm, string aActionNm, string aLinkTxt, string aQueryTxt)
        {
            var lUrI = HttpContext.Current.Request.Url;
            var lUrl = string.IsNullOrEmpty(lUrI.Query) ? lUrI.AbsoluteUri : lUrI.AbsoluteUri.Substring(0, lUrI.AbsoluteUri.IndexOf('?'));

            StringBuilder lQueryTxt = new StringBuilder(aQueryTxt);

            if (IsSelected (ActiveOption_SelectItem))
            {
                lQueryTxt.AppendFormat("{0}{1}={2}", string.IsNullOrEmpty(aQueryTxt) ? "" : "&", "ActiveOption", ActiveOption_SelectItem);
            }

            if (lQueryTxt.Length == 0)
                return string.Format("<a href=\"{0}\">{1}</a>", lUrl, aLinkTxt);
            else
                return string.Format("<a href=\"{0}?{1}\">{2}</a>", lUrl, lQueryTxt.ToString(), aLinkTxt);
        }
#endif
#endregion

#if (NOTYET)
        /// <summary>
        /// controller version of build href for breadcrumb
        /// </summary>
        /// <param name="aControllerNm">controller name</param>
        /// <param name="aActionNm">action name</param>
        /// <param name="aLinkTxt">link display text</param>
        /// <returns>href for breadcrumb</returns>
        public abstract string BuildNodeUrl(string aControllerNm, string aActionNm, string aLinkTxt);

        /// <summary>
        /// viewmodel version of build href for breadcrumb
        /// </summary>
        /// <param name="aControllerNm">controller name</param>
        /// <param name="aActionNm">action name</param>
        /// <param name="aLinkTxt">link display text</param>
        /// <param name="aQueryTxt">query string</param>
        /// <returns>href for breadcrumb</returns>
        protected string BuildNodeUrl(string aControllerNm, string aActionNm, string aLinkTxt, string aQueryTxt)
        {
            var lUrI = HttpContext.Current.Request.Url;
            var lUrl = string.IsNullOrEmpty(lUrI.Query) ? lUrI.AbsoluteUri : lUrI.AbsoluteUri.Substring(0, lUrI.AbsoluteUri.IndexOf('?'));

            StringBuilder lQueryTxt = new StringBuilder(aQueryTxt);

            if (IsSelected(ActiveOption_SelectItem))
            {
                lQueryTxt.AppendFormat("{0}{1}={2}", string.IsNullOrEmpty(aQueryTxt) ? "" : "&", "ActiveOption", ActiveOption_SelectItem);
            }

            if (lQueryTxt.Length == 0)
                return string.Format("<a href=\"{0}\">{1}</a>", lUrl, aLinkTxt);
            else
                return string.Format("<a href=\"{0}?{1}\">{2}</a>", lUrl, lQueryTxt.ToString(), aLinkTxt);
        }
#endif
    }

#if (NEVER)

    public static class EnumExtensions
    {
        private static Regex CamelCase = new Regex ("(?<!^)([A-Z])", RegexOptions.Compiled);

        public static SelectList EnumToSelectList (this Enum arg)
        {
            Type T = arg.GetType();

            List<Object> lItems = new List<Object>();

            foreach (var item in Enum.GetValues(T))
            {
                string lName = CamelCase.Replace(Enum.GetName(T, item), " $1");

                int lValue = (int) Convert.ChangeType (item, item.GetType());

                lItems.Add (new { Name = lName, Value = lValue });
            }

            return new SelectList (lItems, "Name", "Value");
        }
    }

    public static class EnumExtensions
    {
        public static int ToInt<T>(this T arg) where T : IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            return (int)(IConvertible)arg;
        }

        public static int Count<T>(this T arg) where T : IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            return Enum.GetNames(typeof(T)).Length;
        }

        private static Regex CamelCase = new Regex("(?<!^)([A-Z])", RegexOptions.Compiled);

        public static Dictionary<string, int> AsDictionary<T>(this T arg) where T : IConvertible
        {
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            Dictionary<string, int> lResult = new Dictionary<string, int>();

            foreach (var item in Enum.GetValues(typeof(T)))
            {
                string lName = CamelCase.Replace(Enum.GetName(typeof(T), item), " $1");

                int lValue = (int)Convert.ChangeType(item, item.GetType());

                lResult.Add(lName, lValue);
            }

            return lResult;
        }
    }

#endif
}
