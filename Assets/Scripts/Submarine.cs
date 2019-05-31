using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : MonoBehaviour
{
	int structureCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameObject block = Instantiate(Resource.Load<GameObject>("Prefabs/BasicBlock"));
        AddBlock(block);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBlock(GameObject block){
    	block.transform.parent = transform;
    	block.transform.localPosition = SnapToGrid(block.transform.localPosition);
    	structureCount += 1;
    	Block b = block.GetComponent<Block>();
    	b.structure = this;
    	//totalWeight += block.GetComponent<Block>().weight
    }

    Vector3 SnapToGrid(Vector3 input){
		float x = Mathf.Round(input.x);
		float y = Mathf.Round(input.y);
		float z = Mathf.Round(input.z);
		Vector3 output = new Vector3(x, y, z);
		return output;
	}
}
