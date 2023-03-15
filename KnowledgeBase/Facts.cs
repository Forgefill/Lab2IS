using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeBase
{
    public class Person
    {
        public string Name { get; set; }
    }

    public class ParentOf
    {
        public Person Parent { get; set; }
        public Person Child { get; set; }
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
}
