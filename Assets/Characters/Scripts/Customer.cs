using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    [SerializeField][ReadOnly] public CustomerData data;
    [SerializeField] private Image headRenderer;
    [SerializeField] private Transform customersHandler;

    public int indexInQueue = -1;

    public static Stack<Customer> pool = new Stack<Customer>();

    public Request request;
    public ResponseStatus responseStatus = ResponseStatus.Pending;

    public static Action<Customer, Request> OnRequestEvent;
    public static Action<Customer, Response> OnResponseEvent;
    public static Action OnQuitEvent;

    [SerializeField] private int maxResponses = 2;
    [ReadOnly][SerializeField] private int responses = 0;

    [SerializeField] private Vector2 quitShift;


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
            customer.transform.SetParent(Scripts.QueueManager.customersHandler);
        }

        customer.indexInQueue = indexInQueue;
        customer.gameObject.name = "Customer " + indexInQueue;
        customer.responses = 0;

        customer.data = data;
        customer.headRenderer.sprite = data.randomHeadSprites.GetRandom();

        //customer.headRenderer.sortingOrder = -indexInQueue;


        if (indexInQueue == 0) customer.GetRequest();

        return customer;
    }

    public Request GetRequest()
    {
        request = new Request();

        // choose random product
        request.dessert = Scripts.DessertManager.dessertsDatas.GetRandom();
        Debug.Log(request.dessert.name + " is requested!");
        OnRequestEvent?.Invoke(this, request);
        return request;
    }

    public void GetResponse(DessertSO product, int offeredPrice)
    {
        if (indexInQueue != 0 || product == null) return;

        Vector2 range = product.Price * data.targetRelativePriceRange;
        Response response = new Response();
        response.status = offeredPrice >= range.x && offeredPrice <= range.y ? ResponseStatus.Accepted : ResponseStatus.Rejected;
        this.responseStatus = response.status;
        responses++;
        OnResponseEvent?.Invoke(this, response);

        Scripts.RequestPanel.DrawResponse(this, response);
        HandleResponse(response);
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

        responses++;
    }

    public void ForceQuit()
    {
        // disable
        gameObject.SetActive(false);
        indexInQueue = -1;

        // store to the pool
        pool.Push(this);
    }
    public void QuitQueue()
    {
        IEnumerator animate()
        {
            int frames = 50;
            for (int i = 0; i < frames; i++)
            {
                transform.position += (quitShift).ConvertTo3D() / frames;
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(0.25f);

            // play animation

            ForceQuit();

            OnQuitEvent?.Invoke();
        }
        
        StartCoroutine(animate());
    }

    public void MoveForward()
    {
        indexInQueue--;
        gameObject.name = "Customer " + indexInQueue;
        Debug.Log("Moving forward");

        //headRenderer.sortingOrder = -indexInQueue;

        Vector3 shift = -(Scripts.QueueManager.nextCustomerPositionShift * Vector2.one).ConvertTo3D();
        Vector3 scaleShift = -(Scripts.QueueManager.nextCustomerScaleShift * Vector2.one).ConvertTo3D();
        int frames = 50;
        // shift object towards default position
        IEnumerator animate()
        {
            for (int i = 0; i < frames; i++)
            {
                transform.position += shift / frames;
                transform.localScale += scaleShift / frames;
                yield return new WaitForEndOfFrame();
            }
            yield return new WaitForSeconds(0.25f);

            if (indexInQueue != 0) yield break;

            // if first in the queue

            // play request animation?

            // ask products
            this.request = GetRequest();
        }

        StartCoroutine(animate());
    }
}

public struct Response
{
    public ResponseStatus status;

    public Response(ResponseStatus _status) => status = _status;
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