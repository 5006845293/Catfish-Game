// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class TutorialManager : MonoBehaviour
// {
//     public GameObject[] popUps;
//     private int popUpIndex;
//     public float waitTime = 2f;
//     public GameObject player;

//     void Update()
//     {
//         for (int i = 0; i < popUps.Length; i++)
//         {
//             popUps[i].SetActive(i == popUpIndex);
//         }

//         if (popUpIndex == 0)
//         {
//             // Check if the player is moving left or right
//             if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
//             {
//                 popUpIndex++;
//             }
//         }

//         else if (popUpIndex == 1)
//         {
//             // Example condition for triggering second pop-up (adjust as needed)
//             if (player.GetComponent<Hook>().fishCount == 1)
//             {   Debug.Log("one fish");
//                 popUpIndex++;
//             }
//         }
//         else if (popUpIndex == 2)
//         {
//             // Example condition for triggering third pop-up (adjust as needed)
//             if (player.GetComponent<Hook>().fishCount >= 5)
//             {
//                 popUpIndex++;
//             }
//         }
//         // Add more conditions for additional pop-ups if needed
//     }

// }
