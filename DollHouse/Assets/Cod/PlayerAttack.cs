using player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    public class PlayerAttack : MonoBehaviour
    {
        public Transform Interact;
        public float InterectRange;

        [Header("PlayerHit")]
        public GameObject IdleP;
        public GameObject AttackP;
        [SerializeField] Ghost ghostObj;

        // Start is called before the first frame update
        void Start()
        {
            ghostObj = GetComponent<Ghost>();
        }

        // Update is called once per frame
        void Update()
        {
            Ray r = new Ray(Interact.position, Interact.forward);
            Debug.DrawRay(r.origin, r.direction * InterectRange);
            if (Physics.Raycast(r, out RaycastHit hitinfo, InterectRange))
            {
                if (hitinfo.collider.gameObject.tag == "Ghost")
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        print("hitGhost");
                        ghostObj.GetHit();
                    }
                }
            }
            #region AttackAnimetion
            if (Input.GetButtonDown("Fire1"))
            {
                StartCoroutine("AttackReset");
            }
            #endregion
        }

        IEnumerator AttackReset()
        {
            AttackP.SetActive(true);
            IdleP.SetActive(false);
            yield return new WaitForSeconds(0.2f);
            AttackP.SetActive(false);
            IdleP.SetActive(true);
        }
    }

