using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighting : MonoBehaviour {

    [SerializeField]
    public float outlineWidth;

    [HideInInspector]
    public bool isOutlined = false;

    private List<Material> outlineMaterials;

	// Use this for initialization
	void Start () {
        outlineMaterials = new List<Material>();
        if (this.gameObject.layer != LayerMask.NameToLayer("Interactable"))
        {
            this.gameObject.layer = LayerMask.NameToLayer("Interactable");
        }
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            if (r.material.shader.name == "Outlined/UltimateOutline")
            {
                outlineMaterials.Add(r.material);
            }
        }
    }

    public void ToggleOutline()
    {
        if (!isOutlined)
        {
            foreach (Material m in outlineMaterials)
            {
                m.SetFloat("_FirstOutlineWidth", outlineWidth);
            }
            isOutlined = true;
        }
        else
        {
            foreach (Material m in outlineMaterials)
            {
                m.SetFloat("_FirstOutlineWidth", 0);
            }
            isOutlined = false;
        }
    }
}
