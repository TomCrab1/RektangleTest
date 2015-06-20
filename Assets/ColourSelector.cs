using UnityEngine;
using System.Collections;

public class ColourSelector : MonoBehaviour {

    public string colour;
	
    void OnMouseDown()
    {
        this.GetComponentInParent<RandomMatchmaker>().Connect(colour);
        transform.parent.gameObject.SetActive(false);
    }
}
