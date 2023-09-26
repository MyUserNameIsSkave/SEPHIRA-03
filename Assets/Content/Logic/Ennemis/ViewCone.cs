using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCone : MonoBehaviour
{
    [SerializeField]
    Color BaseColor;

    [SerializeField]
    Color DetectedColor;




    Renderer Renderer;

    EnnemisBehavior EnnemisBehavior;


    
    bool SeeBinah = false;


    GameObject Binah;






    private void Awake()
    {
        Renderer = GetComponent<Renderer>();
        Binah = GameObject.FindGameObjectWithTag("Binah");
        EnnemisBehavior = transform.parent.GetComponent<EnnemisBehavior>();
    }




    //private void OnTriggerStay(Collider other)
    //{
    //    if (!other.CompareTag("Binah"))
    //    {
    //        return;
    //    }

    //    bool SeeBinahNow;


    //    //Verifier si il voir Binah et si il devrait voir Binah (Ici en "update" et avec les Event)

    //    Vector3 RayTarget = other.transform.GetChild(1).transform.position;
    //    Vector3 RayStart = transform.GetChild(0).transform.position;
    //    Vector3 Direction = (RayTarget - RayStart).normalized;

    //    Ray VisibilityCheck = new Ray(RayTarget, Direction);




    //    RaycastHit hit;



        
    //    if (Physics.Raycast(VisibilityCheck, out hit))
    //    {
          
            
    //        if (!hit.collider.CompareTag("Binah") && SeeBinah)
    //        {
    //            print("changement DONT SEE");
    //            SeeBinahNow = false;
    //            SeeBinah = false;
                
    //        }

    //        if (hit.collider.CompareTag("Binah") && !SeeBinah)
    //        {
    //            print("changement SEE");
    //            SeeBinahNow = true;
    //            SeeBinah = true;
                
    //        }
    //    }


    //}














    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Binah"))
        {
            //    if (other.GetComponent<BinahMovement>().IsCrouched == false)
            //    {
            //        //Detect Norma
            //        print("Detected");
            //        return;
            //    }

            //    if (other.GetComponent<BinahMovement>().IsCrouched == true)
            //    {
            //        //Test Collision

            //        Vector3 RayTarget = other.transform.GetChild(1).transform.position;
            //        Vector3 RayStart = transform.GetChild(0).transform.position;
            //        Vector3 Direction = (RayTarget - RayStart).normalized;

            //        Ray VisibilityCheck = new Ray(RayTarget, Direction);




            //        RaycastHit hit;


            //        if (Physics.Raycast(VisibilityCheck, out hit))
            //        {
            //            if (hit.collider.CompareTag("Binah"))
            //            {
            //                print("Detected");
            //                //Detect
            //            }
            //            else
            //            {
            //                //Dont Detect
            //                print("Detected");
            //            }
            //        }



            //            return;
            //    }












            if (other.GetComponent<BinahMovement>().IsCrouched == true)
            {
                Vector3 RayTarget = other.transform.GetChild(1).transform.position;
                Vector3 RayStart = transform.GetChild(0).transform.position;
                Vector3 Direction = (RayTarget - RayStart).normalized;

                Ray VisibilityCheck = new Ray(RayTarget, Direction);




                RaycastHit hit;


                if (Physics.Raycast(VisibilityCheck, out hit))
                {
                    if (!hit.collider.CompareTag("Binah"))
                    {
                        SeeBinah = false;
                        EnnemisBehavior.LoseBinah();
                        Renderer.material.color = BaseColor;
                        return;
                    }

                }

            }


            //Debout 
            SeeBinah = true;
            EnnemisBehavior.SeeBinah();
            Renderer.material.color = DetectedColor;
        }
    }


    private void OnTriggerExit(Collider other)
    {


        if (other.CompareTag("Binah"))
        {
            if (other.GetComponent<BinahMovement>().IsCrouched == true)
            {
                return;
            }

            SeeBinah = false;
            EnnemisBehavior.LoseBinah();
            Renderer.material.color = BaseColor;
        }
    }





    




}
