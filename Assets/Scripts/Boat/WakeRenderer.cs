using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class VertexPoint
{
    public Vector3 point;
    public int index;
}

public class WakeRenderer : MonoBehaviour
{
    private const int NUM_VERTICES = 6;

    
    public Transform backRightCorner { set; private get; }
    public Transform backLeftCorner { set; private get; }
    public  int trailFrameLength { set; private get; }

    private MeshFilter mFilter;
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uvs;
    private Vector3 previousRight;
    private Vector3 previousLeft;
    private int frameCount;

    private void Awake()
    {
        mFilter = GetComponent<MeshFilter>();
        if(mFilter == null)
        {
            mFilter = gameObject.AddComponent<MeshFilter>();
        }
        mesh = new Mesh
        {
            name = "Boat wake mesh",
        };
        mFilter.mesh = mesh;
    }

    private void Start()
    {
        previousLeft = backLeftCorner.localPosition;
        previousRight = backRightCorner.localPosition;

        vertices = new Vector3[trailFrameLength * NUM_VERTICES];
        triangles = new int[vertices.Length];
        uvs = new Vector2[trailFrameLength * NUM_VERTICES];
    }

    private void LateUpdate()
    {
        // credit to https://github.com/Tvtig/UnityLightsaber/blob/d3635f5dbc568356923918ad3aca30b7b4268c72/Assets/Scripts/Lighsaber.cs
        if (frameCount == (trailFrameLength * NUM_VERTICES))
        {
            frameCount = 0;
        }

        vertices[frameCount] = backRightCorner.position;
        vertices[frameCount + 1] = backLeftCorner.position;
        vertices[frameCount + 2] = previousRight;

        vertices[frameCount + 3] = backLeftCorner.position;
        vertices[frameCount + 4] = previousLeft;
        vertices[frameCount + 5] = previousRight;

        uvs[frameCount] = new Vector2(backRightCorner.position.x, backRightCorner.position.z);
        uvs[frameCount + 1] = new Vector2(backLeftCorner.position.x, backLeftCorner.position.z);
        uvs[frameCount + 2] = new Vector2(previousRight.x, previousRight.z);
        uvs[frameCount + 3] = new Vector2(backLeftCorner.position.x, backLeftCorner.position.z);
        uvs[frameCount + 4] = new Vector2(previousLeft.x, previousLeft.z);
        uvs[frameCount + 5] = new Vector2(previousRight.x, previousRight.z);

        triangles[frameCount] = frameCount;
        triangles[frameCount + 1] = frameCount + 1;
        triangles[frameCount + 2] = frameCount + 2;
        triangles[frameCount + 3] = frameCount + 3;
        triangles[frameCount + 4] = frameCount + 4;
        triangles[frameCount + 5] = frameCount + 5;

        previousLeft = backLeftCorner.position;
        previousRight = backRightCorner.position;
        frameCount += NUM_VERTICES;

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.SetUVs(0, uvs);
    }
}
