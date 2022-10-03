using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class VoxelGrid : MonoBehaviour
{
    public GameObject squarePrefab;
    [Range(0.0001f, 1)]
    public float noiseRes = 1f;
    public float asteroidBumpiness = 5f;

    [SerializeField]
    private float asteroidSize = 0.25f;

    private GameObject[] squares;
    public GameObject[] Squares
    {
        get { return squares; }
        set { squares = value; }
    }

    private Item[] squaresItems;
    public Item[] SquaresItems
    {
        get { return squaresItems; }
        set { squaresItems = value; }
    }

    private int resolution;

    private float squareSize;
    public void Initialize(float noiseSeed, int resolution, float size, int chunkResolution, Vector2Int position, Material stoneMat, Item stoneItem)
    {
        this.resolution = resolution;
        squareSize = size / resolution;
        squares = new GameObject[resolution * resolution];
        squaresItems = new Item[resolution * resolution];

        for (int i = 0, y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++, i++)
            {
                if (!CheckSquare(chunkResolution, noiseSeed, i, x, y, position)) CreateSquare(noiseSeed, i, x, y, position, stoneMat, stoneItem);
            }
        }
    }

    private void CreateSquare(float noiseSeed, int i, int x, int y, Vector2Int position, Material stoneMat, Item stoneItem)
    {
        GameObject o = Instantiate(squarePrefab);
        o.AddComponent<BoxCollider2D>();
        o.transform.parent = transform;
        o.transform.localPosition = new Vector3((x + 0.5f) * squareSize, (y + 0.5f) * squareSize);
        o.transform.localScale = Vector3.one * squareSize;
        squares[i] = o;
        ChangeColorDepth(i, Mathf.PerlinNoise((((position.x + noiseSeed) * resolution) + x) * noiseRes, (((position.y + noiseSeed) * resolution) + y) * noiseRes), stoneMat);
        SquareItemSlot newSlot = squares[i].GetComponent<SquareItemSlot>();
        newSlot.Item = stoneItem;
        squaresItems[i] = stoneItem;
    }

    private void ChangeColorDepth(int i, float noise, Material mat)
    {
        MeshRenderer squareRenderer = squares[i].GetComponent<MeshRenderer>();
        Material newMat = new Material(mat);
        squareRenderer.material = newMat;
        newMat.color = new Color(noise, noise, noise);
    }

    public void GenerateOre(float noiseSeed, float percentage, Item item, Material material, Vector2Int position)
    {
        for (int i = 0, y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++, i++)
            {
                if (percentage >= Random.Range(0.000f, 100f) && squares[i] != null)
                {
                    MeshRenderer squareRenderer = squares[i].GetComponent<MeshRenderer>();
                    Material newMat = new Material(material);
                    squareRenderer.material = newMat;
                    squaresItems[i] = new Item(item);
                    SquareItemSlot newSlot = squares[i].GetComponent<SquareItemSlot>();
                    newSlot.Item = squaresItems[i];
                    ChangeColorDepth(i, Mathf.PerlinNoise((((position.x + noiseSeed) * resolution) + x) * noiseRes, (((position.y + noiseSeed) * resolution) + y) * noiseRes), squareRenderer.material);
                }
            }
        }
    }

    private bool CheckSquare(int chunkResolution, float noiseSeed, int i, int x, int y, Vector2Int position)
    {
        float fullChunkSize = chunkResolution * resolution * squareSize;
        float squarePosX = fullChunkSize * Mathf.Lerp(0, 1, x + (position.x * resolution * squareSize));
        float squarePosY = fullChunkSize * Mathf.Lerp(0, 1, y + (position.y * resolution * squareSize));

        return (Mathf.Pow(transform.position.x - squarePosX, 2) + Mathf.Pow(transform.position.y - squarePosY, 2) + asteroidBumpiness * Mathf.PerlinNoise((((position.x + noiseSeed) * resolution) + x) * noiseRes, (((position.y + noiseSeed) * resolution) + y) * noiseRes) <= Mathf.Pow(asteroidSize * squareSize, 2));
    }
}
