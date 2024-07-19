using NaughtyAttributes;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField][ReadOnly] public CustomerData data;
    [SerializeField] private SpriteRenderer headRenderer;
    [SerializeField] private SpriteRenderer torsoRenderer;

    public void Init(CustomerData data)
    {
        this.data = data;

    }

    public static Customer Create(CustomerData data)
    {
        var customer = Instantiate(Scripts.CharacterPrefab);
        customer.data = data;
        customer.headRenderer.sprite = data.randomHeadSprites.GetRandom();
        customer.torsoRenderer.sprite = data.randomTorsoSprites.GetRandom();
        return customer;
    }

    public Response GetResponse(Product product, int offeredPrice)
    {
        Vector2 range = product.defaultPrice * data.targetRelativePriceRange;
        Response response = new Response();
        //response.status = ResponseStatus.
        return response;
    }

    public void QuitQueue()
    {
        // play animation
        // disable
        // store to the pool

        Destroy(gameObject);
    }

    public void Approach()
    {
        // play animation
        // ask products
    }
}

public struct Response
{
    public ResponseStatus status;
}

public enum ResponseStatus
{
    Accepted,
    Rejected
}