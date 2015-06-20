using UnityEngine;
using System.Collections;

public class ColourSelector : MonoBehaviour {

    public string colour;

    void OnMouseDown()
    {
        this.GetComponentInParent<RandomMatchmaker>().Connect(this.GetComponent<MeshRenderer>().material.color);
        transform.parent.gameObject.SetActive(false);
    }
}
