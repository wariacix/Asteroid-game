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
    private int resolution;

    [SerializeField]
    private Material mat;

    private float squareSize;
    public void Initialize(float noiseSeed, int resolution, float size, Vector2Int position)
    {
        this.resolution = resolution;
        squareSize = size / resolution;
        squares = new GameObject[resolution * resolution];

        for (int i = 0, y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++, i++)
            {
                if (!CheckSquare(noiseSeed, i, x, y, position)) CreateSquare(noiseSeed, i, x, y, position);
            }
        }
    }

    private void CreateSquare(float noiseSeed, int i, int x, int y, Vector2Int position)
    {
        GameObject o = Instantiate(squarePrefab);
        o.AddComponent<BoxCollider2D>();
        o.transform.parent = transform;
        o.transform.localPosition = new Vector3((x + 0.5f) * squareSize, (y + 0.5f) * squareSize);
        o.transform.localScale = Vector3.one * squareSize;
        squares[i] = o;
        ChangeColor(noiseSeed, i, x, y, position);
    }

    private void ChangeColor(float noiseSeed, int i, int x, int y, Vector2Int position)
    {
        var squareRenderer = squares[i].GetComponent<MeshRenderer>();
        float noise = Mathf.PerlinNoise((((position.x + noiseSeed) * resolution) + x) * noiseRes, (((position.y + noiseSeed) * resolution) + y) * noiseRes);
        Material newMat = mat;
        newMat.color = new Color(noise, noise, noise);
        squareRenderer.material = newMat;
    }

    private bool CheckSquare(float noiseSeed, int i, int x, int y, Vector2Int position)
    {
        return (Mathf.Sqrt(Mathf.Pow(x - resolution + (position.x * resolution), 2) + Mathf.Pow(y - resolution + (position.y * resolution), 2)) + asteroidBumpiness * Mathf.PerlinNoise((((position.x + noiseSeed) * resolution) + x) * noiseRes, (((position.y + noiseSeed) * resolution) + y) * noiseRes) > asteroidSize);
    }
}
