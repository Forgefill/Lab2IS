using System;
using System.Collections.Generic;
using KnowledgeBase;
using NRules;
using NRules.Fluent;

namespace FamilyTreeExample
{
    class Program
    {

        static void Main(string[] args)
        {
            // 1. Configure NRules repository and load rules
            var repository = new RuleRepository();
            repository.Load(x => x.From(typeof(SiblingRule).Assembly));
            repository.Load(x => x.From(typeof(GrandparentRule).Assembly));
            repository.Load(x => x.From(typeof(AuntUncleRule).Assembly));

            // 2. Compile rules
            var factory = repository.Compile();

            // 3. Create a working session
            var session = factory.CreateSession();

            // 4. Insert explicit facts (F)
            var alice = new Person { Name = "Alice" };
            var bob = new Person { Name = "Bob" };
            var carol = new Person { Name = "Carol" };
            var david = new Person { Name = "David" };
            var eva = new Person { Name = "Eva" };
            var frank = new Person { Name = "Frank" };
            var grace = new Person { Name = "Grace" };
            var hannah = new Person { Name = "Hannah" };
            var ian = new Person { Name = "Ian" };

            session.Insert(alice);
            session.Insert(bob);
            session.Insert(carol);
            session.Insert(david);
            session.Insert(eva);
            session.Insert(frank);
            session.Insert(grace);
            session.Insert(hannah);
            session.Insert(ian);

            session.Insert(new ParentOf { Parent = alice, Child = carol });
            session.Insert(new ParentOf { Parent = alice, Child = david });
            session.Insert(new ParentOf { Parent = bob, Child = carol });
            session.Insert(new ParentOf { Parent = bob, Child = david });
            session.Insert(new ParentOf { Parent = carol, Child = eva });
            session.Insert(new ParentOf { Parent = david, Child = frank });
            session.Insert(new ParentOf { Parent = david, Child = grace });
            session.Insert(new ParentOf { Parent = eva, Child = hannah });
            session.Insert(new ParentOf { Parent = frank, Child = ian });

            // 5. Fire all rules and perform inference
            session.Fire();

            // 6. Query and print results
            var siblings = session.Query<SiblingOf>().ToList();
            var grandparents = session.Query<GrandparentOf>().ToList();
            var auntsUncles = session.Query<AuntUncleOf>().ToList();
            
            HashSet<string> result = new HashSet<string>();
            
            result.Add("Sibling relationships:");
            foreach (var sibling in siblings)
            {
                result.Add($"{sibling.Sibling1.Name} is a sibling of {sibling.Sibling2.Name}");
            }

            result.Add("\nGrandparent relationships:");
            foreach (var grandparent in grandparents)
            {
                result.Add($"{grandparent.Grandparent.Name} is a grandparent of {grandparent.Grandchild.Name}");
            }

            result.Add("\nAunt/Uncle relationships:");
            foreach (var auntUncle in auntsUncles)
            {
                result.Add($"{auntUncle.AuntUncle.Name} is an aunt/uncle of {auntUncle.NieceNephew.Name}");
            }

            foreach(var str in result)
            {
                Console.WriteLine(str);
            }
        }
    }
}