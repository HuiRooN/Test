using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
	public int damage;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Item")
		{
			Item item = other.GetComponent<Item>();
			Destroy(other.gameObject);
		}
	}
}
