using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    Dictionary<Vector3Int, Hex> hextileDict = new Dictionary<Vector3Int, Hex>();
    Dictionary<Vector3Int, List<Vector3Int>> hexTileNeighboursDict = new Dictionary<Vector3Int, List<Vector3Int>>();

    private void Start()
    {
        foreach (Hex hex in FindObjectsOfType<Hex>())
        {
            hextileDict[hex.HexCoords] = hex;
        }

        List<Vector3Int> neighbours = GetNeighboursFor(new Vector3Int(0, 0, 0));
        Debug.Log("Neighbours for (0,0,0) are:");
        foreach (Vector3Int neighbourPos in neighbours)
        {
            Debug.Log(neighbourPos);
        }
    }

    public Hex GetTileAt(Vector3Int hexCoordinates)
    {
        Hex result = null;
        hextileDict.TryGetValue(hexCoordinates, out result);
        return result;
    }

    public List<Vector3Int> GetNeighboursFor(Vector3Int hexCoordinates)
    {
        if (hextileDict.ContainsKey(hexCoordinates) == false)
                return new List<Vector3Int>();

        if (hexTileNeighboursDict.ContainsKey(hexCoordinates))
            return hexTileNeighboursDict[hexCoordinates];

        hexTileNeighboursDict.Add(hexCoordinates, new List<Vector3Int>());

        foreach (Vector3Int direction in Direction.GetDirectionList(hexCoordinates.z))
        {
            if (hextileDict.ContainsKey(hexCoordinates + direction))
            {
                hexTileNeighboursDict[hexCoordinates].Add(hexCoordinates + direction);
            }
        }
        return hexTileNeighboursDict[hexCoordinates];
    }
}

public static class Direction
{
    public static List<Vector3Int> directionsOffsetOdd = new List<Vector3Int>
    {
        new Vector3Int(-1,0,1), //N1
        new Vector3Int(0,0,1), //N2
        new Vector3Int(1,0,0), //E
        new Vector3Int(0,0,-1), //S2
        new Vector3Int(-1,0,-1), //S1
        new Vector3Int(-1,0,0), //W
    };

    public static List<Vector3Int> directionsOffsetEven = new List<Vector3Int>
    {
        new Vector3Int(0,0,1), //N1
        new Vector3Int(1,0,1), //N2
        new Vector3Int(1,0,0), //E
        new Vector3Int(1,0,-1), //S2
        new Vector3Int(0,0,-1), //S1
        new Vector3Int(-1,0,0), //W
    };

    public static List<Vector3Int> GetDirectionList(int z)
    {
        return z % 2 == 0 ? directionsOffsetEven : directionsOffsetOdd;
    }
}