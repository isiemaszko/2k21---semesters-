using ProjectATI.Code.Parser;
using ProjectATI.Code.Statements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectATI.Code.QueryProcessor
{
    public class QueryVariable
    {
        private List<Node> variables = null;

        public Token Type { get; set; }
        public List<QueryVariable> ConnectedName { get; } = new List<QueryVariable>();
        public List<QueryVariable> ConnectedValue { get; } = new List<QueryVariable>();
        public List<QueryVariable> ConnectedLine { get; } = new List<QueryVariable>();
        public List<Node> Variables 
        { 
            get => variables;
            set
            {
                variables = value;
                NotifyOthers();
            }
        }

        public void NotifyOthers()
        {
            ConnectedName.ForEach(connected => connected.FilterName(this));
            ConnectedValue.ForEach(connected => connected.FilterValue(this));
            ConnectedLine.ForEach(connected => connected.FilterLine(this));
        }

        private void NotifyOthers(QueryVariable queryVariable)
        {
            ConnectedName.FindAll(connected => connected != queryVariable).ForEach(connected => connected.FilterName(this));
            ConnectedValue.FindAll(connected => connected != queryVariable).ForEach(connected => connected.FilterValue(this));
            ConnectedLine.FindAll(connected => connected != queryVariable).ForEach(connected => connected.FilterLine(this));
        }

        private void FilterLine(QueryVariable queryVariable)
        {
            List<int> values;
            if (queryVariable.Type.Type != TokenType.Constant)
                values = queryVariable.Variables.Select(variable => variable.number).ToList();
            else
                values = queryVariable.Variables.Select(variable => int.Parse(variable.Name)).ToList();
            for (int i = variables.Count - 1; i >= 0; i--)
            {
                if (values.FirstOrDefault(value => value == variables[i].number) == 0)
                    variables.RemoveAt(i);
            }
            NotifyOthers(queryVariable);
        }

        private void FilterValue(QueryVariable queryVariable)
        {
            List<int> values;
            if (queryVariable.Type.Type != TokenType.Constant)
                values = queryVariable.Variables.Select(variable => variable.number).ToList();
            else
                values = queryVariable.Variables.Select(variable => int.Parse(variable.Name)).ToList();
            for (int i = variables.Count - 1; i >= 0; i--)
            {
                if (values.FirstOrDefault(value => value == int.Parse(variables[i].Name)) == 0)
                    variables.RemoveAt(i);
            }
            NotifyOthers(queryVariable);
        }

        private void FilterName(QueryVariable queryVariable)
        {
            for (int i = variables.Count - 1; i >= 0; i--)
            {
                if (queryVariable.Variables.FirstOrDefault(variable => variable.Name == variables[i].Name) is null)
                    variables.RemoveAt(i);
            }
            NotifyOthers(queryVariable);
        }

        public QueryVariable(Token type)
        {
            Type = type;
        }

    }
}
