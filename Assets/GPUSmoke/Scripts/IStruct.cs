using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;

namespace GPUSmoke
{
    public interface IStruct<W> where W : unmanaged
    {
        public abstract int WordCount { get; }
        public abstract void ToWords(Span<W> dst);
        
    }
    
    public struct StructUtil<W, T> where W : unmanaged where T : IStruct<W>, new() {
        public static int WordCount { get => (new T()).WordCount; }
        public static int ByteCount { get => WordCount * UnsafeUtility.SizeOf<W>(); }
        public static W[] ToWords(ICollection<T> span) { 
            int unit = WordCount, ofst = 0;
            W[] words = new W[unit * span.Count];
            foreach (var t in span) {
                t.ToWords(words.AsSpan().Slice(ofst, unit));
                ofst += unit;
            }
            return words;
        }
    }
}