# HighPerfDictionary
An implementation of a high performance dictionary for C#.
# How does it work?
I implemented a frozen dictionary which permutates the hash function so that we get a straightly aligned base array with values. There may be a chance that we can't compile the dictionary.
This dictionary is way quicker that the regular dictionary of .NET:
```plain
|             Method |     Mean |     Error |    StdDev |
|------------------- |---------:|----------:|----------:|
| HighPerfDictionary | 5.876 ns | 0.1588 ns | 0.2173 ns |
|   DotNetDictionary | 8.567 ns | 0.2051 ns | 0.1919 ns |
```