# High performance `Dictionary<T>` written in C#.
An implementation of a high performance dictionary in C#.
# How does it work?
I implemented a frozen (can't be changed) dictionary which permutes the hash function so that we get a straightly aligned base array with values. There may be a chance that we can't compile the dictionary. This is just a proof of concept and not a finished product.
This dictionary has a much quicker `TryGetValue` method than the regular dictionary of .NET (measured `TryGetValue`):
```plaintext
|                 Method |     Mean |     Error |    StdDev |
|----------------------- |---------:|----------:|----------:|
|     HighPerfDictionary | 5.634 ns | 0.1554 ns | 0.1453 ns |
| DotNetFrozenDictionary | 8.843 ns | 0.1126 ns | 0.1054 ns |
|       DotNetDictionary | 8.529 ns | 0.0751 ns | 0.0702 ns |
```
This dictionary uses `string` as `Key`. However, we can also build a `HighPerfDictionary<TKey, TValue>` but we can't use `GetHashCode()` then. We would need our own `GethashCode(int mutator)` method, maybe specified by an `Interface`.
# Changes
* 2023.03.23: The project now uses .NET 8 (preview-2 as of the time of writing this `README.md`) to check if the `FrozenDictionary<string, string>` (with `optimizeForReading: true` set) of .NET gives any better performance. LOL.