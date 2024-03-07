using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CatfishPatterns : MonoBehaviour
{
	public DepthController DepthScript;
	public Hook hookscript;
	public List<GameObject> fishCaught;
	public List<string> fishColors;
	private Dictionary<string, List<List<string>>> fishPatterns = new Dictionary<string, List<List<string>>>();
	private string catfish;
    // Start is called before the first frame update
    void Start()
    {
        fishColors = new List<string>();
		DefinePatterns();
    }

    // Update is called once per frame
    void Update()
    {
		//decde fish
        if(DepthScript.GetCurrentDepth()>=500){
			fishCaught = hookscript.fishesOnHook;
			
			fishColors.Clear(); // Clear the list before populating it with the colors.

            // Loop through each fish and add its color to the fishColors list.
            foreach (var fish in fishCaught)
            {
                string color = fish.GetComponent<Fish>().Selected_Color.ToString();
                fishColors.Add(color);
            }
			while (fishColors.Count < 5)
            {
                fishColors.Add("blank");
            }
            // Check the fishColors list against patterns.
            catfish = CheckPatterns();
			
		}
		
		//send fish selection to next scene
		if(DepthScript.GetCurrentDepth()==0 && !hookscript.isHooking){
			PlayerPrefs.SetString("Catfish", catfish);
			Debug.Log(catfish);
			Debug.Log("transition to next scene");
			SceneManager.LoadScene(6);
		}
    }

	//function for setting the patterns accepted by each fish
	void DefinePatterns()
    {
        // Define patterns for each fish type.
        List<List<string>> zebraPatterns = new List<List<string>>()
        {
            new List<string> {"Black", "White", "Black", "White", "blank"},
            new List<string> {"White", "Black", "White", "Black", "blank"}
        };

        List<List<string>> rainbowPatterns = new List<List<string>>()
        {
            new List<string> {"Red", "Yellow", "Green", "Blue", "Purple"},
            new List<string> {"Purple", "Blue", "Green", "Yellow", "Red"}
        };
		
		List<List<string>> firePatterns = new List<List<string>>()
        {
            new List<string> {"Red", "Yellow", "Red", "Yellow", "Red"},
            new List<string> {"Red", "Red", "Red", "Yellow", "Yellow"},
			new List<string> {"Yellow", "Yellow", "Yellow", "Red", "Red"},
        };
		
		List<List<string>> earthPatterns = new List<List<string>>()
        {
            new List<string> {"Black", "Green", "Green", "Black", "Black"},
            new List<string> {"Green", "Green", "Green", "Green", "Green"}
        };
		
		List<List<string>> airPatterns = new List<List<string>>()
        {
            new List<string> {"White", "White", "White", "White", "White"},
            new List<string> {"White", "White", "White", "White", "blank"},
			new List<string> {"White", "White", "White", "blank", "blank"},
			new List<string> {"White", "White", "blank", "blank", "blank"},
			new List<string> {"White", "blank", "blank", "blank", "blank"}
        };
		
		List<List<string>> waterPatterns = new List<List<string>>()
        {
            new List<string> {"blank", "blank", "blank", "blank", "blank"}
        };
		
		List<List<string>> springPatterns = new List<List<string>>()
        {
            new List<string> {"Yellow", "Green", "Yellow", "Green", "White"},
            new List<string> {"Yellow", "Green", "Yellow", "Green", "blank"},
			new List<string> {"Yellow", "Green", "Yellow", "Green", "Blue"}
        };

		List<List<string>> winterPatterns = new List<List<string>>()
        {
            new List<string> {"Blue", "White", "Blue", "White", "Blue"},
            new List<string> {"White", "White", "Blue", "Blue", "Blue"},
			new List<string> {"White", "White", "White", "Blue", "Blue"},
			new List<string> {"White", "White", "White", "Blue", "Blue"}
        };
		
		List<List<string>> fallPatterns = new List<List<string>>()
        {
            new List<string> {"Red", "Yellow", "White", "Red", "Yellow"},
            new List<string> {"White", "White", "Red", "Yellow", "Red"},
			new List<string> {"White", "Red", "Red", "Yellow", "Red"},
			new List<string> {"Red", "Red", "Red", "Yellow", "Yellow"}
        };
		
		List<List<string>> summerPatterns = new List<List<string>>()
        {
            new List<string> {"Green", "Yellow", "Green", "Yellow", "White"},
            new List<string> {"Green", "Yellow", "Green", "Yellow", "blank"},
			new List<string> {"Green", "Yellow", "Green", "Yellow", "Blue"}
        };
		
		List<List<string>> lovePatterns = new List<List<string>>()
        {
            new List<string> {"Red", "Red", "White", "White", "White"},
            new List<string> {"Red", "White", "Red", "White", "Red"}
        };
		
		List<List<string>> hatePatterns = new List<List<string>>()
        {
            new List<string> {"Red", "Black", "Red", "Black", "Red"},
            new List<string> {"Black", "Red", "Black", "Red", "Black"}
        };
		
        // Add patterns to the dictionary.
        fishPatterns.Add("Zebra Catfish", zebraPatterns);		//color fish	1
        fishPatterns.Add("Rainbow Catfish", rainbowPatterns);	//color fish	2
		fishPatterns.Add("Fire Catfish",firePatterns);			//element fish	3
		fishPatterns.Add("Earth Catfish",earthPatterns);		//element fish	4
		fishPatterns.Add("Air Catfish",airPatterns);			//element fish	5
		fishPatterns.Add("Water Catfish",waterPatterns);		//element fish	6
		fishPatterns.Add("Spring Catfish",springPatterns);		//season fish	7
		fishPatterns.Add("Winter Catfish",winterPatterns);		//season fish	8
		fishPatterns.Add("Fall Catfish",fallPatterns);			//season fish	9
		fishPatterns.Add("Summer Catfish",summerPatterns);		//season fish	10
		fishPatterns.Add("Love Catfish",lovePatterns);			//season fish	11
		fishPatterns.Add("Hate Catfish",hatePatterns);			//season fish	12
		
		
    }
	
	
	string CheckPatterns()
	{
		
		string returnfish = "basic Catfish";

		foreach (var fish in fishPatterns)
		{
			List<List<string>> patterns = fish.Value;

			foreach (var pattern in patterns)
			{
				bool patternMatched = true;

				for (int i = 0; i < pattern.Count; i++)
				{
					if (fishColors[i] != pattern[i])
					{
						patternMatched = false;
					}
				}

				if (patternMatched)
				{
					returnfish = fish.Key;
					break;
				}
			}
		}

		return returnfish;
	}


}
