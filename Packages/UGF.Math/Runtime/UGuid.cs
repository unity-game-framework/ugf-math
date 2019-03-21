using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace UGF.Math.Runtime
{
    [Serializable]
    public struct UGuid : IEquatable<UGuid>, IComparable<UGuid>
    {
        [SerializeField] private long m_value0;
        [SerializeField] private long m_value1;

        public long Value0 { get { return m_value0; } }
        public long Value1 { get { return m_value1; } }

        public static UGuid Empty { get { return m_empty; } }

        private static readonly Converter m_converter = new Converter();
        private static readonly UGuid m_empty;

        [StructLayout(LayoutKind.Explicit)]
        private struct Converter
        {
            [FieldOffset(0)] public Guid Guid;
            [FieldOffset(0)] public UGuid UGuid;
        }

        static UGuid()
        {
            Converter converter = m_converter;

            converter.Guid = Guid.Empty;

            m_empty = converter.UGuid;
        }

        public UGuid(long value0, long value1)
        {
            m_value0 = value0;
            m_value1 = value1;
        }

        public UGuid(UGuid uguid)
        {
            m_value0 = uguid.Value0;
            m_value1 = uguid.Value1;
        }

        public UGuid(Guid guid)
        {
            Converter converter = m_converter;

            converter.Guid = guid;

            UGuid uguid = converter.UGuid;

            m_value0 = uguid.Value0;
            m_value1 = uguid.Value1;
        }

        public UGuid(string guid)
        {
            Converter converter = m_converter;

            converter.Guid = new Guid(guid);

            UGuid uguid = converter.UGuid;

            m_value0 = uguid.Value0;
            m_value1 = uguid.Value1;
        }

        public byte[] ToByteArray()
        {
            Converter converter = m_converter;

            converter.UGuid = this;

            return converter.Guid.ToByteArray();
        }

        public bool Equals(UGuid other)
        {
            return Value0 == other.Value0 && Value1 == other.Value1;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is UGuid other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Value0.GetHashCode() * 397) ^ Value1.GetHashCode();
            }
        }

        public static bool operator ==(UGuid left, UGuid right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(UGuid left, UGuid right)
        {
            return !left.Equals(right);
        }

        public static implicit operator Guid(UGuid uguid)
        {
            Converter converter = m_converter;

            converter.UGuid = uguid;

            return converter.Guid;
        }

        public static implicit operator UGuid(Guid guid)
        {
            Converter converter = m_converter;

            converter.Guid = guid;

            return converter.UGuid;
        }

        public static UGuid NewUGuid()
        {
            Converter converter = m_converter;

            converter.Guid = Guid.NewGuid();

            return converter.UGuid;
        }

        public int CompareTo(UGuid other)
        {
            int value0Comparison = m_value0.CompareTo(other.m_value0);
            
            return value0Comparison != 0 ? value0Comparison : m_value1.CompareTo(other.m_value1);
        }

        public override string ToString()
        {
            Converter converter = m_converter;

            converter.UGuid = this;

            return converter.Guid.ToString("N");
        }
    }
}