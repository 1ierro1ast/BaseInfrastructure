using System.Collections.Generic;
using UnityEngine;

namespace Codebase.Extensions
{
    public static class CollectionsExtensions
     {
         public static T GetRandom<T>(this List<T> list)
         {
             return list.Count == 0 ? default : list[Random.Range(0, list.Count)];
         }
         
         public static T GetRandom<T>(this T[] array)
         {
             return array.Length == 0 ? default : array[Random.Range(0, array.Length)];
         }
     }
 }