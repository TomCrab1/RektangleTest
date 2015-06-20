using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
  public  bool controllable = false;
  public string Name = "test";
  public Color colour;
    

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
    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            string message = colour.r.ToString() + ',' + colour.g.ToString() + ',' + colour.b.ToString();
            
            stream.SendNext(message);
        }
        if (stream.isReading)
        {
            string message = (string)stream.ReceiveNext();
            Color col = new Color();
            col.r = float.Parse(message.Split(',')[0]);
            col.g = float.Parse(message.Split(',')[1]);
            col.b = float.Parse(message.Split(',')[2]);
            setColour(col);
        }
        //Your code here..
    }
}