namespace Dungeon.Handlers
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    public class CameraHandler : MonoBehaviour
    {
        Vector3 cameraOFfset;

        [Header("Drag")]
        [SerializeField] float dragX;
        [SerializeField] float dragZ;



        [Header("Move")]
        [SerializeField] bool canMove;
        [SerializeField] float moveTime = 0.1f;

        [SerializeField] float zoomSens = 0.1f;

        [Header("Zoom")]
        [SerializeField] int zoomMin = 1;
        [SerializeField] int zoomMax = 15;

        void Start()
        {
            cameraOFfset = Camera.main.transform.position;
            canMove = true;
        }
        void Update()
        {

        }

        public void DragCamera(Vector3 dir)
        {
            dir.y = 0f;
            Vector3 pos = Camera.main.transform.position + dir;
            /*
            if (Mathf.Abs(pos.x) < dragX)
            {
                if (Camera.main.transform.position.z < 0)
                {
                    if (Mathf.Abs(pos.z) < dragZ + Mathf.Abs(cameraOFfset.z))
                    {
                        Camera.main.transform.position = pos;
                    }
                }
                else
                {
                    if (Mathf.Abs(pos.z) < dragZ)
                    {
                        Camera.main.transform.position = pos;
                    }
                }
            }
            */
            if (Mathf.Abs(pos.x) < dragX && (Mathf.Abs(pos.z + Mathf.Abs(cameraOFfset.z)) < dragZ))
            {
                Camera.main.transform.position = pos;
            }
            else
            {
                Camera.main.transform.position += (cameraOFfset - Camera.main.transform.position).normalized * .1f;
            }
        }

        public void MoveCamera(Transform obj)
        {
            if (canMove)
            {
                //Camera.main.transform.position = cameraOFfset + obj.position;
                Vector3 objPosWOY = new Vector3(obj.position.x, 0, obj.position.z);
                Vector3 targetPos = cameraOFfset + objPosWOY;
                StartCoroutine(MoveCameraWithSeconds(targetPos, moveTime));
            }
        }

        IEnumerator MoveCameraWithSeconds(Vector3 target, float sec)
        {
            canMove = false;
            float elapsedTime = 0;
            Vector3 startPos = Camera.main.transform.position;
            while (elapsedTime < sec)
            {
                Camera.main.transform.position = Vector3.Lerp(startPos, target, (elapsedTime / sec));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            Camera.main.transform.position = target;
            canMove = true;
        }

        public void Zoom(float increment)
        {
            float _inc = increment * zoomSens;
            Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize - _inc, zoomMin, zoomMax);
        }

    }
}

