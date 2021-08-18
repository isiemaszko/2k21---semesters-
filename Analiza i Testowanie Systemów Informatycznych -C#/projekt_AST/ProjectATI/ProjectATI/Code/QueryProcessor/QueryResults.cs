using ProjectATI.Code.Parser;
using ProjectATI.Code.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.QueryProcessor
{
    public class QueryResults
    {
        private List<Node[]> NodeMatrix { get; set; }
        private List<QueryVariable> InsertedVariables { get; } = new List<QueryVariable>();
        private int NumberOfVariables { get; }

        public QueryResults(int numberOfVariables)
        {
            NumberOfVariables = numberOfVariables;
        }

        internal List<Node> ReturnColumn(QueryVariable queryVariable)
        {
            int index = InsertedVariables.IndexOf(queryVariable);
            List<Node> result = new List<Node>();
            foreach (Node[] row in NodeMatrix)
            {
                result.Add(row[index]);
            }
            return result;
        }

        internal bool IsEmpty()
        {
            return NodeMatrix.Count == 0;
        }

        internal void AddRelationResult(RelationResult relationResult)
        {
            bool containsLeft, containsRight;
            containsLeft = InsertedVariables.Contains(relationResult.LeftVariable);
            containsRight = InsertedVariables.Contains(relationResult.RightVariable);
            int indexLeft, indexRight;
            if (containsLeft || containsRight)
            {
                if (containsLeft & containsRight)
                {
                    indexLeft = InsertedVariables.IndexOf(relationResult.LeftVariable);
                    indexRight = InsertedVariables.IndexOf(relationResult.RightVariable);
                    for (int i = NodeMatrix.Count - 1; i >= 0; i--)
                        if (!relationResult.Contains(NodeMatrix[i][indexLeft], NodeMatrix[i][indexRight]))
                            NodeMatrix.RemoveAt(i);
                }
                else
                    InsertOneSide(relationResult, containsLeft);
            }
            else
            {
                if (InsertedVariables.Count == 0)
                    InsertAsNew(relationResult);
                else
                    InsertBothSides(relationResult);

                InsertedVariables.Add(relationResult.LeftVariable);
                InsertedVariables.Add(relationResult.RightVariable);
            }
        }

        internal void AddVariable(QueryVariable variable)
        {
            if (InsertedVariables.Contains(variable))
                return;
            List<Node[]> matrix = new List<Node[]>();
            if (NodeMatrix is null)
            {
                foreach (Node node in variable.Variables)
                {
                    Node[] row = new Node[NumberOfVariables];
                    row[0] = node;
                    matrix.Add(row);
                }
            }
            else
            {
                foreach (Node[] row in NodeMatrix)
                {
                    foreach (Node node in variable.Variables)
                    {
                        Node[] newRow = (Node[])row.Clone();
                        newRow[InsertedVariables.Count] = node;
                        matrix.Add(newRow);
                    }
                }
            }
            NodeMatrix = matrix;
            InsertedVariables.Add(variable);
        }

        internal void Validate()
        {
            InsertedVariables.ForEach(variable =>
            {
                int index = InsertedVariables.IndexOf(variable);
                for (int i = NodeMatrix.Count - 1; i >= 0; i--)
                {
                    if (!variable.Variables.Contains(NodeMatrix[i][index]) && !((NodeMatrix[i][index] is Variable) && variable.Variables.Any(item => (item as Variable).Token.Value == (NodeMatrix[i][index] as Variable).Token.Value)))
                        NodeMatrix.RemoveAt(i);
                }
            });
        }

        private void InsertBothSides(RelationResult relationResult)
        {
            List<Node[]> matrix = new List<Node[]>();

            foreach (Node[] row in NodeMatrix)
            {
                foreach (Tuple<Node, Node> result in relationResult.Result)
                {
                    Node[] newRow = (Node[])row.Clone();
                    newRow[InsertedVariables.Count] = result.Item1;
                    newRow[InsertedVariables.Count + 1] = result.Item2;
                    matrix.Add(newRow);
                }
            }
            NodeMatrix = matrix;
        }

        private void InsertAsNew(RelationResult relationResult)
        {
            List<Node[]> matrix = new List<Node[]>();

            foreach (Tuple<Node, Node> result in relationResult.Result)
            {
                Node[] row = new Node[NumberOfVariables];
                row[0] = result.Item1;
                row[1] = result.Item2;
                matrix.Add(row);
            }
            NodeMatrix = matrix;
        }

        private void InsertOneSide(RelationResult relationResult, bool containsLeft)
        {
            int indexExisting = InsertedVariables.IndexOf(containsLeft ? relationResult.LeftVariable : relationResult.RightVariable);
            List<Node[]> expandedMatrix = new List<Node[]>();
            foreach (Node[] row in NodeMatrix)
            {
                foreach (Tuple<Node, Node> result in relationResult.Result)
                {
                    if ((containsLeft ? result.Item1 : result.Item2) == row[indexExisting])
                    {
                        Node[] newRow = (Node[])row.Clone();
                        newRow[InsertedVariables.Count] = containsLeft ? result.Item2 : result.Item1;
                        expandedMatrix.Add(newRow);
                    }
                }
            }
            NodeMatrix = expandedMatrix;
        }
    }
}
