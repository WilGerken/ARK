using System;

namespace Library.Resources.Project
{
    public class DalManager : IDalManager
    {
        public T GetProvider<T>() where T : class
        {
            var lName = typeof(T).FullName.Replace ("I_", "memory.");
            var lType = Type.GetType (lName);

            if (lType != null)
                return Activator.CreateInstance (lType) as T;
            else
                throw new NotImplementedException (lName);
        }

        public void Dispose() { }
    }
}
