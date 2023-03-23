# High performance `Dictionary<T>` written in C#.
An implementation of a high performance dictionary in C#.
# How does it work?
I implemented a frozen (can't be changed) dictionary which permutes the hash function so that we get a straightly aligned base array with values. There may be a chance that we can't compile the dictionary. This is just a proof of concept and not a finished product.
This dictionary has a much quicker `TryGetValue` method than the regular dictionary of .NET (measured `TryGetValue`):
```plaintext
|                 Method |     Mean |     Error |    StdDev |
|----------------------- |---------:|----------:|----------:|
|     HighPerfDictionary | 5.538 ns | 0.0936 ns | 0.0876 ns |
| DotNetFrozenDictionary | 9.152 ns | 0.1548 ns | 0.1372 ns |
|       DotNetDictionary | 8.456 ns | 0.0719 ns | 0.0637 ns |
```
This dictionary uses `string` as `Key`. However, we can also build a `HighPerfDictionary<TKey, TValue>` but we can't use `GetHashCode()` then. We would need our own `GethashCode(int mutator)` method, maybe specified by an `Interface`.
# Changes
* 2023.03.23: The project now uses .NET 8 (preview-2 as of the time of writing this README.md) to check if the `FrozenDictionary<string, string>` of .NET gives any better performance. LOL.