using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using Identifier;
using Random = UnityEngine.Random;

namespace Helpers
{
    public static class Randomizer
    {
        /// <summary>
        /// Random enum value. First member (usually "None") exclude.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T RandomEnumValue<T>() where T : Enum
        {
            var v = Enum.GetValues(typeof(T));
            return (T)v.GetValue(Random.Range(1, v.Length));
        }

        /// <summary>
        /// Random int value in range
        /// </summary>
        /// <param name="min">inclusive</param>
        /// <param name="max">inclusive</param>
        /// <returns></returns>
        public static int RandomInt(int min, int max)
        {
            return Random.Range(min, max + 1);
        }
    }
}