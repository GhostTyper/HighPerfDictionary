using System.Diagnostics.CodeAnalysis;

namespace HighPerfDictionary
{
    public class HighPerfDictionary<T>
    {
        private readonly int mutator;
        private readonly T?[] compiled;

        public HighPerfDictionary(params KeyValuePair<string, T>[] entries)
        {
            Random rng = new Random();
            int hash;

            if (entries is null)
                throw new ArgumentNullException(nameof(entries), "entries can't be null.");

            foreach (KeyValuePair<string, T> e in entries)
                if (string.IsNullOrEmpty(e.Key))
                    throw new ArgumentNullException(nameof(entries), "Key of entry can't be null or empty.");

            for (int size = entries.Length; size <= entries.Length * 5 && mutator == 0; size = Math.Max(size * 11 / 10, size + 1))
            {
                compiled = new T[size];

                for (int tries = 0; tries < 65536 && mutator == 0; tries++)
                {
                    Array.Clear(compiled);

                    mutator = rng.Next();

                    foreach (KeyValuePair<string, T> e in entries)
                    {
                        hash = this.hash(e.Key) % compiled.Length;

                        if (compiled[hash] is null)
                            compiled[hash] = e.Value;
                        else
                        {
                            mutator = 0;
                            break;
                        }
                    }
                }
            }

            if (mutator == 0)
                throw new InvalidDataException("Couldn't generate unique array.");
        }

        public bool TryGetValue(string key, [NotNullWhen(true)] out T? value)
        {
            value = compiled[hash(key) % compiled.Length];

            return value is not null;
        }

        private int hash(string key)
        {
            return key.Length ^ (key[((mutator >> 24) & 255) % key.Length] << 24) ^ (key[((mutator >> 16) & 255) % key.Length] << 18) ^ (key[((mutator >> 8) & 255) % key.Length] << 12) ^ (key[(mutator & 255) % key.Length] << 6);
        }
    }
}