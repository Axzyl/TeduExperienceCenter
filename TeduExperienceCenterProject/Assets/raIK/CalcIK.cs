using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace CalcIK
{
    public class CalcIK : MonoBehaviour
    {
        [SerializeField] bool robot;

        public Slider S_Slider;
        public Slider L_Slider;
        public Slider U_Slider;
        public Slider R_Slider;
        public Slider B_Slider;
        public Slider T_Slider;
        public static double[] theta = new double[6];

        [SerializeField] GameObject textObject;
        [SerializeField] GameObject pxTextObject;
        [SerializeField] GameObject pyTextObject;
        [SerializeField] GameObject pzTextObject;
        [SerializeField] GameObject rxTextObject;
        [SerializeField] GameObject ryTextObject;
        [SerializeField] GameObject rzTextObject;
        Text pxText;
        Text pyText;
        Text pzText;
        Text rxText;
        Text ryText;
        Text rzText;

        private float L1, L2, L3, L4, L5, L6;
        private float C3;
        private float prevPx, prevPy, prevPz;  // to avoid impossible pose
        private float prevRx, prevRy, prevRz;
        private Vector3 prevA, prevB;

        // -1.17, -11.8, -117
        Vector3 a = new Vector3(0.5f, 1, 2);
        Vector3 b = new Vector3(0,45,0);
        Vector3 comp = new Vector3(11.9f, 2.76f, 115.12f);
        // +12, +11.76, +119.92
        // new Vector3(11.9f, 2.76f, 112.12f)
        [SerializeField] Transform dockingPoint;
        Transform target;

        float px, py, pz;
        float rx, ry, rz;
        float ax, ay, az, bx, by, bz;
        float asx, asy, asz, bsx, bsy, bsz;
        float p5x, p5y, p5z;
        float C1, C23, S1, S23;

        bool colliding;
        bool ignoreCollision;

        // Use this for initialization
        void Start()
        {
            theta[0] = theta[1] = theta[2] = theta[3] = theta[4] = theta[5] = 0.0;
            L1 = 0.545f;
            L2 = 3f;
            L3 = 3f;
            L4 = 0f;
            L5 = 0f;
            L6 = 0.545f;
            C3 = 0.0f;

            pxText = pxTextObject.GetComponent<Text>();
            pyText = pyTextObject.GetComponent<Text>();
            pzText = pzTextObject.GetComponent<Text>();
            rxText = rxTextObject.GetComponent<Text>();
            ryText = ryTextObject.GetComponent<Text>();
            rzText = rzTextObject.GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            prevPx = px;
            prevPy = py;
            prevPz = pz;
            prevRx = rx;
            prevRy = ry;
            prevRz = rz;
            prevA = a;
            prevB = b;

            //px = S_Slider.value;
            //px = target.localPosition.x;
            float b35 = 0;
            if (Input.GetButton("JB4")) b35 = 1;
            if (Input.GetButton("Fire3")) b35 = -1;
            a += new Vector3(-Input.GetAxis("Axis6"), -Input.GetAxis("Axis5"), b35) * Time.deltaTime / (Input.GetButton("Fire1") ? 1.5f : 3f);
            b += new Vector3(-Input.GetAxis("Axis3"), -Input.GetAxis("Vertical"), Input.GetAxis("Horizontal")) * Time.deltaTime * 45;
            //Vector3 velocity = GameObject.Find("DockingModule").GetComponent<DockingModule>().velocity;
            //a += new Vector3(-velocity.z, -velocity.x, velocity.y) * Time.deltaTime;
            //Debug.Log(a);
            //a = dock.position + comp;

            px = a.x;
            py = a.y;
            pz = a.z;
            pxText.text = px.ToString("F2");
            //py = L_Slider.value;
            //py = target.localPosition.y;
            pyText.text = py.ToString("F2");
            //pz = U_Slider.value;
            //pz = target.localPosition.z;
            pzText.text = pz.ToString("F2");
            //rx = R_Slider.value;
            rx = b.x;
            rxText.text = rx.ToString("F2");
            //ry = B_Slider.value;
            ry = b.y;
            ryText.text = ry.ToString("F2");
            //rz = T_Slider.value;
            rz = 0;
            rzText.text = rz.ToString("F2");

            if (robot)
            {
                
                transform.eulerAngles += Vector3.up * Input.GetAxis("Horizontal") * Time.deltaTime * 8;
            }

            ax = Mathf.Cos(rz * 3.14f / 180.0f) * Mathf.Cos(ry * 3.14f / 180.0f);
            ay = Mathf.Sin(rz * 3.14f / 180.0f) * Mathf.Cos(ry * 3.14f / 180.0f);
            az = -Mathf.Sin(ry * 3.14f / 180.0f);

            p5x = px - (L5 + L6) * ax;
            p5y = py - (L5 + L6) * ay;
            p5z = pz - (L5 + L6) * az;

            if (p5x < 0)
            {
                ResetAll();
                p5x = 0;  //work area limitation
            }
            theta[0] = Mathf.Atan2(p5y, p5x);

            C3 = (Mathf.Pow(p5x, 2) + Mathf.Pow(p5y, 2) + Mathf.Pow(p5z - L1, 2) - Mathf.Pow(L2, 2) - Mathf.Pow(L3 + L4, 2))
                / (2 * L2 * (L3 + L4));
            if (C3 < -1 || C3 > 1)
            {
                ResetAll();
                return;
            }
            theta[2] = Mathf.Atan2(Mathf.Pow(1 - Mathf.Pow(C3, 2), 0.5f), C3);

            float M = L2 + (L3 + L4) * C3;
            float N = (L3 + L4) * Mathf.Sin((float)theta[2]);
            float A = Mathf.Pow(p5x * p5x + p5y * p5y, 0.5f);
            float B = p5z - L1;
            theta[1] = Mathf.Atan2(M * A - N * B, N * A + M * B);

            C1 = Mathf.Cos((float)theta[0]);
            if (C1 < -1 || C1 > 1)
            {
                ResetAll();
                return;
            }
            C23 = Mathf.Cos((float)theta[1] + (float)theta[2]);
            if (C23 < -1 || C23 > 1)
            {
                ResetAll();
                return;
            }
            S1 = Mathf.Sin((float)theta[0]);
            if (S1 < -1 || S1 > 1)
            {
                ResetAll();
                return;
            }
            S23 = Mathf.Sin((float)theta[1] + (float)theta[2]);
            if (S23 < -1 || S23 > 1)
            {
                ResetAll();
                return;
            }

            bx = Mathf.Cos(rx * 3.14f / 180.0f) * Mathf.Sin(ry * 3.14f / 180.0f) * Mathf.Cos(rz * 3.14f / 180.0f)
                - Mathf.Sin(rx * 3.14f / 180.0f) * Mathf.Sin(rz * 3.14f / 180.0f);
            by = Mathf.Cos(rx * 3.14f / 180.0f) * Mathf.Sin(ry * 3.14f / 180.0f) * Mathf.Sin(rz * 3.14f / 180.0f)
                - Mathf.Sin(rx * 3.14f / 180.0f) * Mathf.Cos(rz * 3.14f / 180.0f);
            bz = Mathf.Cos(rx * 3.14f / 180.0f) * Mathf.Cos(ry * 3.14f / 180.0f);

            asx = C23 * (C1 * ax + S1 * ay) - S23 * az;
            asy = -S1 * ax + C1 * ay;
            asz = S23 * (C1 * ax + S1 * ay) + C23 * az;
            bsx = C23 * (C1 * bx + S1 * by) - S23 * bz;
            bsy = -S1 * bx + C1 * by;
            bsz = S23 * (C1 * bx + S1 * by) + C23 * bz;

            theta[3] = Mathf.Atan2(asy, asx);
            theta[4] = Mathf.Atan2(Mathf.Cos((float)theta[3]) * asx + Mathf.Sin((float)theta[3]) * asy, asz);
            theta[5] = Mathf.Atan2(Mathf.Cos((float)theta[3]) * bsy - Mathf.Sin((float)theta[3]) * bsx,
                -bsz / Mathf.Sin((float)theta[4]));

            

            if (robot) {
                Vector3 positionalError;
                float rotationError;
                if (SpaceStationGameManager.stage == 4)
                {
                    positionalError = GameObject.Find("DockingModule").transform.GetChild(0).position - GameObject.Find("Target").transform.position;
                    positionalError += new Vector3(0, 0, 0.3f);
                    rotationError = GameObject.Find("DockingModule").transform.GetChild(0).eulerAngles.y - GameObject.Find("Target").transform.eulerAngles.y;
                    rotationError -= 90;
                }
                else
                {
                    positionalError = dockingPoint.position - target.position;
                    rotationError = dockingPoint.eulerAngles.y - target.eulerAngles.y;
                }

                textObject.GetComponent<Text>().text = "Distance To Target\nX: " + (positionalError.x * 2).ToString("F2") + "m\n" +
                "Y: " + (positionalError.y * 2).ToString("F2") + "m\n" +
                "Z: " + (positionalError.z * 2).ToString("F2") + "m\n" +
                "R: " + (rotationError).ToString("F2") + "°\n\nRotation\n" +
                "RX: " + ry.ToString("F2") + "°\n" +
                "RY: " + transform.eulerAngles.y.ToString("F2") + "°\n" + 
                "RZ: " + rx.ToString("F2") + "°";
            }

            
        }

        private void LateUpdate()
        {
            if (colliding) ResetAll();

        }

        public void Init()
        {
            a = new Vector3(0.5f, 1, 2);
            b = new Vector3(0, 90, 0);
            prevPx = px;
            prevPy = py;
            prevPz = pz;
            prevRx = rx;
            prevRy = ry;
            prevRz = rz;
            prevA = a;
            prevB = b;
            ignoreCollision = true;
            StartCoroutine(Thing());
        }

        public void IgnoreCollision()
        {
            ignoreCollision = true;
        }

        public void SetTarget(Transform t)
        {
            target = t;
        }

        public void SetColliding(bool a)
        {
            colliding = a;
        }

        public Vector3 GetDistanceToTarget()
        {
            return dockingPoint.position - target.position;
        }

        public float GetRotToTarget()
        {
            return dockingPoint.eulerAngles.y - target.eulerAngles.y;
        }

        IEnumerator Thing()
        {
            yield return new WaitForSeconds(1);
            ignoreCollision = false;
        }

        public void ResetAll()
        {
            if (ignoreCollision) return;
            Debug.Log("Reset " + colliding);
            a = prevA;
            b = prevB;
            px = prevPx;
            py = prevPy;
            pz = prevPz;
            rx = prevRx;
            ry = prevRy;
            rz = prevRz;

        }
    }
}
