using UnityEngine;
using System.Collections;

public class CustomGeometry : MonoBehaviour {

    

    public float SideSpawnCooldown = 3;
    float lastSideSpawn;
    public float size = 0.15f;
    private int numVerts = 3;
    public Vector3[] aimVectors;
    public int NumVerts
    {
        get { return numVerts; }
        set
        {
            numVerts = value;
            if (numVerts < 4)
            {
                numVerts = 3;
                //remove layer
            }
            if (numVerts > 8)
            {
                numVerts = 8;
                //add layer

            }
            GenerateMesh(numVerts, size);
        }
    }
    
 
   
    public MeshFilter filter;
 

    public void GenerateMesh(int p_numVerts, float scale)
    {
        filter.mesh = null;
      
        Vector3[] verts = new Vector3[p_numVerts];
        Vector2[] uvs = new Vector2[p_numVerts];
        int[] tris = new int[(p_numVerts * 3)];
        verts[0] = Vector3.zero;
        uvs[0] = new Vector2(0.5f, 0.5f);
        float angle = 360.0f / (float)(p_numVerts - 1);
        aimVectors = new Vector3[p_numVerts];
        for (int i = 1; i < p_numVerts; i++)
        {
           
            verts[i] = Quaternion.AngleAxis(angle * (float)(i - 1), Vector3.back) * Vector3.up * (scale);
            float normedHorizontal = (verts[i].x + 1.0f) * 0.5f;
            float normedVertical = (verts[i].y + 1.0f) * 0.5f;
            uvs[i] = new Vector2(normedHorizontal, normedVertical);
            aimVectors[i-1] = verts[i];

        }
  
      
        for (int i = 0; i + 2 < p_numVerts; i++)
        {
            int index = i * 3;
            tris[index] = 0;
            tris[index + 1] = i + 1;
            tris[index + 2] = i + 2;

        }
        int lastTri = tris.Length - 3;
        tris[lastTri] = 0;
        tris[lastTri + 1] = p_numVerts - 1;
        tris[lastTri + 2] = 1;

        filter.mesh.vertices = verts;
        filter.mesh.uv = uvs;
        filter.mesh.triangles = tris;
     
      
    }
    
	// Use this for initialization
    	void Start () { 
            lastSideSpawn = 0;
            filter = GetComponents<MeshFilter>()[0];          
            filter.mesh = new Mesh();
            GenerateMesh(NumVerts, size);
           // collider.sharedMesh = filter.mesh;
            
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (lastSideSpawn < Time.time)
        {
         
            lastSideSpawn = Time.time+ SideSpawnCooldown;         
            NumVerts++;
           

        }
	}
}
