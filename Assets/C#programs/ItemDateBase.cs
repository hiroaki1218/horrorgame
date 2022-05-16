using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemDateBase : ScriptableObject
{
    [SerializeField]
    public List<Items> itemList = new List<Items>();
}

