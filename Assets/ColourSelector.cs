using UnityEngine;
using System.Collections;

public class ColourSelector : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseDown()
    {
        this.GetComponentInParent<RandomMatchmaker>().Connect(this.GetComponent<MeshRenderer>().material.color);
        transform.parent.gameObject.SetActive(false);
    }
}
