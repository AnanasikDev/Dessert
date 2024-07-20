using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField][ReadOnly] public CustomerData data;
    [SerializeField] private SpriteRenderer headRenderer;
    [SerializeField] private SpriteRenderer torsoRenderer;

    public int indexInQueue;

    public static Stack<Customer> pool = new Stack<Customer>();

    public Request request;
    public ResponseStatus responseStatus = ResponseStatus.Pending;

    public Action<Request> OnRequestEvent;
    public Action<Response> OnResponseEvent;
    public static Action OnQuitEvent;

    [SerializeField] private int maxResponses = 2;
    [ReadOnly][SerializeField] private int responses = 0;


    public static Customer Create(CustomerData data, int indexInQueue)
    {
        Customer customer;

        if (pool.Count > 0)
        {
            customer = pool.Pop();
            customer.gameObject.SetActive(true);
        }
        else
        {
            customer = Instantiate(Scripts.CharacterPrefab);
            customer.data = data;
            customer.OnResponseEvent += customer.HandleResponse;
        }

        customer.data = data;
        customer.headRenderer.sprite = data.randomHeadSprites.GetRandom();
        customer.torsoRenderer.sprite = data.randomTorsoSprites.GetRandom();

        if (indexInQueue == 0) customer.GetRequest();

        return customer;
    }

    public Request GetRequest()
    {
        request = new Request();
        // choose random product
        request.dessert = Scripts.DessertManager.dessertsDatas.GetRandom();
        OnRequestEvent?.Invoke(request);
        return request;
    }

    public Response GetResponse(DessertSO product, int offeredPrice)
    {
        Vector2 range = product.Price * data.targetRelativePriceRange;
        Response response = new Response();
        response.status = offeredPrice > range.x && offeredPrice < range.y ? ResponseStatus.Accepted : ResponseStatus.Rejected;
        this.responseStatus = response.status;
        responses++;
        OnResponseEvent?.Invoke(response);
        return response;
    }

    private void HandleResponse(Response response)
    {
        if (indexInQueue != 0) return;

        if (response.status == ResponseStatus.Accepted)
        {
            // add money to the budget
            // play happy animation

            QuitQueue();

            return;
        }

        if (response.status == ResponseStatus.Rejected)
        {
            if (responses >= maxResponses)
            {
                // no more chances to change the offer, quit the queue without buying

                QuitQueue();
                return;
            }
        }

    }

    public void QuitQueue()
    {
        // play animation
        
        // disable
        gameObject.SetActive(false);
        indexInQueue = -1;
        
        // store to the pool
        pool.Push(this);
        OnQuitEvent?.Invoke();
    }

    public void MoveForward()
    {
        indexInQueue--;

        // shift object towards default position

        if (indexInQueue > 0) return;

        // if first in the queue

        // play request animation?

        // ask products
        this.request = GetRequest();
    }
}

public struct Response
{
    public ResponseStatus status;
}

public struct Request
{
    public DessertSO dessert;
}

public enum ResponseStatus
{
    Pending,
    Accepted,
    Rejected
}