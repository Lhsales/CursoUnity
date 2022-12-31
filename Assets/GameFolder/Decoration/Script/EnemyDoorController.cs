using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoorController : MonoBehaviour
{
    int lifeChange;
    public Transform lifeBar;

    // Start is called before the first frame update
    void Start()
    {
        lifeChange = GetComponent<Character>().life;
    }

    // Update is called once per frame
    void Update()
    {
        int vidaPorta = GetComponent<Character>().life;
        if (lifeChange != vidaPorta)
        {
            GetComponent<Character>().skin.GetComponent<Animator>().Play("EnemyDoorDamage", -1);
            lifeChange = vidaPorta;
        }

        if (lifeChange <= 0)
        {
            Destroy(transform.gameObject);
        }

        lifeBar.localScale = new Vector3(vidaPorta/10f, 1, 1);

    }



}
