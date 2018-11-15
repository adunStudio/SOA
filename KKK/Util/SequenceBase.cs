using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KKK.Util
{
    public abstract class SequenceBase<T> : IEnumerable<T>
    {
        protected T[] m_elements;

        public int Length
        {
            get { return m_elements.Length; }
        }

        protected SequenceBase(params T[] elements)
        {
            m_elements = elements;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return m_elements.Cast<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            return string.Join(",", m_elements);
        }

        protected bool Equals(SequenceBase<T> other)
        {
            if (m_elements.Length != other.m_elements.Length)
            {
                return false;
            }

            // https://docs.microsoft.com/ko-kr/dotnet/api/system.linq.enumerable.sequenceequal?view=netframework-4.7.2
            return m_elements.SequenceEqual(other.m_elements);
        }

        public override bool Equals(object obj)
        {
            // https://docs.microsoft.com/ko-kr/dotnet/api/system.object.referenceequals?view=netframework-4.7.2
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((SequenceBase<T>)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (m_elements.Length + 13) ^
                       ((m_elements.Length != 0
                            ? m_elements[0].GetHashCode() ^ m_elements[m_elements.Length - 1].GetHashCode()
                            : 0) * 397);
            }
        }

    }
}
