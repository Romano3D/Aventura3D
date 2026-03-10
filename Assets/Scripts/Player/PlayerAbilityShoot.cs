using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public List<UIGunUpdater> uIGunUpdaters;

    public List<GunBase> guns;
    public Transform gunPosition;

    private GunBase _currentGun;

    private int _currentGunIndex = 0;
    protected override void Init()
    {
        base.Init();

        CreateGun();

        inputs.GamePlay.Shoot.performed += ctx => StartShoot();
        inputs.GamePlay.Shoot.canceled += ctx => CancelShoot();

        inputs.GamePlay.GunLimit.performed += ctx => ChangeGun(0);
        inputs.GamePlay.GunAngle.performed += ctx => ChangeGun(1);
    }
    private void ChangeGun(int index)
    {
        if (_currentGun != null)
        {
            _currentGun.CancelShoot(); // IMPORTANTE
            Destroy(_currentGun.gameObject);
        }

        _currentGunIndex = index;

        _currentGun = Instantiate(guns[index], gunPosition);
        _currentGun.transform.localPosition = Vector3.zero;
        _currentGun.transform.localEulerAngles = Vector3.zero;

        Debug.Log("Arma trocada para: " + guns[index].name);
    }

    private void CreateGun()
    {
        _currentGun = Instantiate(guns[_currentGunIndex], gunPosition);

        _currentGun.transform.localPosition = Vector3.zero;
        _currentGun.transform.localEulerAngles = Vector3.zero;
    }

    private void StartShoot()
    {
        _currentGun.StartShoot();
        Debug.Log("Shoot");
    }

    private void CancelShoot()
    {
        Debug.Log("Cancel Shoot");
        _currentGun.CancelShoot();
    }
}
