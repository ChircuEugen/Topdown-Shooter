using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GrenadeThrow : MonoBehaviour
{
    public Canvas grenadeCanvas;
    //public Image explosionRadius;
    public Transform grenadeObject;
    private PlayerShoot playerShoot;

    public int grenadeCount = 0;
    bool isDrawn = false;

    [SerializeField] private TMP_Text grenadeCountText;

    private void Start()
    {
        grenadeCanvas.enabled = true;
        playerShoot = GetComponentInChildren<PlayerShoot>();
        UpdateGrenadeUI();
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit rayHit;

        if(isDrawn)
        {
            playerShoot.enabled = false;
            grenadeCanvas.gameObject.SetActive(true);
            if (Physics.Raycast(ray, out rayHit, 100))
            {
                grenadeCanvas.transform.position = rayHit.point;
            }

            if (Input.GetButtonDown("Fire1"))
            {
                if(grenadeCount > 0)
                {
                    Grenade grenade = Instantiate(grenadeObject, transform.position + Vector3.forward, transform.rotation).GetComponent<Grenade>();
                    if (grenade != null)
                    {
                        grenade.FlyTowardsMouse(rayHit.point);
                    }
                    grenadeCount--;
                    UpdateGrenadeUI();
                    //grenadeCanvas.gameObject.SetActive(false);
                    //isDrawn = false;
                }
                else
                {
                    grenadeCountText.color = Color.red;
                    grenadeCountText.text = "NO GRENADES";
                    Invoke("UpdateGrenadeUI", 1);
                }
            }
        }
        else
        {
            grenadeCanvas.gameObject.SetActive(false);
            playerShoot.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            isDrawn = !isDrawn;
            //playerShoot.enabled = false;
            //grenadeCanvas.gameObject.SetActive(true);
            //if (Physics.Raycast(ray, out rayHit, 100))
            //{
            //    grenadeCanvas.transform.position = rayHit.point;
            //}

            //if(Input.GetButtonDown("Fire1") /*&& grenadeCanvas.enabled == true */)
            //{
            //    Grenade grenade = Instantiate(grenadeObject, transform.position + Vector3.forward, transform.rotation).GetComponent<Grenade>();
            //    if (grenade != null)
            //    {
            //        grenade.FlyTowardsMouse(rayHit.point);
            //    }
            //    playerShoot.enabled = true;
            //    grenadeCanvas.gameObject.SetActive(false);
            //}
        }
    }

    public void UpdateGrenadeUI()
    {
        grenadeCountText.color = Color.white;
        grenadeCountText.text = grenadeCount.ToString();
    }
}
