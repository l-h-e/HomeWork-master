using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beamShooter : MonoBehaviour
{
    public LayerMask hitble;
    public LayerMask VideoSphere;
    Vector3 h;
    const int enemyMaskNum = 10;
    LineRenderer beam;
    [SerializeField] public GameObject effect;
    GameObject buildedEffect;
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
            Destroy(buildedEffect);
        }
    }

    void DrawBeam()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        Physics.queriesHitBackfaces = true;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, hitble))
        {
            beam.SetPosition(0, transform.position);
            beam.SetPosition(1, hit.point);
            h = hit.point;
            //            Debug.Log(hit.collider.gameObject.layer);
            if (hit.collider.gameObject.layer == enemyMaskNum)
            {
                hit.collider.gameObject.GetComponent<Enemy>().GetDamage();
                if (buildedEffect == null)
                {
                    buildedEffect = Instantiate(effect, hit.point, Quaternion.Euler(-90, 0, 0));
                }
                {
                    if (buildedEffect) buildedEffect.transform.position = hit.point;
                }



            }
            if (hit.collider.gameObject.layer != enemyMaskNum && buildedEffect != null)
            {
                Destroy(buildedEffect);
            }


        }


    }
}