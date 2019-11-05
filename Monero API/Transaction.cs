using System.Collections.Generic;

public class Target
{
    public string key { get; set; }
}

public class Vout
{
    public int amount { get; set; }
    public Target target { get; set; }
}

public class RctSignatures
{
}

public class RctsigPrunable
{
}

public class Transaction //RootObject ერქვა
{
    public int version { get; set; }
    public int unlock_time { get; set; }
    public List<object> vin { get; set; }
    public List<Vout> vout { get; set; }
    public List<int> extra { get; set; }
    public RctSignatures rct_signatures { get; set; }
    public RctsigPrunable rctsig_prunable { get; set; }
}