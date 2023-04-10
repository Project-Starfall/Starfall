using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGeyser : MonoBehaviour
{
    private AreaEffector2D effector2D;
    private ParticleSystem ps;

    [SerializeField] private float forceMagnitude;
    [SerializeField] private float WaitToStart;
    [SerializeField] private float geyserOnSeconds;
    [SerializeField] private float geyserOffSeconds;

    // Start is called before the first frame update
    void Start()
    {
        effector2D = GetComponent<AreaEffector2D>();
        ps = GetComponent<ParticleSystem>();

        effector2D.forceMagnitude = forceMagnitude;
        EnableGeyser(false);

        StartCoroutine("GeyserCycle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnableGeyser(bool v)
    {
        var em = ps.emission;

        effector2D.enabled = v;
        em.enabled = v;
    }

    private IEnumerator GeyserCycle()
    {
        yield return new WaitForSeconds(WaitToStart);

        while (true)
        {
            EnableGeyser(true);
            yield return new WaitForSeconds(geyserOnSeconds);
            EnableGeyser(false);
            yield return new WaitForSeconds(geyserOffSeconds);
        }
    }
}
