using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class InstantiateByAsset : MonoBehaviour
{
    public string text;

    void Start()
    {
        Debug.Log($"[InstantiateByAsset] text:{text}");
        var meshRenderer = this.GetComponentInChildren<MeshRenderer>();
        if (meshRenderer != null)
        {
            meshRenderer.sharedMaterial.color = Color.magenta;

        }
    }

    void Update()
    {
        this.transform.Rotate(Vector3.up, 10 * Time.deltaTime);
    }
}
