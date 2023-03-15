using NRules.Fluent.Dsl;
using NRules;
using NRules.Fluent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace KnowledgeBase
{

    public class SiblingRule : Rule
    {
        public override void Define()
        {
            ParentOf parentOf1 = null;
            ParentOf parentOf2 = null;
            Person child1 = null;
            Person child2 = null;

                When()
            .Match<ParentOf>(() => parentOf1, x => x.Parent != null)
            .Match<ParentOf>(() => parentOf2, x => x.Parent != null && x.Parent.Name == parentOf1.Parent.Name)
            .Match<Person>(() => child1, x => x == parentOf1.Child)
            .Match<Person>(() => child2, x => x == parentOf2.Child && x.Name != child1.Name);


            Then()
                .Do(ctx => ctx.Insert(new SiblingOf { Sibling1 = child1, Sibling2 = child2 }));
        }
    }

    public class GrandparentRule : Rule
    {
        public override void Define()
        {
            Person grandparent = null;
            Person parent = null;
            Person child = null;
            ParentOf parentOf1 = null;
            ParentOf parentOf2 = null;

            When()
                .Match<Person>(() => grandparent)
                .Match<Person>(() => parent)
                .Match<Person>(() => child)
                .Match<ParentOf>(() => parentOf1, x => x.Parent == grandparent && x.Child == parent)
                .Match<ParentOf>(() => parentOf2, x => x.Parent == parent && x.Child == child);

            Then()
                .Do(ctx => ctx.Insert(new GrandparentOf { Grandparent = grandparent, Grandchild = child }));
        }
    }

    public class AuntUncleRule : Rule
    {
        public override void Define()
        {
            Person auntUncle = null;
            Person sibling = null;
            Person nieceNephew = null;
            SiblingOf siblingOf = null;
            ParentOf parentOf = null;

            When()
                .Match<Person>(() => auntUncle)
                .Match<Person>(() => sibling)
                .Match<Person>(() => nieceNephew)
                .Match<SiblingOf>(() => siblingOf, x => x.Sibling1 == auntUncle && x.Sibling2 == sibling)
                .Match<ParentOf>(() => parentOf, x => x.Parent == sibling && x.Child == nieceNephew);

            Then()
                .Do(ctx => ctx.Insert(new AuntUncleOf { AuntUncle = auntUncle, NieceNephew = nieceNephew }));
        }
    }
}
