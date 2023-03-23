# High performance `Dictionary<T>` written in C#.
An implementation of a high performance dictionary in C#.
# How does it work?
I implemented a frozen (can't be changed) dictionary which permutes the hash function so that we get a straightly aligned base array with values. There may be a chance that we can't compile the dictionary. This is just a proof of concept and not a finished product.
This dictionary has a much quicker `TryGetValue` method than the regular dictionary of .NET (measured `TryGetValue`):
```plaintext
|             Method |     Mean |     Error |    StdDev |
|------------------- |---------:|----------:|----------:|
| HighPerfDictionary | 5.876 ns | 0.1588 ns | 0.2173 ns |
|   DotNetDictionary | 8.567 ns | 0.2051 ns | 0.1919 ns |
```
This dictionary uses `string` as `Key`. However, we can also build a `HighPerfDictionary<TKey, TValue>` but we can't use `GetHashCode()` then. We would need our own `GethashCode(int mutator)` method, maybe specified by an `Interface`.