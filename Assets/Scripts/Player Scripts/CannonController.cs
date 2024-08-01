using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    [SerializeField] private GameObject _cannon;

    private void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = mousePosition -_cannon.transform.position ;

        float angle = Mathf.Atan2(direction.y,direction.x)*Mathf.Rad2Deg;

        _cannon.transform.rotation = Quaternion.Euler(0,0,angle-90f);
    }
}
