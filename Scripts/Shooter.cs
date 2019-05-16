using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public LayerMask enemyMask;
    public LayerMask VideoSphere;
    public Vector3 h;
    const int enemyMaskNum = 9;
    private LineRenderer beam;//たま
    [SerializeField] public GameObject effect;
    public GameObject buildedEffect;
    // Start is called before the first frame update
    void Start()
    {
        Physics.queriesHitBackfaces = true;
        beam = GetComponent<LineRenderer>();
        Rigidbody rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") > 0)
        {
            beam.enabled = true;
            // Debug.Log("Fire");
            DrawBeam();



        }
        else
        {
            beam.enabled = false;
        }
    }

    void DrawBeam()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        Physics.queriesHitBackfaces = true;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity,enemyMask))
        {
            Debug.DrawLine(transform.position, hit.point);
            // Instantiate(effect, hit.point, Quaternion.identity);
            beam.SetPosition(0, transform.position);
            beam.SetPosition(1, hit.point);
            h = hit.point;
            Debug.Log(hit.collider.gameObject.layer);
            if (hit.collider.gameObject.layer == enemyMaskNum && buildedEffect ==null)
            {
                Debug.Log("aaa");
                buildedEffect = Instantiate(effect, hit.point, Quaternion.identity);
            }
            else
            {
                if(buildedEffect) buildedEffect.transform.position = hit.point;
            }
            if (hit.collider.gameObject.layer != enemyMaskNum && buildedEffect != null)
            {
                Destroy(buildedEffect);
            }

        }


        beam.SetPosition(0, transform.position);
        beam.SetPosition(1, hit.point);
        //Debug.Log(hit.point);

    }
    
    /*
    void OnTriggerEnter(Collider collider)
    {
        Debug.Log("aaa");
        Rigidbody rb = GetComponent<Rigidbody>();
        if (collider.gameObject.tag == "Enemy")
        {
            Debug.Log(": OnTriggerEnter");
        }

    }*/

}
