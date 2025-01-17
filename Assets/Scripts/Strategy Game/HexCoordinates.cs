using System;
using UnityEngine;

public class HexCoordinates : MonoBehaviour
{
    public static float xOffset = 2, yOffset = 1, zOffset = 1.73f;

    [Header("Offset coordinates")]
    [SerializeField]
    private Vector3Int offsetCoordinates;

    internal Vector3Int GetHexCoords()
    {
        return offsetCoordinates;
    }

    private void Awake()
    {
        offsetCoordinates = ConvertPositionToOffset(transform.position);
    }

    private Vector3Int ConvertPositionToOffset(Vector3 position)
    {
        int x = Mathf.CeilToInt(position.x/ xOffset);
        int y = Mathf.RoundToInt(position.y / yOffset);
        int z = Mathf.RoundToInt(position.z / zOffset);
        return new Vector3Int(x, y, z);
    }
}
