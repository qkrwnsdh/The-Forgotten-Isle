using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public Sprite image { get; private set; }
    public string name { get; private set; }
    public string count { get; private set; }
    public string description { get; private set; }

    public Item(string name, string description, string count)
    {
        this.name = name;
        this.description = description;
        this.count = count;
    }

}
