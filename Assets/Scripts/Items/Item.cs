using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public event Action<Item> Collected;

    protected void DisableObject()
    {
        Collected?.Invoke(this);
    }
}
