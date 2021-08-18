using ProjectATI.Code.Enums;
using ProjectATI.Code.Parser;
using ProjectATI.Code.PKB;
using ProjectATI.Code.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.QueryProcessor
{
    public class QueryEvaluator
    {
        public QueryResults QueryResults { get; }
        public QueryResultProjector ResultProjector
        {
            get
            {
                if (Evaluated)
                {
                    return new QueryResultProjector(Query.EvaluatedVariable, QueryResults);
                }
                throw new InvalidOperationException("Query not evaluated yet");
            }
        }
        private Query Query { get; set; }
        private Pkb Pkb { get; } = Pkb.GetPkbInstance();
        private bool Evaluated { get; set; } = false;

        public QueryEvaluator(Query query)
        {
            Query = query;
            QueryResults = new QueryResults(Query.AllVariables.Count);
        }

        public void Evaluate()
        {
            LoadCandidates();

            HandleWithBlocks();

            HandlePatterns();

            HandleSuchThatBlocks();

            FillAndValidateResult();
        }

        private void FillAndValidateResult()
        {
            if (Evaluated)
                return;
            Query.AllVariables.ForEach(variable =>
            {
                QueryResults.AddVariable(variable);
            });
            QueryResults.Validate();
            Evaluated = true;
        }

        private void HandlePatterns()
        {
            List<PatternBlock> patternBlocks = GetBlocks<PatternBlock>();
            patternBlocks.ForEach(pattern =>
            {
                QueryVariable queryVariable = Query.AllVariables.FindLast(variable =>
                {
                    return variable.Type.Value == pattern.synonym.Type.Value && variable.Type.Type == pattern.synonym.Type.Type;
                });
                switch (queryVariable.Type.Type)
                {
                    case TokenType.Assignment:
                        queryVariable.Variables = queryVariable.Variables.FindAll(variable =>
                        {
                            AssignStatement assignment = variable as AssignStatement;
                            if (pattern.secondParam.first.Type.Type == TokenType._Underscore && pattern.secondParam.second is null)
                            {
                                if (pattern.firstParam.Type.Type == TokenType._Underscore)
                                    return true;
                                return assignment.left.Token.Value == pattern.firstParam.Type.Value;
                            }
                            bool patternValid = PatternMatches(assignment.RPN, pattern.secondParam.second, pattern.secondParam.first);
                            if (!patternValid || pattern.firstParam.Type.Type == TokenType._Underscore)
                                return patternValid;
                            return assignment.left.Token.Value == assignment.left.Token.Value;
                        });
                        break;

                    case TokenType.If:
                        queryVariable.Variables = queryVariable.Variables.FindAll(variable =>
                        {
                            IfStatement ifStatement = variable as IfStatement;
                            if (pattern.firstParam.Type.Type == TokenType._Underscore)
                                return true;
                            return ifStatement.variable.Token.Value == pattern.firstParam.Type.Value;
                        });
                        break;

                    case TokenType.While:
                        queryVariable.Variables = queryVariable.Variables.FindAll(variable =>
                        {
                            WhileStatement whileStatement = variable as WhileStatement;
                            if (pattern.firstParam.Type.Type == TokenType._Underscore)
                                return true;
                            return whileStatement.variable.Token.Value == pattern.firstParam.Type.Value;
                        });
                        break;

                }
            });
        }

        private bool PatternMatches(string originalRpn, string searchedRpn, QueryVariable dash)
        {
            if (dash is null)
                return originalRpn == searchedRpn;
            else
                return originalRpn.Contains(searchedRpn);
        }

        private void LoadCandidates()
        {
            Query.AllVariables.ForEach(variable =>
            {
                if (variable.Type.Type != TokenType.Constant)
                {
                    Node node = null;

                    switch (variable.Type.Type)
                    {
                        case TokenType.varName:
                            node = Pkb.GetAllVariables().FindLast(var => var.Token.Value == variable.Type.Value);
                            break;
                        case TokenType.Number:
                            node = Pkb.statementsList.FindLast(statement => statement.number.ToString() == variable.Type.Value);
                            break;
                        case TokenType.procName:
                            node = Pkb.statementsList.FindLast(procedure => procedure is ProcedureStatement && procedure.Name.ToLower() == variable.Type.Value.ToLower());
                            break;
                        default:
                            break;
                    }

                    if (node == null)
                    {
                        switch(variable.Type.Type)
                        {
                            case TokenType.Variable:
                                variable.Variables = Pkb.GetAllVariables().Cast<Node>().ToList();
                                break;
                            default:
                                variable.Variables = FilterNodes(Pkb.statementsList, variable.Type.Type);
                                break;
                        }
                        
                    }
                    else
                    {
                        variable.Variables = new List<Node>() { node };
                    }
                }
                else
                {
                    variable.Variables = Pkb.GetAllNumericValues()
                        .Distinct()
                        .Select(value =>
                        {
                            Node node = new Node();
                            node.Name = value.ToString();
                            return node;
                        }).ToList();
                }
            });
        }

        private List<Node> FilterNodes(List<Node> nodes, TokenType token)
        {
            if (nodes is null)
                return new List<Node>();
            List<Node> result = new List<Node>();
            switch (token)
            {
                case TokenType.Assignment:
                    result = nodes.FindAll(statement => statement is AssignStatement);
                    break;

                case TokenType.If:
                    result = nodes.FindAll(statement => statement is IfStatement);
                    break;

                case TokenType.Statement:
                    result = nodes.FindAll(statement => Helpers.CheckIfStatement(statement));
                    break;

                case TokenType.While:
                    result = nodes.FindAll(statement => statement is WhileStatement);
                    break;

                case TokenType.Procedure:
                case TokenType.procName:
                    result = nodes.FindAll(statement => statement is ProcedureStatement);
                    break;

                case TokenType.Prog_line:
                    result = nodes.FindAll(statement => statement.number != 0);
                    break;

                case TokenType.Any:
                case TokenType.Number:
                    result = nodes;
                    break;
                case TokenType.varName:
                case TokenType.Variable:
                    result = nodes.FindAll(statement => statement is Variable);
                    break;
                default:
                    result = new List<Node>();
                    break;
            }
            return result;
        }

        private void HandleSuchThatBlocks()
        {
            List<SuchThatAndBlock> suchThatAndBlocks = GetBlocks<SuchThatAndBlock>();
            suchThatAndBlocks.ForEach(block =>
            {
                if (Evaluated)
                    return;
                RelationResult relationResult = EvaluateBlock(block);
                RemoveFromCandidateList(relationResult);
                QueryResults.AddRelationResult(relationResult);
                if (QueryResults.IsEmpty())
                    Evaluated = true;
            });
        }

        private void RemoveFromCandidateList(RelationResult relationResult)
        {
            List<Node> leftResult = relationResult.Result.Select(element => element.Item1).ToList();
            relationResult.LeftVariable.Variables = IntersectVariables(relationResult.LeftVariable.Variables, leftResult);

            List<Node> rightResult = relationResult.Result.Select(element => element.Item2).ToList();
            relationResult.RightVariable.Variables = IntersectVariables(relationResult.RightVariable.Variables, rightResult);

            //for(int i = relationResult.Result.Count - 1; i >= 0; i--)
            //{
            //    if (!relationResult.LeftVariable.Variables.Contains(relationResult.Result[i].Item1) || !relationResult.RightVariable.Variables.Contains(relationResult.Result[i].Item2))
            //        relationResult.Result.RemoveAt(i);

            //}
        }

        private RelationResult EvaluateBlock(SuchThatAndBlock block)
        {
            List<Tuple<Node, Node>> result = new List<Tuple<Node, Node>>();
            bool fromLeft = false;
            List<Node> variables = null;
            TokenType searchedType;
            if (block.relation.firstParam.Variables.Count <= block.relation.secondParam.Variables.Count)
            {
                fromLeft = true;
                variables = block.relation.firstParam.Variables;
                searchedType = block.relation.secondParam.Type.Type;
            }
            else
            {
                variables = block.relation.secondParam.Variables;
                searchedType = block.relation.firstParam.Type.Type;
            }

            variables.ForEach(statement =>
            {
                List<Node> nodes = EvaluateRelation(block.relation, statement, fromLeft);
                FilterNodes(nodes, searchedType).ForEach(node =>
                {
                    if (node != null)
                    {
                        if (fromLeft)
                            result.Add(new Tuple<Node, Node>(statement, node));
                        else
                            result.Add(new Tuple<Node, Node>(node, statement));
                    }
                });
            });

            return new RelationResult(block.relation.firstParam, block.relation.secondParam, result);
        }

        private List<Node> EvaluateRelation(RelationToken relation, Node statement, bool leftGiven)
        {
            switch (relation.Type)
            {
                case RelationEnum.Parent:
                    if (leftGiven)
                        return Pkb.GetChildren(statement);
                    return new List<Node>() { Pkb.GetParent(statement) };

                case RelationEnum.ParentStar:
                    if (leftGiven)
                        return Pkb.GetChildrenStar(statement);
                    return Pkb.GetParentStar(statement);

                case RelationEnum.Follows:
                    if (leftGiven)
                        return new List<Node>() { Pkb.GetFollows(statement) };
                    return new List<Node>() { Pkb.GetFollowedBy(statement) };

                case RelationEnum.FollowsStar:
                    if (leftGiven)
                        return Pkb.GetFollowsStar(statement);
                    return Pkb.GetFollowedByStar(statement);

                case RelationEnum.Modifies:
                    if (leftGiven)
                    {
                        if (statement is ProcedureStatement procedureStatement)
                            return Pkb.GetModifies(procedureStatement).Cast<Node>().ToList();
                        else if (Helpers.CheckIfStatement(statement))
                            return Pkb.GetModifies(statement).Cast<Node>().ToList();
                        else
                            return new List<Node>();
                    }
                    return Pkb.GetModifiesBy(statement as Variable).Cast<Node>().ToList();

                case RelationEnum.Uses:
                    if (leftGiven)
                        return Pkb.GetUsing(statement);
                    return Pkb.GetUsedBy(statement);

                case RelationEnum.Calls:
                    if (leftGiven)
                        return Pkb.GetCalled(statement);
                    return Pkb.GetCallers(statement);

                case RelationEnum.CallsStar:
                    if (leftGiven)
                        return Pkb.GetCalledStar(statement);
                    return Pkb.GetCallerStar(statement);

                default:
                    return new List<Node>();
            }
        }

        private void HandleWithBlocks()
        {
            List<WithBlock> withBlocks = GetBlocks<WithBlock>();
            withBlocks.ForEach(block =>
            {
                if (block.parameter is null)
                {
                    switch (block.relation.Type)
                    {
                        case AtributeNameEnum.StmtLine:
                            block.relationParam.Variable.ConnectedLine.Add(block.relation.Variable);
                            break;

                        case AtributeNameEnum.varName:
                            block.relationParam.Variable.ConnectedName.Add(block.relation.Variable);
                            break;

                        case AtributeNameEnum.value:
                            block.relationParam.Variable.ConnectedValue.Add(block.relation.Variable);
                            break;

                        case AtributeNameEnum.procName:
                            block.relationParam.Variable.ConnectedName.Add(block.relation.Variable);
                            break;
                    }

                    block.relationParam.Variable.NotifyOthers();

                    switch (block.relationParam.Type)
                    {
                        case AtributeNameEnum.StmtLine:
                            block.relation.Variable.ConnectedLine.Add(block.relationParam.Variable);
                            break;

                        case AtributeNameEnum.varName:
                            block.relation.Variable.ConnectedName.Add(block.relationParam.Variable);
                            break;

                        case AtributeNameEnum.value:
                            block.relation.Variable.ConnectedValue.Add(block.relationParam.Variable);
                            break;

                        case AtributeNameEnum.procName:
                            block.relation.Variable.ConnectedName.Add(block.relationParam.Variable);
                            break;
                    }

                    block.relation.Variable.NotifyOthers();
                }
                else
                {
                    QueryVariable queryVariable = Query.AllVariables.FindLast(variable =>
                    {
                        return variable.Type.Value == block.relation.Variable.Type.Value;
                    });
                    List<Node> variables = new List<Node>();
                    switch (block.relation.Type)
                    {
                        case AtributeNameEnum.StmtLine:

                            variables.Add(FilterNodes(Pkb.statementsList, block.relation.Variable.Type.Type)
                                .FindLast(statement => statement.number.ToString() == block.parameter.Type.Value));
                            break;

                        case AtributeNameEnum.varName:
                            variables.Add(Pkb.GetAllVariables().FindLast(var => var.Token.Value == block.parameter.Type.Value));
                            break;

                    }

                    if (queryVariable.Variables != null)
                    {
                        variables = IntersectVariables(queryVariable.Variables, variables);
                    }
                    queryVariable.Variables = variables;
                }
            });
        }

        private List<Node> IntersectVariables(List<Node> variables1, List<Node> variables2)
        {
            return variables1.FindAll(element => variables2.Contains(element) || 
            (element is Variable && variables2.Any(element2 => (element2 as Variable).Token.Value == ((Variable)element).Token.Value)));
        }

        private List<T> GetBlocks<T>() where T : QueryBlock
        {
            return Query.QueryBlocks.OfType<T>().ToList();
        }
    }
}
