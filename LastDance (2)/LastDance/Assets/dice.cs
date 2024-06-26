using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dice : MonoBehaviour
{
    public Rigidbody RB;
    public Transform[] Nums;
    public int num;

    public IEnumerator Roll()
    {
        yield return null;
        RB.AddForce(new Vector3(0, 4, 0), ForceMode.Impulse);
        transform.localEulerAngles = new Vector3(Random.Range(-90f, 90f), Random.Range(-90f, 90f), Random.Range(-90f, 90f));
        RB.angularVelocity = Random.insideUnitSphere * Random.Range(-1000, 1000);

        yield return new WaitForSeconds(3);
        while(true)
        {
            yield return null;
            if (RB.velocity.sqrMagnitude < 0.001f) break;
        }
        for(int i = 0; i<6; i++)
        {
            if (Nums[i].position.y > 1f)
            {
                num = i + 1;
                print(num);
                break;
            }
        }
    }
}                        
