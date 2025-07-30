using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;


public static class Entry
{
    public static void Start()
    {
        Debug.Log("Hello World Entry.Start");
        Run_InstantiateByAddComponent();
        Run_AOTGeneric();
    }

    private static void Run_InstantiateByAddComponent()
    {
        GameObject cube = new GameObject("");
        cube.AddComponent<InstantiateByAddComponent>();
    }


    struct MyVec3
    {
        public int x;
        public int y;
        public int z;
    }

    private static void Run_AOTGeneric()
    {
        var arr = new List<MyVec3>();
        arr.Add(new MyVec3 { x = 1 });
        Debug.Log(arr[0].x);
    }
}