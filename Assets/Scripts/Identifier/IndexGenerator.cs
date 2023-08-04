using System;

namespace Identifier
{
    public static class IndexGenerator
    {
        private const int HASH = 10070531;

        public static int GenerateIndex(this string data)
        {
            return GetIndexForType(data);
        }

        public static int GetIndexForType(Type c)
        {
            var typeName = c.Name;
            return GetIndexForType(typeName);
        }

        public static int GetIndexForType(string typeName)
        {
            var lenght = typeName.Length;
            var index = lenght + typeName[0].GetHashCode();

            for (var i = 0; i < lenght; i++)
            {
                var charC = typeName[i].GetHashCode() * i;
                index += (charC + (101161 * (i + 3)) + (HASH ^ (charC * i)));
            }

            return index;
        }

        public static int GetUniqueIndex()
        {
            return Guid.NewGuid().GetHashCode();
        }
    }
}