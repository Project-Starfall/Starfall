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
    [SerializeField] private bool  pushLeft;

    // Start is called before the first frame update
    void Start()
    {
        effector2D = GetComponent<AreaEffector2D>();
        ps = GetComponent<ParticleSystem>();

        effector2D.forceMagnitude = forceMagnitude;
        effector2D.useGlobalAngle = false;
        effector2D.forceAngle = pushLeft ? 180f : 0f;
        EnableGeyser(false);

        StartCoroutine("GeyserCycle");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Turns the geyser on and off
    public void EnableGeyser(bool v)
    {
        var em = ps.emission;

        effector2D.enabled = v;
        em.enabled = v;
    }

    // Cycles turning the geyser on and off
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
