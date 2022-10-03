using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoxelMap : MonoBehaviour
{
    [Header("Voxel Map Size")]
    public float size = 2f;
    [Range(1, 50)]
    public int voxelResolution = 8;
    [Range(1, 10)]
    public int chunkResolution = 2;

    private float noiseSeed;

    public VoxelGrid voxelGridPrefab;
    private VoxelGrid[] chunks;

    [SerializeField]
    private OreList oreList;

    public OreList OreList
    {
        get { return oreList; }
        set { oreList = value; }
    }

    private float chunkSize, voxelSize, halfSize;

    public void Awake()
    {
        noiseSeed = Random.Range(100.0f, 10000.0f);

        halfSize = size * 0.5f;
        chunkSize = size / chunkResolution;
        voxelSize = chunkSize / voxelResolution;

        chunks = new VoxelGrid[chunkResolution * chunkResolution];
        for (int i = 0, y = 0; y < chunkResolution; y++)
        {
            for (int x = 0; x < chunkResolution; x++, i++)
            {
                CreateChunk(i, x, y);
            }
        }

        for (int i = 0, y = 0; y < chunkResolution; y++)
        {
            for (int x = 0; x < chunkResolution; x++, i++)
            {
                GenerateChunkOres(i, x, y);
            }
        }
    }

    private void CreateChunk(int i, int x, int y)
    {
        VoxelGrid chunk = Instantiate(voxelGridPrefab);
        chunks[i] = chunk;
        chunk.Initialize(noiseSeed, voxelResolution, chunkSize, chunkResolution, new Vector2Int(x, y), OreList.StoneMaterial, OreList.BaseStone);
        chunk.transform.parent = transform;
        chunk.transform.localPosition = new Vector3(x * chunkSize - halfSize, y * chunkSize - halfSize);
    }

    private void GenerateChunkOres(int i, int x, int y)
    {
        for (int a = 0; a < OreList.Ores.Count; a++)
        {
            chunks[i].GenerateOre(noiseSeed, OreList.Percentage[a], OreList.Ores[a], OreList.OreMaterial[a], new Vector2Int(x, y));
        }
    }
}
