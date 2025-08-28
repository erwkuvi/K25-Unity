using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

[ExecuteInEditMode]
public class GameObjectModifier : MonoBehaviour
{
    public enum LayoutType {radial, linear}
    // start the enum as radial to be changed if needed
    public LayoutType layout = LayoutType.radial;
    
    public GameObject prefab;
    public int count = 2;

    [Header("Radial Settings")] 
    public float radius = 5f;
    
    [Header("Linear Settings")] 
    public float spacing = 2f;
    public Vector3 direction = Vector3.right;


    
    void Start()
    {
        Spawn();
    }

    private void OnDrawGizmos()
    {
        if (count <= 0) return;
        
        Gizmos.color = Color.cyan;
        for (int i = 0; i < count; i++)
        {
            Vector3 localPos = Vector3.zero;

            if (layout == LayoutType.radial)
            {
                float angle = i * Mathf.PI * 2f / count;
                localPos = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * radius;
            }
            else if (layout == LayoutType.linear)
            {
                localPos = direction.normalized * spacing * i;
            }

            Vector3 worldPos = transform.TransformPoint(localPos);
            Gizmos.DrawWireSphere(worldPos, 0.25f); // Visual indicator (can be cube or custom shape)
        }
    }
    

    void Spawn()
    {
        if (transform.childCount > 0)
        {
            Debug.Log("Children already exist, skipping spawn.");
            return;
        }
        if (prefab == null)
        {
            Debug.unityLogger.Log("Prefab is null");
            return;
        }
        
        Vector3 pos = Vector3.zero;

        for (int i = 0; i < count; i++)
        {
            if (layout == LayoutType.radial)
            {
                float angle = i * Mathf.PI * 2 / count;
                pos = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            }
            else if (layout == LayoutType.linear)
            {
                pos = direction.normalized * spacing * i;

            }
            GameObject obj = Instantiate(prefab, transform);
            obj.transform.localPosition = pos;
            obj.transform.localRotation = Quaternion.identity;
        }
    }
  
}
