using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase
{
    public class Person : IEquatable<Person>
    {
        public string Name { get; set; }

        public bool Equals(Person other)
        {
            if (other == null) return false;
            return this.Name == other.Name;
        }

        public override int GetHashCode()
        {
            return this.Name.GetHashCode();
        }
    }

    public class ParentOf : IEquatable<ParentOf>
    {
        public Person Parent { get; set; }
        public Person Child { get; set; }

        public bool Equals(ParentOf other)
        {
            if (other == null) return false;
            return this.Parent.Equals(other.Parent) && this.Child.Equals(other.Child);
        }

        public override int GetHashCode()
        {
            return this.Parent.GetHashCode() ^ this.Child.GetHashCode();
        }
    }

    public class SiblingOf
    {
        public Person Sibling1 { get; set; }
        public Person Sibling2 { get; set; }
    }

    public class GrandparentOf
    {
        public Person Grandparent { get; set; }
        public Person Grandchild { get; set; }
    }

    public class AuntUncleOf
    {
        public Person AuntUncle { get; set; }
        public Person NieceNephew { get; set; }
    }

    public class MarriedTo
    {
        public Person Spouse1 { get; set; }
        public Person Spouse2 { get; set; }
    }
}
