using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCheckTest : MonoBehaviour
{
    public float skillCheckInterval = 2f; // Time interval between skill checks
    public float skillCheckWindow = 0.5f; // Time window for successful skill check

    private float nextSkillCheckTime;

    void Start()
    {
        nextSkillCheckTime = Time.time + skillCheckInterval;
    }

    void Update()
    {
        if (Time.time >= nextSkillCheckTime)
        {
            // Trigger a skill check
            // Display UI elements (progress bar, marker) for the skill check

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                float currentTime = Time.time;
                float elapsedTime = currentTime - nextSkillCheckTime;

                if (Mathf.Abs(elapsedTime) <= skillCheckWindow)
                {
                    // Successful skill check
                    Debug.Log("Skill Check Successful!");
                    // Handle successful skill check logic here
                }
                else
                {
                    // Failed skill check
                    Debug.Log("Skill Check Failed!");
                    // Handle failed skill check logic here
                }

                // Schedule the next skill check
                nextSkillCheckTime = currentTime + skillCheckInterval;
            }
        }
    }
}

