namespace Galebra.Security.Headers.Csp.Infrastructure;

internal class CspNonce : ICspNonce
{
    public CspNonce(IDictionary<string, CspPolicyGroup> policyGroups)
    {
        foreach (var group in policyGroups)
        {
            Nonces.Add(group.Key, new NonceGenerator(group.Value));
        }
    }
    public Dictionary<string, INonceGenerator> Nonces { get; init; }
        = new Dictionary<string, INonceGenerator>();
}
