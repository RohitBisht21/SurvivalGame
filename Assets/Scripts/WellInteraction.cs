using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellInteraction : MonoBehaviour
{
    public float thirstIncreaseRate = 20f;
    public float thirstDecreaseRate = 10f;

    private void Update()
    {
        // Perform raycasting to detect interactions
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 1f))
        {
            if (hit.collider.CompareTag("Well"))
            {
                if (Input.GetKey(KeyCode.T))
                {
                    Survival.Instance.Thirst += thirstIncreaseRate * Time.deltaTime;
                    Survival.Instance.Thirst = Mathf.Clamp(Survival.Instance.Thirst, 0, Survival.Instance.MaxThirst);
                    Survival.Instance.UpdateSliders();
                }
                else
                {
                    float thirstDecreaseRate = Survival.Instance.ThirstOT;
                    Survival.Instance.Thirst -= thirstDecreaseRate * Time.deltaTime;
                    Survival.Instance.Thirst = Mathf.Clamp(Survival.Instance.Thirst, 0, Survival.Instance.MaxThirst);
                    Survival.Instance.UpdateSliders();
                }
            }
        }
    }
}
