using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

    public Vector3 direction;
    public float speed = 1.0f;
    void FixedUpdate()
    {
        transform.position += speed * Vector3.Normalize(direction);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.name == "Background")
        {
          
            Destroy(this.gameObject);
        }

    }
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            string message = speed.ToString() + ';' + direction.x.ToString() + ',' + direction.y.ToString() + ',' + direction.z.ToString();
            stream.SendNext(message);
        }
        else
        {
            string message = (string)stream.ReceiveNext();
            speed = float.Parse(message.Split(';')[0]);
            direction = new Vector3(float.Parse(message.Split(';')[1].Split(',')[0]), float.Parse(message.Split(';')[1].Split(',')[1]), float.Parse(message.Split(';')[1].Split(',')[2]));
        }
    }
}
