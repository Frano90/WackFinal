using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppController : MonoBehaviour
{
    private Maquinola _currentMaquinola;
    private bool isMaquinolaDeployed = false;
    public Camera arCamera;
    public MartilloOp2 martillo;

    public float maxDistance;
    //public Martillo martillo;

    private void Start()
    {
        //_currentMaquinola = Instantiate<Maquinola>(maquinola_prefab);
    }

    public void BackToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void OnMaquinolaDeployed()
    {
        isMaquinolaDeployed = !isMaquinolaDeployed;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Tiro rayo");
            Ray rayo = arCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit objectHit;
            if (Physics.Raycast(rayo, out objectHit))
            {
                Debug.Log("Hay enemigo?");
                Debug.Log(objectHit.collider.gameObject.name);
                Enemy hit = objectHit.transform.parent.GetComponent<Enemy>();
                if (hit != null)
                {
                    Debug.Log("Encontre enemigo?");
                    if (Vector3.Distance(hit.transform.position, arCamera.transform.position) >=
                        maxDistance)
                        return;

                    Debug.Log("Estoy cerca de enemigo?");
                    MartilloOp2 hammer = Instantiate(martillo, hit.spawner.hammerPlace.transform.position,
                        Quaternion.identity);


                    Debug.Log("Hago pegar");
                    hammer.DoTheHit(hit, delegate { hit.DamageEnemy(1); });
                }
            }
        }

            //hacer que cuando toco la pantalla, tire un rayo en la direccion seleccionada
            //si choca contra un enemigo, le hago daño al enemigo

            //if (!isMaquinolaDeployed)
            //    return;

            if (Input.touchCount == 1)
            {
                //Debug.Log("toque");

                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Ended)
                {
                    Ray ray = arCamera.ScreenPointToRay(touch.position);
                    RaycastHit hitObject;
                    if (Physics.Raycast(ray, out hitObject))
                    {
                        Enemy hittedObject = hitObject.transform.GetComponent<Enemy>();
                        if (hittedObject != null)
                        {
                            if (Vector3.Distance(hittedObject.transform.position, arCamera.transform.position) >=
                                maxDistance)
                                return;

                            Debug.Log("Creo un martillo");
                            MartilloOp2 hammer = Instantiate(martillo,
                                hittedObject.spawner.hammerPlace.transform.position,
                                Quaternion.identity);



                            hammer.DoTheHit(hittedObject, delegate { hittedObject.DamageEnemy(1); });
                           
                        }
                    }
                }
            }
        


        //TODO la parte de AR sera en el segundo paso, pero hay que hacer que detecte un plano, coloque ahi la maquinola.
    }
}
