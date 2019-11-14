using System.Collections.Generic;

public class Key
{
    public object amount { get; set; }
    public List<int> key_offsets { get; set; }
    public string k_image { get; set; }
}

public class Vin
{
    public Key key { get; set; }
}

public class Target
{
    public string key { get; set; }
}

public class Vout
{
    public object amount { get; set; }
    public Target target { get; set; }
}

public class RootObject
{
    public int version { get; set; }
    public int unlock_time { get; set; }
    public List<Vin> vin { get; set; }
    public List<Vout> vout { get; set; }
    public List<int> extra { get; set; }
    public List<string> signatures { get; set; }
}