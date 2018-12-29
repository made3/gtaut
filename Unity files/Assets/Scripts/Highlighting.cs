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

    [SerializeField]
    private bool takeAllChildRenderer = true;

    [SerializeField]
    private List<Renderer> outlineRenderer = new List<Renderer>();

    private List<Material> outlineMaterials = new List<Material>();

    // Tried out to use a spotlight as highlight indicator

    //private GameObject lightObject;

    // Use this for initialization
    void Start () {
        if (this.gameObject.layer != LayerMask.NameToLayer("Interactable") && this.gameObject.layer != LayerMask.NameToLayer("Pickupable"))
        {
            this.gameObject.layer = LayerMask.NameToLayer("Interactable");
        }
        if (takeAllChildRenderer)
        {
            foreach(Renderer r in GetComponentsInChildren<Renderer>())
            {
                outlineMaterials.Add(r.material);
                startShader = r.material.shader;
            }
        }
        else
        {
            foreach (Renderer r in outlineRenderer)
            {
                outlineMaterials.Add(r.material);
                startShader = r.material.shader;
            }
        }
    }

    public void ToggleOutline()
    {
        if (!isOutlined)
        {
            foreach (Material m in outlineMaterials)
            {
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

        // Tried out to use a spotlight as highlight indicator

        //if (!isOutlined)
        //{
        //    lightObject = Instantiate(new GameObject(), Camera.main.transform.position, Camera.main.transform.rotation);
        //    lightObject.transform.LookAt(this.transform);
        //    lightObject.AddComponent<Light>();
        //    Light light = lightObject.GetComponent<Light>();
        //    light.type = LightType.Spot;
        //    light.intensity = 2;

        //    isOutlined = true;
        //}
        //else
        //{
        //    Destroy(lightObject);
        //    isOutlined = false;
        //}
    }
}
