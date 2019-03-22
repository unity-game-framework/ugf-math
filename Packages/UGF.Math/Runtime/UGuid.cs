using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace UGF.Math.Runtime
{
    /// <summary>
    /// Represents the Unity serializable Guid.
    /// </summary>
    [Serializable]
    public struct UGuid : IEquatable<UGuid>, IComparable<UGuid>
    {
        [SerializeField] private long m_value0;
        [SerializeField] private long m_value1;

        /// <summary>
        /// Gets the first value part of the guid data.
        /// </summary>
        public long Value0 { get { return m_value0; } }
        
        /// <summary>
        /// Gets the second value part of the guid data.
        /// </summary>
        public long Value1 { get { return m_value1; } }

        /// <summary>
        /// Gets the empty uguid, that represents a Guid.Empty.
        /// </summary>
        public static UGuid Empty { get { return m_empty; } }

        private static readonly UGuid m_empty;

        [StructLayout(LayoutKind.Explicit)]
        private struct Converter
        {
            [FieldOffset(0)] public Guid Guid;
            [FieldOffset(0)] public UGuid UGuid;
        }

        static UGuid()
        {
            var converter = new Converter { Guid = Guid.Empty };

            m_empty = converter.UGuid;
        }

        /// <summary>
        /// Creates uguid from the specified first and second value parts of the guid data.
        /// </summary>
        /// <param name="value0">The first value part of the guid data.</param>
        /// <param name="value1">The second value part of the guid data.</param>
        public UGuid(long value0, long value1)
        {
            m_value0 = value0;
            m_value1 = value1;
        }

        /// <summary>
        /// Creates uguid from the specified uguid data.
        /// </summary>
        /// <param name="uguid">The source uguid.</param>
        public UGuid(UGuid uguid)
        {
            m_value0 = uguid.m_value0;
            m_value1 = uguid.m_value1;
        }

        /// <summary>
        /// Creates uguid from the specified guid.
        /// </summary>
        /// <param name="guid">The source guid.</param>
        public UGuid(Guid guid)
        {
            var converter = new Converter { Guid = guid };

            UGuid uguid = converter.UGuid;

            m_value0 = uguid.m_value0;
            m_value1 = uguid.m_value1;
        }

        /// <summary>
        /// Creates uguid from the specified guid string representation.
        /// </summary>
        /// <param name="guid">The guid representation.</param>
        public UGuid(string guid)
        {
            var converter = new Converter { Guid = new Guid(guid) };

            UGuid uguid = converter.UGuid;

            m_value0 = uguid.m_value0;
            m_value1 = uguid.m_value1;
        }

        /// <summary>
        /// Creates uguid from the specified byte array representation of the guid data.
        /// </summary>
        /// <param name="bytes">The byte array representation of the guid data.</param>
        public UGuid(byte[] bytes)
        {
            var converter = new Converter { Guid = new Guid(bytes) };

            UGuid uguid = converter.UGuid;

            m_value0 = uguid.m_value0;
            m_value1 = uguid.m_value1;
        }
        
        /// <summary>
        /// Converts uguid data to new byte array.
        /// </summary>
        public byte[] ToByteArray()
        {
            var converter = new Converter { UGuid = this };

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
            var converter = new Converter { UGuid = uguid };

            return converter.Guid;
        }

        public static implicit operator UGuid(Guid guid)
        {
            var converter = new Converter { Guid = guid };

            return converter.UGuid;
        }

        /// <summary>
        /// Generates new uguid.
        /// </summary>
        public static UGuid NewUGuid()
        {
            var converter = new Converter { Guid = Guid.NewGuid() };

            return converter.UGuid;
        }

        public int CompareTo(UGuid other)
        {
            int value0Comparison = m_value0.CompareTo(other.m_value0);
            
            return value0Comparison != 0 ? value0Comparison : m_value1.CompareTo(other.m_value1);
        }

        public override string ToString()
        {
            var converter = new Converter { UGuid = this };

            return converter.Guid.ToString("N");
        }
    }
}