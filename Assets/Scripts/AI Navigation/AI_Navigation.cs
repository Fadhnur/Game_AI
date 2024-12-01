using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Navigation : MonoBehaviour
{
    private GameObject destination;
    private NavMeshAgent agent;
    private Animator _animation;
    private Collider _collider;

    private bool istrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        destination = GameObject.FindGameObjectWithTag("Destination");  //Mencari objek dengan tag destinasi
        _animation = GetComponent<Animator>();
        _collider = GetComponent<Collider>();
        agent = GetComponent<NavMeshAgent>();   //mengambil komponen navmesh agent pada objek yang memiliki script ini
        // agent.SetDestination(destination.transform.position);   //mengatur destinasi dengan menimpan posisi target destinasi
        // ToDestionation();
        // Transform targetTransform = destination.transform;
    }

    private void Update() {
        ToDestionation();
        // if(Input.GetKeyDown(KeyCode.Space)) Debug.Log(agent.transform.position);
    }

    private void ToDestionation()
    {
        if(istrigger){
            agent.isStopped = false;
            agent.SetDestination(destination.transform.position);   //mengatur destinasi dengan menimpan posisi target destinasi
            _animation.SetBool("Walking", true);
            transform.LookAt(destination.transform);

            Vector3 differenceVector = agent.transform.position - destination.transform.position;
            differenceVector.y = 0; // Abaikan sumbu y

            if (differenceVector.magnitude <= 0.1f)
            {
                _animation.SetBool("Walking", false);
                // agent.isStopped = true;
                // _collider.enabled = false;
                // Jalankan kode selanjutnya
            }
        }
        else{
            _animation.SetBool("Walking", false);
            agent.isStopped = true;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.CompareTag("Player")){
            // ToDestionation();
            istrigger = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        // _animation.SetBool("Walking", false);
        istrigger = false;
    }

}
