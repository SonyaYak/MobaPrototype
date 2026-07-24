using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public static class BaseExtensions
{
    /// <summary>
    /// Check object for class or struct and equals for null or default.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool IsNullOrDefault<T>(this T obj)
    {
        if (obj is null)
            return true;
        if (typeof(T).IsValueType)
            return EqualityComparer<T>.Default.Equals(obj, default);
        return obj.Equals(null);
    }

    public static bool InheritsFrom(this Type type, Type baseType)
    {
        // null does not have base type
        if (type == null)
        {
            return false;
        }

        // only interface or object can have null base type
        if (baseType == null)
        {
            return type.IsInterface || type == typeof(object);
        }

        // check implemented interfaces
        if (baseType.IsInterface)
        {
            return ((IList)type.GetInterfaces()).Contains(baseType);
        }

        // check all base types
        var currentType = type;
        while (currentType != null)
        {
            if (currentType.BaseType == baseType)
            {
                return true;
            }

            currentType = currentType.BaseType;
        }

        return false;
    }
}
