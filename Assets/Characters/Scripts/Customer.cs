using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] public CustomerData data;

    public void Init(CustomerData data)
    {
        this.data = data;
    }

    public Response GetResponse(Product product, int offeredPrice)
    {
        Vector2 range = product.defaultPrice * data.targetRelativePriceRange;
        Response response = new Response();
        //response.status = ResponseStatus.
        return response;
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