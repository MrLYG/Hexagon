using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        PlayerPrefs.SetString("PlayerWeapon", "Stick");

        // If have weapons from previou level, carry over
        if (PlayerPrefs.HasKey("PlayerWeapon"))
        {
            m_Weapon = Instantiate(m_BattleManager.getWeapon(PlayerPrefs.GetString("PlayerWeapon")));
            m_Weapon.GetComponent<IWeapon>().SetUp(gameObject);
        }
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
        }
        canAttack = false;
        attackCDCount = 0;
    }

    public void SwitchWeaponSide()
    {
        if (m_Weapon != null)
        {
            m_Weapon.GetComponent<IWeapon>().SwitchSide();
        }
    }
}
