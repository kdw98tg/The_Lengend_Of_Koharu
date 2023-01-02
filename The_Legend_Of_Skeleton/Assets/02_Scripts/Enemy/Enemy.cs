using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class Enemy : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float currentEnemyHp;
    [SerializeField] private float maxEnemyHp = 100f;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private D_Player player;

    private float moveSpeed = 10f;
    private float originalMoveSpeed;
    private float convertTime = 0f;
    [SerializeField] private float exitFreezeTime = 3f;
    private float freezeTime;
    //private bool isFreezed = false;

    private MeshRenderer mesh;
    private Color originMatColor;
    private LineRenderer lr;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        currentEnemyHp = maxEnemyHp;
        mesh = GetComponent<MeshRenderer>();
        originMatColor = mesh.materials[0].color;
        lr = GetComponent<LineRenderer>();
        originalMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        //Debug.Log(moveSpeed);
        MoveRightLeft();
    }

    private void OnTriggerStay(Collider _other)
    {
        if (_other.gameObject.CompareTag("TORNADO"))
        {
            currentEnemyHp -= _other.gameObject.GetComponent<Tornado>().Damage();//토네이도의 종류에따라 데미지를 다르게 받음
        }
    }
    private void MoveRightLeft()//좌우 움직임 -> ice tornado를 맞았을때 속도가 느려지는걸 보려고 만듦
    {
        convertTime += Time.deltaTime;


        if (convertTime < 1f)
        {
            transform.position = transform.position + new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
        }
        else if (convertTime < 2f && convertTime >= 1f)
        {
            transform.position = transform.position - new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
        }
        else
        {
            convertTime = 0f;
        }


    }

    public void Attacked(float _damage, int _elementIdx)
    {
        currentEnemyHp -= _damage;
    }

    public Rigidbody ReturnEnemyRigidbody()
    {
        return rb;
    }

    public Vector3 ReturnEnemyPos()
    {
        return transform.position;
    }

    public float EnemyHp()
    {
        return currentEnemyHp;
    }

    #region FireTornado

    public IEnumerator EnemyDotDamage(float _dotDamagePerSeconds, int _dotCnt)
    {
        for (int i = 0; i < _dotCnt; i++)
        {
            currentEnemyHp -= _dotDamagePerSeconds;
            SetEnemyAttackedMaterial(new Color(255f, 0f, 0f, 50f));
            yield return new WaitForSeconds(0.1f);
            ReturnEnemyOriginMaterial();
            yield return new WaitForSeconds(1f);
        }
    }

    #endregion


    #region IceTornado
    public IEnumerator EnemyFreezed(float _enemySlowSpeed)
    {
        moveSpeed = _enemySlowSpeed;
        freezeTime = 0f;
        freezeTime += Time.deltaTime;
        //얼음프리팹 생성

        if (freezeTime < exitFreezeTime)
        {
            _enemySlowSpeed = originalMoveSpeed;

            //얼음프리팹 삭제
        }
        yield return null;
        //얼음상태
        //얼음상태일때 공격하면 깨지면서 데미지 반환
    }


    #endregion

    #region LightingTornado
    //public IEnumerator StrikeLightning()
    //{
    //    Collider[] colls = Physics.OverlapSphere(transform.position, 10f, enemyLayer);

    //    for (int j = 0; j < 10; j++)
    //    {
    //        lr.SetPosition(0, player.transform.position);

    //        for (int i = 0; i < colls.Length - 1; i++)
    //        {
    //            lr.SetPosition(i + 1, colls[i + 1].transform.position);
    //            //yield return new WaitForSeconds(0.1f);
    //        }
    //        yield return new WaitForSeconds(1f);
    //    }
    //}

    //public IEnumerator StrikeLightning(List<Enemy> _hitEnemies, GameObject _lightningPrefab)
    //{
    //    GameObject lightning;
    //    for(int i = 0; i< _hitEnemies.Count-1; ++i)
    //    {
    //        if ((_hitEnemies[i].transform.position - _hitEnemies[i+1].transform.position).magnitude < 10f)
    //        {
    //            lightning = Instantiate(_lightningPrefab, _hitEnemies[i].transform.position, _hitEnemies[i].transform.rotation);

    //            lightning.transform.position = Vector3.Lerp(_hitEnemies[i].transform.position, _hitEnemies[i + 1].transform.position,10f);
    //            //orm.Translate(_hitEnemies[i].transform.position, _hitEnemies[i+1].transform);
    //        }
    //    }
    //    yield return null;
    //}
    public IEnumerator StrikeLightning(List<Enemy> _enemies, GameObject _lightningPrefab)
    {
            GameObject lightning = Instantiate(_lightningPrefab, Vector3.zero, Quaternion.identity);
        for (int i = 0; i < _enemies.Count - 1; ++i)
        {
            lightning.GetComponent<LightningBoltScript>().StartPosition = _enemies[i].transform.localPosition;
            lightning.GetComponent<LightningBoltScript>().EndPosition = _enemies[i+1].transform.localPosition;
            yield return new WaitForSeconds(0.5f);
            
            //라이트닝 사라지고 나타나고 데미지 주는 로직 만들기
        }
            yield return null;

    }

        #endregion

        #region EnemyMaterial
        public Material SetEnemyAttackedMaterial(Color _color)//맞았을때 표시할 머티리얼 색 
        {
            Material dmgMat = mesh.materials[0];
            dmgMat.color = _color;
            return dmgMat;
        }

        public Material ReturnEnemyOriginMaterial()//원래의 머티리얼로 돌아옴
        {
            mesh.materials[0].color = originMatColor;
            return mesh.materials[0];
        }
        #endregion

    }