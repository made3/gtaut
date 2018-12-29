using UnityEngine;
using UnityEditor;

public class Level_Add_Scripts : ScriptableObject
{

    [MenuItem("Custom/Level add scripts")]
    static void LevelAddScripts()
    {

        GameObject[] allObjects = (GameObject[])Editor.FindObjectsOfType(typeof(GameObject));
        foreach (GameObject obj in allObjects)
        {
            if (obj.GetComponent<Highlighting>() != null)
            {
                obj.GetComponent<Highlighting>().outlineWidth = 0.01f;
            }
        }
    }

    [MenuItem("Custom/AddLayerToPickupables")]
    static void AddLayer()
    {

        GameObject[] allObjects = (GameObject[])Editor.FindObjectsOfType(typeof(GameObject));
        foreach (GameObject obj in allObjects)
        {
            if (obj.CompareTag("Pickupable"))
            {
                obj.layer = LayerMask.NameToLayer("Pickupable");
            }
        }
    }


}
