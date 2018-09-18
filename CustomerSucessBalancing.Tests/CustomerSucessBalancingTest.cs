using System.Collections.Generic;
using CustomerSucessBalancing.CustomerSucessBalancing;
using NUnit.Framework;

namespace CustomerSucessBalancing.Tests
{
    [TestFixture]
    public class Tests
    {
        #region Mocks
        public Dictionary<int,int> CSS()
        {
            Dictionary<int,int> css = new Dictionary<int, int>();
            css.Add(1,60);
            css.Add(2,20);
            css.Add(3,90);
            css.Add(4,30);
            return css;
        }
        public List<int> Vacant_CSS()
        {
            List<int> vacant_css = new List<int>();
            vacant_css.Add(2);
            vacant_css.Add(4);
            return vacant_css;
        }
        public Dictionary<int,int> Customers()
        {
            Dictionary<int,int> customers = new Dictionary<int, int>();
            customers.Add(1,90);
            customers.Add(2,20);
            customers.Add(3,70);
            customers.Add(4,50);
            customers.Add(5,40);
            customers.Add(6,60);
            return customers;
        }
        public Dictionary<int,int> CustomerCSS()
        {
            Dictionary<int,int> customerCSS = new Dictionary<int, int>();
            customerCSS.Add(1,1);
            customerCSS.Add(2,1);
            customerCSS.Add(3,1);
            customerCSS.Add(4,2);
            return customerCSS;
        }
        # endregion

        [Test]
        public void Should_Return_Active_CSS()
        {
            var activeCSS = Program.GetActiveCSS(CSS(), Vacant_CSS());
            Assert.That(activeCSS, Has.Count.EqualTo(2));
        }

        [Test]
        public void Should_Return_Active_NoVacants()
        {   
            var noVacants = Vacant_CSS(); 
            noVacants.Clear();        
            var activeCSS = Program.GetActiveCSS(CSS(), noVacants);
            Assert.That(activeCSS, Has.Count.EqualTo(4));
        }

        [Test]
        public void Should_Return_MaxActiveCSS()
        {
            var activeCSS = Program.csRush(4,6,CSS(),Customers(),Vacant_CSS());
            Assert.AreEqual(1,activeCSS);
        }

        [Test]
        public void Should_Return_MaxActiveCSS2()
        {  
            var customers = Customers();
            customers.Add(7,90);
            customers.Add(8,90);
            customers.Add(9,90);
            customers.Add(10,90);
            var activeCSS = Program.csRush(4,6,CSS(),customers,Vacant_CSS());
            Assert.AreEqual(3,activeCSS);
        }

        [Test]
        public void Should_Return_Zero_When_Unattended_Customer ()
        {
            var customers = Customers();
            customers.Add(7,99);
            var activeCSS = Program.csRush(4,6,CSS(),customers,Vacant_CSS());
            Assert.AreEqual(0,activeCSS);
        }

        [Test]
        public void Shoul_Return_Best_CSSA_ctive()
        {
            var bestCssActive = Program.GetBestActiveCSS(CustomerCSS());
            Assert.AreEqual(1,bestCssActive);
        }

        [Test]
        public void Shoul_Return_Zero_When_CSS_Tie()
        {
            Dictionary<int,int> customers = new Dictionary<int, int>();
            customers.Add(1,90);
            customers.Add(2,20);

            Dictionary<int,int> css = new Dictionary<int, int>();
            css.Add(1,90);
            css.Add(3,20);

            var activeCSS = Program.csRush(4,6,css,customers,Vacant_CSS());
            Assert.AreEqual(0,activeCSS);
        }
    }
}
