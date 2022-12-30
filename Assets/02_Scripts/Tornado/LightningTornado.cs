using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTornado : Tornado
{
    private List<Enemy> hitEnemies = new List<Enemy>();
    public List<Enemy> HitEnemies { get { return hitEnemies; } }

    [SerializeField] private GameObject lightningPrefab;

    protected override void Awake()
    {
        base.Awake();
        dmg = 10f;
    }
    public override float Damage(float _magnification = 1f)
    {
        return dmg * _magnification;

    }
    private void OnTriggerEnter(Collider _other)
    {
        hitEnemies.Add(_other.gameObject.GetComponent<Enemy>());//맞은애들 List에 저장
    }
    public override void Attack(Collider _other)
    {
        base.Attack(_other);
        
        _other.gameObject.GetComponent<Enemy>().SetEnemyAttackedMaterial(new Color(0f, 155f, 155f, 60f));
    }
    public override void FinishAttack(Collider _other)
    {
        base.FinishAttack(_other);

        //MakeSpark();
        _other.gameObject.GetComponent<Enemy>().StartCoroutine(_other.transform.gameObject.GetComponent<Enemy>().StrikeLightning(hitEnemies, lightningPrefab));
        _other.gameObject.GetComponent<Enemy>().ReturnEnemyOriginMaterial();

    }
    private void MakeSpark()
    {
        for (int i = 0; i < hitEnemies.Count; ++i)
        {
            Instantiate(lightningPrefab, hitEnemies[i].transform.position, hitEnemies[i].transform.rotation);
        }
    }
}
