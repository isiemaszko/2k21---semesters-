using ProjectATI.Code.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.Statements
{
    public class IfStatement:Node
    {
        public Variable variable;
        private List<Node> statementListThen;
        private List<Node> statementListElse;

        public IfStatement(Variable var, List<Node> statementListThen, List<Node> statementListElse, int number)
        {
            this.variable = var;
            this.statementListThen = new List<Node>();
            this.statementListElse = new List<Node>();
            this.number = number;

            PKB.Pkb.GetPkbInstance().SetUses(this,var);

            foreach (Node node in statementListThen)
            {
                if (node is NoOp)
                    continue;
                node.Parent = this;
                PopUses(node);
                PopModifies(node);
                this.statementListThen.Add(node);
            }

            foreach (Node node in statementListElse)
            {
                if (node is NoOp)
                    continue;
                node.Parent = this;
                PopUses(node);
                PopModifies(node);
                this.statementListElse.Add(node);
            }
            PKB.Pkb.GetPkbInstance().AddStatement(this);
        }

        public IfStatement(Variable var, List<Node> statementListThen, int number)
        {
            this.variable = var;
            this.statementListThen = new List<Node>();
            this.number = number;

            foreach (Node node in statementListThen)
            {
                if (node is NoOp)
                    continue;
                node.Parent = this;
                PopUses(node);
                PopModifies(node);
                this.statementListThen.Add(node);
            }
        }

        public IfStatement(Variable var, List<Node> statementListThen, List<Node> statementListElse)
        {
            this.variable = var;
            this.statementListThen = new List<Node>();
            this.statementListElse = new List<Node>();
            

            foreach (Node node in statementListThen)
            {
                if (node is NoOp)
                    continue;
                node.Parent = this;
                PopUses(node);
                PopModifies(node);
                this.statementListThen.Add(node);
            }

            foreach (Node node in statementListElse)
            {
                if (node is NoOp)
                    continue;
                node.Parent = this;
                PopUses(node);
                PopModifies(node);
                this.statementListElse.Add(node);
            }
        }

        public IfStatement(Variable var, List<Node> statementListThen)
        {
            this.variable = var;
            this.statementListThen = new List<Node>();
            

            foreach (Node node in statementListThen)
            {
                if (node is NoOp)
                    continue;
                node.Parent = this;
                PopUses(node);
                PopModifies(node);
                this.statementListThen.Add(node);
            }
        }

        private void PopUses(Node node)
        {
            if (node is AssignStatement)
            {
                var varlst = PKB.Pkb.GetPkbInstance().GetUsing(node);
                foreach (Variable vari in varlst)
                {
                    PKB.Pkb.GetPkbInstance().SetUses(this, vari);
                }
            }
            if (node is IfStatement)
            {
                foreach (var nd in node.GetThenList())
                {
                    PopUses(nd);
                }
                foreach (var nd in node.GetElseList())
                {
                    PopUses(nd);
                }
            }
            if (node is WhileStatement)
            {
                foreach (var nd in node.GetChildList())
                {
                    PopUses(nd);
                }
            }
        }

        private void PopModifies(Node node)
        {
            if (node is AssignStatement)
            {
                PKB.Pkb.GetPkbInstance().SetModifies(this, (node as AssignStatement).left);
            }
            if (node is IfStatement)
            {
                foreach (var nd in node.GetThenList())
                {
                    PopModifies(nd);
                }
                foreach (var nd in node.GetElseList())
                {
                    PopModifies(nd);
                }
            }
            if (node is WhileStatement)
            {
                foreach (var nd in node.GetChildList())
                {
                    PopModifies(nd);
                }
            }
        }
        public override List<Node> GetChildList()
        {
            if (this.statementListElse != null)
                return this.statementListThen.Concat(this.statementListElse).ToList();
            else if (this.statementListThen != null)
                return this.statementListThen;
            else return null;
        }

        public override List<Node> GetThenList()
        {
            return this.statementListThen;
        }

        public override List<Node> GetElseList()
        {
            return this.statementListElse;
        }
    }
}
