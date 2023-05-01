using System;

namespace Utils
{
    public static class Extension
    {
        public static T CastTo<T>(this object data)
        {
            if (!(data is T castedData))
            {
                throw new InvalidCastException(typeof(T).ToString());
            }

            return castedData;
        }
    }
}