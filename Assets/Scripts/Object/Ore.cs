using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : MonoBehaviour
{
    //체력이 0이되면 여러개 나옴
    //광석의 종류
    #region PublicVariables
    #endregion

    #region PrivateVariables
    private int Hp;
    #endregion

    #region PublicMethod
    public void Hit(int PlayerAtk) //매개변수
    {
        //플레이어의 공격력에 따라 체력이 닳음
        Hp -= PlayerAtk;
        if (Hp <= 0)
        {
            DestroyOre();
        }
    }
    #endregion

    #region PrivateMethod
    private void DestroyOre()
    {
        Destroy(gameObject);
    }
    #endregion
}
