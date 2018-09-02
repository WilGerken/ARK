using System;
using System.Collections.Generic;

namespace Library.Resources
{
    public interface IDalManager : IDisposable
    {
        T GetProvider<T>() where T : class;
    }

    public class DalFactory
    {
        public const string ARK_COMMON_SCHEMA_NM = "Common";
        public const string ARK_ENTITY_SCHEMA_NM = "Entity";
        public const string ARK_STORY_SCHEMA_NM  = "Story";

        private const string MANAGER_TYPE_NAME = "Library.Resources.{0}.DalManager, Library";

        private static Dictionary<string, Type> _dalTypes = new Dictionary<string, Type>();

        public static IDalManager GetManager (string aSchemaNm)
        {
            if (! _dalTypes.ContainsKey (aSchemaNm))
            {
                string lName = string.Format (MANAGER_TYPE_NAME, aSchemaNm);
                Type   lType = Type.GetType (lName);

                if (lType == null)
                    throw new ArgumentException (string.Format ("Resource Type {0} not found", lName));

                _dalTypes.Add (aSchemaNm, lType);
            }
            return (IDalManager) Activator.CreateInstance (_dalTypes[aSchemaNm]);
        }
    }
#if (NEVER)
    public class DalFactory : IDalManager
    {
        private const string MANAGER_TYPE_NAME = "ApocForms.RepositoryLayer.DalManager, ApocForms.RepositoryLayer";

        private static Type _dalType = null;

        public static IDalManager GetInstance ()
        {
            if (_dalType == null)
            {
                _dalType = Type.GetType (MANAGER_TYPE_NAME);

                if (_dalType == null)
                    throw new ArgumentException (string.Format ("Repository Type {0} not found", MANAGER_TYPE_NAME));
            }
            return (IDalManager) Activator.CreateInstance (_dalType);
        }

        //private static string PROVIDER_TYPE_MASK = typeof (DalManager).FullName.Replace("DalManager", @"{0}");

        public T GetProvider<T>() where T : class
        {
            //var typeName = string.Format (PROVIDER_TYPE_MASK, typeof(T).Name.Substring(2));
            var typeName = typeof(T).FullName.Replace ("I_", string.Empty);
            var type     = Type.GetType (typeName);

            if (type != null)
                return Activator.CreateInstance (type) as T;
            else
                throw new NotImplementedException (typeName);
        }

        public void Dispose() { }
    }
#endif
}
