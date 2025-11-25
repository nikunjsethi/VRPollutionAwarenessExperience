using UnityEngine;
using System.Collections.Generic; // Needed for Lists
using System.Linq; // Needed for advanced list operations

public class GlowHandler : MonoBehaviour
{
    [Header("Settings")]
    public Material glowMaterial; // Drag your Transparent Glow Material here

    // You can drag objects here in the Inspector, or pass them via code
    public List<GameObject> targetObjects; 

    void Start()
    {
        foreach(var obj in targetObjects) DisableGlow(obj);
        EnableGlow(targetObjects[0]);
    }

    /// <summary>
    /// Adds the glow material to the end of the object's material list.
    /// </summary>
    public void EnableGlow(GameObject target)
    {
        if (target == null) return;

        Renderer rend = target.GetComponent<Renderer>();
        if (rend == null) return;

        // 1. Get the current list of materials
        // We convert to a List because arrays are hard to resize in C#
        List<Material> matList = new List<Material>(rend.materials);

        // 2. Safety Check: Is the glow already there?
        // We check if the last material is already our glow material
        // (Note: Unity adds " (Instance)" to names, so we check using StartsWith or reference)
        bool alreadyHasGlow = matList.Any(m => m.name.StartsWith(glowMaterial.name));

        if (!alreadyHasGlow)
        {
            // 3. Add the glow material to the list
            matList.Add(glowMaterial);

            // 4. Assign the new list back to the Renderer
            rend.materials = matList.ToArray();
        }
    }

    /// <summary>
    /// Removes the glow material from the object's material list.
    /// </summary>
    public void DisableGlow(GameObject target)
    {
        if (target == null) return;

        Renderer rend = target.GetComponent<Renderer>();
        if (rend == null) return;

        List<Material> matList = new List<Material>(rend.materials);

        // 1. Find the glow material in the list
        // We look for a material that matches our glow material's name
        Material glowInstance = matList.LastOrDefault(m => m.name.StartsWith(glowMaterial.name));

        if (glowInstance != null)
        {
            // 2. Remove it
            matList.Remove(glowInstance);

            // 3. Assign the updated list back
            rend.materials = matList.ToArray();
            
            // Optional: Destroy the instance to free memory immediately
            // (Unity creates a copy when you access .materials)
            Destroy(glowInstance); 
        }
    }

    public void ManageGlow(int index)
    {
        if(index == 1)
        {
            DisableGlow(targetObjects[0]);
            EnableGlow(targetObjects[1]);
            EnableGlow(targetObjects[2]);
            EnableGlow(targetObjects[3]);
        }
        if(index == 2)
        {
            DisableGlow(targetObjects[1]);
            DisableGlow(targetObjects[2]);
            DisableGlow(targetObjects[3]);
            EnableGlow(targetObjects[4]);
        }
        if(index == 3)
        {
            DisableGlow(targetObjects[4]);
            EnableGlow(targetObjects[5]);
            EnableGlow(targetObjects[6]);
        }
    }
}