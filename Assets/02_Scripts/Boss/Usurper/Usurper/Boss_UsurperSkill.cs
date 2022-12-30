using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_UsurperSkill : MonoBehaviour
{
    [SerializeField] private GameObject m_ThunderPlanePrefab = null;
    private ThunderPlane[] m_ThunderPlanes;
    [SerializeField] private int planeNum = 8;

    private void Awake()
    {
        m_ThunderPlanes = new ThunderPlane[planeNum];
        for (int i = 0; i < planeNum; ++i)
        {
            m_ThunderPlanes[i] = Instantiate(m_ThunderPlanePrefab, this.transform.position, Quaternion.identity, this.transform).GetComponent<ThunderPlane>();
            m_ThunderPlanes[i].SetActive(false);
        }
    }


    public void ThunderPlane(float _summonsInterval)
    {
        Vector3[] planePoses = new Vector3[planeNum];

        for (int i = 0; i < planeNum; ++i)
        {
            planePoses[i] = GetPlanePos(this.transform.position, i % 4);
        }

        StartCoroutine(SummonsThunderCoroutine(planePoses, _summonsInterval));
    }

    public void ThunderPlaneRange(Vector3 _targetPos)
    {
        Vector3 planePos = GetPlanePos(_targetPos, 4);
        SetThunderPlane(0, planePos);
    }

    public void ThunderPlaneLazy(float _summonsInterval, Vector3 _targetPos)
    {
        ThunderPlane(_summonsInterval);
        ThunderPlaneRange(_targetPos);
    }


    private IEnumerator SummonsThunderCoroutine(Vector3[] _planePoses, float _summonsInterval)
    {
        WaitForSeconds summonsIntervalTime = new WaitForSeconds(_summonsInterval);

        for (int i = 0; i < m_ThunderPlanes.Length; ++i)
        {
            SetThunderPlane(i, _planePoses[i]);
            yield return summonsIntervalTime;
        }
    }

    private void SetThunderPlane(int _idx, Vector3 _pos)
    {
        m_ThunderPlanes[_idx].SetPosition(_pos);
        m_ThunderPlanes[_idx].SetActive(true);
    }


    private Vector3 GetPlanePos(Vector3 _centerPos, int _quadrant)
    {
        Vector3 planePos = Vector3.zero;
        float posX = 0f;
        float posZ = 0f;

        switch (_quadrant)
        {
            case 0:
                posX = Random.Range(2f, 10f);
                posZ = Random.Range(2f, 10f);
                break;

            case 1:
                posX = Random.Range(-10f, 2f);
                posZ = Random.Range(2f, 10f);
                break;

            case 2:
                posX = Random.Range(-10f, 2f);
                posZ = Random.Range(-10f, 2f);
                break;

            case 3:
                posX = Random.Range(2f, 10f);
                posZ = Random.Range(-10f, 2f);
                break;

            case 4:
                posX = Random.Range(0f, 0.5f);
                posX = Random.Range(0f, 0.5f);
                break;
        }

        planePos = new Vector3(_centerPos.x + posX, 0f, _centerPos.z + posZ);
        return planePos;
    }
}
