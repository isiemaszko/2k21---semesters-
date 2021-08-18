using ProjectATI.Code.Parser;
using ProjectATI.Code.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ProjectATI.Code.Interfaces;

namespace ProjectATI.Code.PKB
{
    public class Pkb
    {
        private static Pkb Instance = null;

        private Follows followLists;
        private Parent parentLists;
        private Modifies modifiesLists;
        private Calls callsLists;
        private Uses usesList;
        private Next nextList;
        private List<Node> callsListsTmp;
        private VarTable varTableList;
        private List<Node> procedureList;
        public List<Node> statementsList { get; private set; }
        private ProgramStatement AST;
        private List<int> NumericValues;

        private Pkb()
        {
            this.followLists = new Follows();
            this.parentLists = new Parent();
            this.varTableList = new VarTable();
            this.statementsList = new List<Node>();
            this.usesList = new Uses();
            this.modifiesLists = new Modifies();
            this.callsLists = new Calls();
            this.callsListsTmp = new List<Node>();
            this.nextList = new Next();
            this.procedureList = new List<Node>();
            this.NumericValues = new List<int>();
        }

        public ProgramStatement GetAST()
        {
            return AST;
        }

        public void SetAST(ProgramStatement AST)
        {
            this.AST = AST;
        }

        public void AddStatement(Node n)
        {
            this.statementsList.Add(n);
        }

        public static Pkb GetPkbInstance()
        {
            if (Instance == null)
                Instance = new Pkb();

            return Instance;
        }

        public void AddCallsListsTmp(Node node)
        {
            this.callsListsTmp.Add(node);
        }

        public Node SetFollows(Node follower, Node followee)
        {
            var node = followLists.SetFollows(follower, followee);
            if (node != null)
                return node;

            return null;
        }

        public Node GetFollows(Node follower)
        {
            var node = followLists.GetFollows(follower);
            if (node != null)
                return node;

            return null;
        }

        public Node GetFollowedBy(Node followee)
        {
            var node = followLists.GetFollowedBy(followee);
            if (node != null)
                return node;

            return null;
        }

        public List<Node> GetFollowsStar(Node follower)
        {
            var nodes = followLists.GetFollowsStar(follower);
            if (nodes != null)
                return nodes;

            return null;
        }

        public List<Node> GetFollowedByStar(Node followee)
        {
            var nodes = followLists.GetFollowedByStar(followee);
            if (nodes != null)
                return nodes;

            return null;
        }

        public Node SetParent(Node parent, Node children)
        {
            var node = parentLists.SetParent(parent, children);
            if (node != null)
                return node;

            return null;
        }

        public Node GetParent(Node statement)
        {
            var node = parentLists.GetParent(statement);
            if (node != null)
                return node;

            return null;
        }

        public List<Node> GetChildren(Node statement)
        {
            var nodes = parentLists.GetChildren(statement);
            if (nodes != null)
                return nodes;

            return null;
        }

        public List<Node> GetParentStar(Node statement)
        {
            var nodes = parentLists.GetParentStar(statement);
            if (nodes != null)
                return nodes;

            return null;
        }

        public List<Node> GetChildrenStar(Node statement)
        {
            var nodes = parentLists.GetChildrenStar(statement);
            if (nodes != null)
                return nodes;

            return null;
        }

        public Node SetCalls(Node caller, string calledName)
        {
            Node proc1 = null;
            Node proc2 = null;

            foreach (var stmt in AST.GetChildList())
            {
                if (stmt.Name == calledName)
                {
                    proc2 = stmt;
                }
            }

            while (true)
            {
                if(caller.Parent.GetType() == typeof(ProcedureStatement))
                {
                    proc1 = caller.Parent;
                    break;
                }
                else
                {
                    caller = caller.Parent;
                }
            }
            var node = callsLists.SetCalls(proc1, proc2);
            if (node != null)
            {
                return node;
            }
            return null;
        }

        public List<(Node, Node)> GetCallsPairs()
        {
            return callsLists.GetCallsPairs();
        }

        public Node GetCaller(Node statement)
        {
            var node = callsLists.GetCaller(statement);
            if (node != null)
                return node;

            return null;
        }

        public List<Node> GetCallers(Node statement)
        {
            var node = callsLists.GetCallers(statement);
            if (node != null)
                return node;

            return null;
        }

        public List<Node> GetCalled(Node statement)
        {
            var nodes = callsLists.GetCalled(statement);
            if (nodes != null)
                return nodes;

            return null;
        }

        public List<Node> GetCallerStar(Node statement)
        {
            var nodes = callsLists.GetCallerStar(statement);
            if (nodes != null)
                return nodes;

            return null;
        }

        public List<Node> GetCalledStar(Node statement)
        {
            var nodes = callsLists.GetCalledStar(statement);
            if (nodes != null)
                return nodes;

            return null;
        }

        public int InsertVar(Variable var)
        {
            var idx = varTableList.InsertVar(var);
            return idx;
        }

        public string GetVarName(int varIndex)
        {
            var name = varTableList.GetVarNameByIndex(varIndex);
            return name;
        }

        public int GetVarIndex(string varName)
        {
            var idx = varTableList.GetVarIndexByName(varName);
            return idx;
        }

        public int GetSize()
        {
            var idx = varTableList.GetSize();
            return idx;
        }

        public Node SetUses(Node statement, Variable variable)
        {
            var node = this.usesList.SetUses(statement, variable);
            if (node != null)
                return node;

            return null;
        }

        public List<Node> GetUsing(Node statement)
        {
            var nodes = this.usesList.GetUsing(statement);
            if (nodes.Any())
                return nodes;

            return new List<Node>();
        }
        public List<Node> GetUsedBy(Node variable)
        {
            var nodes = this.usesList.GetUsedBy(variable);
            if (nodes.Any())
                return nodes;

            return new List<Node>();
        }

        public Node SetModifies(Node statement, Variable variable)
        {
            var node = this.modifiesLists.SetModifies(statement, variable);
            if (node != null)
                return node;

            return null;
        }

        public List<Node> GetModifies(Node statement)
        {
            var nodes = this.modifiesLists.GetModifies(statement);
            if (nodes.Any())
                return nodes;

            return new List<Node>();
        }
        public List<Node> GetModifiesBy(Node variable)
        {
            var nodes = this.modifiesLists.GetModifiesBy(variable);
            if (nodes.Any())
                return nodes;

            return new List<Node>();
        }

        public Node SetNext(Node first, Node second)
        {
            var node = this.nextList.SetNext(first, second);
            if (node != null)
                return node;

            return null;
        }

        public List<Node> GetPrev(Node second)
        {
            var nodes = this.nextList.GetPrev(second);
            if (nodes.Any())
                return nodes;

            return new List<Node>();
        }

        public List<Node> GetNext(Node first)
        {
            var nodes = this.nextList.GetNext(first);
            if (nodes.Any())
                return nodes;

            return new List<Node>();
        }
        //public List<Variable> GetModified(Node statement)
        //{
        //    var variables = modifiesStatementLists.GetModified(statement);
        //    if (variables.Any())
        //        return variables;

        //    return null;
        //}

        //public List<Node> GetStatementsModify(Variable variable)
        //{
        //    var nodes = modifiesStatementLists.GetStatementsModify(variable);
        //    if (nodes.Any())
        //        return nodes;

        //    return null;
        //}

        //public bool IsModified(Variable variable, Node statement)
        //{
        //    return modifiesStatementLists.IsModified(variable, statement); 
        //}

        public void PopulateTables(Node n)
        {
            PopulateParents(n);
            PopulateFollows(n);
            PopulateCalls();
            PopulateProcUses(n);
            PopulateProcModifies(n);
            PopulateNext(n);

        }

        public void PopulateNext(Node n)
        {
            List<Node> tmp;
            foreach (var proc in n.GetChildList())
            {
                tmp = proc.GetChildList();
                for (int i = 0; i < tmp.Count-1; i++)
                {
                    if (tmp[i] is IfStatement)
                    {
                        (Node, Node) tupleIf = PopulateIfNext((IfStatement)tmp[i]);
                        SetNext(tupleIf.Item1, tmp[i + 1]);
                        SetNext(tupleIf.Item2, tmp[i + 1]);
                        continue;
                    }
                    else if (tmp[i] is WhileStatement)
                        PopulateWhileNext(tmp[i]);
                    SetNext(tmp[i], tmp[i + 1]);
                }
            }

        }

        public void PopulateWhileNext(Node whileNode)
        {
            List<Node> tmp = whileNode.GetChildList();
            SetNext(whileNode, tmp.First());
            for (int i = 0; i < tmp.Count - 1; i++)
            {
                if (tmp[i] is IfStatement)
                {
                    (Node, Node) tupleIf = PopulateIfNext((IfStatement)tmp[i]);
                    SetNext(tupleIf.Item1, tmp[i + 1]);
                    SetNext(tupleIf.Item2, tmp[i + 1]);
                    continue;
                }
                else if (tmp[i] is WhileStatement)
                    PopulateWhileNext(tmp[i]);
                SetNext(tmp[i], tmp[i + 1]);
            }
            SetNext(tmp.Last(), whileNode);
        }

        public (Node, Node) PopulateIfNext(IfStatement ifNode)
        {
            List<Node> ifList = ifNode.GetThenList();
            List<Node> elseList = ifNode.GetElseList();
            SetNext(ifNode, ifList.First());
            SetNext(ifNode, elseList.First());

            for (int i = 0; i < ifList.Count - 1; i++)
            {
                if (ifList[i] is IfStatement)
                {
                    (Node, Node) tupleIf = PopulateIfNext((IfStatement)ifList[i]);
                    SetNext(tupleIf.Item1, ifList[i + 1]);
                    SetNext(tupleIf.Item2, ifList[i + 1]);
                    continue;
                }
                else if (ifList[i] is WhileStatement)
                    PopulateWhileNext(ifList[i]);
                SetNext(ifList[i], ifList[i + 1]);
            }

            for (int i = 0; i < elseList.Count - 1; i++)
            {
                if (elseList[i] is IfStatement)
                {
                    (Node, Node) tupleIf = PopulateIfNext((IfStatement)elseList[i]);
                    SetNext(tupleIf.Item1, elseList[i + 1]);
                    SetNext(tupleIf.Item2, elseList[i + 1]);
                    continue;
                }
                else if (elseList[i] is WhileStatement)
                    PopulateWhileNext(elseList[i]);
                SetNext(elseList[i], elseList[i + 1]);
            }
            return (ifList.Last(), elseList.Last());
        }
        public void PopulateCalls()
        {
            foreach(var n in callsListsTmp)
            {
                this.SetCalls(n, n.Name);
            }
        }

        public void PopulateProcModifies(Node n)
        {
            int max = modifiesLists.modifiesList.Count;
            for (int i = 0; i < max; i++)
            {
                Node parent = modifiesLists.modifiesList[i].Parent;

                while (true)
                {
                    if (parent.GetType() == typeof(ProcedureStatement))
                    {
                        break;
                    }
                    parent = parent.Parent;
                }

                SetModifies(parent, (Variable)modifiesLists.modifiesByList[i][0]);
            }

            while (true)
            {
                List<Node> oldModifiesList = modifiesLists.modifiesList.GetRange(0, modifiesLists.modifiesList.Count);
                List<List<Node>> oldModifiesByList = new List<List<Node>>();

                foreach (List<Node> i in modifiesLists.modifiesByList)
                {
                    oldModifiesByList.Add(i.GetRange(0, i.Count));
                }

                foreach (var proc in n.GetChildList())
                {
                    for (int i = 0; i < callsLists.callerList.Count; i++)
                    {
                        if (callsLists.callerList[i] == proc)
                        {
                            for (int j = 0; j < callsLists.calledList[i].Count; j++)
                            {
                                var list = modifiesLists.GetModifies(callsLists.calledList[i][j]);
                                foreach (Variable v in list)
                                {
                                    SetModifies(proc, v);
                                }
                            }
                        }
                    }
                }

                bool isEqual1 = Enumerable.SequenceEqual(oldModifiesList, this.modifiesLists.modifiesList);
                bool isEqual2 = false;
                for (int i = 0; i < oldModifiesByList.Count; i++)
                {
                    if (oldModifiesByList[i].Count == modifiesLists.modifiesByList[i].Count)
                    {
                        isEqual2 = true;
                    }
                    else
                    {
                        isEqual2 = false;
                        break;
                    }
                }

                if (isEqual1 && isEqual2)
                {
                    break;
                }

            }

            foreach (var stmt in statementsList)
            {
                if (stmt is CallsStatement)
                {
                    String name = stmt.Name;
                    for (int i = 0; i < modifiesLists.modifiesList.Count; i++)
                    {
                        if (modifiesLists.modifiesList[i].Name == name)
                        {
                            foreach (var ub in modifiesLists.modifiesByList[i])
                            {
                                SetModifies(stmt, (Variable)ub);
                            }
                        }
                    }
                }
            }

            foreach (var stmt in modifiesLists.modifiesList)
            {
                if( stmt is WhileStatement)
                {
                    foreach (var stmt2 in stmt.GetChildList())
                    {
                        if (stmt2 is CallsStatement)
                        {
                            foreach (var stmt3 in modifiesLists.modifiesList)
                            {
                                if(stmt2 == stmt3)
                                {
                                    int id = modifiesLists.modifiesList.IndexOf(stmt3);
                                    foreach(var v in modifiesLists.modifiesByList[id])
                                    {
                                        SetModifies(stmt, (Variable)v);
                                    }
                                }
                            }
                        }
                    }
                }
                if (stmt is IfStatement)
                {
                    foreach (var stmt2 in stmt.GetThenList())
                    {
                        if (stmt2 is CallsStatement)
                        {
                            foreach (var stmt3 in modifiesLists.modifiesList)
                            {
                                if (stmt2 == stmt3)
                                {
                                    int id = modifiesLists.modifiesList.IndexOf(stmt3);
                                    foreach (var v in modifiesLists.modifiesByList[id])
                                    {
                                        SetModifies(stmt, (Variable)v);
                                    }
                                }
                            }
                        }
                    }
                    foreach (var stmt2 in stmt.GetElseList())
                    {
                        if (stmt2 is CallsStatement)
                        {
                            foreach (var stmt3 in modifiesLists.modifiesList)
                            {
                                if (stmt2 == stmt3)
                                {
                                    int id = modifiesLists.modifiesList.IndexOf(stmt3);
                                    foreach (var v in modifiesLists.modifiesByList[id])
                                    {
                                        SetModifies(stmt, (Variable)v);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void PopulateProcUses(Node n)
        {
            int max = usesList.usesList.Count;
            for (int i = 0; i < max; i++)
            {
                Node parent = usesList.usesList[i].Parent;

                while (true)
                {
                    if (parent.GetType() == typeof(ProcedureStatement))
                    {
                        break;
                    }
                    parent = parent.Parent;
                }

                foreach(Variable v in usesList.usedByList[i])
                {
                    SetUses(parent, v);
                }
            }

            while (true)
            {
                List<Node> oldUsesList = usesList.usesList.GetRange(0, usesList.usesList.Count);
                List<List<Node>> oldUsedByList = new List<List<Node>>();

                foreach (List<Node> i in usesList.usedByList)
                {
                    oldUsedByList.Add(i.GetRange(0, i.Count));
                }

                foreach (var proc in n.GetChildList())
                {
                    for (int i = 0; i < callsLists.callerList.Count; i++)
                    {
                        if (callsLists.callerList[i] == proc)
                        {
                            for (int j = 0; j < callsLists.calledList[i].Count; j++)
                            {
                                var list = usesList.GetUsing(callsLists.calledList[i][j]);
                                foreach (Variable v in list)
                                {
                                    SetUses(proc, v);
                                }
                            }
                        }
                    }
                }

                bool isEqual1 = Enumerable.SequenceEqual(oldUsesList, this.usesList.usesList);
                bool isEqual2 = false;
                for (int i = 0; i < oldUsedByList.Count; i++)
                {
                    if (oldUsedByList[i].Count == usesList.usedByList[i].Count)
                    {
                        isEqual2 = true;
                    }
                    else
                    {
                        isEqual2 = false;
                        break;
                    }
                }

                if (isEqual1 && isEqual2)
                {
                    break;
                }
            }
            //while (true)
            //{
            //    Uses oldUsesList = new Uses (
            //        usesList.usesList,
            //        usesList.usedByList
            //    );

            //    foreach (Node proc in n.GetChildList())
            //    {
            //        foreach (Node child in proc.GetChildList())
            //        {
            //            foreach (Variable t in this.usesList.GetUsing(child))
            //            {
            //                this.SetUses(proc, t);
            //            }
            //        }
            //    }
            //    bool isEqual1 = Enumerable.SequenceEqual(oldUsesList.usesList, this.usesList.usesList);
            //    bool isEqual2 = Enumerable.SequenceEqual(oldUsesList.usedByList, this.usesList.usedByList);
            //    if (isEqual1 && isEqual2)
            //    {
            //        break;
            //    }
            //}
            foreach (var stmt in statementsList)
            {
                if(stmt is CallsStatement)
                {
                    String name = stmt.Name;
                    for(int i=0; i< usesList.usesList.Count;i++)
                    {
                        if(usesList.usesList[i].Name == name)
                        {
                            foreach (var ub in usesList.usedByList[i])
                            {
                                SetUses(stmt, (Variable)ub);
                            }
                        }
                    }
                }
            }

            foreach (var stmt in usesList.usesList)
            {
                if (stmt is WhileStatement)
                {
                    foreach (var stmt2 in stmt.GetChildList())
                    {
                        if (stmt2 is CallsStatement)
                        {
                            foreach (var stmt3 in usesList.usesList)
                            {
                                if (stmt2 == stmt3)
                                {
                                    int id = usesList.usesList.IndexOf(stmt3);
                                    foreach (var v in usesList.usedByList[id])
                                    {
                                        SetUses(stmt, (Variable)v);
                                    }
                                }
                            }
                        }
                    }
                }
                if (stmt is IfStatement)
                {
                    foreach (var stmt2 in stmt.GetThenList())
                    {
                        if (stmt2 is CallsStatement)
                        {
                            foreach (var stmt3 in usesList.usesList)
                            {
                                if (stmt2 == stmt3)
                                {
                                    int id = usesList.usesList.IndexOf(stmt3);
                                    foreach (var v in usesList.usedByList[id])
                                    {
                                        SetUses(stmt, (Variable)v);
                                    }
                                }
                            }
                        }
                    }
                    foreach (var stmt2 in stmt.GetElseList())
                    {
                        if (stmt2 is CallsStatement)
                        {
                            foreach (var stmt3 in usesList.usesList)
                            {
                                if (stmt2 == stmt3)
                                {
                                    int id = usesList.usesList.IndexOf(stmt3);
                                    foreach (var v in usesList.usedByList[id])
                                    {
                                        SetUses(stmt, (Variable)v);
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        public void PopulateParents(Node n)
        {
            if (Helpers.CheckIfHaveChildList(n))
            {
                foreach (var stmt in n.GetChildList())
                {
                    this.SetParent(n, stmt);
                    PopulateParents(stmt);
                }
            }
            else if (n is StatementList)
            {
                foreach (var stmt in n.GetChildList())
                    PopulateParents(stmt);
            }
            else return;
        }

        public void PopulateFollows(Node n)
        {
            if (n is ProgramStatement)
            {
                foreach (var proc in n.GetChildList())
                {
                    for (int i=0; i<proc.GetChildList().Count-1; i++)
                    {
                        this.SetFollows(proc.GetChildList()[i], proc.GetChildList()[i + 1]);
                        if (Helpers.CheckIfHaveChildList(proc.GetChildList()[i]))
                        {
                            PopulateFollows(proc.GetChildList()[i]);
                        }
                    }
                }
            }
            else if(n is IfStatement)
            {
                for (int i = 0; i < n.GetThenList().Count - 1; i++)
                {
                    this.SetFollows(n.GetChildList()[i], n.GetChildList()[i + 1]);
                    if (Helpers.CheckIfHaveChildList(n.GetChildList()[i]))
                    {
                        PopulateFollows(n.GetChildList()[i]);
                    }
                }
                for (int i = 0; i < n.GetElseList().Count - 1; i++)
                {
                    this.SetFollows(n.GetElseList()[i], n.GetElseList()[i + 1]);
                    if (Helpers.CheckIfHaveChildList(n.GetElseList()[i]))
                    {
                        PopulateFollows(n.GetElseList()[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < n.GetChildList().Count - 1; i++)
                {
                    this.SetFollows(n.GetChildList()[i], n.GetChildList()[i + 1]);
                    if (Helpers.CheckIfHaveChildList(n.GetChildList()[i]))
                    {
                        PopulateFollows(n.GetChildList()[i]);
                    }
                }
            }
            return;
        }

        public void SetProcedureList()
        {
            int stop = 1;
            this.procedureList = this.AST.statementList;
        }

        public List<string> getProcedureNames()
        {
            List<string> procNames = new List<string>();

            foreach(var proc in this.procedureList)
            {
                procNames.Add(proc.Name.ToLower());
            }
            return procNames;
        }
        public List<Variable> GetAllVariables()
        {
            return this.varTableList.GetAll();
        }

        public void SetNumericValue(int val)
        {
            this.NumericValues.Add(val);
        }

        public List<int> GetAllNumericValues()
        {
            return this.NumericValues;
        }
    }
}
