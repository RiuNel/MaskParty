using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public bool is_clicked;

    public bool ClickOn()
    {
        return (is_clicked ? true : false);
    }
    private void OnTriggerEnter(Collider other)
    {
        is_clicked = true;
    }
}
