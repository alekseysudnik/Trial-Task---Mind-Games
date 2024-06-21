using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    [SerializeField] private ItemType _itemType;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Color _color;
    [SerializeField] private GameObject _prefab;

    public ItemType itemType { get => _itemType; }
    public Sprite icon { get => _icon; }
    public Color color { get => _color; }
    public GameObject prefab { get => _prefab; }
}


