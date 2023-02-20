using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBattle : MonoBehaviour
{
    private GameObject m_Weapon;
    private BattleManager m_BattleManager;

    private bool canAttack = true;
    private float attackCDCount;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag("BattleManager"))
        {
            m_BattleManager = gameObject.GetComponent<BattleManager>();
        }
        // Just for testing
        //PlayerPrefs.SetString("PlayerWeapon", "Stick");
        //if (SceneManager.GetActiveScene().name.Equals("Testing"))
        //{
            PlayerPrefs.DeleteKey("PlayerWeapon");
        //}

        // If have weapons from previou level, carry over
        if (PlayerPrefs.HasKey("PlayerWeapon"))
        {
            ChangeWeapon(PlayerPrefs.GetString("PlayerWeapon"));
        }
    }

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
        if (Input.GetMouseButton(0) && canAttack)
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

    public void SwitchWeaponSide()
    {
        if (m_Weapon != null)
        {
            m_Weapon.GetComponent<IWeapon>().SwitchSide();
        }
    }
}
