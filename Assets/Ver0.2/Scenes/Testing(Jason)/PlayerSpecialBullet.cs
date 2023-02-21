using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecialBullet : MonoBehaviour
{
    [SerializeField] private float initLaunchForce;
    [SerializeField] private float maxLaunchForce;
    [SerializeField] private float forceIncreasingSpeed;
    [SerializeField] private GameObject predicionDot;
    [SerializeField] private int numDots;
    private List<GameObject> predicionDots = new List<GameObject>();

    private float curLaunchForce;
    private bool charging = false;

    [SerializeField] private List<GameObject> BulletPrefabs;
    [SerializeField] private List<GameObject> Bullets;
    private GameObject curBullet;

    // Start is called before the first frame update
    void Start()
    {
        // Reseting for now
        PlayerPrefs.DeleteKey("PlayerBlueLight");

        // If have blue lights
        if (PlayerPrefs.HasKey("PlayerBlueLight"))
        {
            getPower(BulletPrefabs[0]);
        }

        for (int i = 0; i < numDots; i++)
        {
            predicionDots.Add(Instantiate(predicionDot, transform.position, Quaternion.identity));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (curBullet != null)
        {
            // Start Charging
            if (Input.GetMouseButtonDown(1) && !charging)
            {
                curLaunchForce = initLaunchForce;
                charging = true;
                showDots(true);
            }
            // Release
            if (Input.GetMouseButtonUp(1) && charging)
            {
                charging = false;
                showDots(false);

                GameObject bullet = Instantiate(curBullet, transform.position, Quaternion.identity);
                if (GetComponent<PlayerControl>().facingRight)
                    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(1, 1) * curLaunchForce;
                else
                    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 1) * curLaunchForce;
            }
            if (charging)
            {
                DrawProjectile();
                curLaunchForce += forceIncreasingSpeed * Time.deltaTime;
                if (curLaunchForce > maxLaunchForce)
                {
                    curLaunchForce = maxLaunchForce;
                }
            }
        }
    }

    public void getPower(GameObject bullet)
    {
        if(bullet.name.Equals("ReverseBullet"))
        {
            PlayerPrefs.SetInt("PlayerBlueLight", 1);
        }

        if (!Bullets.Contains(bullet))
        {
            Bullets.Add(bullet);
        }
        curBullet = Bullets[Bullets.Count - 1];
    }

    // For showing prediction line
    private void showDots(bool show)
    {
        for (int i = 0; i < numDots; i++)
        {
            predicionDots[i].SetActive(show);
        }
    }

    private void DrawProjectile()
    {
        for (int i = 0; i < numDots; i++)
        {
            predicionDots[i].transform.position = PredictLocation(i * 0.1f);
        }
    }

    private Vector3 PredictLocation(float time)
    {
        Vector3 direction;
        if (GetComponent<PlayerControl>().facingRight)
        {
            direction = new Vector3(1, 1, 0);
        }
        else
        {
            direction = new Vector3(-1, 1, 0);
        }
        Vector2 gravityScale = GetComponent<ObjectGravity>().getCurrentGravity();
        return transform.position + direction * curLaunchForce * time + 0.5f * new Vector3(gravityScale.x, gravityScale.y, 0) * Mathf.Pow(time, 2);
    }
}
