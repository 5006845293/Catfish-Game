using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CatfishPatterns : MonoBehaviour
{
	public DepthController DepthScript;
	public Hook hookscript;
	public List<GameObject> fishCaught;
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(DepthScript.GetCurrentDepth()>=500){
			fishCaught = hookscript.fishesOnHook;
			
			Debug.Log(fishCaught[0].GetComponent<Fish>().Selected_Color);
			Debug.Log(fishCaught[1].GetComponent<Fish>().Selected_Color);
			Debug.Log(fishCaught[2].GetComponent<Fish>().Selected_Color);
			Debug.Log(fishCaught[3].GetComponent<Fish>().Selected_Color);
			Debug.Log(fishCaught[4].GetComponent<Fish>().Selected_Color);
		}
		
		
    }

}
