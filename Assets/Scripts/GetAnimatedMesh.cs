using UnityEngine;
using System.Collections;

public class GetAnimatedMesh : MonoBehaviour
{

    public SkinnedMeshRenderer meshRenderer;
    public Mesh mesh;

    // Update is called once per frame
    void Update()
    {
        meshRenderer.BakeMesh(mesh);
    }
}