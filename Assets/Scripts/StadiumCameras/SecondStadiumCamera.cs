using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondStadiumCamera : StadiumCamera
{
    [SerializeField]
    private GameObject stadium;
    [SerializeField]
    private GameObject mapViewTarget;
    [SerializeField]
    private GameObject rotateTarget;
    [SerializeField]
    private GameObject redTeamTarget;
    [SerializeField]
    private GameObject blueTeamTarget;
    [SerializeField]
    private GameObject panOutTarget;
    [SerializeField]
    private Animator blackScreenAnimator;

    private WaitForSeconds fadeWait = new WaitForSeconds(1);

    public override IEnumerator IntroPan(Action action)
    {
        // View map
        transform.SetParent(mapViewTarget.transform);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = new Vector3(-25, 0, 0);

        float count = 5.5f;
        bool fadein = false;
        while (count > 0)
        {
            count -= Time.deltaTime;
            transform.Translate((mapViewTarget.transform.right * 110 - mapViewTarget.transform.forward * 30) * Time.deltaTime, Space.World);

            if (count <= 1 && !fadein)
            {
                blackScreenAnimator.SetTrigger("fade_in");
                fadein = true;
            }
            yield return null;
        }


        // Circle view stadium
        transform.SetParent(stadium.transform);
        transform.localPosition = new Vector3(0, 20, -30);
        transform.localEulerAngles = new Vector3(40, 0, 0);
        transform.SetParent(rotateTarget.transform);
        blackScreenAnimator.SetTrigger("fade_out");

        count = 4f;
        fadein = false;
        while (count > 0)
        {
            count -= Time.deltaTime;
            rotateTarget.transform.Rotate(0, -20 * Time.deltaTime, 0);

            if (count <= 1 && !fadein)
            {
                blackScreenAnimator.SetTrigger("fade_in");
                fadein = true;
            }
            yield return null;
        }

        // View red team
        transform.SetParent(redTeamTarget.transform);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        blackScreenAnimator.SetTrigger("fade_out");

        count = 4f;
        fadein = false;
        while (count > 0)
        {
            count -= Time.deltaTime;
            transform.Translate((redTeamTarget.transform.right * 13 + redTeamTarget.transform.forward * 3.5f) * Time.deltaTime, Space.World);

            if (count <= 1 && !fadein)
            {
                blackScreenAnimator.SetTrigger("fade_in");
                fadein = true;
            }
            yield return null;
        }

        // View blue team
        transform.SetParent(blueTeamTarget.transform);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        blackScreenAnimator.SetTrigger("fade_out");

        count = 4f;
        fadein = false;
        while (count > 0)
        {
            count -= Time.deltaTime;
            transform.Translate((blueTeamTarget.transform.right * 13 + blueTeamTarget.transform.forward * 3.5f) * Time.deltaTime, Space.World);

            if (count <= 1 && !fadein)
            {
                blackScreenAnimator.SetTrigger("fade_in");
                fadein = true;
            }
            yield return null;
        }

        // Pan out
        transform.SetParent(panOutTarget.transform);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
        blackScreenAnimator.SetTrigger("fade_out");

        count = 2;
        fadein = false;
        while (count > 0)
        {
            count -= Time.deltaTime;
            transform.Translate(panOutTarget.transform.right * 20 * Time.deltaTime);
            yield return null;
        }
        count = 5;
        while (count > 0)
        {
            count -= Time.deltaTime;
            transform.Translate(Vector3.up * (60 - count * 3) * Time.deltaTime, Space.World);
            transform.LookAt(panOutTarget.transform);

            if (count <= 1 && !fadein)
            {
                blackScreenAnimator.SetTrigger("fade_in");
                fadein = true;
            }
            yield return null;
        }

        // Done intro scene, get rid of camera.
        yield return fadeWait;
        action();
        gameObject.SetActive(false);
    }
}
