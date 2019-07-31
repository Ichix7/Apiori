using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Winden_Apriori
{
    class Program
    {
        /*
            1-Implement the apriori algorithm with the language of your choice. 

            2- After Implementing Apriori, you will have candidate itemsets. Build a component that extracts  
                all possiel rules from the each itemsets then test the confidence value for these rules. 
                the algorithm then outputs all "interesting" rules or observations from the data. 
                minimum acceptable support and confidence values should be a fixed input fro the user.  

            3-Test the algorithm using the basket analysis data in the lecture slides.
         */
        static public List<AssociationRule> allRules = new List<AssociationRule>();
        static public CreateData newData = new CreateData();
        static public ItemsetCollection data = newData.setData();
        static public ItemsetCollection itemList = new ItemsetCollection();

        static public double support = 0;
        static public double confidence = 0;

        static void Main(string[] args)
        {
            RunningProg();
            DisplayItemSet(itemList, allRules);
        }

        static void RunningProg()
        {
            Console.WriteLine("Hello Friend");
            Console.Write("Please insert support level: ");
            String input_support = Console.ReadLine();
            support = Convert.ToDouble(input_support);
            Console.Write("Please insert confidence level: ");
            String input_confidence = Console.ReadLine();
            confidence = Convert.ToDouble(input_confidence);
            Console.WriteLine("Thank You, Calculating...");
            AprioriCalc(data, support);
            Rules(data, itemList, confidence);
        }

        static ItemsetCollection AprioriCalc(ItemsetCollection db, double supportThreshold)
        {
            Itemset I = db.GetUniqueItems();
            ItemsetCollection LitemList = new ItemsetCollection();
            ItemsetCollection PitemList = new ItemsetCollection();

            foreach (string item in I)
            {
                PitemList.Add(new Itemset() { item });
            }
            int k = 2;
            while (PitemList.Count != 0)
            {
                LitemList.Clear();
                foreach (Itemset index in PitemList)
                {
                    index.Support = db.FindSupport(index);

                    if (index.Support >= supportThreshold)
                    {
                        LitemList.Add(index);
                        itemList.Add(index);
                    }
                }
                PitemList.Clear();
                PitemList.AddRange(Bit.FindSubsets(LitemList.GetUniqueItems(), k)); //get k-item subsets
                k += 1;
            }
            return (itemList);

        }
        static List<AssociationRule> Rules(ItemsetCollection db, ItemsetCollection L, double confidenceThreshold)
        {           
            foreach (Itemset itemset in L)
            {
                ItemsetCollection subsets = Bit.FindSubsets(itemset, 0); //get all subsets
                foreach (Itemset subset in subsets)
                {
                    double confidence = (db.FindSupport(itemset) / db.FindSupport(subset)) * 100.0;
                    if (confidence >= confidenceThreshold)
                    {
                        AssociationRule rule = new AssociationRule();
                        rule.X.AddRange(subset);
                        rule.Y.AddRange(itemset.Remove(subset));
                        rule.Support = db.FindSupport(itemset);
                        rule.Confidence = confidence;
                        if (rule.X.Count > 0 && rule.Y.Count > 0)
                        {
                            allRules.Add(rule);
                        }
                    }
                }
            }
            return (allRules);
        }

        static void DisplayItemSet(ItemsetCollection itemlist, List<AssociationRule> rule)
        {
            
            Console.WriteLine("Below is the Following: ItemSet using support of " + support + "%");
            foreach (Itemset c in itemlist)
            {
                Console.Write(c.ToString() + " ,");
            }
            
            Console.WriteLine("\nBelow is the Following: ItemSet using confidence of " + confidence + "%");

            foreach (AssociationRule c in rule)
            {
                Console.Write(c.ToString() + " ,");
            }

            Console.WriteLine("\n<Press Enter>");
            Console.ReadLine();
        }
    }
}
