using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ore List", menuName = "Asteroid Generation/Ore List")]
public class OreList : ScriptableObject
{
    [SerializeField]
    private Item baseStone;
    public Item BaseStone
    {
        get { return baseStone; }
        set { baseStone = value; }
    }

    [SerializeField]
    private Material stoneMaterial;
    public Material StoneMaterial
    {
        get { return stoneMaterial; }
        set { stoneMaterial = value; }
    }

    [SerializeField]
    private List<Item> ores;
    public List<Item> Ores
    {
        get { return ores; }
        set { ores = value; }
    }

    [SerializeField]
    private List<Material> oreMaterial;
    public List<Material> OreMaterial
    {
        get { return oreMaterial; }
        set { oreMaterial = value; }
    }

    [SerializeField]
    private List<float> percentage;
    public List<float> Percentage
    {
        get { return percentage; }
        set { percentage = value; }
    }
}
