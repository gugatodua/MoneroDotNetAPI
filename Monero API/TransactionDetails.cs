using System.Collections;
using System.Collections.Generic;

public class Target
{
    public string key { get; set; }
}

public class Vout
{
    public object amount { get; set; }
    public Target target { get; set; }
}

public class RootObject:IEnumerable<Vout>
{
    public List<Vout> vout { get; set; }

    public IEnumerator<Vout> GetEnumerator()
    {
        return vout.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return vout.GetEnumerator();
    }
}

public class ReturningTransactionDetails{
    public List<string> Keys { get; set; }
    public List<int> Extra { get; set; }

}