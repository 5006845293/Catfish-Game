using UnityEngine;
using TMPro; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Tutorial : MonoBehaviour
{
    public TextMeshProUGUI dialog;
	public GameObject Continue;
    public GameObject[] arrows;
    public GameObject[] baitfish;
	public Animator animator;
	public TextMeshProUGUI depth;
	public TextMeshProUGUI fishcount;
	public GameObject depthbox;
	public GameObject hookbox;
	public GameObject fishcntbox;
	public GameObject lilfish;
	public GameObject catfish;

    private bool CanContinue;
    private int counter = 0;
    private bool[] functionCalled = {false, false, false, false, false};

    void Update()
    {
        // Check if Esc key is pressed and the other variable is true
        if (Input.GetKeyDown(KeyCode.Escape) && CanContinue)
        {
            counter++; // Increment the counter
			CanContinue = false;
			Debug.Log("WE GO TO NEXT");
        }

        // Use a switch statement to handle different counter values
        switch (counter)
        {
            case 0:
                if (!functionCalled[0])
                {
                    StartCoroutine(TutorialScene1());
                    functionCalled[0] = true;
                }
                break;
            case 1:
                if (!functionCalled[1])
                {
                    StartCoroutine(TutorialScene2());
                    functionCalled[1] = true;
                }
                break;
			case 2:
				if (!functionCalled[2])
                {
                    StartCoroutine(TutorialScene3());
                    functionCalled[2] = true;
                }
				break;
			case 3:
				if (!functionCalled[3])
                {
                    StartCoroutine(TutorialScene4());
                    functionCalled[3] = true;
                }
				break;
			case 4:
				if (!functionCalled[4])
                {
                    StartCoroutine(TutorialScene5());
                    functionCalled[4] = true;
                }
				break;
            default:
                // Reset the counter and function call flags if it goes beyond the desired range
                SceneManager.LoadScene("Fishing Location");
                break;
        }
    }


    IEnumerator TutorialScene1()
    {
        dialog.text = "Welcome to Whisker Fish!";

        yield return new WaitForSeconds(3f);
		dialog.text = "This is where you will be catching your baitfish.";
		yield return new WaitForSeconds(3f);
		dialog.text = "It's broken into 3 different parts.";
		yield return new WaitForSeconds(3f);
		dialog.text = "The light ocean \n(0m-150m)";
		arrows[0].SetActive(true);
		animator.SetTrigger("Bob");
		yield return new WaitForSeconds(3f);
		dialog.text = "The middle ocean \n(150m-350m)";
		arrows[1].SetActive(true);
		yield return new WaitForSeconds(3f);
		dialog.text = "The deep ocean \n(350m-500m)";
		arrows[2].SetActive(true);
		
		CanContinue = true;
		Continue.SetActive(true);
		
		
    }

    IEnumerator TutorialScene2()
    {
		Continue.SetActive(false);
		CanContinue = false;
		dialog.text = "These are the fish you can catch in each of the areas.";
		yield return new WaitForSeconds(3f);

		// Define the number of fish to activate in each group
		int[] groupSizes = new int[] { 2, 3, 2 };

		// Define the corresponding dialog texts
		string[] dialogTexts = new string[] { "For the light ocean.", "For the middle ocean.", "For the deep ocean." };

		int fishIndex = 0;

		// Iterate through each group size and activate fish accordingly
		for (int i = 0; i < groupSizes.Length; i++)
		{
			int groupSize = groupSizes[i];

			// Activate fish in the current group
			for (int j = 0; j < groupSize; j++)
			{
				baitfish[fishIndex].SetActive(true);
				fishIndex++;
			}

			// Update dialog text for the current group
			dialog.text = dialogTexts[i];
			yield return new WaitForSeconds(3f);
		}
		
		CanContinue = true;
		Continue.SetActive(true);
    }
	
	IEnumerator TutorialScene3()
    {
		Continue.SetActive(false);
		CanContinue = false;
		depthbox.SetActive(true);
		dialog.text = "You can see what depth you are at anytime.";
		yield return new WaitForSeconds(3f);
		dialog.text = "use the depth counter in the top left corner";
		yield return new WaitForSeconds(3f);
		
		 foreach (GameObject arrow in arrows)
		{
			arrow.SetActive(false);
		}
		
		
		dialog.text = "Now lets move in a little closer";
		yield return new WaitForSeconds(3f);
		dialog.text = "to start catching some fish!";

		CanContinue = true;
		Continue.SetActive(true);
	}
	
	IEnumerator TutorialScene4()
    {
		Continue.SetActive(false);
		CanContinue = false;
		Camera.main.transform.position = new Vector3(0f, -1.2f, -10f);
		depthbox.SetActive(false);
		// Change the camera size
		Camera.main.orthographicSize = 25f;

		// Display dialog about the hook
		dialog.text = "This is your hook.";
		hookbox.SetActive(true);
		yield return new WaitForSeconds(3f);
		
		dialog.text = "Hitting a fish with this will catch it.";
		yield return new WaitForSeconds(3f);

		// Change dialog to caution about fish limit
		dialog.text = "But be careful!";
		yield return new WaitForSeconds(3f);
		dialog.text = "you can only have 5 fish on your hook at a time.";
		yield return new WaitForSeconds(3f);

		// Change dialog to inform about fish count display
		dialog.text = "You can always see how many fish you have on your hook";
		dialog.text = "by either the fish count in the top left corner";
		fishcntbox.SetActive(true);
		yield return new WaitForSeconds(3f);
		dialog.text = "or by the little fish added to your hook.";
		lilfish.SetActive(true);

		// Wait for a few seconds before enabling continue button
		yield return new WaitForSeconds(3f);

		CanContinue = true;
		Continue.SetActive(true);
	}
	
	IEnumerator TutorialScene5()
    {
		Continue.SetActive(false);
		CanContinue = false;
		fishcntbox.SetActive(false);
		lilfish.SetActive(false);
		dialog.text = "Now for the best part!";
		yield return new WaitForSeconds(3f);
		dialog.text = "How to catch the catfish!";
		yield return new WaitForSeconds(3f);
		dialog.text = "These magical catfish are attracted by the pattern of fish on your hook";
		yield return new WaitForSeconds(3f);
		dialog.text = "We believe the pattern they are attracted to has something to do with their names";
		yield return new WaitForSeconds(3f);
		dialog.text = "for instance to attract the zebra catfish";
		catfish.SetActive(true);
		yield return new WaitForSeconds(3f);
		dialog.text = "you can catch any alternation of black and white fish";
		
		
		CanContinue = true;
		Continue.SetActive(true);
	}
}
