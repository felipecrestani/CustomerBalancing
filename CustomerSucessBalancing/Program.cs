using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerSucessBalancing.CustomerSucessBalancing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            #region Mock
            Dictionary<int,int> css = new Dictionary<int, int>();
            css.Add(1,60);
            css.Add(2,20);
            css.Add(3,95);
            css.Add(4,30);

            Dictionary<int,int> customers = new Dictionary<int, int>();
            customers.Add(1,90);
            customers.Add(2,20);
            customers.Add(3,70);
            customers.Add(4,50);
            customers.Add(5,40);
            customers.Add(6,60);

            List<int> vacant_css = new List<int>();
            vacant_css.Add(2);
            vacant_css.Add(4);
            #endregion

            var bestcss = csRush(4,6,css,customers,vacant_css);
            System.Console.WriteLine($"Best CSS {bestcss}");
        }

        public static int csRush(int n, int m, Dictionary<int,int> css, Dictionary<int,int> customers, List<int> vacant_css)
        {
            var activeCSS = GetActiveCSS(css, vacant_css);
            
            Dictionary<int,int> customerCSS = new Dictionary<int, int>();

            foreach (var customer in customers)
            {
                if(customer.Value > css.Values.Max())
                    return 0;

                var bestCSSForCustomer = css.Where(x => x.Value >= customer.Value).OrderBy(x => x.Value).FirstOrDefault();

                foreach (var item in css)
                {
                    if(item.Key == bestCSSForCustomer.Key)
                        continue;

                    if(item.Value == bestCSSForCustomer.Value)
                        return 0;
                }

                customerCSS.Add(customer.Key,bestCSSForCustomer.Key);        
            }

            var bestActiveCSS = GetBestActiveCSS(customerCSS);  

            return bestActiveCSS;
        }

        public static int GetBestActiveCSS(Dictionary<int,int> customerCSS)
        {
            //CSS X Customer
            Dictionary<int, int> countCustomerPerCSS = new Dictionary<int, int>();

            foreach (var item in customerCSS)
            {
                countCustomerPerCSS.TryGetValue(item.Value, out var currentCount); 
                countCustomerPerCSS[item.Value] = currentCount + 1;
            }

            //Check Tie
            var first = countCustomerPerCSS.OrderByDescending(x => x.Value).FirstOrDefault();
            var second = countCustomerPerCSS.OrderByDescending(x => x.Value).Skip(1).FirstOrDefault();

            if(first.Value == second.Value)
                return 0;

            return first.Key;
        }

        public static Dictionary<int,int> GetActiveCSS(Dictionary<int,int> css, List<int> vacant_css)
        {
            foreach (var vacantcss in vacant_css)            
                css.Remove(vacantcss);          

            return css;
        }
    }
}
