using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Move : MonoBehaviour {
    [Header("Control")]
    public bool canControl = true;

	[Header("Key controls")]
	public KeyCode mForward = KeyCode.W;
	public KeyCode mBackward = KeyCode.S;
	public KeyCode mRight = KeyCode.D;
	public KeyCode mLeft = KeyCode.A;
	public KeyCode mJump = KeyCode.Space;
	public KeyCode mCrouch = KeyCode.C;
	public KeyCode mRun = KeyCode.LeftShift;

	[Header("Moving features")]
	[SerializeField] private float normal_speed = 5f;
	[SerializeField] private float run_speed = 10f;
	[SerializeField] private float crouch_speed = 2.5f;
	[SerializeField] private float trg_speed = 1.5f;
	[SerializeField] private float jumpHGH = 15f;
	[SerializeField] private float gravity = -9.8f;
	[SerializeField] private float max_gravity = -11f;
	[SerializeField] private float minFAL = -1.5f;

	[Header("MouseLook features")]
	[SerializeField] private Camera cam;
	[SerializeField] private float mouse_look_speed = 5f;
	[SerializeField] private float min_turn = -70f;
	[SerializeField] private float max_turn = 70f;

	private bool _canJump = true;
    private bool isRun = false;
    private bool isCrouch = false;

    private Animator character_anim;
    private CharacterController chr_ctrl;

    private float speed;

	public bool canJump {
		get { return _canJump; }
		set { _canJump = value; }
	}

	private float deltaTurnX, deltaTurnY = 0f;
	private Vector3 deltaMove;
	private float vertSpeed;

	void Start () {
        chr_ctrl = GetComponent<CharacterController>();
        character_anim = GetComponent<Animator>();

        deltaMove = Vector3.zero;

        speed = normal_speed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

	void Update () {
        if (canControl)
        {
            if (Input.GetKeyDown(mRun))
            {
                isRun = true;
                isCrouch = false;
            }
            if (Input.GetKeyUp(mRun))
            {
                isRun = false;
            }
            if (Input.GetKeyDown(mCrouch))
            {
                isRun = false;
                isCrouch = true;
            }
            if (Input.GetKeyUp(mCrouch))
            {
                isCrouch = false;
            }

            if (isRun)
            {
                speed = run_speed;
            }
            else
            if (isCrouch)
            {
                speed = crouch_speed;
            }
            else
            {
                speed = normal_speed;
            }

            if (Input.GetKey(mForward))
            {
                deltaMove.z = speed;
            }
            if (Input.GetKey(mBackward))
            {
                deltaMove.z = -speed;
            }
            if (Input.GetKey(mRight))
            {
                deltaMove.x = speed;
            }
            if (Input.GetKey(mLeft))
            {
                deltaMove.x = -speed;
            }

            if (chr_ctrl.isGrounded)
            {
                if (_canJump)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        vertSpeed = jumpHGH;
                    }
                    else
                    {
                        vertSpeed = minFAL;
                    }
                }
            }
            else
            {
                vertSpeed += gravity * 5 * Time.deltaTime;
                if (vertSpeed < max_gravity)
                {
                    vertSpeed = max_gravity;
                }
            }


            deltaMove.y = vertSpeed;
            Vector3.ClampMagnitude(deltaMove, speed);

            //character_anim.SetFloat("X", deltaMove.x);
            //character_anim.SetFloat("Y", deltaMove.z);

            deltaMove *= Time.deltaTime;
            deltaMove = transform.TransformDirection(deltaMove);

            chr_ctrl.Move(deltaMove);

            float mYs = Input.GetAxis("Mouse X");
            transform.Rotate(0, mouse_look_speed * mYs, 0);

            //Rotate PlayerCamera to look up - down;
            deltaTurnY -= Input.GetAxis("Mouse Y") * mouse_look_speed;

            deltaTurnY = Mathf.Clamp(deltaTurnY, min_turn, max_turn);

            float horROT = cam.transform.localEulerAngles.y;

            cam.transform.localEulerAngles = new Vector3(deltaTurnY, horROT, 0);
        }
	}

	void OnGUI() {
		//GUI.Label (new Rect (cam.pixelWidth / 2, cam.pixelHeight / 2,20,20),"+");
	}

    public void Teleport(Vector3 dest)
    {
        chr_ctrl.enabled = false;
        transform.position = dest;
        chr_ctrl.enabled = true;
    }
}
