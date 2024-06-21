using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeVisual : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;

    public void Awake()
    {
        if (TryGetComponent(out IPickable pickableObject))
        {
            Color color = pickableObject.GetPickableSO().color;
            meshRenderer.material.color = color;
        }
    }

}
