using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static partial class GFunc
{
    public static void DebugNonFindObject(this GameObject object_, string name_)
    {
        Debug.Log($"{name_} object cannot be found in the {object_.name} object");
    }

    public static void DebugNonFindComponent(this GameObject object_, Type type_)
    {
        Debug.Log($"{object_.name} not found {type_}");
    }

    public static void DebugNonChildren(this GameObject object_)
    {
        Debug.Log($"{object_.name} has no children");
    }

    public static void DebugNonFindComponentType(Type type_)
    {
        Debug.Log($"Type {type_} is not found Component type");
    }

    public static void DebugNonFindPrimitiveType(PrimitiveType type_)
    {
        Debug.Log($"{type_} is not a valid PrimitiveType");
    }

    public static void DebugError(Type type_)
    {
        Debug.LogError($"{type_} is Error");
    }

    public static void DebugTypeToString(string value_)
    {
        Debug.Log($"{value_} is not ComponentType");
    }

    public static void DebugNonFindSceneToObject(string name_)
    {
        Debug.Log($"{SceneManager.GetActiveScene().name} not found {name_}");
    }
}