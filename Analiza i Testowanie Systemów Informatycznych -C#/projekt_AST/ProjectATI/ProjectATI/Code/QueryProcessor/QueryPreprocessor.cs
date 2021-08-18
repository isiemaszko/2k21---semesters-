using ProjectATI.Code.Parser;
using ProjectATI.Code.Statements;
using ProjectATI.Code.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using ProjectATI.Code.PKB;
namespace ProjectATI.Code.QueryProcessor

{
    public class QueryPreprocessor
    {
        private List<string> Relationships { get; } = new List<string>() { "parent", "parent*", "follows", "follows*", "modifies","uses","calls","calls*" };
        private List<string> StmtLines { get; } = new List<string>() { "stmt#", "varname", "value", "procname" };
        private List<QueryVariable> QueryStatementList { get; } = new List<QueryVariable>();
        private List<QueryVariable> QueryVariables { get; } = new List<QueryVariable>();
        private string SelectQuery { get; set; }
        private List<QueryBlock> QueryBlocksList { get; set; }
        public Query Query { get; private set; }
        private Pkb Pkb { get; } = Pkb.GetPkbInstance();
        public QueryPreprocessor(string queryWithStatementList)
        {
            Pkb = Pkb.GetPkbInstance();
            // split text from input into lines
            string[] querySplitToLines = queryWithStatementList.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            int noOfLines = querySplitToLines.Length;
            switch (noOfLines)
            {
                case 0:
                    throw new InvalidQueryException("No input");
                case 1:
                    if (!querySplitToLines[0].ToLower().Contains("select"))
                        throw new InvalidQueryException("Line should have SELECT query");
                    if (!querySplitToLines[0].ToLower().Contains("boolean"))
                        throw new InvalidQueryException("Line should have BOOLEAN type");
                    SelectQuery = querySplitToLines[0].ToLower();
                    break;
                    
                case 2:
                    if (querySplitToLines[0].ToLower().Contains("select"))
                        throw new InvalidQueryException("First line should only contain synonyms declaration");
                    if (!querySplitToLines[0].EndsWith(";"))
                        throw new InvalidQueryException("First line is not ended properly");
                    if (!querySplitToLines[1].ToLower().StartsWith("select"))
                        throw new InvalidQueryException("First line must start with query");
                    SetQueryStatementList(querySplitToLines[0].ToLower());
                    SelectQuery = querySplitToLines[1].ToLower();
                    break;
                default:
                    throw new InvalidQueryException("Too many lines");
            }



            //w teorii sprawdzenie poprawności wpisanego Query i variabli
            //założenie że pierwsza to synonimy ( variables ) a druga to docelowe query
            
            

            Process();
        }

        //podzielenie na instrukcje pomiędzy ";"
        void SetQueryStatementList(string statementList)
        {
            string[] semicolonSplitTable = statementList.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < semicolonSplitTable.Length; i++)
            {
                string nodeWithToken = semicolonSplitTable[i].TrimStart(' ');
                EvaluateToken(nodeWithToken);
            }
        }

        // wyznaczanie typu tokenu i dodanie do listy
        void EvaluateToken(string query)
        {
            var designEntity = query.Split(new string[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);
            switch (designEntity[0])
            {
                case "stmt":
                    AddToTokenList(designEntity, TokenType.Statement);
                    break;
                case "assign":
                    AddToTokenList(designEntity, TokenType.Assignment);
                    break;
                case "variable":
                    AddToTokenList(designEntity, TokenType.Variable);
                    break;
                case "constant":
                    AddToTokenList(designEntity, TokenType.Constant);
                    break;
                case "while":
                    AddToTokenList(designEntity, TokenType.While);
                    break;
                case "prog_line":
                    AddToTokenList(designEntity, TokenType.Prog_line);
                    break;
                case "procedure":
                    AddToTokenList(designEntity, TokenType.Procedure);
                    break;
                case "if":
                    AddToTokenList(designEntity, TokenType.If);
                    break;
                case "calls":
                    AddToTokenList(designEntity, TokenType.Calls);
                    break;
                

                default:
                    throw new InvalidQueryException("Wrong synonyms declaration!");
            }
        }

        //dodanie tokenu do listy
        void AddToTokenList(string[] designEntity, TokenType type)
        {
            for (int i = 1; i < designEntity.Length; i++)
            {
                Token token = new Token(type, designEntity[i].TrimEnd(','));
                QueryStatementList.Add(new QueryVariable(token));
            }
        }

        void Process()
        {
            var block = SelectQuery.Split(new string[] { " pattern ", " such that ", " with ", " and " }, StringSplitOptions.None);
            // var varName = relation[i].ToLower().Split(' ');
            QueryVariable var = null;
            CheckRelation(block, ref var);

            Query = new Query(var, QueryStatementList.Concat(QueryVariables).ToList(), QueryBlocksList);
        }

        void CheckRelation(string[] relation, ref QueryVariable variable)
        {
            QueryBlocksList = new List<QueryBlock>();
            for (int i = 0; i < relation.Length; i++)
            {
                string[] relationshipSplit = relation[i].Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (relationshipSplit[0].ToLower().Contains("select"))
                {
                    var varName = relation[i].ToLower().Split(' ');
                    variable = GetVariable(varName[1]);
                    //retrurn
                    int stop = 1;
                }
                else if (CheckVariable(relationshipSplit[0]))
                {
                    QueryBlocksList.Add(CreatePatternBlock(relation[i]));
                    int stop = 1;
                }
                else if (relation[i].Contains('='))
                {
                    QueryBlocksList.Add(CreateWithBlock(relation[i]));
                }
                else
                {
                    QueryBlocksList.Add(CreateSuchThatBlock(relation[i]));
                }
            }
        }

        SuchThatAndBlock CreateSuchThatBlock(string block)
        {
            string[] relationshipSplit = block.Split(new char[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (relationshipSplit.Length < 2) throw new InvalidQueryException("MUST be space after relationship eg. Parent (a,a1)");
            int indexOfRelationship = Relationships.IndexOf(relationshipSplit[0].ToLower());
            if(indexOfRelationship==-1) throw new InvalidQueryException(" ");
            RelationEnum relationEnum = (RelationEnum)indexOfRelationship;
            
            var splitRelationshipParameters = relationshipSplit[1].Split(new char[] { ' ', ',', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
            
            QueryVariable firstParam = GetVariable(splitRelationshipParameters[0]);
            QueryVariable secondParam = GetVariable(splitRelationshipParameters[1]);
            
            var checkValidityFunction = ReturnCheckValidityFunction(relationEnum);
            if(checkValidityFunction!=null) checkValidityFunction(firstParam, secondParam);

            return new SuchThatAndBlock(new RelationToken(relationEnum, firstParam, secondParam));
        }

        WithBlock CreateWithBlock(string block)
        {
            string[] relationshipSplit = block.Split(new string[] { " ", ".", "=", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            //sprawdzenie czy dany variable ma property np. statement s1.procName = ŹLE

            var evaluatedVariable = GetVariable(relationshipSplit[0]);
            CheckIfVariablePropertyExists(evaluatedVariable, relationshipSplit[1].ToLower());
            int indexOfAtribute = StmtLines.IndexOf(relationshipSplit[1].ToLower());
            if (indexOfAtribute == -1) throw new InvalidQueryException(" ");
            AtributeNameEnum relationEnum = (AtributeNameEnum)indexOfAtribute;



            if (relationshipSplit.Length > 3)
            {
                var paramVariable = GetVariable(relationshipSplit[2]);
                CheckIfVariablePropertyExists(paramVariable, relationshipSplit[3].ToLower());


                int indexOfParamAtribute = StmtLines.IndexOf(relationshipSplit[3].ToLower());
                if (indexOfParamAtribute == -1)
                    throw new InvalidQueryException(" ");
                AtributeNameEnum relationParamEnum = (AtributeNameEnum)indexOfParamAtribute;

                return new WithBlock(new AtributeNameToken(relationEnum, evaluatedVariable), new AtributeNameToken(relationParamEnum, paramVariable));
            }
            else
            {

                var param = GetVariable(relationshipSplit[2]);
                return new WithBlock(new AtributeNameToken(relationEnum, evaluatedVariable), param);
            }


        }

        PatternBlock CreatePatternBlock(string block)
        {
            string[] splitBlock = block.Split(new[] { ' ' }, 2);
            // SYTUACJA GDY BRAKUJE SZUKANEGO PARAMETRU mp. Select a pattern (_, _”d + 1”_)
            //gdy brakuje np. "a1"
            // wchodzi do CreateSuchThatBlock
            var tokenType = CheckPatternSynonym(splitBlock[0]);
            QueryVariable patternBlockQV = new QueryVariable(new Token(tokenType,splitBlock[0]));
            string parameters = splitBlock[1];
            if (!parameters.StartsWith("(") || !parameters.EndsWith(")"))
                throw new InvalidQueryException("zła składnia. prawdopodobnie złe nawiasy");
            parameters = parameters.TrimStart('(');
            parameters=parameters.TrimEnd(')');
            string[] splitParams = parameters.Split(new[] { ',' }, 3);
            PatternBlock patternBlock=null;
            switch (tokenType)
            {
                case TokenType.Assignment: 
                    var firstAssignPatternBlockParam = EvaluateFirstParam(splitParams[0]);
                    var secondAssignPatternBlockParam = EvaluatePatternSecondParam(splitParams[1]);
                    patternBlock = new PatternBlock(patternBlockQV, firstAssignPatternBlockParam, secondAssignPatternBlockParam);
                    break;
                case TokenType.While:
                    //’ varRef ‘,’ ‘_’ ‘)’
                    var firstWhilePatternBlockParam = EvaluateFirstParam(splitParams[0]);
                    if(isUnderground(splitParams[1])) 
                    patternBlock = new PatternBlock(patternBlockQV, firstWhilePatternBlockParam, null);
                    break;
                case TokenType.If:
                    //’ varRef ‘,’ ‘_’ ‘,’ ‘_’ ‘)’
                    var firstIfPatternBlockParam = EvaluateFirstParam(splitParams[0]);
                    if (isUnderground(splitParams[1]) && isUnderground(splitParams[2]))
                    patternBlock = new PatternBlock(patternBlockQV, firstIfPatternBlockParam, null);
                    break;
                default:
                    throw new InvalidQueryException("Invalid pattern synonym type");
            }
            return patternBlock;
        }

        QueryVariable GetVariable(string evaluatedVariable)
        {
            QueryVariable result = null;
            result = QueryStatementList.FindLast(variable => variable.Type.Value.Equals(evaluatedVariable));
            //Variable variables = null;
            if (result == null)
            {
                result = QueryVariables.FindLast(variable => variable.Type.Value.Equals(evaluatedVariable));
                if (result == null)
                {
                    Token tempToken = null;
                    int.TryParse(evaluatedVariable, out int variableNumber);
                    if (variableNumber != 0)
                    {
                        tempToken = new Token(TokenType.Number, variableNumber.ToString());
                    }
                    else if (evaluatedVariable.First() == '\"' && evaluatedVariable.Last() == '\"')
                    {
                        int length = evaluatedVariable.Length - 2;
                        string extracted = evaluatedVariable.Substring(1, length);
                        var res = Pkb.getProcedureNames();
                        bool value = res.Contains(extracted);
                        if (value) tempToken = new Token(TokenType.procName, extracted);
                        else tempToken = new Token(TokenType.varName, extracted);
                        int stop = 1;
                    }
                    else if (evaluatedVariable.Equals("boolean"))
                    {
                        tempToken = new Token(TokenType.BOOLEAN,"boolean");
                    }
                    else if(evaluatedVariable.Equals("_"))
                    {
                        tempToken = new Token(TokenType.Any,"_");
                    }
                    if (tempToken != null)
                    {
                        result = new QueryVariable(tempToken);
                        QueryVariables.Add(result);
                    }
                }
            }
            if (result == null)
                throw new InvalidQueryException("Unknown identifier used: " + evaluatedVariable);
            return result;
        }

        bool CheckVariable(string evaluatedVariable)
        {
            for (int i = 0; i < QueryStatementList.Count; i++)
            {
                if (QueryStatementList[i].Type.Value.Equals(evaluatedVariable))
                {
                    return true;
                }
            }
            return false;
        }

        void CheckIfVariablePropertyExists(QueryVariable variable, string property)
        {
            switch (property)
            {
                case "procname":
                    if (variable.Type.Type != TokenType.Procedure)
                        throw new InvalidQueryException("Bad property for variable: " + variable.Type.Value + ", which is type of: " + variable.Type.Type);
                    break;
                case "varname":
                    if (variable.Type.Type != TokenType.Variable)
                        throw new InvalidQueryException("Bad property for variable: " + variable.Type.Value + ", which is type of: " + variable.Type.Type);
                    break;
                case "stmt#":
                    if (variable.Type.Type != TokenType.Statement)
                        throw new InvalidQueryException("Bad property for variable: " + variable.Type.Value + ", which is type of: " + variable.Type.Type);
                    break;
                case "value":
                    if (variable.Type.Type != TokenType.Constant)
                        throw new InvalidQueryException("Bad property for variable: " + variable.Type.Value + ", which is type of: " + variable.Type.Type);
                    break;
                default:
                    throw new InvalidQueryException("Invalid property: " + property);
            }
        }

        //tylko procedury
        void IsCallsValid(QueryVariable firstParam, QueryVariable secondParam)
        {
            //sprawdzanie po nazwach parametrów czy dobrago typu są
            if (firstParam.Type.Type != TokenType.Procedure && secondParam.Type.Type != TokenType.Procedure)
                throw new InvalidQueryException("First param is of wrong type: " + firstParam.Type.Value +" or secornd param is of wrong type "+secondParam.Type.Value);
        }
        //statement lub procedura a drugie zmienna
        void IsUsesModifiesValid(QueryVariable firstParam, QueryVariable secondParam)
        {
            //sprawdzanie po nazwach parametrów czy dobrago typu są
            if (firstParam.Type.Type != TokenType.Procedure && firstParam.Type.Type != TokenType.Statement && firstParam.Type.Type != TokenType.Any && firstParam.Type.Type != TokenType.Assignment && firstParam.Type.Type != TokenType.While && firstParam.Type.Type != TokenType.If)
                throw new InvalidQueryException("First param is of wrong type: " + firstParam.Type.Value);
            if (secondParam.Type.Type != TokenType.Variable && secondParam.Type.Type != TokenType.Any && secondParam.Type.Type != TokenType.varName)
                throw new InvalidQueryException("First param is of wrong type: " + secondParam.Type.Value);
        }

        void EmptyFun(QueryVariable firstParam, QueryVariable secondParam)
        {

        }
        void CheckIf()
        {

        }
        #region pattern heleprs
        TokenType CheckPatternSynonym(string synonym)
        {
            var result = QueryStatementList.Find(stat => stat.Type.Value == synonym);
            return result.Type.Type;
        }

        QueryVariable EvaluateFirstParam(string param)
        {
            if (param.First() == '\"' && param.Last() == '\"')
            {
                int length = param.Length - 2;
                string extractedParamName = param.Substring(1, length);
                var varTable = Pkb.GetAllVariables().Find(variable => variable.Token.Value == extractedParamName);
                if (varTable == null) throw new InvalidQueryException("PKD does not containt variable with such name: " + extractedParamName);
                return new QueryVariable(new Token(TokenType.varName, extractedParamName));
            }
            else if (param.Equals("_"))
            {
                return new QueryVariable(new Token(TokenType._Underscore));
            }          
            throw new InvalidQueryException("First param does not follows handbook rules: "+param);
        }
        PatternSecondParam EvaluatePatternSecondParam(string param)
        {
            var split = param.Split(new string[] { "\""," " }, StringSplitOptions.RemoveEmptyEntries);
            PatternSecondParam patternSecondParam = new PatternSecondParam(null, null, null);
            if (split.Length>3)
                throw new InvalidQueryException("Second param wrong zapis");
            var regexItem = new Regex("^[a-zA-Z0-9+*/()]*$");

            switch (split.Length)
            {
                case 1:
                    if (split[0].Equals("_")) {
                        patternSecondParam.first = new QueryVariable(new Token(TokenType._Underscore));
                    }
                    else
                    {
                        if (!regexItem.IsMatch(split[0])) throw new InvalidQueryException("Second param wrong zapis");
                        patternSecondParam.second = ConvertToRPN(new Token(TokenType.Calc, split[0]));
                    }
                    break;
                
                case 3:
                    if(!split[0].Equals("_") || !regexItem.IsMatch(split[1]) || !split[2].Equals("_")) throw new InvalidQueryException("Params wrong zapis");
                    patternSecondParam.first = new QueryVariable(new Token(TokenType._Underscore));
                    patternSecondParam.second = ConvertToRPN(new Token(TokenType.Calc, split[1]));
                    patternSecondParam.third = new QueryVariable(new Token(TokenType._Underscore));
                    break;
                default:
                    throw new InvalidQueryException("Second param wrong zapis");
            }
            return patternSecondParam;
        }

        public Boolean isUnderground(string param)
        {
            var split = param.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (!split[0].Equals("_")) throw new InvalidQueryException("Parameter "+param+" must be '_'");
            return true;
        }
        #endregion

        public Action<QueryVariable, QueryVariable> ReturnCheckValidityFunction(RelationEnum relationEnum)
        {
            Action<QueryVariable, QueryVariable> returnFunction = null;
            switch (relationEnum)
            {
                case RelationEnum.Calls:
                    returnFunction = IsCallsValid;
                    break;
                case RelationEnum.CallsStar:
                    returnFunction = IsCallsValid;
                    break;
                case RelationEnum.Modifies:
                    returnFunction = IsUsesModifiesValid;
                    break;
                case RelationEnum.Uses:
                    returnFunction = IsUsesModifiesValid;
                    break;
                
                default:
                    break;
            }
            return returnFunction;
        }


        public string ConvertToRPN(Token calc)
        {
            List<string> operators = new List<string>();
            List<string> signs = new List<string>();
            string s = calc.Value.Replace(" ", "");
            string result = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (Helpers.operatorsAndPriority.ContainsKey(s[i].ToString()))
                {
                    if (s[i] == '(')
                    {
                        operators.Add(s[i].ToString());
                        continue;
                    }
                    else if (s[i] == ')')
                    {
                        while (operators[operators.Count - 1] != "(")
                        {
                            result += operators[operators.Count - 1];
                            operators.RemoveAt(operators.Count - 1);
                        }
                        operators.RemoveAt(operators.Count - 1);
                        continue;
                    }
                    if (operators.Count != 0)
                    {
                        var lastOperator = operators[operators.Count - 1].ToString();
                        var lastOperatorPriority = Helpers.operatorsAndPriority.Where(item => item.Key == lastOperator).FirstOrDefault().Value;
                        var newOperatorPriority = Helpers.operatorsAndPriority.Where(item => item.Key == s[i].ToString()).FirstOrDefault().Value;
                        while (operators.Count != 0 && lastOperatorPriority >= newOperatorPriority && lastOperatorPriority != 0)
                        {
                            result += operators[operators.Count - 1];
                            operators.RemoveAt(operators.Count - 1);
                            if (operators.Count != 0)
                            {
                                lastOperator = operators[operators.Count - 1].ToString();
                                lastOperatorPriority = Helpers.operatorsAndPriority.Where(item => item.Key == lastOperator).FirstOrDefault().Value;
                            }
                        }
                    }
                    operators.Add(s[i].ToString());
                }
                else
                    result += s[i];

            }
            for (int i = operators.Count - 1; i >= 0; i--)
            {
                result += operators[i];
            }
            return result;
        }


    }
}
