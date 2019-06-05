﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс-генератор маршрута из пула точек
/// </summary>
public class RouteCompile : MonoBehaviour
{
    int lastNum;//последняя добавленная точка(устаревшее)
    float X;
    float Y;
    float Z;

    public Vector3[] Compile(Vector3 startPosition, float range)
    {
        int length = Random.Range(4, 10);//генерация размера маршрута
        Debug.Log("Length: " + length);
        Vector3[] route = new Vector3[length];
        for(int i = 0; i < length; i++)
        {
            if(i == 0)
            {
                X = Random.Range(startPosition.x - range, startPosition.x + range);//генерация случайной координаты Х в заданной области
                Z = startPosition.z + Mathf.Sqrt(Mathf.Pow(range, 2) - Mathf.Pow(X - startPosition.x, 2));//рассчет координаты Z исходя из значения координаты Х
                Y = Terrain.activeTerrain.SampleHeight(new Vector3(X, 0, Z));

            }
            else if(i%2 == 0)
            {
                X = Random.Range(startPosition.x, startPosition.x + range);
                Z = startPosition.z + Mathf.Sqrt(Mathf.Pow(range, 2) - Mathf.Pow(X - startPosition.x, 2));//рассчет координаты Z исходя из значения координаты Х
                Y = Terrain.activeTerrain.SampleHeight(new Vector3(X, 0, Z));
            }
            else if(i%3 == 0)
            {
                X = Random.Range(startPosition.x - range, startPosition.x);
                Z = startPosition.z + Mathf.Sqrt(Mathf.Pow(range, 2) - Mathf.Pow(X - startPosition.x, 2));//рассчет координаты Z исходя из значения координаты Х
                Y = Terrain.activeTerrain.SampleHeight(new Vector3(X, 0, Z));
            }
            else
            {
                float delta = Mathf.Abs(startPosition.x) - Mathf.Abs(X);
                if(X < startPosition.x)
                {
                    X = startPosition.x + delta;
                }
                else
                {
                    X = startPosition.x - delta;
                }
                Z = startPosition.z + Mathf.Sqrt(Mathf.Pow(range, 2) - Mathf.Pow(X - startPosition.x, 2));//рассчет координаты Z исходя из значения координаты Х
                Y = Terrain.activeTerrain.SampleHeight(new Vector3(X, 0, Z));
            }

            route[i] = new Vector3(X, Y, Z);
            Debug.Log(route[i]);

        }
        Debug.Log("Route created");
        return route;
    }
}
