using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
  public  bool controllable = false;
  public string Name = "test";
  public Color colour;
  Vector3 targetDir;
  public GameObject bullet;

  public Vector3 lerpto;
    

    // Use this for initialization
    void Start()
    {
        setColour(colour);
      
    }
    public void setColour(Color col)
    {
        colour = col;
        Material mat = new Material(GetComponentInChildren<MeshRenderer>().material);
        mat.color = colour;
        GetComponentInChildren<MeshRenderer>().material = mat;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0)&&controllable)
        {
            if (GetComponentInChildren<CustomGeometry>().NumVerts < 5) return;
            GetComponentInChildren<CustomGeometry>().NumVerts--;
            GameManager.addBullet(0.1f, targetDir, this.transform.position + targetDir);
   
        }
        float speed = 0.2f;
        if (controllable)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.position = transform.position + new Vector3(0, speed, 0);

            }
            if (Input.GetKey(KeyCode.S))
            {
                transform.position = transform.position + new Vector3(0, -speed, 0);

            }
            if (Input.GetKey(KeyCode.A))
            {
                transform.position = transform.position + new Vector3(-speed, 0, 0);

            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position = transform.position + new Vector3(speed, 0, 0);

            }
        }
        else
        {
        transform.position=    Vector3.Lerp(transform.position, lerpto, 0.1f);
        }            
        Vector3 dist = Vector3.Normalize(Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10)) - transform.position) * GetComponentInChildren<CustomGeometry>().size;
    
        float targetDist = 999;
        foreach (Vector3 target in GetComponentInChildren<CustomGeometry>().aimVectors)
        {
           
            if(Vector3.Distance(dist,target)<targetDist){
                targetDist = Vector3.Distance(dist, target);
                targetDir = target;
          
            }
        }
        gameObject.transform.GetChild(0).GetChild(0).transform.localPosition = targetDir;
    }
    void OnDrawGizmos()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
        Gizmos.DrawSphere(mousePos, 1.0f);
    }
   
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            string message = colour.r.ToString() + ',' + colour.g.ToString() + ',' + colour.b.ToString();
            message += ';';
            message += transform.position.x.ToString() + ',' + transform.position.y.ToString() + ',' + transform.position.z.ToString();
            message += ';';
            message += GetComponentInChildren<CustomGeometry>().NumVerts;           
       
            stream.SendNext(message);
        }
        if (stream.isReading)
        {
            string message = (string)stream.ReceiveNext();
            Debug.Log(message);
            Color col = new Color();
            col.r = float.Parse(message.Split(';')[0].Split(',')[0]);
            col.g = float.Parse(message.Split(';')[0].Split(',')[1]);
            col.b = float.Parse(message.Split(';')[0].Split(',')[2]);
            Vector3 targetPos = new Vector3();
            targetPos.x = float.Parse(message.Split(';')[1].Split(',')[0]);
            targetPos.y = float.Parse(message.Split(';')[1].Split(',')[1]);
            targetPos.z = float.Parse(message.Split(';')[1].Split(',')[2]);
            lerpto = targetPos;
            GetComponentInChildren<CustomGeometry>().NumVerts = int.Parse(message.Split(';')[2]);
            setColour(col);
        }
        //Your code here..
    }
    void OnGUI()
    {
     
        if (controllable){

            GUI.Label(new Rect(Screen.width / 2 - 50, 20, 100, 20), new GUIContent(Name));
        
        }
    }
}