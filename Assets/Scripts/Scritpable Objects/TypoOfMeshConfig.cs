using UnityEngine;

[CreateAssetMenu(fileName = "NewMesh Conig", menuName = "MyScriptables/Create New Mesh config")]
public class TypeOfMeshConfg : ScriptableObject
{
    public TypeOfMeshList TypeOfMesh;
}

public enum TypeOfMeshList
{
    Renderer,
    SkinnedMesh
}