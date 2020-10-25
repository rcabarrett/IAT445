using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyType keyType;

    public enum KeyType
    {
        Pink,
        Teal,
        Yellow
    }

    public KeyType GetKeyType()
    {
        return keyType;
    }

    public static Color GetColor(KeyType keyType)
    {
        switch (keyType)
        {
            default:
            case KeyType.Pink:   return Color.magenta;
            case KeyType.Teal: return Color.cyan;
            case KeyType.Yellow:  return Color.yellow;
        }
    }
}
