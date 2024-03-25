using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;

[RequireComponent(typeof(Rigidbody))]
public class ShipController : MonoBehaviour
{
    [Header("Movement Variables")] 
    [SerializeField] private float _yawForce;

    [SerializeField] private float _pitchForce, _rollForce, _thrustForce, _upDownForce, _strafeForce;
    private float _glideForce, _strafeGlide, _verticalGlide;
    
    [SerializeField, Range(0f,1f)] private float
        _thrustReduction, _upDownReduction, _strafeReduction;

    [Header("Shooting Variables")] 
    public WeaponsManager WeaponsManager;

    private int _currentWeapon;
    
    [SerializeField] private float _fireDelay;

    [Header("Map Ui")] [SerializeField] private GameObject _cameraMap;
    [SerializeField] private GameObject inGameUI;
    
    //input
    private float _thrust, _upDown, _strafe, _roll;
    private Vector2 _pitch;
    private bool _shoot, _rocket,_rocketSwap, _isPaused, _isMap;
    
    //logic variables
    private bool isShooting1;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Gamepad.current != null && Gamepad.current.selectButton.wasPressedThisFrame)
        {
            _isMap = !_isMap;
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            _isMap = !_isMap;
        }
        if (Gamepad.current != null && Gamepad.current.buttonWest.wasPressedThisFrame)
        {
            WeaponsManager.ChangeRocket();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            WeaponsManager.ChangeRocket();
        }
        if (_isMap)
        {
            _cameraMap.SetActive(true);
            PostProcessVolume ppVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
            ppVolume.enabled = true;
            inGameUI.SetActive(false);
        }
        else
        {
            _cameraMap.SetActive(false);
            PostProcessVolume ppVolume = Camera.main.gameObject.GetComponent<PostProcessVolume>();
            ppVolume.enabled = false;
            inGameUI.SetActive(true);
        }
        if (_isPaused || _isMap)
        {
            return;
        }
        if (_shoot)
        {
            WeaponsManager.Shoot();
        }
        if (_rocket)
        {
            WeaponsManager.ShootRocket();
        }
        if (_rocketSwap)
        {
            Debug.Log("performed");
            WeaponsManager.ChangeRocket();
        }
    }
    
    private void FixedUpdate()
    {
        if (_isPaused || _isMap)
        {
            return;
        }
        MovementLogic();
    }

    private void MovementLogic()
    {
        if (_roll > 0.1 || _roll < -0.1)
        {
            _rb.AddRelativeTorque(Vector3.back * _roll * _rollForce * Time.fixedDeltaTime);
        }
        if (_pitch.y > 0.1 || _pitch.y < -0.1)
        {
            _rb.AddRelativeTorque(Vector3.right * Mathf.Clamp(-_pitch.y, -1, 1) * _pitchForce * Time.fixedDeltaTime);
        }
        if (_pitch.x > 0.1 || _pitch.x < -0.1)
        {
            _rb.AddRelativeTorque(Vector3.up * Mathf.Clamp(_pitch.x, -1, 1) * _yawForce * Time.fixedDeltaTime);
        }

        
        if (_thrust > 0.1 || _thrust < -0.1)
        {
            
            _rb.AddRelativeForce(Vector3.forward * _thrust * _thrustForce * Time.fixedDeltaTime);
            _glideForce = _thrust * _thrustForce;
        }
        else
        {
            _rb.AddRelativeForce(Vector3.forward * _glideForce * Time.deltaTime);
            _glideForce *= _thrustReduction;
        }
        
        if (_upDown > 0.1 || _upDown < -0.1)
        {
            
            _rb.AddRelativeForce(Vector3.up * _upDown * _upDownForce * Time.fixedDeltaTime);
            _verticalGlide = _upDown * _upDownForce;
        }
        else
        {
            _rb.AddRelativeForce(Vector3.up * _verticalGlide * Time.deltaTime);
            _verticalGlide *= _upDownReduction;
        }
        
        if (_strafe > 0.1 || _strafe < -0.1)
        {
            
            _rb.AddRelativeForce(Vector3.right * _strafe * _strafeForce * Time.fixedDeltaTime);
            _strafeGlide = _strafe * _strafeForce;
        }
        else
        {
            _rb.AddRelativeForce(Vector3.right * _strafeGlide * Time.deltaTime);
            _strafeGlide *= _strafeReduction;
        }
    }
    
    #region Input
    
    public void OnThrust(InputAction.CallbackContext cont)
    {
        _thrust = cont.ReadValue<float>();
    }
    public void OnStrafe(InputAction.CallbackContext cont)
    {
        _strafe = cont.ReadValue<float>();
    }
    public void OnUpDown(InputAction.CallbackContext cont)
    {
        _upDown = cont.ReadValue<float>();
    }
    public void OnRoll(InputAction.CallbackContext cont)
    {
        _roll = cont.ReadValue<float>();
    }
    public void OnPitch(InputAction.CallbackContext cont)
    {
        _pitch= cont.ReadValue<Vector2>();
    }

    public void OnShoot(InputAction.CallbackContext cont)
    {
        _shoot = cont.performed;
    }
    public void OnChangingWeapon(InputAction.CallbackContext cont)
    {
        if (cont.started)
        {
            _currentWeapon = WeaponsManager.WeaponUsing;
            WeaponsManager.ChangeWeapon(0, true);
        }
    }
    public void EquipWeapon0()
    {
        _currentWeapon = 0;
        WeaponsManager.ChangeWeapon(_currentWeapon,false);
    }
    public void EquipWeapon1()
    {
        _currentWeapon = 1;
        WeaponsManager.ChangeWeapon(_currentWeapon,false);
    }
    public void EquipWeapon2()
    {
        _currentWeapon = 2;
        WeaponsManager.ChangeWeapon(_currentWeapon,false);
    }
    public void ShootRocket(InputAction.CallbackContext cont)
    {
        _rocket = cont.performed;
    }
    
    
    public void OnPausing(InputAction.CallbackContext cont)
    {
        if (cont.performed)
        {
            _isPaused = !_isPaused;
        }
    }
    #endregion
    
}
