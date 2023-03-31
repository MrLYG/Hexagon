using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBattle : MonoBehaviour
{
    // Reference of current weapon and battle manager
    private GameObject m_Weapon;
    private BattleManager m_BattleManager;

    // Params related to attacking using weapon
    private bool canAttack = true;
    private float attackCDCount;

    void Start()
    {
        // Get reference of Battle Manager
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("BattleManager"))
        {
            m_BattleManager = gameObject.GetComponent<BattleManager>();
        }

        // If have weapons from previou level, carry over
        if (PlayerPrefs.HasKey("PlayerWeapon"))
        {
            ChangeWeapon(PlayerPrefs.GetString("PlayerWeapon"));
        }
    }

    // Switch to a new weapon
    public bool ChangeWeapon(string weaponName)
    {
        GameObject newWeapon = m_BattleManager.getWeapon(weaponName);
        if (newWeapon != null)
        {
            if (m_Weapon != null)
            {
                Destroy(m_Weapon);
            }
            m_Weapon = Instantiate(m_BattleManager.getWeapon(weaponName));
            m_Weapon.GetComponent<IWeapon>().SetUp(gameObject);
            PlayerPrefs.SetString("PlayerWeapon", weaponName);
            return true;
        }
        return false;
    }

    private void Update()
    {
        if (!canAttack)
        {
            attackCDCount += Time.deltaTime;
            if(attackCDCount > m_Weapon.GetComponent<IWeapon>().AttackCD)
            {
                canAttack = true;
            }
            else
            {
                return;
            }
        }

        // Attack with left click
        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.J)) && canAttack && !GetComponent<PlayerPushPull>().pushing)
        {
            AttackWithWeapon();
        }
    }

    private void AttackWithWeapon()
    {
        if(m_Weapon != null)
        {
            m_Weapon.GetComponent<IWeapon>().StartAttack();
            canAttack = false;
            attackCDCount = 0;
        }
    }

    // Reset weapon location based on current player's facing
    public void SwitchWeaponSide()
    {
        if (m_Weapon != null)
        {
            m_Weapon.GetComponent<IWeapon>().SwitchSide();
        }
    }

    // Reverse weapon when the gravity reverses
    public void ReverseWeapon() {
        if (m_Weapon != null)
        {
            m_Weapon.GetComponent<IWeapon>().ReverseWeapon();
        }
    }

    // Completely reset the weapon, used during respawn
    public void ResetWeapon() { 
        if (m_Weapon != null)
        {
            m_Weapon.GetComponent<IWeapon>().SetUp(gameObject);
        }
    }
}
