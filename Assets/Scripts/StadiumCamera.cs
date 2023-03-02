using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StadiumCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject stadium;
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
    [SerializeField]
    private GameStats gameStats;

    private WaitForSeconds fadeWait = new WaitForSeconds(1);

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IntroPan());
    }

    private IEnumerator IntroPan()
    {
        // Circle view stadium
        transform.localPosition = new Vector3(0, 4, -6);
        transform.localEulerAngles = new Vector3(35, 0, 0);
        transform.SetParent(rotateTarget.transform);

        float count = 6;
        bool fadein = false;
        while (count > 0)
        {
            count -= Time.deltaTime;
            rotateTarget.transform.Rotate(0, -15 * Time.deltaTime, 0);

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

        count = 4;
        fadein = false;
        while (count > 0)
        {
            count -= Time.deltaTime;
            transform.Translate((redTeamTarget.transform.right * 10 + redTeamTarget.transform.forward * 4) * Time.deltaTime);

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

        count = 4;
        fadein = false;
        while (count > 0)
        {
            count -= Time.deltaTime;
            transform.Translate((-blueTeamTarget.transform.right * 10 + -blueTeamTarget.transform.forward * 4) * Time.deltaTime);

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

        count = 2.5f;
        fadein = false;
        while (count > 0)
        {
            count -= Time.deltaTime;
            transform.Translate(panOutTarget.transform.right * 15 * Time.deltaTime);
            yield return null;
        }
        count = 6;
        while (count > 0)
        {
            count -= Time.deltaTime;
            transform.Translate(Vector3.up * (50 - count * 3) * Time.deltaTime, Space.World);
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
        gameObject.SetActive(false);
    }
}
