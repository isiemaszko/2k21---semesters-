using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectATI;
using ProjectATI.Code.Parser;
using ProjectATI.Code.PKB;
using ProjectATI.Code.Statements;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class CallsTests
    {
        string code;
        Pkb pkb = Pkb.GetPkbInstance();

        public CallsTests()
        {
            string filename = "source.txt";
            string[] lines = System.IO.File.ReadAllLines(filename);
            foreach (string line in lines)
            {
                code += line + "\r\n";
            }
            if(pkb.GetAST() == null)
            {
                Parser parser = new Parser(code);
                Node node = parser.Parse();
                pkb.SetAST((ProgramStatement)node);
                pkb.PopulateTables(node);
            }
        }


        [TestMethod]
        public void getCallerTest()
        {
            // Arrange
            Node nodeSecond = pkb.GetAST().GetChildList()[1];
            Node nodeThird = pkb.GetAST().GetChildList()[2];

            // Act
            Node callerSec = pkb.GetCaller(nodeSecond);
            Node callerThir = pkb.GetCaller(nodeThird);

            // Assert
            bool test;
            if(callerSec.Name == "First" && callerThir.Name == "Second")
            {
                test = true;
            }
            else
            {
                test = false;
            }
            Assert.IsTrue(test);

        }

        [TestMethod]
        public void getCallerStarTest()
        {
            // Arrange
            Node nodeSecond = pkb.GetAST().GetChildList()[1];
            Node nodeThird = pkb.GetAST().GetChildList()[2];

            // Act
            List<Node> callersSec = pkb.GetCallerStar(nodeSecond);
            List<Node> callersThir = pkb.GetCallerStar(nodeThird);

            // Act
            string node = null;

            // Assert
            bool test;
            bool c1 = callersSec.Any(i => i.Name == "First");
            bool c2 = callersThir.Any(i => i.Name == "First");
            bool c3 = callersThir.Any(i => i.Name == "Second");
            if (c1 && c2 && c3)
            {
                test = true;
            }
            else
            {
                test = false;
            }
            Assert.IsTrue(test);

        }

        [TestMethod]
        public void getCalledTest()
        {
            // Arrange
            Node nodeFirst = pkb.GetAST().GetChildList()[0];
            Node nodeSecond = pkb.GetAST().GetChildList()[1];

            // Act
            List<Node> calledF = pkb.GetCalled(nodeFirst);
            List<Node> calledS = pkb.GetCalled(nodeSecond);

            // Assert
            bool test;
            bool c1 = calledF.Any(i => i.Name == "Second");
            bool c2 = calledS.Any(i => i.Name == "Third");
            if (c1 && c2)
            {
                test = true;
            }
            else
            {
                test = false;
            }
            Assert.IsTrue(test);

        }

        [TestMethod]
        public void getCalledStarTest()
        {
            // Arrange
            Node nodeFirst = pkb.GetAST().GetChildList()[0];
            Node nodeSecond = pkb.GetAST().GetChildList()[1];

            // Act
            List<Node> calledF = pkb.GetCalledStar(nodeFirst);
            List<Node> calledS = pkb.GetCalledStar(nodeSecond);

            // Assert
            bool test;
            bool c1 = calledF.Any(i => i.Name == "Second");
            bool c2 = calledS.Any(i => i.Name == "Third");
            bool c3 = calledF.Any(i => i.Name == "Third");
            if (c1 && c2 && c3)
            {
                test = true;
            }
            else
            {
                test = false;
            }
            Assert.IsTrue(test);

        }

        [TestMethod]
        public void GetCallsPairsTest()
        {
            // Arrange
            Node nodeFirst = pkb.GetAST().GetChildList()[0];
            Node nodeSecond = pkb.GetAST().GetChildList()[1];
            Node nodeThird = pkb.GetAST().GetChildList()[2];

            // Act
            List<(Node, Node)> pairs = pkb.GetCallsPairs();

            // Assert
            bool test;
            bool c1 = pairs.Any(i => i == (nodeFirst,nodeSecond));
            bool c2 = pairs.Any(i => i == (nodeSecond, nodeThird));
            if (c1 && c2)
            {
                test = true;
            }
            else
            {
                test = false;
            }
            Assert.IsTrue(test);

        }
    }
}
