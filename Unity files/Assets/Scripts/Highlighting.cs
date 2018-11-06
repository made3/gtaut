using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighting : MonoBehaviour {

    [SerializeField]
    public float outlineWidth;

    [SerializeField]
    private Color outlineCol = new Color32(171, 159, 139, 255);

    [HideInInspector]
    public bool isOutlined = false;

    [SerializeField]
    private Shader outlineShader;

    private Shader startShader;

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
            outlineMaterials.Add(r.material);
            startShader = r.material.shader;
        }
    }

    public void ToggleOutline()
    {
        if (!isOutlined)
        {
            foreach (Material m in outlineMaterials)
            {
                print("Use OutlineShader");
                m.shader = outlineShader;
                //m.SetColor("_Color", Color.white);
                m.SetColor("_FirstOutlineColor", outlineCol);
                m.SetFloat("_FirstOutlineWidth", outlineWidth);
                m.SetFloat("_SecondOutlineWidth", 0);
            }
            isOutlined = true;
        }
        else
        {
            foreach (Material m in outlineMaterials)
            {
                m.shader = startShader;
            }
            isOutlined = false;
        }
    }
}
